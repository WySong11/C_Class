using System;
using System.Collections.Generic;

namespace TestClass
{
    public class GameManager
    {
        // private 로Static 객체를 생성
        private static GameManager? instance;

        // Player List
        List<PlayerClass> PlayerList = new List<PlayerClass>();

        private GameManager() { }


        // public 으로 Instance 를 사용
        public static GameManager Instance
        {
            get
            {
                // instance 가 null 이면
                // instance 를 생성
                if (instance == null)
                {
                    // 객체 생성
                    instance = new GameManager();
                }

                // 생성된 객체를 반환
                return instance;
            }
        }

        public void PrintPlayerList()
        {
            foreach (PlayerClass playerClass in PlayerList)
            {
                playerClass.PrintInfo();
            }
        }

        public void AddPlayer(PlayerClass player)
        {
            // List Contains 를 통해서 중복 체크
            if(IsPlayer(player) == false )
            {
                PlayerList.Add(player);
            }
        }

        public void RemovePlayer(PlayerClass player)
        {
            // List Contains 를 통해서 중복 체크
            if (IsPlayer(player) == true)
            {
                PlayerList.Remove(player);
            }

            Console.WriteLine($"Remove Player => {player.GetID()}");
        }

        public void AttackPlayer(PlayerClass offence, PlayerClass defence)
        {
            // offence 와 defence 플레이어 중 한 명이라도 없으면 리턴
            if (IsPlayer(offence) && IsPlayer(defence) == false)
            {
                Console.WriteLine("Not find Player");
                return;
            }

            Console.WriteLine($"{offence.GetName()}이 {defence.GetName()}을 공격했습니다.");

            defence.TakeDamage(offence.GetAttackPower());           
        }

        public bool IsPlayer(PlayerClass player)
        {
            return PlayerList.Contains(player);
        }
    }
}
