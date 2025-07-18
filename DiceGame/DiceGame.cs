using DiceGame.Character;
using System;
using System.Threading.Tasks;
using System.Threading; // 스레드 및 Task 사용을 위한 네임스페이스

namespace DiceGame
{
    /// <summary>
    /// 주사위 게임의 메인 로직을 담당하는 클래스
    /// 플레이어와 적 캐릭터의 생성, 전투, 레벨 설정, 게임 반복 등을 관리
    /// </summary>
    public class DiceGame
    {
        /// <summary>
        /// 플레이어 캐릭터 인스턴스
        /// </summary>
        private PlayerCharacter? _playerCharacter;

        /// <summary>
        /// 적 캐릭터 인스턴스
        /// </summary>
        private EnemyCharacter? _enemyCharacter;

        /// <summary>
        /// 게임 종료 여부를 나타내는 플래그 (스레드/Task에서 공유)
        /// </summary>
        private volatile bool _gameOver = false;

        /// <summary>
        /// 멀티스레드 환경에서 데이터 동기화를 위한 lock 객체
        /// </summary>
        private readonly object _lock = new();

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 동기 방식의 게임 루프. 플레이어와 적이 공격 속도에 맞춰 번갈아가며 공격
        /// </summary>
        public void StartGame()
        {
            Console.Clear();

            // 플레이어와 적 캐릭터 생성 및 초기화
            _playerCharacter = new PlayerCharacter();
            _enemyCharacter = new EnemyCharacter();

            // 레벨 입력 및 능력치 설정
            _playerCharacter.SetLevelData(PromptLevel("Enter your character's level (1-10): "));
            _enemyCharacter.SetLevelData(PromptLevel("Enter the enemy's level (1-10): "));

            // 각 캐릭터의 다음 공격 시각을 추적
            DateTime playerNextAttack = DateTime.Now;
            DateTime enemyNextAttack = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{_playerCharacter.Name} vs {_enemyCharacter.Name}");
            Console.WriteLine($"\n{_playerCharacter.Name} Health: {_playerCharacter.Health}, Attack Power: {_playerCharacter.AttackPower}, Attack Speed: {_playerCharacter.AttackSpeed}ms");
            Console.WriteLine($"\n{_enemyCharacter.Name} Health: {_enemyCharacter.Health}, Attack Power: {_enemyCharacter.AttackPower}, Attack Speed: {_enemyCharacter.AttackSpeed}ms\n");

            // 둘 중 한 명의 체력이 0 이하가 될 때까지 반복
            while (_playerCharacter.Health > 0 && _enemyCharacter.Health > 0)
            {
                bool playerAttacked = false;
                bool enemyAttacked = false;

                DateTime now = DateTime.Now;

                // 플레이어 공격 타이밍 도달 시 공격
                if (now >= playerNextAttack)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine();

                    int attackPower = _playerCharacter?.GetAttackPower() ?? 0;
                    _enemyCharacter.TakeDamage(attackPower);
                    Console.WriteLine($"{_playerCharacter?.Name} attacks {_enemyCharacter.Name} for {attackPower} damage!");
                    playerNextAttack = now.AddMilliseconds(_playerCharacter.AttackSpeed);
                    playerAttacked = true;

                    // 상태 출력
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{_playerCharacter.Name} -> {_playerCharacter.Health} vs {_enemyCharacter.Health} <- {_enemyCharacter.Name}");

                    if (_enemyCharacter.Health <= 0)
                    {
                        Console.WriteLine($"\n{_enemyCharacter.Name} has been defeated!\n");
                        break;
                    }
                }

                // 적 공격 타이밍 도달 시 공격
                if (now >= enemyNextAttack)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();

                    int attackPower = _enemyCharacter?.GetAttackPower() ?? 0;
                    _playerCharacter.TakeDamage(attackPower);
                    Console.WriteLine($"{_enemyCharacter?.Name} attacks {_playerCharacter.Name} for {attackPower} damage!");
                    enemyNextAttack = now.AddMilliseconds(_enemyCharacter.AttackSpeed);
                    enemyAttacked = true;

                    // 상태 출력
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{_playerCharacter.Name} -> {_playerCharacter.Health} vs {_enemyCharacter.Health} <- {_enemyCharacter.Name}");

                    if (_playerCharacter.Health <= 0)
                    {
                        Console.WriteLine($"\n{_playerCharacter.Name} has been defeated!\n");
                        break;
                    }
                }

                // 둘 다 공격하지 않은 경우, 다음 공격까지 남은 시간만큼 대기
                if (!playerAttacked && !enemyAttacked)
                {
                    // 다음 공격까지 남은 시간 계산
                    double waitMs = Math.Min(
                        (playerNextAttack - now).TotalMilliseconds,
                        (enemyNextAttack - now).TotalMilliseconds
                    );

                    // 남은 시간만큼 대기
                    if (waitMs > 0)
                        Thread.Sleep((int)waitMs);
                }
                else
                {
                    // 한 번의 라운드마다 약간의 텀을 둠 (가독성)
                    Thread.Sleep(100);
                }
            }

