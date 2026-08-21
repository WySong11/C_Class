using System;


namespace TestClass
{
    public class WizardClass : PlayerClass, ISkillUser, IUseItem
    {
        public WizardClass(int id, string name, int maxHP, int hp, int attackPower) : base(id, name, maxHP, hp, attackPower)
        {
            SetClassType(ClassType.Wizard);

            Console.WriteLine($"33: WizardClass {m_name} 생성됨");
        }

        // 추상화 클래스를 구현함
        public override void Attack()
        {
            Console.WriteLine($"Wizard Attack => {m_attackPower}");
        }

        //////////////////////////////////
        // Interfacte 구현

        public void UseSkill()
        {
            Console.WriteLine($"\n{m_name} 스킬을 사용한다.\n");
        }

        public void UseItem()
        {
            Console.WriteLine($"\n{m_name} 아이템을 사용한다.\n");
        }
    }
}
