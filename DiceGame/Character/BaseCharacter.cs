using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Character
{
    /// <summary>
    /// 캐릭터가 공격을 받을 수 있음을 나타내는 인터페이스
    /// </summary>
    public interface IAttackable
    {
        /// <summary>
        /// 데미지를 받는 메서드
        /// </summary>
        /// <param name="amount">받는 데미지 양</param>
        void TakeDamage(int amount);
    }

    /// <summary>
    /// 모든 캐릭터의 기본 속성과 동작을 정의하는 클래스
    /// </summary>
    public class BaseCharacter : IAttackable
    {   
        /// <summary>
        /// 캐릭터 이름
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 캐릭터의 현재 체력
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// 캐릭터의 공격력
        /// </summary>
        public int AttackPower { get; set; }

        /// <summary>
        /// 기본 생성자. 속성을 기본값으로 초기화
        /// </summary>
        public BaseCharacter()
        {
            Name = string.Empty;
            Health = 0;
            AttackPower = 0;
        }

        /// <summary>
        /// 소멸자
        /// </summary>
        ~BaseCharacter()
        {
        }

        /// <summary>
        /// 이름, 체력, 공격력을 지정하는 생성자
        /// </summary>
        /// <param name="name">캐릭터 이름</param>
        /// <param name="health">초기 체력</param>
        /// <param name="attackPower">초기 공격력</param>
        public BaseCharacter(string name, int health, int attackPower)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
        }

        /// <summary>
        /// 데미지를 받아 체력을 감소시킴
        /// </summary>
        /// <param name="amount">받는 데미지 양</param>
        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0; // 체력이 0 이하로 내려가지 않도록 보정
            Console.WriteLine($"{Name} takes {amount} damage! Remaining health: {Health}");
        }

        /// <summary>
        /// 대상 캐릭터를 공격하여 체력을 감소시킴
        /// </summary>
        /// <param name="target">공격 대상 캐릭터</param>
        public void Attack(BaseCharacter target)
        {
            target.Health -= AttackPower;
            Console.WriteLine($"{Name} attacks {target.Name} for {AttackPower} damage!");
        }

        /// <summary>
        /// 캐릭터가 살아있는지 여부 반환
        /// </summary>
        /// <returns>체력이 0보다 크면 true, 아니면 false</returns>
        public bool IsAlive()
        {
            return Health > 0;
        }
    }
}
