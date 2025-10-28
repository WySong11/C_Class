using System;
using static System.Console;
using System.Collections.Generic;

namespace TestProperty
{
    // 1) 자동 구현 속성(Auto-Property)과 private set
    // - 숨겨진 백킹 필드를 컴파일러가 자동으로 만들어 줍니다.
    // - 외부에서는 읽을 수만 있고(Person.Age), 내부 메서드로만 바꿀 수 있게(private set) 설계해 봅니다.
    class Person
    {
        public string Name { get; set; } = "Unknown";     // 읽기/쓰기 모두 허용
        public int Age { get; private set; } = 1;         // 외부에서 set 금지

        public void Birthday() => Age++;                  // 내부 메서드로만 값 변경
        public override string ToString() => $"{Name} ({Age})";
    }

    // 2) 백킹 필드(Backing Field)와 검증이 들어간 속성
    // - set에서 유효성 검사, 범위 제한, 형식 변환 등 로직을 넣을 수 있습니다.
    class BankAccount
    {
        private decimal _balance; // 실제 데이터는 보통 private 필드에 저장

        public decimal Balance
        {
            get => _balance;                              // 값을 읽을 때
            private set => _balance = value < 0 ? 0 : value; // 음수 방지 같은 간단 검증
        }

        public BankAccount(decimal startBalance = 0m)
        {
            Balance = startBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0m)
            {
                Console.WriteLine("[입금 실패] 0 이하 금액은 불가");
                return;
            }
            Balance += amount;
            Console.WriteLine($"[입금] +{amount} => 잔액 {Balance}");
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0m)
            {
                Console.WriteLine("[출금 실패] 0 이하 금액은 불가");
                return false;
            }
            if (amount > Balance)
            {
                Console.WriteLine("[출금 실패] 잔액 부족");
                return false;
            }
            Balance -= amount;
            Console.WriteLine($"[출금] -{amount} => 잔액 {Balance}");
            return true;
        }
    }

    // 3) 계산 전용 속성(Read-only Computed Property)
    // - 다른 값으로부터 즉석에서 계산됩니다.
    // - set 이 없고 => 식 본문으로 간결하게 작성했습니다.
    class Rectangle
    {
        public float Width { get; set; }
        public float Height { get; set; }

        public float Area => Width * Height;          // 면적은 계산만 함
        public float Perimeter => 2f * (Width + Height);   // 둘레도 계산만 함

        public Rectangle(float w, float h)
        {
            Width = w;
            Height = h;
        }
    }

    // 4) 생성자 전용 설정과 사실상 불변 패턴
    // - Id는 생성자로만 설정하고 이후 변경 불가(읽기 전용).
    // - Nickname은 외부 변경을 막고 메서드로만 통제(private set).
    // - C# 9 이상이면 init 접근자도 사용할 수 있습니다(주석 참고).
    class Player
    {
        public string Id { get; }                   // 생성자에서만 설정 가능한 읽기 전용 속성
        public string Nickname { get; private set; } = "Rookie"; // 외부 set 금지

        // C# 9 이상인 경우에는 이렇게도 가능: public string Title { get; init; } = "Beginner";
        // Unity 2021+ 에서도 대체로 지원되지만 환경에 따라 다를 수 있어요.

        public int Level { get; private set; } = 1; // 외부에서 마음대로 못 올리게 보호

        public Player(string id, string nickname = null)
        {
            Id = id;
            if (!string.IsNullOrWhiteSpace(nickname))
                Nickname = nickname;
        }

        public void LevelUp()
        {
            Level++;
            Console.WriteLine($"[레벨업] {Nickname} => Lv.{Level}");
        }

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("[이름 변경 실패] 공백은 불가");
                return;
            }
            Nickname = newName.Trim();
            Console.WriteLine($"[이름 변경] 새 닉네임: {Nickname}");
        }

        public override string ToString() => $"{Id} / {Nickname} / Lv.{Level}";
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== 1) Auto-Property & private set ===");
            var p = new Person { Name = "Jaei" };   // set 사용
            Console.WriteLine(p);                   // get 사용
            p.Birthday();                           // 내부 메서드로 Age 증가
            Console.WriteLine(p);

            Console.WriteLine("\n=== 2) Backing Field & 검증 속성 ===");
            var account = new BankAccount(10_000m);
            account.Deposit(5_000m);                // 정상 입금
            account.Withdraw(30_000m);              // 잔액 부족
            account.Withdraw(8_000m);               // 정상 출금
            Console.WriteLine($"최종 잔액: {account.Balance}"); // get

            Console.WriteLine("\n=== 3) 계산 전용 속성 ===");
            var rect = new Rectangle(3f, 4f);
            Console.WriteLine($"가로:{rect.Width}, 세로:{rect.Height}");
            Console.WriteLine($"면적:{rect.Area}, 둘레:{rect.Perimeter}");

            Console.WriteLine("\n=== 4) 읽기 전용·불변 패턴 ===");
            var player = new Player(id: "P001", nickname: "SlimeHunter");
            Console.WriteLine(player);
            player.LevelUp();
            player.Rename("ProHunter");
            Console.WriteLine(player);

            // 아래 줄은 컴파일 에러 예시입니다(주석 해제 금지).
            // player.Level = 99;           // set 이 private 이라 외부 변경 불가
            // player.Id = "Hack";          // 읽기 전용이라 재설정 불가
            // account.Balance = 999999m;   // set 이 private 이라 외부 변경 불가
        }
    }
}
