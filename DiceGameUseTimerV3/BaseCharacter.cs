using System;
using System.Diagnostics;
using System.Threading;

namespace DiceGameUseTimer
{
    public enum ClassType
    {
        None,
        Warrior,
        Mage,
        Healer,
    }

    /// <summary>
    /// 캐릭터의 능력치 묶음(체력, 최대체력, 공격력, 공격속도)
    /// </summary>
    public class CharacterStats
    {
        public int Health;
        public int MaxHealth;
        public int AttackPower;
        public double AttackSpeed;

        public CharacterStats() { }

        public CharacterStats(int health, int maxHealth, int attackPower, double attackSpeed)
        {
            Health = health;
            MaxHealth = maxHealth;
            AttackPower = attackPower;
            AttackSpeed = attackSpeed;
        }
    }

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
        /// 캐릭터 능력치 묶음 (Health, MaxHealth, AttackPower, AttackSpeed)
        /// </summary>
        public CharacterStats Stats;

        /// <summary>
        /// 목표 ID (공격 대상의 ID, 0이면 대상 없음)
        /// </summary>
        public int TargetID { get; private set; } = 0;

        /// <summary>
        /// 공격 이벤트 (공격 시 발생, 구독자에게 AttackEventArgs 전달)
        /// </summary>
        public event Action<AttackEventArgs>? OnAttackEvent;

        /// <summary>
        /// 체력 변화 이벤트 (체력 변경 시 발생, 구독자에게 HealthChangedEventArgs 전달)
        /// </summary>
        public event Action<HealthChangedEventArgs>? OnHealthChangedEvent;

        /// <summary>
        /// 자동 공격을 위한 타이머 (공격 주기마다 콜백 실행)
        /// </summary>
        private Timer? _autoAttackTimer;

        public BaseSkill? skill;

        public ClassType classtype = ClassType.None;

        // 2025.12.04
        // 스킬용 상태 값 추가
        protected int _attackCountSinceLastSkill = 0;
        protected DateTime _lastSkillTime = DateTime.MinValue;

        public bool IsHaveSkill
        {
            get
            {
                return skill != null;
            }
        }


        /// <summary>
        /// 기본 생성자. 능력치를 기본값으로 초기화
        /// </summary>
        public BaseCharacter()
        {
            ID = 0;
            Name = string.Empty;
            // 기본 능력치 설정: Health 10, MaxHealth 10, AttackPower 10, AttackSpeed 100ms
            Stats = new CharacterStats(10, 10, 10, 100);
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
            Stats = new CharacterStats(health, health, attackPower, attackSpeed);
            TargetID = 0; // 기본값
        }

        /// <summary>
        /// 레벨에 따라 캐릭터의 능력치를 증가시킴
        /// </summary>
        /// <param name="level">적용할 레벨(1~10)</param>
        public void SetLevelData(int level)
        {
            if (Stats == null) Stats = new CharacterStats(10, 10, 10, 100);

            // 레벨에 따라 체력, 공격력, 공격속도 증가
            Stats.Health += level * 10;          // 레벨당 체력 10 증가
            Stats.AttackPower += level * 5;      // 레벨당 공격력 5 증가
            Stats.AttackSpeed += level * 300;    // 레벨당 공격 속도 300ms 증가(공격이 느려짐)

            // 레벨 반영 후의 체력을 최대 체력으로 사용
            Stats.MaxHealth = Stats.Health;
        }

        /// <summary>
        /// 공격 대상의 ID를 설정
        /// </summary>
        /// <param name="targetID">공격 대상의 ID</param>
        public void SetTargetID(int targetID)
        {
            TargetID = targetID;
        }

        /// <summary>
        /// 데미지를 받아 체력을 감소시키고, 체력 변화 이벤트를 발생시킴
        /// </summary>
        /// <param name="amount">받는 데미지 양</param>
        public void TakeDamage(int amount)
        {
            if (Stats == null) return;

            Stats.Health -= amount;
            if (Stats.Health < 0) Stats.Health = 0; // 체력이 0 이하로 내려가지 않도록 보정

            string logMessage = $"{Name} 이 {amount} 공격받았습니다! 남은 체력 : {Stats.Health}\n";
            SaveLog.WriteLog(ConsoleColor.Cyan, logMessage);

            // 체력 변경 이벤트 발생 (구독자에게 알림)
            OnHealthChangedEvent?.Invoke(new HealthChangedEventArgs(ID, Stats.Health));
        }

        /// <summary>
        /// 공격 이벤트 + 로그를 공통으로 처리하는 메서드
        /// (자식 클래스는 이 메서드를 호출만 하면 됨)
        /// </summary>
        /// <param name="damage">공격 데미지</param>
        /// <param name="isSkill">스킬 공격 여부</param>
        protected void RaiseAttack(int damage, bool isSkill = false)
        {
            if (!IsAlive()) return;

            if (isSkill)
            {
                string skillLog = skill != null ? skill.UseSkillMessage() : string.Empty;
                SaveLog.WriteLog(ConsoleColor.Magenta, skillLog);
            }
            else
            {
                string logMessage = $"{Name} 이 {damage} 공격했습니다.\n";
                SaveLog.WriteLog(ConsoleColor.Red, logMessage);
            }

            OnAttackEvent?.Invoke(new AttackEventArgs(ID, TargetID, damage));
        }

