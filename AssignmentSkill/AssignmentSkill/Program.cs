using System;
using System.Collections.Generic;
using System.Timers;
using Timer = System.Timers.Timer;

public class Program
{
    static void Main(string[] args)
    {
        // 스킬 리스트
        List<Skill> skills = new List<Skill>();

        // 스킬 생성
        // FireSkill 인스턴스 생성
        Skill fireball = new FireSkill("Fireball", TimeSpan.FromSeconds(5), 100);

        // HealSkill 인스턴스 생성
        Skill heal = new HealSkill("Heal", TimeSpan.FromSeconds(3), 50);

        // 스킬 리스트에 추가
        skills.Add(fireball);
        skills.Add(heal);

        // 스킬 쿨타임 종료 이벤트 구독
        foreach (Skill skill in skills)
        {            
            skill.CooldownFinished += (s) =>
            {
                Console.WriteLine($"[{s.Name}] 스킬의 쿨타임이 종료되었습니다. 다시 사용할 수 있습니다.");
            };
        }

        Console.WriteLine("=== 스킬 사용 프로그램 ===");
        Console.WriteLine("1 : 파이어볼 사용");
        Console.WriteLine("2 : 힐 사용");
        Console.WriteLine("3 : 스킬 상태 보기");
        Console.WriteLine("ESC : 종료");
        Console.WriteLine("=========================");
        Console.WriteLine();

        bool running = true;

        while (running)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                    // 파이어볼 사용
                    fireball.TryUse();
                    break;
                case ConsoleKey.D2:
                    // 힐 사용
                    heal.TryUse();
                    break;
                case ConsoleKey.D3:
                    // 스킬 상태 보기
                    foreach (Skill skill in skills)
                    {
                         TimeSpan remain = skill.GetRemainingCooldonw();
                         Console.WriteLine($"[{skill.Name}] 스킬의 남은 쿨타임 : {Math.Ceiling(remain.TotalSeconds)}초");
                    }
                    break;

                case ConsoleKey.Escape:
                    running = false;
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    break;
            }
        }
    }
}

public class Skill
{
    // 스킬 이름
    public string Name;

    // 스킬 쿨타임
    public TimeSpan Cooldown;

    // 마지막 사용 시간
    public DateTime _lastUsedTime;

    // 쿨타임 타이머
    public Timer _cooldownTimer;

    // 쿨타임 종료 이벤트
    public event Action<Skill> CooldownFinished;

    public Skill()
    {
        _lastUsedTime = DateTime.MinValue;
    }

    public Skill(string name, TimeSpan cooldown)
    {
        Name = name;
        Cooldown = cooldown;
        _lastUsedTime = DateTime.MinValue;

        _cooldownTimer = new Timer();
        _cooldownTimer.AutoReset = false;
        _cooldownTimer.Elapsed += OnCooldownElapsed;
    }

    // 스킬 쿨타이머 이벤트 핸들러
    public void OnCooldownElapsed(object sender, ElapsedEventArgs e)
    {
        CooldownFinished?.Invoke(this);
    }

    // 스킬 사용 가능 여부 확인
    public bool CanUse()
    {
        // 경과 시간 계산. 현재시간 - 마지막 사용 시간
        TimeSpan elapsed = DateTime.Now - _lastUsedTime;

        // 경과 시간이 쿨타임보다 크면 스킬 사용 가능
        return elapsed.TotalSeconds > Cooldown.TotalSeconds;
    }

    // 남은 쿨타임 계산
    public TimeSpan GetRemainingCooldonw()
    {
        // 경과 시간 계산. 현재시간 - 마지막 사용 시간
        TimeSpan elapsed = DateTime.Now - _lastUsedTime;

        // 남은 쿨타임 = 쿨타임 - 경과 시간
        TimeSpan remain = Cooldown - elapsed;

        // 남은 쿨타임이 0보다 작으면 0 반환
        if (remain < TimeSpan.Zero)
        {
            return TimeSpan.Zero;
        }

        // 남은 쿨타임 반환
        return remain;
    }

    // 스킬 사용 시도
    public void TryUse()
    {
        if (CanUse() == false)
        {
            // 현재 남은 쿨타임 가져오기
            TimeSpan remain = GetRemainingCooldonw();

            Console.WriteLine($"[{Name}] 스킬은 아직 사용 할 수 없습니다.");
            // 남은 쿨타임 출력
            // Math.Ceiling : 올림 함수
            Console.WriteLine($"[{Name}] 스킬은 아직 쿨타임이 [{Math.Ceiling(remain.TotalSeconds)}]초 남았습니다.");

            return;
        }

        // 스킬 사용 처리
        //Console.WriteLine($"[{Name}] 스킬을 사용했습니다.");

        // 마지막 사용 시간 갱신
        _lastUsedTime = DateTime.Now;

        // 쿨타임 타이머 설정 및 시작
        _cooldownTimer.Stop();
        _cooldownTimer.Interval = Cooldown.TotalMilliseconds;
        _cooldownTimer.Start();

        OnUse();
    }

    // 스킬 사용 시 동작
    // 가상 메서드로 선언하여 자식 클래스에서 재정의 가능
    // virtual 키워드 사용
    public virtual void OnUse()
    {
        Console.WriteLine($"Base => [{Name}] 스킬을 사용했습니다.");
    }
}

public class FireSkill : Skill
{
    public int Damage;

    // 생성자
    public FireSkill(string name, TimeSpan cooldonw, int damage ) : base(name, cooldonw) 
    {
        Damage = damage;
    }

    // 스킬 사용 시 동작 재정의
    // virtual 키워드로 선언된 메서드를 재정의할 때는 override 키워드 사용
    public override void OnUse()
    {
        base.OnUse();
        Console.WriteLine($"[{Name}] 불꽃이 타오릅니다! => [{Damage}] 피해를 줬다~!!");
    }
}

public class  HealSkill : Skill
{
    public int HealAmount;

    // 생성자
    public HealSkill(string name, TimeSpan cooldonw, int healamount) : base(name, cooldonw) 
    {
        HealAmount = healamount;
    }

    // 스킬 사용 시 동작 재정의
    // virtual 키워드로 선언된 메서드를 재정의할 때는 override 키워드 사용
    public override void OnUse()
    {
        base.OnUse();
        Console.WriteLine($"[{Name}] 생명력이 회복됩니다! => [{HealAmount}] 생명력 증가~!!");
    }
}