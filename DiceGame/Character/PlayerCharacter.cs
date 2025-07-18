using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Character
{
    /// <summary>
    /// 저장 가능한 객체를 나타내는 인터페이스
    /// </summary>
    public interface ISaveable
    {
        /// <summary>
        /// 객체를 저장하는 메서드
        /// </summary>
        void Save();
    }

    /// <summary>
    /// 플레이어 캐릭터의 속성과 동작을 정의하는 클래스
    /// </summary>
    public class PlayerCharacter : BaseCharacter, ISaveable
    {
        /// <summary>
        /// 기본 생성자. 이름, 체력, 공격력을 기본값으로 초기화
        /// </summary>
        public PlayerCharacter() : base("Player", 80, 10, 1000)
        {
        }

        /// <summary>
        /// 소멸자
        /// </summary>
        ~PlayerCharacter()
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
        /// 플레이어 정보를 저장하는 메서드
        /// </summary>
        public void Save()
        {
            // 저장 로직
            JSONFileManager.JSONFileProcessor.Instance.Write(
                "PlayerCharacter.json",
                this,
                new System.Text.Json.JsonSerializerOptions { WriteIndented = true },
                System.Text.Encoding.UTF8
            );
        }
    }
}
