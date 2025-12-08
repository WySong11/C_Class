public class HealSkill : BaseSkill
{
    public HealSkill()
    {
        SkillName = "힐링";
        Condition = UseSkillCondition.HealthRatio;
        ConditionValue = 50; // 체력 30% 이하일 때 사용
        CooldownSeconds = 5.0f; // 쿨타임 5초
        SkillMultiplier = 0.3f; // 최대 체력의 30% 회복
    }

    // CSV 에서 읽어온 BaseSkill 설정을 그대로 복사하는 생성자
    public HealSkill(BaseSkill config)
    {
        // 이름은 CSV 에서 온 이름을 우선 사용
        // 비어 있으면 기본 이름 사용
        SkillName = string.IsNullOrWhiteSpace(config.SkillName)
            ? "Heal"
            : config.SkillName;

        Condition = config.Condition;
        ConditionValue = config.ConditionValue;
        CooldownSeconds = config.CooldownSeconds;
        SkillMultiplier = config.SkillMultiplier;
    }
}
