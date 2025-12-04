
public class BaseSkill
{
    public string SkillName;

    public UseSkillCondition Condition;

    public int ConditionValue;

    public float CooldownSeconds;

    public enum UseSkillCondition
    {
        Always,         // 쿨마다 무조건 사용
        HealthRatio,    // 체력 비율 이하일 때 사용
        AttackCount,    // 공격 횟수마다 사용
    }

    public string UseSkillMessage()
    {
        return $"{SkillName} 스킬 사용!";
    }
}