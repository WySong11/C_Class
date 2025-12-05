using DiceGameUseTimer;
using System;
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

    public override void UseSkill()
    {        
        if (skill == null) return;

        base.UseSkill();

        if (skill is FireballSkill)
        {
            // 파이어볼 스킬: 평타의 3배 데미지 예시
            float damage = GetAttackPower() * 1.5f;
            RaiseAttack((int)MathF.Ceiling(damage), true);
            return;
        }

        // 혹시 다른 스킬 타입이 추가되었을 때 기본 처리
        int defaultDamage = GetAttackPower();
        RaiseAttack(defaultDamage, true);
    }
}
