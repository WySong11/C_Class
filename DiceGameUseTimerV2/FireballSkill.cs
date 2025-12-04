public class FireballSkill : BaseSkill
{
    public FireballSkill()
    {
        SkillName = "파이어볼";
        Condition = UseSkillCondition.AttackCount;
        ConditionValue = 5; // 5번 공격마다 사용
        CooldownSeconds = 3.0f; // 쿨타임 3초
    }
}