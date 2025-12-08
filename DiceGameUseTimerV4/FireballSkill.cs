public class FireballSkill : BaseSkill
{
    public FireballSkill()
    {
        SkillName = "파이어볼";
        Condition = UseSkillCondition.AttackCount;
        ConditionValue = 3; // 5번 공격마다 사용
        CooldownSeconds = 3.0f; // 쿨타임 3초
        SkillMultiplier = 1.5f; // 150% 데미지
    }

    public FireballSkill(BaseSkill config)
    {
        // 이름은 CSV 에서 온 이름을 우선 사용
        // 비어 있으면 기본 이름 사용
        SkillName = string.IsNullOrWhiteSpace(config.SkillName)
            ? "Fireball"
            : config.SkillName;
        Condition = config.Condition;
        ConditionValue = config.ConditionValue;
        CooldownSeconds = config.CooldownSeconds;
        SkillMultiplier = config.SkillMultiplier;
    }
}