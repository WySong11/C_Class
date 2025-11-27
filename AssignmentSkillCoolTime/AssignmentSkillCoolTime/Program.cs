using System;
using System.Timers;
using Timer = System.Timers.Timer;

namespace SkillCooldownSimple
{
    // 공통 스킬 기본 클래스
    public abstract class Skill
    {
        public string Name { get; }
        public TimeSpan Cooldown { get; }

        private DateTime _lastUsedTime;
        public DateTime LastUsedTime => _lastUsedTime;

        private readonly Timer _cooldownTimer;

        // 쿨타임 종료 이벤트
        public event Action<Skill>? CooldownFinished;

        protected Skill(string name, TimeSpan cooldown)
        {
            Name = name;
            Cooldown = cooldown;
            _lastUsedTime = DateTime.MinValue;

            _cooldownTimer = new Timer();
            _cooldownTimer.AutoReset = false;
            _cooldownTimer.Elapsed += OnCooldownTimerElapsed;
        }

        // 지금 사용 가능한지 확인
        public bool CanUse()
        {
            TimeSpan elapsed = DateTime.Now - _lastUsedTime;
            return elapsed >= Cooldown;
        }

        // 남은 쿨타임 계산
        public TimeSpan GetRemainingCooldown()
        {
            TimeSpan elapsed = DateTime.Now - _lastUsedTime;
            TimeSpan remain = Cooldown - elapsed;

            if (remain < TimeSpan.Zero)
                return TimeSpan.Zero;

            return remain;
        }

        // 스킬 사용 시도
        public void TryUse()
        {
            if (!CanUse())
            {
                TimeSpan remain = GetRemainingCooldown();
                Console.WriteLine($"[{Name}] 스킬은 아직 사용 불가입니다.");
                Console.WriteLine($"남은 쿨타임: {Math.Ceiling(remain.TotalSeconds)}초");
                return;
            }

            _lastUsedTime = DateTime.Now;

            _cooldownTimer.Stop();
            _cooldownTimer.Interval = Cooldown.TotalMilliseconds;
            _cooldownTimer.Start();

            OnUse();
        }

        // 타이머가 끝났을 때 호출
        private void OnCooldownTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            CooldownFinished?.Invoke(this);
        }

        // 실제 사용 시 출력용
        protected abstract void OnUse();
    }

    // 공격 스킬
    public class AttackSkill : Skill
    {
        public AttackSkill(string name, TimeSpan cooldown)
            : base(name, cooldown)
        {
        }

        protected override void OnUse()
        {
            Console.WriteLine($"[{Name}] 사용!");
            Console.WriteLine("공격 스킬이 발동했습니다.");
        }
    }

    // 회복 스킬
    public class HealSkill : Skill
    {
        public HealSkill(string name, TimeSpan cooldown)
            : base(name, cooldown)
        {
        }

        protected override void OnUse()
        {
            Console.WriteLine($"[{Name}] 사용!");
            Console.WriteLine("회복 스킬이 발동했습니다.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Skill fireball = new AttackSkill("파이어볼", TimeSpan.FromSeconds(5));
            Skill heal = new HealSkill("힐", TimeSpan.FromSeconds(8));

            // 쿨타임 종료 이벤트 구독
            fireball.CooldownFinished += OnSkillCooldownFinished;
            heal.CooldownFinished += OnSkillCooldownFinished;

            Console.WriteLine("=== 스킬 쿨타임 콘솔 데모 ===");
            Console.WriteLine("1 키: 파이어볼 사용");
            Console.WriteLine("2 키: 힐 사용");
            Console.WriteLine("S 키: 스킬 상태 보기");
            Console.WriteLine("ESC 키: 종료");
            Console.WriteLine();

            bool running = true;

            while (running)
            {
                Console.Write("입력 (1/2/S/ESC): ");

                // 키 입력을 기다리는 동안은 자동으로 멈춰 있음
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                Console.WriteLine();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        fireball.TryUse();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        heal.TryUse();
                        break;

                    case ConsoleKey.S:
                        PrintSkillStatus(fireball);
                        PrintSkillStatus(heal);
                        break;

                    case ConsoleKey.Escape:
                        running = false;
                        break;

                    default:
                        Console.WriteLine("알 수 없는 키입니다.");
                        break;
                }

                Console.WriteLine();
            }

            Console.WriteLine("프로그램을 종료합니다.");
        }

        // 쿨타임 종료 이벤트 핸들러
        private static void OnSkillCooldownFinished(Skill skill)
        {
            Console.WriteLine();
            Console.WriteLine($"*** [{skill.Name}] 스킬 쿨타임이 종료되었습니다.");
            Console.WriteLine("지금 다시 사용할 수 있습니다. ***");
            Console.WriteLine();
        }

        // 스킬 상태 출력
        private static void PrintSkillStatus(Skill skill)
        {
            Console.WriteLine($"--- [{skill.Name}] 상태 ---");

            if (skill.LastUsedTime == DateTime.MinValue)
            {
                Console.WriteLine("아직 한 번도 사용하지 않았습니다.");
            }
            else
            {
                Console.WriteLine($"마지막 사용 시각: {skill.LastUsedTime:HH:mm:ss}");

                if (skill.CanUse())
                {
                    Console.WriteLine("현재 사용 가능: 예");
                }
                else
                {
                    TimeSpan remain = skill.GetRemainingCooldown();
                    Console.WriteLine("현재 사용 가능: 아니오");
                    Console.WriteLine($"남은 쿨타임: {Math.Ceiling(remain.TotalSeconds)}초");
                }
            }
        }
    }
}
