using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Character
{
    /// <summary>
    /// 적 캐릭터의 속성과 동작을 정의하는 클래스
    /// </summary>
    public class EnemyCharacter : BaseCharacter
    {
        /// <summary>
        /// 기본 생성자. 이름, 체력, 공격력을 기본값으로 초기화
        /// </summary>
        public EnemyCharacter() : base("Enemy", 100, 10, 1000)
        {   
        }

        /// <summary>
        /// 소멸자
        /// </summary>
        ~EnemyCharacter()
        {
        }

        /// <summary>
        /// 이름, 체력, 공격력을 지정하는 생성자
        /// </summary>
        /// <param name="name">적 이름</param>
        /// <param name="health">초기 체력</param>
        /// <param name="attackPower">초기 공격력</param>
        /// <param name="attackSpeed">초기 공격 속도 (밀리초 단위)</param>"
        public EnemyCharacter(string name, int health, int attackPower, double attackSpeed) : base(name, health, attackPower, attackSpeed)
        {
        }

        /// <summary>
        /// 적 캐릭터가 도발하는 동작을 수행
        /// </summary>
        public void Taunt()
        {
            Console.WriteLine($"{Name} taunts you!");
        }
        
        /// <summary>
        /// 적 캐릭터의 정보를 문자열로 반환
        /// </summary>
        /// <returns>캐릭터 정보 문자열</returns>
        public override string ToString()
        {
            return $"{Name} (Enemy) - Health: {Health}, Attack Power: {AttackPower}";
        }
    }
}
