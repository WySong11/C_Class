using System;
using System.Diagnostics;
using System.Threading;

namespace DiceGameUseTimer
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
    /// (플레이어, 적 등 모든 캐릭터의 공통 부모 클래스)
    /// </summary>
    public class BaseCharacter : IAttackable
    {
        /// <summary>
        /// 캐릭터의 고유 ID (게임 내에서 각 캐릭터를 구분하는 용도)
        /// </summary>
        public int ID { get; private set; } = 0;

        /// <summary>
        /// 캐릭터 이름
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 캐릭터의 현재 체력
        /// </summary>
        public int Health { get; private set; }

        /// <summary>
        /// 캐릭터의 공격력
        /// </summary>
        public int AttackPower { get; private set; }

        /// <summary>
        /// 캐릭터의 공격 속도 (밀리초 단위, 값이 작을수록 빠름)
        /// </summary>
        public double AttackSpeed { get; private set; }

        /// <summary>
        /// 목표 ID (공격 대상의 ID, 0이면 대상 없음)
        /// </summary>
        public int TargetID { get; private set; } = 0;

        /// <summary>
        /// 공격 이벤트 (공격 시 발생, 구독자에게 AttackEventArgs 전달)
        /// </summary>
        private event Action<AttackEventArgs>? OnAttackEvent;

        /// <summary>
        /// 체력 변화 이벤트 (체력 변경 시 발생, 구독자에게 HealthChangedEventArgs 전달)
        /// </summary>
        private event Action<HealthChangedEventArgs>? OnHealthChangedEvent;

        /// <summary>
        /// 자동 공격을 위한 타이머 (공격 주기마다 콜백 실행)
        /// </summary>
        private Timer? _autoAttackTimer;

        /// <summary>
        /// 기본 생성자. 속성을 기본값으로 초기화
        /// </summary>
        public BaseCharacter()
        {
            ID = 0;
            Name = string.Empty;
            Health = 10;
            AttackPower = 10;
            AttackSpeed = 100;
            TargetID = 0;
        }

        /// <summary>
        /// 소멸자 (특별한 리소스 해제 없음)
        /// </summary>
        ~BaseCharacter()
        {
        }

        /// <summary>
        /// 모든 속성을 지정하는 생성자
        /// </summary>
        /// <param name="id">캐릭터 고유 ID</param>
        /// <param name="name">캐릭터 이름</param>
        /// <param name="health">초기 체력</param>
        /// <param name="attackPower">초기 공격력</param>
        /// <param name="attackSpeed">초기 공격 속도 (ms)</param>
        public BaseCharacter(int id, string name, int health, int attackPower, double attackSpeed)
        {
            ID = id;
            Name = name;
            Health = health;
            AttackPower = attackPower;
            AttackSpeed = attackSpeed;
            TargetID = 0; // 기본값
        }

        /// <summary>
        /// 레벨에 따라 캐릭터의 능력치를 증가시킴
        /// </summary>
        /// <param name="level">적용할 레벨(1~10)</param>
        public void SetLevelData(int level)
        {
            // 레벨에 따라 체력, 공격력, 공격속도 증가
            Health += level * 10; // 레벨당 체력 10 증가
            AttackPower += level * 5; // 레벨당 공격력 5 증가
            AttackSpeed += level * 300; // 레벨당 공격 속도 300ms 증가(공격이 느려짐)

            Console.WriteLine($"\n{Name} has been leveled up to level {level}! New Health: {Health}, New Attack Power: {AttackPower}, New Attack Speed: {AttackSpeed} ms\n");
        }

        /// <summary>
        /// 공격 대상의 ID를 설정
        /// </summary>
        /// <param name="targetID">공격 대상의 ID</param>
        public void SetTargetID(int targetID)
        {
            TargetID = targetID;
            Console.WriteLine($"{Name} 의 공격 대상이 {TargetID} 로 설정되었습니다.");
        }

        /// <summary>
        /// 데미지를 받아 체력을 감소시키고, 체력 변화 이벤트를 발생시킴
        /// </summary>
        /// <param name="amount">받는 데미지 양</param>
        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0; // 체력이 0 이하로 내려가지 않도록 보정

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Name} 이 {amount} 공격받았습니다! 남은 체력 : {Health}");

            // 체력 변경 이벤트 발생 (구독자에게 알림)
            OnHealthChangedEvent?.Invoke(new HealthChangedEventArgs(ID, Health));
        }

        /// <summary>
        /// 공격 속도에 맞춰 캐릭터가 자동으로 공격을 반복하는 타이머를 시작
        /// </summary>
        public void StartAutoAttackTimer()
        {
            // 이미 타이머가 동작 중이면 중복 실행 방지
            if (_autoAttackTimer != null)
                return;

            // 첫 공격은 AttackSpeed만큼 대기 후 시작, 이후 AttackSpeed 간격으로 반복
            _autoAttackTimer = new Timer(_ =>
            {
                Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - _autoAttackTimer - {Name}");

                // 캐릭터가 죽었으면 타이머 중지
                if (!IsAlive())
                {
                    Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - IsAlive == false - {Name}");
                    StopAutoAttackTimer();
                    return;
                }

                int damage = GetAttackPower();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{Name} 이 {damage} 공격했습니다.\n");

                // 공격 대상이 지정되어 있으면 공격 이벤트 발생
                if (TargetID > 0)
                {
                    Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - TargetID > 0 - {Name}");
                }
                else
                {
                    Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - TargetID == 0 - {Name}");
                }

                // 공격 이벤트 발생 (구독자에게 알림)
                OnAttackEvent?.Invoke(new AttackEventArgs(ID, TargetID, damage));

            }, null, (int)AttackSpeed, (int)AttackSpeed); // AttackSpeed 후 첫 실행, AttackSpeed 간격 반복
        }

        /// <summary>
        /// 자동 공격 타이머를 중지
        /// </summary>
        public void StopAutoAttackTimer()
        {
            _autoAttackTimer?.Dispose();
            _autoAttackTimer = null;
        }

        /// <summary>
        /// 자동 공격 타이머를 재시작 (중지 후 다시 시작)
        /// </summary>
        public void ResetAutoAttackTimer()
        {
            StopAutoAttackTimer();
            StartAutoAttackTimer();
        }

        /// <summary>
        /// 모든 이벤트 핸들러를 해제 (이벤트 구독 해제)
        /// </summary>
        public void ClearAllEvents()
        {
            OnAttackEvent = null;
            OnHealthChangedEvent = null;
        }

        /// <summary>
        /// 캐릭터가 살아있는지 여부 반환
        /// </summary>
        /// <returns>체력이 0보다 크면 true, 아니면 false</returns>
        public bool IsAlive()
        {
            return Health > 0;
        }

        /// <summary>
        /// 실제 공격 데미지를 랜덤으로 계산 (공격력의 70%~130% 범위, 최소 1)
        /// </summary>
        /// <returns>계산된 공격 데미지</returns>
        public int GetAttackPower()
        {
            double min = AttackPower * 0.7;
            double max = AttackPower * 1.3;
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return Math.Max(1, random.Next((int)min, (int)max + 1));
        }

        /// <summary>
        /// 공격 이벤트 구독자 추가
        /// </summary>
        /// <param name="action">공격 이벤트 발생 시 실행할 델리게이트</param>
        public void AddOnAttackEvent(Action<AttackEventArgs> action)
        {
            if