            // 게임 종료 후 재시작 여부 확인
            if (PromptContinue("Do you want to continue? (y/n) : "))
            {
                StartGame();
            }
            else
            {
                Console.WriteLine("Game ended.");
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 스레드를 이용한 비동기 전투 루프. 플레이어와 적의 공격이 각각 별도 스레드에서 동작
        /// </summary>
        public void StartGameThread()
        {
            Console.Clear();

            _playerCharacter = new PlayerCharacter();
            _enemyCharacter = new EnemyCharacter();

            _playerCharacter.SetLevelData(PromptLevel("Enter your character's level (1-10): "));
            _enemyCharacter.SetLevelData(PromptLevel("Enter the enemy's level (1-10): "));

            int playerHealth = _playerCharacter?.Health ?? 0;
            int enemyHealth = _enemyCharacter?.Health ?? 0;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{_playerCharacter.Name} vs {_enemyCharacter.Name}");
            Console.WriteLine($"\n{_playerCharacter.Name} Health: {_playerCharacter.Health}, Attack Power: {_playerCharacter.AttackPower}, Attack Speed: {_playerCharacter.AttackSpeed}ms");
            Console.WriteLine($"\n{_enemyCharacter.Name} Health: {_enemyCharacter.Health}, Attack Power: {_enemyCharacter.AttackPower}, Attack Speed: {_enemyCharacter.AttackSpeed}ms\n");

            _gameOver = false;

            // 플레이어와 적의 공격 루프를 각각 별도 스레드에서 실행
            Thread playerThread = new Thread(PlayerAttackLoop);
            Thread enemyThread = new Thread(EnemyAttackLoop);

            playerThread.Start();
            enemyThread.Start();

            // 상태 출력 루프 (메인 스레드)
            while (!_gameOver)
            {
                lock (_lock)
                {
                    // 상태가 변경되었는지 확인
                    if (playerHealth != _playerCharacter?.Health || enemyHealth != _enemyCharacter?.Health)
                    {
                        playerHealth = _playerCharacter?.Health ?? 0;
                        enemyHealth = _enemyCharacter?.Health ?? 0;
                        // 상태 출력
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n{_playerCharacter.Name} -> {_playerCharacter.Health} vs {_enemyCharacter.Health} <- {_enemyCharacter.Name}");
                    }
                }
                Thread.Sleep(100);
            }

            // 두 스레드가 모두 종료될 때까지 대기
            playerThread.Join();
            enemyThread.Join();

            if (PromptContinue("Do you want to continue? (y/n) : "))
            {
                StartGameThread();
            }
            else
            {
                Console.WriteLine("Game ended.");
            }
        }

        /// <summary>
        /// 플레이어의 공격을 담당하는 스레드 루프
        /// </summary>
        private void PlayerAttackLoop()
        {
            while (!_gameOver)
            {
                Thread.Sleep((int)(_playerCharacter?.AttackSpeed ?? 1000));
                lock (_lock)
                {
                    if (_gameOver) return;

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Blue;

                    int attackPower = _playerCharacter?.GetAttackPower() ?? 0;
                    _enemyCharacter?.TakeDamage(attackPower);
                    Console.WriteLine($"{_playerCharacter?.Name} attacks {_enemyCharacter?.Name} for {attackPower} damage!");
                    if (_enemyCharacter != null && _enemyCharacter.Health <= 0)
                    {
                        Console.WriteLine($"\n{_enemyCharacter.Name} has been defeated!\n");
                        _gameOver = true;
                    }
                }
            }
        }

        /// <summary>
        /// 적의 공격을 담당하는 스레드 루프
        /// </summary>
        private void EnemyAttackLoop()
        {
            while (!_gameOver)
            {
                Thread.Sleep((int)(_enemyCharacter?.AttackSpeed ?? 1000));
                lock (_lock)
                {
                    if (_gameOver) return;

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;

                    int attackPower = _enemyCharacter?.GetAttackPower() ?? 0;
                    _playerCharacter?.TakeDamage(attackPower);
                    Console.WriteLine($"{_enemyCharacter?.Name} attacks {_playerCharacter?.Name} for {attackPower} damage!");
                    if (_playerCharacter != null && _playerCharacter.Health <= 0)
                    {
                        Console.WriteLine($"\n{_playerCharacter.Name} has been defeated!\n");
                        _gameOver = true;
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Task/async를 이용한 비동기 전투 루프. 플레이어와 적의 공격이 각각 비동기 Task로 동작
        /// </summary>
        public void StartGameAsync()
        {
            Console.Clear();

            _playerCharacter = new PlayerCharacter();
            _enemyCharacter = new EnemyCharacter();

            _playerCharacter.SetLevelData(PromptLevel("Enter your character's level (1-10): "));
            _enemyCharacter.SetLevelData(PromptLevel("Enter the enemy's level (1-10): "));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{_playerCharacter.Name} vs {_enemyCharacter.Name}");
            Console.WriteLine($"\n{_playerCharacter.Name} Health: {_playerCharacter.Health}, Attack Power: {_playerCharacter.AttackPower}, Attack Speed: {_playerCharacter.AttackSpeed}ms");
            Console.WriteLine($"\n{_enemyCharacter.Name} Health: {_enemyCharacter.Health}, Attack Power: {_enemyCharacter.AttackPower}, Attack Speed: {_enemyCharacter.AttackSpeed}ms\n");

            _gameOver = false;

            // 비동기 공격 루프 시작
            var playerTask = PlayerAttackLoopAsync();
            var enemyTask = EnemyAttackLoopAsync();

            int playerHealth = _playerCharacter?.Health ?? 0;
            int enemyHealth = _enemyCharacter?.Health ?? 0;

            // 상태 출력 루프 (메인 스레드)
            while (!_gameOver)
            {
                lock (_lock)
                {
                    // 상태가 변경되었는지 확인
                    if (playerHealth != _playerCharacter?.Health || enemyHealth != _enemyCharacter?.Health)
                    {
                        playerHealth = _playerCharacter?.Health ?? 0;
                        enemyHealth = _enemyCharacter?.Health ?? 0;
                        // 상태 출력
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n{_playerCharacter.Name} -> {_playerCharacter.Health} vs {_enemyCharacter.Health} <- {_enemyCharacter.Name}");
                    }
                }
                Thread.Sleep(100);
            }

            // 두 Task가 모두 끝날 때까지 대기
            Task.WaitAll(playerTask, enemyTask);

            if (PromptContinue("Do you want to continue? (y/n) : "))
            {
                StartGameAsync();
            }
            else
            {
                Console.WriteLine("Game ended.");
            }
        }

        /// <summary>
        /// 플레이어의 공격을 담당하는 비동기 Task 루프
        /// </summary>
        private async Task PlayerAttackLoopAsync()
        {
            while (!_gameOver)
            {
                await Task.Delay((int)(_playerCharacter?.AttackSpeed ?? 1000));
                lock (_lock)
                {
                    if (_gameOver) return;

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Blue;

                    int attackPower = _playerCharacter?.GetAttackPower() ?? 0;
                    _enemyCharacter?.TakeDamage(attackPower);
                    Console.WriteLine($"{_playerCharacter?.Name} attacks {_enemyCharacter?.Name} for {attackPower} damage!");
                    if (_enemyCharacter != null && _enemyCharacter.Health <= 0)
                    {
                        Console.WriteLine($"\n{_enemyCharacter.Name} has been defeated!\n");
                        _gameOver = true;
                    }
                }
            }
        }

        /// <summary>
        /// 적의 공격을 담당하는 비동기 Task 루프
        /// </summary>
        private async Task EnemyAttackLoopAsync()
        {
            while (!_gameOver)
            {
                await Task.Delay((int)(_enemyCharacter?.AttackSpeed ?? 1000));
                lock (_lock)
                {
                    if (_gameOver) return;

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;

                    int attackPower = _enemyCharacter?.GetAttackPower() ?? 0;
                    _playerCharacter?.TakeDamage(attackPower);
                    Console.WriteLine($"{_enemyCharacter?.Name} attacks {_playerCharacter?.Name} for {attackPower} damage!");
                    if (_playerCharacter != null && _playerCharacter.Health <= 0)
                    {
                        Console.WriteLine($"\n{_playerCharacter.Name} has been defeated!\n");
                        _gameOver = true;
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 레벨 입력을 받아 정수로 반환. 유효하지 않으면 1 반환
        /// </summary>
        /// <param name="inMessage">입력 안내 메시지</param>
        /// <returns>입력받은 레벨(1~10), 잘못된 입력 시 1</returns>
        public int PromptLevel(string inMessage)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(inMessage);
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int level) && level >= 1 && level <= 10)
            {
                return level;
            }
            else
            {
                Console.WriteLine("Invalid level. Defaulting to level 1.");
                return 1;
            }
        }

        /// <summary>
        /// 게임을 계속할지 여부를 입력받아 bool로 반환
        /// </summary>
        /// <param name="inMessage">입력 안내 메시지</param>
        /// <returns>y/yes 입력 시 true, 그 외 false</returns>
        public bool PromptContinue(string inMessage)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(inMessage);
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                return false; // 기본값으로 false 반환
            }
            return input.Trim().ToLower() == "y" || input.Trim().ToLower() == "yes";
        }
    }
}
