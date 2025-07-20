using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGameUseTimer
{
    public class PlayerCharacter : BaseCharacter
    {
        /// <summary>
        /// 기본 생성자, 플레이어 캐릭터의 기본 속성을 설정합니다.
        /// </summary>
        public PlayerCharacter() : base("Player", 80, 10, 1000)
        {
        }

        /// <summary>
        /// 이름, 체력, 공격력을 지정하는 생성자
        /// </summary>
        /// <param name="name">플레이어 이름</param>
        /// <param name="health">초기 체력</param>
        /// <param name="attackPower">초기 공격력</param>
        /// <param name="attackSpeed">초기 공격 속도 (밀리초 단위)</param>"
        public PlayerCharacter(string name, int health, int attackPower, double attackSpeed) : base(name, health, attackPower, attackSpeed)
        {
        }

        /// <summary>
        /// 소멸자
        /// </summary>
        ~PlayerCharacter()
        {
        }
    }
}
