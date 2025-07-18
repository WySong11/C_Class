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
        /// 캐 릭터의 공격 속도 (밀리초 단위)
        /// </summary>
        public double AttackSpeed { get; set; }

        /// <summary>
        /// 기본 생성자. 속성을 기본값으로 초기화
        /// </summary>
        public BaseCharacter()
        {
            Name = string.Empty;
            Health = 10;
            AttackPower = 10;
            AttackSpeed = 100;
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
        /// <param name="attackSpeed">초기 공격 속도 (밀리초 단위)</param>
        public BaseCharacter(string name, int health, int attackPower, double attackSpeed)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
            AttackSpeed = attackSpeed;
        }

        public void SetLevelData(int level)
        {
            // 레벨에 따라 캐릭터의 속성을 설정하는 로직을 구현할 수 있습니다.
            // 예시로, 레벨에 따라 공격력과 체력을 증가시키는 간단한 로직을 추가합니다.
            Health += level * 10; // 레벨당 체력 10 증가
            AttackPower += level * 5; // 레벨당 공격력 2 증가
            AttackSpeed += level * 200; // 레벨당 공격 속도 5 감소 (더 느리게 공격)
            
            Console.WriteLine($"\n{Name} has been leveled up to level {level}! New Health: {Health}, New Attack Power: {AttackPower}, New Attack Speed: {AttackSpeed} ms\n");
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
            target.TakeDamage(AttackPower);
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

        public int GetAttackPower()
        {
            //return Random.Shared.Next(AttackPower - 5, AttackPower + 5); // 공격력에 약간의 변동을 줌

            // 공격력의 80% ~ 120% 범위에서 랜덤하게 결정 (최소 1 보장)
            double min = AttackPower * 0.7;
            double max = AttackPower * 1.3;

            Random random = new Random();
            return random.Next( (int)min, (int)max); ;
        }
    }
}
