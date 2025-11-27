using DiceGame.Character;
using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Threading; // 스레드 및 Task 사용을 위한 네임스페이스
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

        // 자동 공격 타이머를 이용한 비동기 전투 루프
        public void StartGameEvent()
        {
            Console.Clear();

            // 플레이어와 적 캐릭터 생성 및 초기화
            _playerCharacter = new PlayerCharacter();
            _enemyCharacter = new EnemyCharacter();

            // 레벨 입력 및 능력치 설정
            _playerCharacter.SetLevelData(PromptLevel("플레이어 레벨을 입력하세요 (1-10): "));
            _enemyCharacter.SetLevelData(PromptLevel("적 레벨을 입력하세요. (1-10): "));

            _playerCharacter.AddOnAttackEvent( (attackPower) =>
            {
                Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log -  _playerCharacter.AddOnAttackEvent {attackPower}");

                // _enemyCharacter가 null이 아니고 살아있을 때만 데미지 적용
                if (_enemyCharacter != null && _enemyCharacter.IsAlive())
                    _enemyCharacter.TakeDamage(attackPower);
            });

            _enemyCharacter.AddOnAttackEvent( (attackPower) =>
            {
                Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log -  _enemyCharacter.AddOnAttackEvent {attackPower}");

                // _playerCharacter가 null이 아니고 살아있을 때만 데미지 적용
                if (_playerCharacter != null && _playerCharacter.IsAlive())
                    _playerCharacter.TakeDamage(attackPower);
            });

            _playerCharacter.AddOnHealthChangedEvent((health) =>
            {
                Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log -  _playerCharacter.AddOnHealthChangedEvent {health}\n");

                if(_gameOver == false)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{_playerCharacter?.Name} -> {_playerCharacter?.Health} vs {_enemyCharacter?.Health} <- {_enemyCharacter?.Name}");
                    Console.WriteLine(new string('-', 50));

                    if (health <= 0)
                    {
                        _playerCharacter?.StopAutoAttack();
                        _enemyCharacter?.StopAutoAttack();

                        _gameOver = true; // 게임 종료 플래그 설정
                    }
                }                
            });

            _enemyCharacter.AddOnHealthChangedEvent((health) =>
            {
                Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log -  _enemyCharacter.AddOnHealthChangedEvent {health}\n");

                if (_gameOver == false)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{_playerCharacter?.Name} -> {_playerCharacter?.Health} vs {_enemyCharacter?.Health} <- {_enemyCharacter?.Name}");
                    Console.WriteLine(new string('-', 50));

                    if (health <= 0)
                    {
                        // 게임 종료 시 자동 공격 중지
                        _playerCharacter?.StopAutoAttack();
                        _enemyCharacter?.StopAutoAttack();

                        _gameOver = true; // 게임 종료 플래그 설정
                    }
                }
            });

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{_playerCharacter.Name} vs {_enemyCharacter.Name}");
            Console.WriteLine($"\n{_playerCharacter.Name} 체력: {_playerCharacter.Health}, 공격력: {_playerCharacter.AttackPower}, 공격 속도: {_playerCharacter.AttackSpeed}ms");
            Console.WriteLine($"\n{_enemyCharacter.Name} 체력: {_enemyCharacter.Health}, 공격력: {_enemyCharacter.AttackPower}, 공격 속도: {_enemyCharacter.AttackSpeed}ms\n");            
            Console.WriteLine(new string('-', 50));

            Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - _playerCharacter.StartAutoAttack");

            // 플레이어와 적 캐릭터의 공격을 각각 한 번만 시작하도록 수정
            _playerCharacter.StartAutoAttackTimer(
                damage =>
                {
                    Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log -  _enemyCharacter.TakeDamage {damage}");

                    // _enemyCharacter가 null이 아니고 살아있을 때만 데미지 적용
                    if (_enemyCharacter != null && _enemyCharacter.IsAlive())
                        _enemyCharacter.TakeDamage(damage);
                });

            Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - _enemyCharacter.StartAutoAttack");

            _enemyCharacter.StartAutoAttackTimer(
                damage =>
                {
                    Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log -  _playerCharacter.TakeDamage {damage}");

                    // _playerCharacter가 null이 아니고 살아있을 때만 데미지 적용
                    if (_playerCharacter != null && _playerCharacter.IsAlive())
                        _playerCharacter.TakeDamage(damage);
                });           

            // 공격 타이머는 한 번만 시작하면 되므로, while 루프에서 반복 호출하지 않도록 수정
            while (_gameOver == false)
            {
               
            }

            Console.ForegroundColor = ConsoleColor.Blue;

            if ( _playerCharacter?.IsAlive() == false && _enemyCharacter?.IsAlive() == false)
            {
                Console.WriteLine("\n플레이어와 적 모두 쓰러졌습니다. 무승부입니다!\n");
            }
            else if (_playerCharacter?.IsAlive() == true)
            {
                Console.WriteLine($"\n{_playerCharacter?.Name} 승리했습니다!\n");
            }
            else
            {
                Console.WriteLine($"\n{_enemyCharacter?.Name} 승리했습니다!\n");
            }
        }

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
            _playerCharacter.SetLevelData(PromptLevel("플레이어 레벨을 입력하세요 (1-10): "));
            _enemyCharacter.SetLevelData(PromptLevel("적 레벨을 입력하세요. (1-10): "));

            // 각 캐릭터의 다음 공격 시각을 추적
            DateTime playerNextAttack = DateTime.Now;
            DateTime enemyNextAttack = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{_playerCharacter.Name} vs {_enemyCharacter.Name}");
            Console.WriteLine($"\n{_playerCharacter.Name} 체력: {_playerCharacter.Health}, 공격력: {_playerCharacter.AttackPower}, 공격 속도: {_playerCharacter.AttackSpeed}ms");
            Console.WriteLine($"\n{_enemyCharacter.Name} 체력: {_enemyCharacter.Health}, 공격력: {_enemyCharacter.AttackPower}, 공격 속도: {_enemyCharacter.AttackSpeed}ms\n");

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
                    Console.WriteLine($"{_playerCharacter?.Name} 공격해서 {_enemyCharacter.Name} 에게 {attackPower} 데미지 입힘!");
                    playerNextAttack = now.AddMilliseconds(_playerCharacter.AttackSpeed);
                    playerAttacked = true;

                    // 상태 출력
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{_playerCharacter.Name} -> {_playerCharacter.Health} vs {_enemyCharacter.Health} <- {_enemyCharacter.Name}");

                    if (_enemyCharacter.Health <= 0)
                    {
                        Console.WriteLine($"\n{_playerCharacter.Name} 승리했습니다!\n");
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
                    Console.WriteLine($"{_enemyCharacter?.Name} 공격해서 {_playerCharacter.Name} 에게 {attackPower} 데미지 입힘!");
                    enemyNextAttack = now.AddMilliseconds(_enemyCharacter.AttackSpeed);
                    enemyAttacked = true;

                    // 상태 출력
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{_playerCharacter.Name} -> {_playerCharacter.Health} vs {_enemyCharacter.Health} <- {_enemyCharacter.Name}");

                    if (_playerCharacter.Health <= 0)
                    {
                        Console.WriteLine($"\n{_enemyCharacter.Name} 승리했습니다!\n");
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
            if (PromptContinue("계속하시겠습니까? (y/n) : "))
            {
                StartGame();
            }
            else
            {
                Console.WriteLine("게임 끝.");
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
        public async Task StartGameAsync()
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

            // 예시: 플레이어와 적 각각의 공격 Task 실행
            var playerTask = _playerCharacter.StartAutoAttackAsync(() => _enemyCharacter, () => _gameOver);
            var enemyTask = _enemyCharacter.StartAutoAttackAsync(() => _playerCharacter, () => _gameOver);
            await Task.WhenAny(playerTask, enemyTask);

            if (PromptContinue("Do you want to continue? (y/n) : "))
            {
                await StartGameAsync();
            }
            else
            {
                Console.WriteLine("Game ended.");
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
