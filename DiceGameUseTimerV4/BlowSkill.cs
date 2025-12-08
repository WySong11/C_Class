public class BlowSkill : BaseSkill
{
    public BlowSkill()
    {
        SkillName = "강타";
        Condition = UseSkillCondition.Always;
        ConditionValue = 0; // 항상 사용
        CooldownSeconds = 10.0f; // 쿨타임 10초
        SkillMultiplier = 1.2f; // 120% 데미지
    }

    public BlowSkill(BaseSkill config)
    {
        // 이름은 CSV 에서 온 이름을 우선 사용
        // 비어 있으면 기본 이름 사용
        SkillName = string.IsNullOrWhiteSpace(config.SkillName)
            ? "Blow"
            : config.SkillName;
        Condition = config.Condition;
        ConditionValue = config.ConditionValue;
        CooldownSeconds = config.CooldownSeconds;
        SkillMultiplier = config.SkillMultiplier;
    }
}

