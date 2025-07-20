using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGameUseTimer
{
    /// <summary>
    /// 적 캐릭터의 속성과 동작을 정의하는 클래스
    /// </summary>
    public class EnemyCharacter : BaseCharacter
    {
        /// <summary>
        /// 기본 생성자, 플레이어 캐릭터의 기본 속성을 설정합니다.
        /// </summary>
        public EnemyCharacter() : base(2000, "Enemy", 100, 7, 1050)
        {
        }

        /// <summary>
        /// 이름, 체력, 공격력을 지정하는 생성자
        /// </summary>
        /// <param name="name">적 이름</param>
        /// <param name="health">초기 체력</param>
        /// <param name="attackPower">초기 공격력</param>
        /// <param name="attackSpeed">초기 공격 속도 (밀리초 단위)</param>"
        public EnemyCharacter(int id, string name, int health, int attackPower, double attackSpeed) : base(id, name, health, attackPower, attackSpeed)
        {
        }

        /// <summary>
        /// 소멸자
        /// </summary>
        ~EnemyCharacter()
        {
        }
    }
}
