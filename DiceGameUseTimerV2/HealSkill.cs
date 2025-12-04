public class HealSkill : BaseSkill
{
    public HealSkill()
    {
        SkillName = "힐링";
        Condition = UseSkillCondition.HealthRatio;
        ConditionValue = 30; // 체력 30% 이하일 때 사용
        CooldownSeconds = 5.0f; // 쿨타임 5초
    }
}
