using DiceGameUseTimer;
using System;

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

    public override void UseSkill()
    {
        if (skill == null) return;

        base.UseSkill();

        if (skill is BlowSkill)
        {
            // 강타 스킬: 평타의 2배 데미지 예시
            float damage = GetAttackPower() * 1.2f;
            RaiseAttack((int)MathF.Ceiling(damage), true);   // true 이므로 RaiseAttack에서 스킬 로그 출력
            return;
        }

        // 혹시 다른 스킬 타입이 추가되었을 때 기본 처리
        int defaultDamage = GetAttackPower();
        RaiseAttack(defaultDamage, true);
    }
}
