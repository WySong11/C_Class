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
}