        /// <summary>
        /// 자동 공격 한 틱에서 수행할 동작
        /// (스킬 사용 가능하면 스킬, 아니면 평타)
        /// </summary>
        protected virtual void OnAutoAttackTick()
        {
            if (!IsAlive()) return;

            // 평타(또는 스킬) 시도 카운트 증가
            _attackCountSinceLastSkill++;

            // 스킬 사용 가능한지 먼저 체크
            if (IsSkillUable())
            {
                UseSkill();
                return;
            }

            // 스킬을 못 쓰면 평타
            int damage = GetAttackPower();
            RaiseAttack(damage, false);
        }

        /// <summary>
        /// 공격 속도에 맞춰 캐릭터가 자동으로 공격을 반복하는 타이머를 시작
        /// </summary>
        public void StartAutoAttackTimer()
        {
            if (Stats == null) return;

            // 이미 타이머가 동작 중이면 중복 실행 방지
            if (_autoAttackTimer != null)
                return;

            // 안전하게 attack speed를 정수로 변환
            int interval = Math.Max(1, (int)Stats.AttackSpeed);

            // 첫 공격은 AttackSpeed만큼 대기 후 시작, 이후 AttackSpeed 간격으로 반복
            _autoAttackTimer = new Timer(_ =>
            {
                // 캐릭터가 죽었으면 타이머 중지
                if (!IsAlive())
                {
                    StopAutoAttackTimer();
                    return;
                }

                // 한 틱의 자동 공격 동작
                OnAutoAttackTick();

            }, null, interval, interval);
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
            return Stats != null && Stats.Health > 0;
        }

        /// <summary>
        /// 실제 공격 데미지를 랜덤으로 계산 (공격력의 70%~130% 범위, 최소 1)
        /// </summary>
        /// <returns>계산된 공격 데미지</returns>
        public int GetAttackPower()
        {
            if (Stats == null) return 1;
            double min = Stats.AttackPower * 0.7;
            double max = Stats.AttackPower * 1.3;
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return Math.Max(1, random.Next((int)min, (int)max + 1));
        }

        /// <summary>
        /// 퍼센트 기반 힐 (예: 0.2 = 최대 체력의 20%)
        /// </summary>
        /// <param name="percentage">0~1 사이 힐 비율</param>
        protected void HealPercentage(double percentage)
        {
            if (Stats == null) return;
            if (percentage <= 0) return;
            if (Stats.MaxHealth <= 0) return;
            if (!IsAlive()) return;

            int healAmount = (int)(Stats.MaxHealth * percentage);
            if (healAmount <= 0) healAmount = 1;

            int before = Stats.Health;
            Stats.Health += healAmount;
            if (Stats.Health > Stats.MaxHealth) Stats.Health = Stats.MaxHealth;

            int actualHeal = Stats.Health - before;

            string logMessage = $"{Name} 이 {actualHeal} 만큼 회복했습니다! 현재 체력 : {Stats.Health}\n";
            SaveLog.WriteLog(ConsoleColor.Green, logMessage);

            OnHealthChangedEvent?.Invoke(new HealthChangedEventArgs(ID, Stats.Health));
        }

        /// <summary>
        /// 공격 이벤트 구독자 추가
        /// </summary>
        /// <param name="action">공격 이벤트 발생 시 실행할 델리게이트</param>
        public void AddOnAttackEvent(Action<AttackEventArgs> action)
        {
            if (action != null)
            {
                OnAttackEvent += action;
            }
        }

        public void AddOnHealthChangedEvent(Action<HealthChangedEventArgs> action)
        {
            if (action != null)
            {
                OnHealthChangedEvent += action;
            }
        }

        public bool IsSkillUable()
        {
            if (!IsHaveSkill) return false;
            if (!IsAlive()) return false;
            if (Stats == null) return false;

            var now = DateTime.Now;

            // 쿨타임 체크
            if ((now - _lastSkillTime).TotalSeconds < skill!.CooldownSeconds)
                return false;

            switch (skill.Condition)
            {
                case BaseSkill.UseSkillCondition.Always:
                    // 쿨만 돌았으면 사용
                    return true;

                case BaseSkill.UseSkillCondition.AttackCount:
                    // 일정 공격 횟수마다 사용
                    return _attackCountSinceLastSkill >= skill.ConditionValue;

                case BaseSkill.UseSkillCondition.HealthRatio:
                    // 체력 비율이 ConditionValue(%) 이하일 때 사용
                    if (Stats.MaxHealth <= 0) return false;
                    double ratio = (double)Stats.Health / Stats.MaxHealth * 100.0;
                    return ratio <= skill.ConditionValue;

                default:
                    return false;
            }
        }

        public virtual void UseSkill()
        {
            _lastSkillTime = DateTime.Now;
            _attackCountSinceLastSkill = 0;

            // 혹시 다른 스킬 타입이 추가되었을 때 기본 처리
/*            int defaultDamage = GetAttackPower();
            RaiseAttack(defaultDamage, true);*/
        }
    }
}