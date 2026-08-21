using System;
using System.Xml.Linq;


namespace TestClass
{
    public class WarriorClass : PlayerClass, ISkillUser
    {
        public WarriorClass() 
        {
            Console.WriteLine($"11: Warrior {m_name} 생성됨");
        }

        /*public WarriorClass(int id, string name, int maxHP, int hp, int attackPower)
        {
            Console.WriteLine($"Create Warrior => {id} , {name} , {hp} , {attackPower}");

            m_id = id;
            m_name = name;

            m_maxHp = maxHP;
            m_hp = hp;
            m_attackPower = attackPower;
        }*/

        /*public WarriorClass(int id, string name, int maxHP, int hp, int attackPower) : base(id, name, maxHP, hp, attackPower)
        {
            Console.WriteLine($"22: WarriorClass {m_name} 생성됨");
        }*/

        public WarriorClass(int id, string name, int maxHP, int hp, int attackPower)
        {
            // base : 부모 클래스
            base.InitPlayerData(id, name, maxHP, hp, attackPower);

            // this : 내 자신 클래스
            InitPlayerData(id, name, maxHP, hp, attackPower);

            SetClassType(ClassType.Warrior);

            Console.WriteLine($"22: WarriorClass {m_name} 생성됨");
        }

        // override : virtual 함수를 재정의한다고 선언
        public override void InitPlayerData(int id, string name, int maxHP, int hp, int attackPower)
        {
            Console.WriteLine($"Warrior InitPlayerData => {id} , {name} , {hp} , {attackPower}");

            m_id = id;
            m_name = name;
            m_maxHp = maxHP;
            m_hp = hp;
            m_attackPower = attackPower;
        }

        public override void TakeDamage(int damage)
        {
            // base 를 호출해서 공통 기능을 사용하고
            base.TakeDamage(damage);

            Console.WriteLine("Warrior Class TakeDamage~!!!");
        }

        // 추상화 클래스를 구현함
        public override void Attack()
        {
            Console.WriteLine($"Warrior Attack => {m_attackPower}");
        }

        //////////////////////////////////
        // Interfacte 구현

        public void UseSkill()
        {
            Console.WriteLine($"\n{m_name} 스킬을 사용한다.\n");
        }
    }
}
