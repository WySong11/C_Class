using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace DiceGame.Character
{
    /// <summary>
    /// 캐릭터가 공격을 받을 수 있음을 나타내는 인터페이스
    /// </summary>
    public interface IAttackable
    {
        /// <summary>
        /// 데미지를 받는 메서드
        /// </summary>
        /// <param name="amount">받는 데미지 양</param>
        void TakeDamage(int amount);
    }

    /// <summary>
    /// 모든 캐릭터의 기본 속성과 동작을 정의하는 클래스
    /// </summary>
    public class BaseCharacter : IAttackable
    {   
        /// <summary>
        /// 캐릭터 이름
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 캐릭터의 현재 체력
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// 캐릭터의 공격력
        /// </summary>
        public int AttackPower { get; set; }

        /// <summary>
        /// 캐릭터의 공격 속도 (밀리초 단위)
        /// </summary>
        public double AttackSpeed { get; set; }

        private event Action<int>? OnAttackEvent;
        private event Action<int>? OnHealthChangedEvent;

        private Timer? _autoAttackTimer;

        /// <summary>
        /// 기본 생성자. 속성을 기본값으로 초기화
        /// </summary>
        public BaseCharacter()
        {
            Name = string.Empty;
            Health = 10;
            AttackPower = 10;
            AttackSpeed = 100;
        }

        /// <summary>
        /// 소멸자
        /// </summary>
        ~BaseCharacter()
        {
        }

        /// <summary>
        /// 이름, 체력, 공격력을 지정하는 생성자
        /// </summary>
        /// <param name="name">캐릭터 이름</param>
        /// <param name="health">초기 체력</param>
        /// <param name="attackPower">초기 공격력</param>
        /// <param name="attackSpeed">초기 공격 속도 (밀리초 단위)</param>
        public BaseCharacter(string name, int health, int attackPower, double attackSpeed)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
            AttackSpeed = attackSpeed;
        }

        public void SetLevelData(int level)
        {
            // 레벨에 따라 캐릭터의 속성을 설정하는 로직을 구현할 수 있습니다.
            Health += level * 10; // 레벨당 체력 10 증가
            AttackPower += level * 5; // 레벨당 공격력 5 증가
            AttackSpeed += level * 300; // 레벨당 공격 속도 300 증가 (더 느리게 공격)
            
            Console.WriteLine($"\n{Name} has been leveled up to level {level}! New Health: {Health}, New Attack Power: {AttackPower}, New Attack Speed: {AttackSpeed} ms\n");
        }

        /// <summary>
        /// 데미지를 받아 체력을 감소시킴
        /// </summary>
        /// <param name="amount">받는 데미지 양</param>
        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0; // 체력이 0 이하로 내려가지 않도록 보정

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Name} 이 {amount} 공격받았습니다! 남은 체력 : {Health}");

            // 체력 변경 이벤트 발생
            OnHealthChangedEvent?.Invoke(Health);
        }

        // Attack() 메서드 내 타이머 해제 부분 수정
        public void Attack()
        {
            if (!IsAlive())
            {
                Console.WriteLine($"{Name} 은(는) 이미 죽었습니다!");
                return;
            }

            // 공격 속도에 따라 대기 시간 설정
            int waitTime = (int)AttackSpeed;
            //Console.WriteLine($"{Name} 이(가) 공격을 준비합니다... (대기 시간: {waitTime} ms)");

            Timer? timer = null;
            timer = new Timer((state) =>
            {
                // 현재 시간 출력
                //Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {Name} 이(가) 공격을 시작합니다!");

                // 공격이 준비되면 공격 메서드 호출
                PerformAttack();
                // timer가 null이 아닐 때만 Dispose 호출
                timer?.Dispose();
            }, null, waitTime, Timeout.Infinite);
        }

        public void PerformAttack()
        {
            // 공격력 계산
            int Att = GetAttackPower();

            // 대상에게 데미지 적용
            Console.WriteLine($"{Name} 가 {Att} 공격했습니다!");

            // 공격 이벤트 발생
            OnAttackEvent?.Invoke(Att);
        }

        /// <summary>
        /// 공격 속도에 맞춰 캐릭터가 계속 공격을 반복하는 메서드 (Timer 기반, 스레드 직접 사용하지 않음)
        /// </summary>
        /// <param name="takeDamage">공격력이 전달되는 델리게이트 (상대방의 TakeDamage 등)</param>
        public void StartAutoAttackTimer(Action<int> takeDamage)
        {
            // 이미 타이머가 동작 중이면 중복 실행 방지
            if (_autoAttackTimer != null)
                return;

            // 첫 공격은 AttackSpeed만큼 대기 후 시작
            _autoAttackTimer = new Timer(_ =>
            {
                Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - _autoAttackTimer - {Name}");

                // 캐릭터가 죽었으면 타이머 중지
                if (!IsAlive())
                {
                    Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - IsAlive == false - {Name}");
                    StopAutoAttack();
                    return;
                }

                Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - GetAttackPower - {Name}");

                int att = GetAttackPower();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{Name} 이 {att} 공격했습니다.\n");
                
                OnAttackEvent?.Invoke(att);

            }, null, (int)AttackSpeed, (int)AttackSpeed); // AttackSpeed 후 첫 실행, AttackSpeed 간격 반복
        }

        /// <summary>
        /// 공격 속도에 맞춰 캐릭터가 계속 공격을 반복하는 메서드 (Timer 기반)
        /// </summary>
        /// <param name="getTarget">공격 대상을 반환하는 델리게이트</param>
        /// <param name="isGameOver">게임 종료 여부를 반환하는 델리게이트</param>
        public void StartAutoAttackThread(Action<int> takeDamage)
        {
            // 이미 타이머가 동작 중이면 중복 실행 방지
            if (_autoAttackTimer != null)
                return;

            _autoAttackTimer = new Timer(_ =>
            {
                Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - _autoAttackTimer - {Name}");

                // 게임이 끝났거나 캐릭터가 죽었으면 타이머 중지
                if (IsAlive() == false)
                {
                    Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - IsAlive == false - {Name}");

                    StopAutoAttack();
                    _autoAttackTimer?.Dispose();
                    _autoAttackTimer = null;
                    return;
                }

                Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - GetAttackPower - {Name}");

                int att = GetAttackPower();
                OnAttackEvent?.Invoke(att);

            }, null, (int)AttackSpeed, (int)AttackSpeed); // 즉시 시작, AttackSpeed 간격 반복
        }

        /// <summary>
        /// 각 캐릭터가 독립적으로 공격 속도에 따라 공격을 반복하는 비동기 메서드
        /// </summary>
        /// <param name="getTarget">공격 대상을 반환하는 델리게이트</param>
        /// <param name="isGameOver">게임 종료 여부를 반환하는 델리게이트</param>
        /// <returns>Task</returns>
        public async Task StartAutoAttackAsync(Func<BaseCharacter?> getTarget, Func<bool> isGameOver)
        {
            while (!isGameOver() && IsAlive())
            {
                await Task.Delay((int)AttackSpeed);
                var target = getTarget();
                if (target != null && target.IsAlive())
                {
                    int att = GetAttackPower();
                    target.TakeDamage(att);
                    Console.WriteLine($"{Name} attacks {target.Name} for {att} damage!");
                    OnAttackEvent?.Invoke(att);
                }
            }
        }

        /// <summary>
        /// 자동 공격을 중지하는 메서드
        /// </summary>
        public void StopAutoAttack()
        {
            _autoAttackTimer?.Dispose();
            _autoAttackTimer = null;
        }

        /// <summary>
        /// 캐릭터가 살아있는지 여부 반환
        /// </summary>
        /// <returns>체력이 0보다 크면 true, 아니면 false</returns>
        public bool IsAlive()
        {
            return Health > 0;
        }

        public int GetAttackPower()
        {
            // 공격력의 70% ~ 130% 범위에서 랜덤하게 결정 (최소 1 보장)
            double min = AttackPower * 0.7;
            double max = AttackPower * 1.3;
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return Math.Max(1, random.Next((int)min, (int)max + 1));
        }

        public void AddOnAttackEvent(Action<int> action)
        {
            if (action != null)
            {
                OnAttackEvent += action;
            }
        }

        public void AddOnHealthChangedEvent(Action<int> action)
        {
            if (action != null)
            {
                OnHealthChangedEvent += action;
            }
        }
    }
}
