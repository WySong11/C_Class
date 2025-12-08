using DiceGameUseTimer;
using System;
using System.Reflection.Emit;

public class HealerCharacter : BaseCharacter
{
    public HealerCharacter(int id, string name, int level)
        : base(id, name, health: 150, attackPower: 10, attackSpeed: 1250)
    {
        classtype = ClassType.Healer;
        skill = new HealSkill();

        // 예시) HealSkill 사용
        // skill = new HealSkill( ... );

        SetLevelData(level);
    }

    public HealerCharacter(CharacterConfig config, int id, string name, int level)
        : base(id, name, config.Stats.Health, config.Stats.AttackPower, config.Stats.AttackSpeed)
    {
        classtype = ClassType.Healer;

        // 여기에서 BaseSkill -> HealSkill 로 감싸서 실제 타입을 HealSkill 로 만든다
        if (config.Skill != null)
        {
            skill = new HealSkill(config.Skill);
        }
        else
        {
            skill = new HealSkill();   // 혹시 CSV 에 Skill 이 비어 있으면 기본값 사용
        }

        SetLevelData(level);
    }

    public override void UseSkill()
    {
        if (skill == null) return;

        base.UseSkill();

        // 스킬 종류에 따라 효과 분기
        if (skill is HealSkill)
        {
            // 힐 스킬
            SaveLog.WriteLog(ConsoleColor.Magenta, skill.UseSkillMessage());

            // 예시로 최대 체력의 30% 회복
            HealPercentage(skill.SkillMultiplier );
            return;
        }

        // 혹시 다른 스킬 타입이 추가되었을 때 기본 처리
        int defaultDamage = GetAttackPower();
        RaiseAttack(defaultDamage, true);
    }
}
