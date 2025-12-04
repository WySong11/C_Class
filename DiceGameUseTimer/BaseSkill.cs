
public class BaseSkill
{
    public string SkillName;

    public UseSkillCondition Condition;

    public int ConditionValue;

    public float CooldownSeconds;

    public enum UseSkillCondition
    {
        Always,
        Health,
        AttackCount,
    }
}