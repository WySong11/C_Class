using System;

namespace DiceGameUseTimer
{
    public class PlayerCharacter : BaseCharacter
    {
        // 5번 공격마다 스킬 사용
        private int _attackCount = 0;

        // 스킬 쿨타임 3초
        private static readonly int SkillCooldownMilliseconds = 3000;
        private DateTime _lastSkillTime = DateTime.MinValue;

        /// <summary>
        /// 기본 생성자, 플레이어 캐릭터의 기본 속성을 설정합니다.
        /// </summary>
        public PlayerCharacter() : base(1000, "Player", 80, 10, 1000)
        {
        }

        /// <summary>
        /// 이름, 체력, 공격력을 지정하는 생성자
        /// </summary>
        /// <param name="name">플레이어 이름</param>
        /// <param name="health">초기 체력</param>
        /// <param name="attackPower">초기 공격력</param>
        /// <param name="attackSpeed">초기 공격 속도 (밀리초 단위)</param>
        public PlayerCharacter(int id, string name, int health, int attackPower, double attackSpeed)
            : base(id, name, health, attackPower, attackSpeed)
        {
        }

        /// <summary>
        /// 자동 공격 한 틱마다 호출.
        /// 5번 공격마다, 쿨이 돌아 있으면 5배 데미지 스킬 사용.
        /// </summary>
        protected override void OnAutoAttackTick()
        {
            DateTime now = DateTime.Now;
            bool skillReady = (now - _lastSkillTime).TotalMilliseconds >= SkillCooldownMilliseconds;

            _attackCount++;

            if (skillReady && _attackCount >= 5)
            {
                // 스킬 발동
                _attackCount = 0;
                _lastSkillTime = now;

                int baseDamage = GetAttackPower();
                int skillDamage = baseDamage * 5;

                // 부모가 가진 RaiseAttack을 호출해서 이벤트 + 로그 처리
                RaiseAttack(skillDamage, true);
            }
            else
            {
                // 일반 공격
                int damage = GetAttackPower();
                RaiseAttack(damage, false);
            }
        }

        /// <summary>
        /// 소멸자
        /// </summary>
        ~PlayerCharacter()
        {
        }
    }
}
