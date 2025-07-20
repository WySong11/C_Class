using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGameUseTimer
{
    // EventArgs는 C#에서 이벤트를 처리할 때 사용하는 기본 클래스
    // 이벤트 발생 시 전달할 추가 정보를 담는 클래스를 상속받아 사용
    // 이벤트 핸들러 메서드에서 EventArgs를 통해 이벤트 발생 정보를 전달할 수 있음
    // 예를 들어, 공격 이벤트 발생 시 공격자, 대상, 데미지 정보를 담는 클래스를 정의할 수 있음

    /// <summary>
    /// 공격 이벤트 발생 시 전달되는 정보(공격자, 대상, 데미지)를 담는 클래스
    /// </summary>
    public class AttackEventArgs : EventArgs
    {
        /// <summary>
        /// 공격자의 고유 ID
        /// </summary>
        public int AttackerID;
        /// <summary>
        /// 공격 대상의 고유 ID
        /// </summary>
        public int TargetID;
        /// <summary>
        /// 공격 데미지
        /// </summary>
        public int Damage { get; }
        /// <summary>
        /// 생성자: 공격자ID, 대상ID, 데미지 전달
        /// </summary>
        /// <param name="attackerID">공격자 ID</param>
        /// <param name="targetID">공격 대상 ID</param>
        /// <param name="damage">공격 데미지</param>
        public AttackEventArgs(int attackerID, int targetID, int damage)
        {
            Damage = damage;
            AttackerID = attackerID;
            TargetID = targetID;
        }
    }

    /// <summary>
    /// 체력 변화 이벤트 발생 시 전달되는 정보(캐릭터ID, 체력)를 담는 클래스
    /// </summary>
    public class HealthChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 체력이 변한 캐릭터의 고유 ID
        /// </summary>
        public int CharacterID;
        /// <summary>
        /// 변경된 체력 값
        /// </summary>
        public int Health { get; }
        /// <summary>
        /// 생성자: 캐릭터ID, 체력 전달
        /// </summary>
        /// <param name="characterID">체력이 변한 캐릭터의 ID</param>
        /// <param name="health">변경된 체력 값</param>
        public HealthChangedEventArgs(int characterID, int health)
        {
            Health = health;
            CharacterID = characterID;
        }
    }

    /// <summary>
    /// 타이머 기반 주사위 게임의 메인 클래스
    /// </summary>
    public class DiceGameUseTimer
    {
        /// <summary>
        /// 게임에 참여하는 모든 캐릭터 목록 (플레이어, 적 등)
        /// </summary>
        private List<BaseCharacter> _characters = new List<BaseCharacter>();

        /// <summary>
        /// 게임 종료 여부 플래그
        /// </summary>
        private bool _gameOver = false;

        // async/await: 선언적으로 비동기를 다루는 방식
        // 자동으로 Task를 반환해줘서 코드가 깔끔해지고 가독성이 높아짐
        // await를 통해 비동기 작업의 결과를 기다리면서도 스레드를 차단하지 않음

        /// <summary>
        /// 게임을 시작하고, 캐릭터 생성, 이벤트 등록, 전투를 진행하는 비동기 메서드
        /// </summary>
        public async Task StartGameEventAsync()
        {
            Console.Clear();
            _gameOver = false;
            _characters.Clear();

            // C#에서 비동기 프로그래밍을 할 때, 직접 Task를 만들고 제어할 수 있게 해주는 도구
            // 일반적으로 async/await를 사용하면 Task가 자동으로 생기지만,
            // 때때로 비동기 흐름을 사용자 정의해서 컨트롤하고 싶을 때 씀.
            // 주로 비동기 메서드를 작성할 때 사용돼.
            // 직접 Task를 생성하고 언제 완료되는지 결정할 수 있어
            // 이 예제에서는 게임의 전투가 끝날 때까지 기다리는 동안 다른 작업을 수행할 수 있도록 함

            // TaskCompletionSource를 통해 게임 종료까지 비동기 대기
            var tcs = new TaskCompletionSource();

            // 플레이어와 적 캐릭터 생성 및 초기화
            PlayerCharacter _playerCharacter = new PlayerCharacter();
            EnemyCharacter _enemyCharacter = new EnemyCharacter();

            // 레벨 입력 및 능력치 설정
            _playerCharacter.SetLevelData(PromptLevel("플레이어 레벨을 입력하세요 (1-10): "));
            _enemyCharacter.SetLevelData(PromptLevel("적 레벨을 입력하세요. (1-10): "));

            // 각 캐릭터의 공격 대상 ID 설정
            _playerCharacter.SetTargetID(_enemyCharacter.ID); // 플레이어의 공격 대상은 적
            _enemyCharacter.SetTargetID(_playerCharacter.ID); // 적의 공격 대상은 플레이어

            // 캐릭터 목록에 추가
            _characters.Add(_playerCharacter);
            _characters.Add(_enemyCharacter);

            // 각 캐릭터의 이벤트를 DiceGameUseTimer에서 직접 구독
            foreach (var character in _characters)
            {
                // 공격 이벤트 핸들러 등록
                character.AddOnAttackEvent(attackEvent =>
                {
                    // 게임이 종료되지 않은 경우에만 처리
                    if (!_gameOver)
                    {
                        // 공격자와 대상 캐릭터를 ID로 찾음
                        var attecker = _characters.Find(c => c.ID == attackEvent.AttackerID);
                        var target = _characters.Find(c => c.ID == attackEvent.TargetID && c.IsAlive());
                        if (target != null)
                        {
                            if (attecker == null)
                            {
                                Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - Attacker not found for ID {target.ID}");
                                return;
                            }
                            Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - {attecker.Name} attacks {target.Name} ({attackEvent.Damage})");
                            // 대상에게 데미지 적용
                            target.TakeDamage(attackEvent.Damage);
                        }
                    }
                });

                // 체력 변화 이벤트 핸들러 등록
                character.AddOnHealthChangedEvent(healthEvent =>
                {
                    // 게임이 종료되지 않은 경우에만 처리
                    if (!_gameOver)
                    {
                        // 체력이 변한 캐릭터를 ID로 찾음
                        var target = _characters.Find(c => c.ID == healthEvent.CharacterID);

                        if (target == null)
                        {
                            Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - Character not found for ID {healthEvent.CharacterID}");
                            return;
                        }

                        Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - {target.Name} HealthChanged {healthEvent.Health}");

                        // 체력 변화 정보 출력
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n{target.Name} -> {target.Health}");
                        Console.WriteLine(new string('-', 50));

                        // 게임 종료 조건 체크: 체력이 0 이하가 되면 게임 종료
                        if (target.Health <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"\n{target?.Name} 죽었습니다!\n");
                            EndGame();         // 게임 종료 처리
                            tcs.TrySetResult(); // TaskCompletionSource로 대기 중인 Task 완료
                        }
                    }
                });
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // 게임 시작 시 캐릭터 정보 출력
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{_playerCharacter.Name} vs {_enemyCharacter.Name}");
            Console.WriteLine($"\n{_playerCharacter.Name} 체력: {_playerCharacter.Health}, 공격력: {_playerCharacter.AttackPower}, 공격 속도: {_playerCharacter.AttackSpeed}ms");
            Console.WriteLine($"\n{_enemyCharacter.Name} 체력: {_enemyCharacter.Health}, 공격력: {_enemyCharacter.AttackPower}, 공격 속도: {_enemyCharacter.AttackSpeed}ms\n");
            Console.WriteLine(new string('-', 50));

            // 모든 캐릭터의 자동 공격 타이머 시작
            foreach (var character in _characters)
            {
                character.StartAutoAttackTimer();
            }

            // busy-wait 루프 대신 비동기 대기
            await tcs.Task;

            Console.ForegroundColor = ConsoleColor.White;

            // 게임 종료 후 재시작 여부 확인
            if (PromptContinue("게임을 다시 시작하시겠습니까? (y/yes 또는 Enter 키를 누르세요): "))
            {
                // 재귀적으로 게임을 다시 시작 (비동기)
                await StartGameEventAsync();
            }
            else
            {
                Console.WriteLine("게임을 종료합니다.");
            }
        }

        /// <summary>
        /// 게임 종료 시 호출되는 메서드. 모든 캐릭터의 자동 공격을 중지하고 게임 종료 플래그를 설정
        /// </summary>
        private void EndGame()
        {
            _gameOver = true;

            // 모든 캐릭터의 자동 공격 타이머와 이벤트 핸들러 해제
            foreach (var character in _characters)
            {
                character.StopAutoAttackTimer(); // 자동 공격 타이머 중지
                character.ClearAllEvents();      // 모든 이벤트 핸들러 제거
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
