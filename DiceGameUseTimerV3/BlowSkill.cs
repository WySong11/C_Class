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
}

