using System;

namespace TestClass
{
    public enum ClassType
    {
        Warrior,
        Wizard,
        Archer,
        Healer,
    }

    // private : 외부에 노출하지 않을 때 사용
    // public : 공용 클래스
    // abstaract : 추상화
    public abstract class PlayerClass
    {
        protected ClassType m_type;

        // 변수 선언은 기본이 Private
        // Protected : 자식 클래스에서도 사용 가능
        protected int m_id;
        protected string m_name;

        protected int m_maxHp;
        protected int m_hp;
        protected int m_attackPower;

        public int m_moveSpeed;

        protected int m_targetId;

        // 생성자 : 클래스가 생성될 때 호출됨
        public PlayerClass()
        {
            Console.WriteLine($"Player {m_name} 생성됨");

            GameManager.Instance.AddPlayer(this);
        }

        // 소멸자 : 클래스가 파괴될 때 호출됨
        ~PlayerClass()
        {
            Console.WriteLine($"{m_name} 파괴됨");
        }

        // 생성할 때 인자들을 통해, 변수값을 설정
        public PlayerClass(int id, string name, int maxHP, int hp, int attackPower)
        {
            Console.WriteLine($"Create Player => {id} , {name} , {hp} , {attackPower}");

            m_id = id;
            m_name = name;
            m_maxHp = maxHP;
            m_hp = hp;
            m_attackPower = attackPower;

            GameManager.Instance.AddPlayer(this);
        }

        // virtual : 자식 클래스들이 재정의 할 수 있다고 선언
        public virtual void InitPlayerData(int id, string name, int maxHP, int hp, int attackPower)
        {
            Console.WriteLine($"Player InitPlayerData => {id} , {name} , {hp} , {attackPower}");

            m_id = id;
            m_name = name;
            m_maxHp = maxHP;
            m_hp = hp;
            m_attackPower = attackPower;
        }

        public void PrintData()
        {
            Console.WriteLine($"\nID : {m_id} , Name : {m_name}\n");
        }

        private void SetHP(int hp)
        {
            float percent = CommonUtil.GetPercent(m_maxHp, Math.Clamp(hp, 0, m_maxHp));


            Console.WriteLine($"{m_name} ( {m_hp} => {Math.Clamp(hp, 0, m_maxHp)} , ( {CommonUtil.GetPercentString(percent)}% )");

            Console.WriteLine( CommonUtil.GetPercentConvert(percent).ToString() );

            //m_hp = hp;

            // Math.Max : 두 수 중에 큰 수를 반환한다.
            // m_hp 가 음수를 가져가지 못하게, 최소값을 0으로 설정한다.
            //m_hp = Math.Max( 0, hp );            

            // Math.Min : 두 수 중에 작은 수를 반환한다.
            //m_hp = Math.Min( m_maxHP, hp );

            // Math.clamp : ( 현재값 , 최소값 , 최대값 )
            // 현재값이 최소값보다 작으면 최소값을 반환
            // 현재값이 최대값도다 크면 최대값을 반환
            // 현재값이 최소값과 최대값 사이면 현재값을 반환
            m_hp = Math.Clamp(hp, 0, m_maxHp);

            if (m_hp <= 0)
            {
                Console.WriteLine($"{m_name} is Die~!!");

                GameManager.Instance.RemovePlayer(this);                
            }
        }

        public virtual void TakeDamage(int damage)
        {
            SetHP( m_hp -  damage );
        }

        public void TakeHeal(int heal)
        {
            SetHP( m_hp + heal );
        }

        public abstract void Attack();

        protected void SetClassType(ClassType inType)
        {
            m_type = inType;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"{m_name} , {m_type}"
                );
        }

        public void SetTargetId(int id)
        {
            m_targetId = id;
        }

        public int GetID() => m_id;
        public string GetName() => m_name;
        public int GetAttackPower() => m_attackPower;
    }
}
