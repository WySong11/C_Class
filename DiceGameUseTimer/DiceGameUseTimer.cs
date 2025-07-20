using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGameUseTimer
{
    public class DiceGameUseTimer
    {
        /// <summary>
        /// 플레이어 캐릭터 인스턴스
        /// </summary>
        private PlayerCharacter? _playerCharacter;

        /// <summary>
        /// 적 캐릭터 인스턴스
        /// </summary>
        private EnemyCharacter? _enemyCharacter;

        private bool _gameOver = false;

        public void StartGameEvent()
        {
            Console.Clear();
            _gameOver = false;

            // 플레이어와 적 캐릭터 생성 및 초기화
            _playerCharacter = new PlayerCharacter();
            _enemyCharacter = new EnemyCharacter();

            // 레벨 입력 및 능력치 설정
            _playerCharacter.SetLevelData(PromptLevel("플레이어 레벨을 입력하세요 (1-10): "));
            _enemyCharacter.SetLevelData(PromptLevel("적 레벨을 입력하세요. (1-10): "));

            _playerCharacter.AddOnAttackEvent((attackPower) =>
            {
                Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log -  _playerCharacter.AddOnAttackEvent {attackPower}");

                // _enemyCharacter가 null이 아니고 살아있을 때만 데미지 적용
                if (_enemyCharacter != null && _enemyCharacter.IsAlive())
                    _enemyCharacter.TakeDamage(attackPower);
            });

            _enemyCharacter.AddOnAttackEvent((attackPower) =>
            {
                Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log -  _enemyCharacter.AddOnAttackEvent {attackPower}");

                // _playerCharacter가 null이 아니고 살아있을 때만 데미지 적용
                if (_playerCharacter != null && _playerCharacter.IsAlive())
                    _playerCharacter.TakeDamage(attackPower);
            });

            _playerCharacter.AddOnHealthChangedEvent((health) =>
            {
                Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log -  _playerCharacter.AddOnHealthChangedEvent {health}\n");

                if (_gameOver == false)
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

            if (_playerCharacter?.IsAlive() == false && _enemyCharacter?.IsAlive() == false)
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

            if(PromptContinue("게임을 다시 시작하시겠습니까? (y/yes 또는 Enter 키를 누르세요): "))
            {
                StartGameEvent(); // 재귀 호출로 게임 재시작
            }
            else
            {
                Console.WriteLine("게임을 종료합니다.");
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
