using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Game
{
    private List<BaseCharacter> characters = new List<BaseCharacter>();

    bool gameOver = false;

    // 게임 시작 이벤트
    // aync/await : 비동기 프로그래밍을 위한 키워드
    // await 를 통해 비동기 작업의 결과를 기다리면서도 다른 작업을 수행할 수 있음
    public async Task StartGameEventAsync()
    {
        Console.Clear();
        gameOver = false;
        characters.Clear();
        SaveLog.ClearLogs(); // 이전 로그 초기화

        // C# 에서 비동기 프로그래밍을 할 때, 직접 Task를 만들고 제어할 수 있는 클래스
        // TaskCompletionSource 는 비동기 작업의 완료를 나타내는 Task 객체를 생성하고 제어하는 데 사용됩니다.
        // 주로 비동기 작업이 완료되었음을 외부에서 알리거나, 비동기 작업의 결과를 설정하는 데 사용됩니다.
        // 이 예제에서는 게임의 전투가 끝날 때까지 기다리는 용도로 사용됨

        // TaskCompletionSource 를 사용하여 게임 종료를 비동기적으로 대기
        var tcs = new TaskCompletionSource();

        //PlayerCharacter player = new PlayerCharacter(1000, "Player", new Stats(80, 10, 1000));
        //EnemyCharacter enemy = new EnemyCharacter(2000, "Enemy", new Stats(100, 7, 1000));

        BaseCharacter player = CreateClassPlayer("Player");
        player.SetLevelData(PromptLevel("플레이어의 레벨을 입력하세요: "));

        BaseCharacter enemy = CreateClassPlayer("Enemy");        
        enemy.SetLevelData( PromptLevel("적의 레벨을 입력하세요: ") );

        // 서로의 타겟 ID 설정
        player.SetTargetID(enemy.ID);
        enemy.SetTargetID(player.ID);

        // 캐릭터 리스트에 플레이어와 적 추가
        characters.Add(player);
        characters.Add(enemy);

        foreach(var character in characters)
        {
            character.AddOnAttackEvent(attackEvent =>
            {
                if(gameOver == false)
                {
                    // 리스트에서 공격자와 타겟을 찾아옴
                    var attacker = characters.Find(c => c.ID == attackEvent.AttackerID);
                    var target = characters.Find(c => c.ID == attackEvent.TargetID);

                    if(target != null && attacker != null)
                    { 
                        // 공격 처리
                        target.TakeDamage(attackEvent.Damage);
                    }
                }
            });

            character.AddOnHealthChangedEvent(healthEvent =>
            {
                if(gameOver == false)
                {
                    var target = characters.Find(c => c.ID == healthEvent.CharacterID);

                    if( target != null )
                    {
                        string txtDamage = $"{target.Name}의 남은 체력: {healthEvent.Health}";
                        SaveLog.WriteLog(txtDamage, ConsoleColor.Green);
                        Console.WriteLine();

                        // 캐릭터가 사망했는지 확인
                        if(target.IsAlive() == false)
                        {
                            string txtDeath = $"{target.Name}이(가) 사망했습니다.";
                            SaveLog.WriteLog(txtDeath, ConsoleColor.Blue);

                            EndGame();
                            tcs.TrySetResult(); // 게임 종료 알림                            
                        }
                    }
                }
            });

            string txtInfo = $"캐릭터 생성: {character.Name}, 체력: {character.CharacterStats.Health}, " +
                $"공격력: {character.CharacterStats.AttackPower}, 공격 속도: {character.CharacterStats.AttackSpeed}ms";

            SaveLog.WriteLog(txtInfo, ConsoleColor.Yellow);


            // 자동 공격 타이머 시작
            character.StartAutoAttackTimer();
        }

        //////////////////////////////////////////////////////////////////////////////////
        ///

        await tcs.Task; // 게임 종료까지 대기

        SaveLog.Write(); // 로그 파일로 저장

        Console.ForegroundColor = ConsoleColor.White;

        if(ProptContinue("게임을 다시 시작하시겠습니까? : "))
        {
            // 재귀적으로 게임을 다시 시작 (비동기 호출)
            await StartGameEventAsync();
        }
        else
        {
            Console.WriteLine("게임을 종료합니다. 감사합니다!");
        }
    }

    public void EndGame()
    {
        gameOver = true;

        // 모든 캐릭터의 자동 공격 타이머 중지 및 이벤트 핸들러 제거
        foreach (var character in characters)
        {
            character.StopAutoAttackTimer();
            character.ClearEvents();
        }
    }

    public int PromptLevel(string inMessage)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(inMessage);

        string? input = Console.ReadLine();

        if(int.TryParse(input, out int level))
        {
            if(level < 1)
            {
                level = 1;
            }
        }
        else
        {
            level = 1;
        }

        return level;
    }

    public bool ProptContinue(string inMessage)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(inMessage + " (Y/N)");
        string? input = Console.ReadLine();
        if(input != null && (input.Equals("Y", StringComparison.OrdinalIgnoreCase) || 
            input.Equals("Yes", StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }
        return false;
    }

    public BaseCharacter CreateClassPlayer(string name)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("직업을 선택하세요: 1. 전사  2. 마법사  3. 힐러");
        string? input = Console.ReadLine();
        BaseCharacter character;
        switch(input)
        {
            case "1":
                character = new WarriorClass(1500, name, new Stats(100, 8, 1000));
                break;
            case "2":
                character = new MageClass(1000, name, new Stats(80, 12, 800));
                break;
            case "3":
                character = new HealerClass(1200, name, new Stats(90, 10, 900));
                break;
            default:
                character = new WarriorClass(1500, name, new Stats(100, 8, 1000));
                break;
        }
        return character;
    }
}
