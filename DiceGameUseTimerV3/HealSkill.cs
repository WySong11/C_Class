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
}
