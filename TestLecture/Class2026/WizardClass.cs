using System;


namespace TestClass
{
    public class WizardClass : PlayerClass
    {
        public WizardClass(int id, string name, int maxHP, int hp, int attackPower) : base(id, name, maxHP, hp, attackPower)
        {
            SetClssType(ClassType.Wizard);

            Console.WriteLine($"33: WizardClass {m_name} 생성됨");
        }

        // 추상화 클래스를 구현함
        public override void Attack()
        {
            Console.WriteLine($"Wizard Attack => {m_attackPower}");
        }
    }
}
