using System;

namespace TestClass
{
    public class ArcherClass : PlayerClass, IUseItem
    {
        public ArcherClass(int id, string name, int maxHP, int hp, int attackPower) : base(id, name, maxHP, hp, attackPower)
        {
            SetClassType(ClassType.Archer);

            Console.WriteLine($"44: ArcherClass {m_name} 생성됨");
        }

        // 추상화 클래스를 구현함
        public override void Attack()
        {
            Console.WriteLine($"Archer Attack => {m_attackPower}");
        }

        public void UseItem()
        {
            Console.WriteLine($"\n{m_name} 아이템을 사용한다.\n");
        }
    }
}
