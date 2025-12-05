public enum UseSkillCondition
{
    None,
    Always,
    HealthRatio,
    AttackCount,
}

public class BaseSkill
{
    public string? SkillName;

    public UseSkillCondition Condition = UseSkillCondition.None;

    public int ConditionValue = -1;

    public float SkillMoltiplier = 1f;

    public float CooldownSeconds = 0f;

    public BaseSkill()
    {}

    public BaseSkill(string skillName, UseSkillCondition condition, int conditionValue, float skillMoltiplier, float cooldownSeconds)
    {
        SkillName = skillName;
        Condition = condition;
        ConditionValue = conditionValue;
        SkillMoltiplier = skillMoltiplier;
        CooldownSeconds = cooldownSeconds;
    }

    public override string? ToString()
    {
        return $"{SkillName} (Condition: {Condition}, Value: {ConditionValue}, Multiplier: {SkillMoltiplier}, Cooldown: {CooldownSeconds}s)";
    }

    public string UseSkillMessage()
    {
        return $"{SkillName} 스킬 사용!\n";
    }
}

public class BlowSkill : BaseSkill
{
    public BlowSkill() : base("강타", UseSkillCondition.Always, 0, 1.2f, 5.0f)
    {
    }
}

public class HealSkill : BaseSkill
{
    public HealSkill() : base("치유", UseSkillCondition.HealthRatio, 50, 20f, 10.0f)
    {
    }
}

public class FireballSkill : BaseSkill
{
    public FireballSkill() : base("파이어볼", UseSkillCondition.AttackCount, 5, 1.5f, 7.0f)
    {
    }
}
