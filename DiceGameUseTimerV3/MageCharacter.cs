using DiceGameUseTimer;
using System.Reflection.Emit;

public class MageCharacter : BaseCharacter
{
    public MageCharacter(int id, string name, int level)
        : base(id, name, health: 100, attackPower: 15, attackSpeed: 1500)
    {
        classtype = ClassType.Mage;
        skill = new FireballSkill();

        // 예시) FireballSkill 사용
        // skill = new FireballSkill( ... );

        SetLevelData(level);
    }
}
