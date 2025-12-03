using System;

namespace DiceGameUseTimer
{
    /// <summary>
    /// 적 캐릭터의 속성과 동작을 정의하는 클래스
    /// </summary>
    public class EnemyCharacter : BaseCharacter
    {
        // 힐 쿨타임 5초
        private static readonly int HealCooldownMilliseconds = 5000;
        private DateTime _lastHealTime = DateTime.MinValue;

        /// <summary>
        /// 기본 생성자, 적 캐릭터의 기본 속성을 설정합니다.
        /// </summary>
        public EnemyCharacter() : base(2000, "Enemy", 100, 7, 1050)
        {
        }

        /// <summary>
        /// 이름, 체력, 공격력을 지정하는 생성자
        /// </summary>
        /// <param name="name">적 이름</param>
        /// <param name="health">초기 체력</param>
        /// <param name="attackPower">초기 공격력</param>
        /// <param name="attackSpeed">초기 공격 속도 (밀리초 단위)</param>
        public EnemyCharacter(int id, string name, int health, int attackPower, double attackSpeed)
            : base(id, name, health, attackPower, attackSpeed)
        {
        }

        /// <summary>
        /// 자동 공격 한 틱마다 호출.
        /// 체력이 50% 이하고 쿨이 끝났으면 힐 스킬 사용, 아니면 평타.
        /// </summary>
        protected override void OnAutoAttackTick()
        {
            if (ShouldUseHealSkill())
            {
                UseHealSkill();
                return;
            }

            // 평타는 부모 기본 로직 사용
            base.OnAutoAttackTick();
        }

        private bool ShouldUseHealSkill()
        {
            if (Stats.MaxHealth <= 0)
                return false;

            // 체력 비율 50% 이하인지
            double ratio = (double)Stats.Health / Stats.MaxHealth;
            if (ratio > 0.5)
                return false;

            // 쿨타임 체크
            if ((DateTime.Now - _lastHealTime).TotalMilliseconds < HealCooldownMilliseconds)
                return false;

            return true;
        }

        private void UseHealSkill()
        {
            _lastHealTime = DateTime.Now;

            string log = $"{Name} 의 힐 스킬 발동!\n";
            SaveLog.WriteLog(ConsoleColor.Magenta, log);

            // 최대 체력의 20% 회복
            HealPercentage(0.2);
        }

        /// <summary>
        /// 소멸자
        /// </summary>
        ~EnemyCharacter()
        {
        }
    }
}
