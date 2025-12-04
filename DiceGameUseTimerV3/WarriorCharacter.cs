using DiceGameUseTimer;

public class WarriorCharacter : BaseCharacter
{
    public WarriorCharacter(int id, string name, int level)
        : base(id, name, health: 200, attackPower: 7, attackSpeed: 1000)
    {
        classtype = ClassType.Warrior;
        skill = new BlowSkill();

        // 예시) 이미 BlowSkill 같은 스킬 클래스를 만들었다면 여기서 연결
        // skill = new BlowSkill( ... );

        SetLevelData(level);
    }
}
