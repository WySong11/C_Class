using System;

namespace DiceGameUseTimer
{
    /// <summary>
    /// 적 캐릭터의 속성과 동작을 정의하는 클래스
    /// </summary>
    public class EnemyCharacter : BaseCharacter
    {
        /// <summary>
        /// 기본 생성자, 적 캐릭터의 기본 속성을 설정합니다.
        /// </summary>
        public EnemyCharacter() : base(2000, "Enemy", 100, 7, 1050)
        {
            // ★ 적은 힐 스킬 사용
            skill = new HealSkill();
            //classtype = ClassType.Healer;
        }

        /// <summary>
        /// 이름, 체력, 공격력을 지정하는 생성자
        /// </summary>
        public EnemyCharacter(int id, string name, int health, int attackPower, double attackSpeed)
            : base(id, name, health, attackPower, attackSpeed)
        {
            // ★ 생성자 오버로드에서도 동일하게 스킬 세팅
            skill = new HealSkill();
            //classtype = ClassType.Healer;
        }

        /// <summary>
        /// 자동 공격 한 틱마다 호출.
        /// 별도 특수 로직 없으면 부모 로직 사용.
        /// (체력 30% 이하 + 5초 쿨이면 힐, 아니면 평타)
        /// </summary>
        protected override void OnAutoAttackTick()
        {
            base.OnAutoAttackTick();
        }

        ~EnemyCharacter()
        {
        }
    }
}
