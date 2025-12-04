using DiceGameUseTimer;
using System.Reflection.Emit;

public class HealerCharacter : BaseCharacter
{
    public HealerCharacter(int id, string name, int level)
        : base(id, name, health: 150, attackPower: 10, attackSpeed: 1250)
    {
        classtype = ClassType.Healer;
        skill = new HealSkill();

        // 예시) HealSkill 사용
        // skill = new HealSkill( ... );

        SetLevelData(level);
    }
}
