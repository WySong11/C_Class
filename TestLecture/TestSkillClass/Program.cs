using System;
using static System.Console;
using System.Timers;
using System.Collections.Generic;

public class Skill
{
    public string skillName { get; set; }

    // 마지막으로 사용한 시간
    // MinValue : DateTime 구조체가 표현할 수 있는 가장 이른 날짜와 시간을 나타내는 정적 속성입니다.
    public DateTime lastTime = DateTime.MinValue;

    // 쿨타임
    // FromSeconds : 지정된 초 수를 나타내는 TimeSpan 객체를 생성하는 정적 메서드입니다.
    public TimeSpan coolTime = TimeSpan.FromSeconds(5);

    public Timer coolTimer;

    public event Action<Skill> CooldownFinished;

    public Skill() { }

    public Skill(string name, TimeSpan cool)
    {
        skillName = name;
        coolTime = cool;

        // 타이머 설정
        coolTimer = new Timer();
        coolTimer.AutoReset = false; // 타이머가 반복되지 않도록 설정
        coolTimer.Elapsed += OnCooldownFinished; // 타이머 이벤트 핸들러 등록
    }

    public void OnCooldownFinished(object sender, ElapsedEventArgs e)
    {
        CooldownFinished?.Invoke(this);
    }

    public virtual bool TrySkill()
    {
        // 현재 시간
        DateTime now = DateTime.Now;
        // 마지막으로 사용한 시간에 쿨타임을 더한 시간이 현재 시간보다 이전인지 확인
        if (lastTime + coolTime <= now)
        {
            // 스킬 사용 가능
            lastTime = now; // 마지막 사용 시간을 현재 시간으로 업데이트
            WriteLine($"{skillName} Skill Used!");

            coolTimer.Stop(); // 타이머 중지
            coolTimer.Interval = coolTime.TotalMilliseconds; // 타이머 간격 설정
            coolTimer.Start(); // 타이머 시작

            return true;
        }
        else
        {
            // 스킬 사용 불가
            WriteLine($"{skillName} Skill is on cooldown.");
            WriteLine($"Time remaining: {(lastTime + coolTime - now).TotalSeconds:F1} seconds");
            return false;
        }
    }
}

public class Fireball : Skill
{
    int damage = 100;

    public Fireball() { }

    public Fireball(string name, TimeSpan cool, int dmg) : base(name, cool)
    {
        damage = dmg;
    }

    public override bool TrySkill()
    {
        if( base.TrySkill() == false )
            return false;

        WriteLine($"Fireball deals {damage} damage.");
        return true;
    }
}

public class Heal : Skill
{
    int healingAmount = 50;

    public Heal() { }

    public Heal(string name, TimeSpan cool, int healAmt) : base(name, cool)
    {
        healingAmount = healAmt;
    }

    public override bool TrySkill()
    {
        if (base.TrySkill() == false)
            return false;

        WriteLine($"Heal restores {healingAmount} health.");
        return true;
    }
}

public class Program
{
    public static void Main()
    {
        /*List<Skill> skills = new()
        {
            new Fireball("Fireball", TimeSpan.FromSeconds(3), 150),
            new Heal("Heal", TimeSpan.FromSeconds(5), 75)
        };*/

        List<Skill> skills = new();

        Skill fireball = new Fireball("Fireball", TimeSpan.FromSeconds(3), 150);
        Skill heal = new Heal("Heal", TimeSpan.FromSeconds(5), 75);

        skills.Add(fireball);
        skills.Add(heal);

        foreach (var skill in skills)
        {
            skill.CooldownFinished += (sskill) =>
            {
               WriteLine($"{sskill.skillName} is ready to use again!");
            };
        }

        bool running = true;

        while (running)
        {
            Console.WriteLine("=== 스킬 사용 프로그램 ===");
            Console.WriteLine("1 : 파이어볼 사용");
            Console.WriteLine("2 : 힐 사용");
            Console.WriteLine("3 : 스킬 상태 보기");
            Console.WriteLine("0 : 종료");
            Console.WriteLine("=========================");
            Console.WriteLine();


            string input = ReadLine();
            switch (input)
            {
                case "1":
                    skills[0].TrySkill();
                    break;
                case "2":
                    skills[1].TrySkill();
                    break;
                case "3":
                    foreach (var skill in skills)
                    {
                        DateTime now = DateTime.Now;
                        if (skill.lastTime + skill.coolTime <= now)
                        {
                            WriteLine($"{skill.skillName} is ready to use.");
                        }
                        else
                        {
                            WriteLine($"{skill.skillName} is on cooldown. Time remaining: {(skill.lastTime + skill.coolTime - now).TotalSeconds:F1} seconds");
                        }
                    }
                    break;
                case "0":
                    running = false;
                    break;
                default:
                    WriteLine("Invalid input. Please try again.");
                    break;
            }
        }

    }
}