using DiceGameUseTimer;
using System;
using System.Reflection.Emit;

public class MageCharacter : BaseCharacter
{
    public MageCharacter(int id, string name, int level)
        : base(id, name, health: 100, attackPower: 15, attackSpeed: 1500)
    {
        classtype = ClassType.Mage;
        skill = new FireballSkill();

        // 예시) FireballSkill 사용
        // skill = new FireballSkill( ... );

        SetLevelData(level);
    }

    public MageCharacter(CharacterConfig config, int id, string name, int level)
        : base(id, name, config.Stats.Health, config.Stats.AttackPower, config.Stats.AttackSpeed)
    {
        classtype = ClassType.Warrior;

        // 여기에서 BaseSkill -> HealSkill 로 감싸서 실제 타입을 HealSkill 로 만든다
        if (config.Skill != null)
        {
            skill = new FireballSkill(config.Skill);
        }
        else
        {
            skill = new FireballSkill();   // 혹시 CSV 에 Skill 이 비어 있으면 기본값 사용
        }

        SetLevelData(level);
    }

    public override void UseSkill()
    {        
        if (skill == null) return;

        base.UseSkill();

        if (skill is FireballSkill)
        {
            // 파이어볼 스킬: 평타의 3배 데미지 예시
            float damage = GetAttackPower() * 1.5f;
            RaiseAttack((int)MathF.Ceiling(damage), true);
            return;
        }

        // 혹시 다른 스킬 타입이 추가되었을 때 기본 처리
        int defaultDamage = GetAttackPower();
        RaiseAttack(defaultDamage, true);
    }
}
