using System;

namespace DiceGameUseTimer
{
    public class PlayerCharacter : BaseCharacter
    {
        /// <summary>
        /// 기본 생성자, 플레이어 캐릭터의 기본 속성을 설정합니다.
        /// </summary>
        public PlayerCharacter() : base(1000, "Player", 80, 10, 1000)
        {
            // ★ 플레이어는 강타 스킬 사용
            skill = new BlowSkill();
            //classtype = ClassType.Warrior;
        }

        /// <summary>
        /// 이름, 체력, 공격력을 지정하는 생성자
        /// </summary>
        public PlayerCharacter(int id, string name, int health, int attackPower, double attackSpeed)
            : base(id, name, health, attackPower, attackSpeed)
        {
            // ★ 생성자 오버로드에서도 동일하게 스킬 세팅
            skill = new BlowSkill();
            //classtype = ClassType.Warrior;
        }

        /// <summary>
        /// 자동 공격 한 틱마다 호출.
        /// 특별한 추가 로직이 없으면 부모 로직 그대로 사용.
        /// </summary>
        protected override void OnAutoAttackTick()
        {
            base.OnAutoAttackTick();
        }

        ~PlayerCharacter()
        {
        }
    }
}
