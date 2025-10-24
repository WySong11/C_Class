using System;

namespace EnumBasicsDemo
{
    // 가장 기본적인 열거형 예시
    // 값은 0부터 자동 증가하지만, 가독성을 위해 직접 지정해도 돼

    enum Direction
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
    }

    // 기본 저장 형식은 int지만 이렇게 byte 등으로 바꿀 수도 있어
    enum ItemRarity : byte
    {
        Common = 1,
        Rare = 2,
        Epic = 3,
        Legendary = 4,
    }

    // 0은 보통 '알 수 없음' 같은 안전한 기본값으로 예약하면 좋아
    enum OrderStatus
    {
        Unknown = 0,
        Pending = 1,
        Paid = 2,
        Shipped = 3,
        Completed = 4,
        Cancelled = 5,
    }

    // 여러 상태를 동시에 담고 싶을 때는 [Flags]와 1,2,4,8처럼 2의 거듭제곱으로 값 지정
    [Flags]
    enum Permission
    {
        None = 0,      // 아무 권한 없음
        Read = 1 << 0, // 1
        Write = 1 << 1, // 2
        Execute = 1 << 2, // 4
        Delete = 1 << 3, // 8

        All = Read | Write | Execute | Delete // 1+2+4+8 = 15
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 1) 기본 사용과 형 변환
            Direction dir = Direction.Left;

            Console.WriteLine($"지금 방향: {dir}");                   // 이름 출력
            Console.WriteLine($"숫자 값: {(int)dir}");               // 정수 값 출력

            // 2) switch와 함께 쓰기
            switch (dir)
            {
                case Direction.Up:
                    Console.WriteLine("위로 이동합니다.");
                    break;

                case Direction.Down:
                    Console.WriteLine("아래로 이동합니다.");
                    break;

                case Direction.Left:
                    Console.WriteLine("왼쪽으로 이동합니다.");
                    break;

                case Direction.Right:
                    Console.WriteLine("오른쪽으로 이동합니다.");
                    break;
            }

            // 3) 문자열 ↔ enum 변환
            string input = "right"; // 사용자가 소문자로 입력했다고 가정

            if (Enum.TryParse(input, ignoreCase: true, out Direction parsed))
            {
                Console.WriteLine($"입력된 문자열을 열거형으로 변환: {parsed}");
            }
            else
            {
                Console.WriteLine("변환 실패");
            }

            // 4) 모든 이름과 값 출력
            Console.WriteLine("Direction의 모든 멤버:");

            foreach (string name in Enum.GetNames(typeof(Direction)))
            {
                var value = (int)Enum.Parse(typeof(Direction), name);
                Console.WriteLine($"- {name} = {value}");
            }

            // 5) 기본값 패턴
            OrderStatus status = default; // 0
            Console.WriteLine($"기본 주문 상태: {status}"); // Unknown

            // 6) Flags enum 사용
            Permission p = Permission.Read | Permission.Write; // 둘을 합침
            Console.WriteLine($"현재 권한: {p}"); // Read, Write

            // 권한 포함 여부 확인
            bool canWrite1 = p.HasFlag(Permission.Write); // 쉬운 방법
            bool canWrite2 = (p & Permission.Write) != 0; // 빠르고 명확한 비트 체크

            Console.WriteLine($"쓰기 권한 있음(HasFlag): {canWrite1}");
            Console.WriteLine($"쓰기 권한 있음(비트 체크): {canWrite2}");

            // 권한 추가와 제거
            p |= Permission.Execute; // 실행 권한 추가
            Console.WriteLine($"권한 추가 후: {p}");

            p &= ~Permission.Write;  // 쓰기 권한 제거
            Console.WriteLine($"쓰기 제거 후: {p}");

            // 7) 잘못된 숫자 값 방어 코드
            int unknownValue = 123;
            bool valid = Enum.IsDefined(typeof(Direction), unknownValue);

            Console.WriteLine($"123은 Direction에 유효한 값인가요? {valid}");

            // 8) 저장 형식을 바꾼 enum
            ItemRarity rarity = ItemRarity.Legendary;
            Console.WriteLine($"희귀도: {rarity} ({(byte)rarity})");
            Console.WriteLine("엔터를 누르면 종료합니다.");
            Console.ReadLine();
        }
    }
}


/*

using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EnumAdvancedDemo
{
    // 사람이 읽기 쉬운 라벨을 달고 싶을 때 Description 특성을 붙여 둡니다.
    enum OrderStatus
    {
        [Description("알 수 없음")]
        Unknown = 0,

        [Description("결제 대기")]
        Pending = 1,

        [Description("결제 완료")]
        Paid = 2,

        [Description("배송 준비")]
        Packed = 3,

        [Description("배송 중")]
        Shipped = 4,

        [Description("거래 완료")]
        Completed = 5,

        [Description("취소됨")]
        Cancelled = 6,
    }

    // 여러 상태를 동시에 담기 위한 비트 플래그 예시입니다.
    [Flags]
    enum Permission : ushort // 저장 크기를 줄이고 싶을 때 기반 형식을 바꿉니다.
    {
        None    = 0,
        Read    = 1 << 0, // 0001
        Write   = 1 << 1, // 0010
        Execute = 1 << 2, // 0100
        Delete  = 1 << 3, // 1000
        All     = Read | Write | Execute | Delete
    }

    // JSON 직렬화에서 enum을 문자열로 다루는 실전 예시용 클래스입니다.
    class Order
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public Permission Perms { get; set; }
    }

    static class EnumUtils
    {
        // Description 특성을 읽어 사람이 읽을 수 있는 라벨을 돌려줍니다.
        public static string GetDescription(this Enum value)
        {
            var mem = value.GetType().GetMember(value.ToString());
            if (mem.Length > 0)
            {
                var attr = mem[0].GetCustomAttribute<DescriptionAttribute>();
                if (attr != null) return attr.Description;
            }
            return value.ToString();
        }

        // 대소문자 구분 없이 안전하게 문자열을 enum으로 변환합니다.
        public static bool TryParseIgnoreCase<TEnum>(string text, out TEnum result)
            where TEnum : struct, Enum
        {
            return Enum.TryParse(text, ignoreCase: true, out result);
        }

        // Flags enum에 섞여 들어온 잘못된 비트를 정리합니다.
        public static TEnum NormalizeFlags<TEnum>(TEnum value)
            where TEnum : struct, Enum
        {
            ulong v = Convert.ToUInt64(value);
            ulong mask = 0UL;
            foreach (var defined in Enum.GetValues(typeof(TEnum)))
                mask |= Convert.ToUInt64(defined);
            ulong cleaned = v & mask;
            return (TEnum)Enum.ToObject(typeof(TEnum), cleaned);
        }
    }

    class Program
    {
        // 상태 전이 테이블을 사전으로 정의합니다.
        static readonly System.Collections.Generic.Dictionary<OrderStatus, OrderStatus[]> Allowed =
            new()
            {
                { OrderStatus.Unknown,  new[]{ OrderStatus.Pending, OrderStatus.Cancelled } },
                { OrderStatus.Pending,  new[]{ OrderStatus.Paid, OrderStatus.Cancelled } },
                { OrderStatus.Paid,     new[]{ OrderStatus.Packed, OrderStatus.Cancelled } },
                { OrderStatus.Packed,   new[]{ OrderStatus.Shipped } },
                { OrderStatus.Shipped,  new[]{ OrderStatus.Completed } },
                { OrderStatus.Completed,new OrderStatus[0] },
                { OrderStatus.Cancelled,new OrderStatus[0] },
            };

        static bool TryTransition(ref OrderStatus current, OrderStatus next, out string reason)
        {
            if (!Allowed.TryGetValue(current, out var nexts))
            {
                reason = "정의되지 않은 현재 상태입니다.";
                return false;
            }

            foreach (var n in nexts)
            {
                if (n == next)
                {
                    current = next;
                    reason = "전이 성공";
                    return true;
                }
            }

            reason = $"'{current}'에서 '{next}'(으)로 전이할 수 없습니다.";
            return false;
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 1. Description 라벨 사용 예시
            var s = OrderStatus.Pending;
            Console.WriteLine($"현재 상태 = {s} / 표시용 = {s.GetDescription()}");

            // 2. switch 식으로 뱃지 색을 결정합니다.
            string badgeColor = s switch
            {
                OrderStatus.Pending  => "Yellow",
                OrderStatus.Paid     => "Blue",
                OrderStatus.Packed   => "Teal",
                OrderStatus.Shipped  => "Purple",
                OrderStatus.Completed=> "Green",
                OrderStatus.Cancelled=> "Gray",
                _ => "LightGray"
            };
            Console.WriteLine($"UI 뱃지 색 = {badgeColor}");

            // 3. 대소문자 무시 문자열 → enum 변환 예시
            if (EnumUtils.TryParseIgnoreCase<OrderStatus>("shipped", out var parsed))
                Console.WriteLine($"문자열 'shipped' → {parsed} 변환 성공");
            else
                Console.WriteLine("변환 실패");

            // 4. Flags 조합과 검사
            Permission p = Permission.Read | Permission.Write;
            Console.WriteLine($"초기 권한 = {p}");
            bool canWrite = (p & Permission.Write) != 0;
            Console.WriteLine($"쓰기 가능? = {canWrite}");
            p |= Permission.Execute;
            Console.WriteLine($"실행 권한 추가 후 = {p}");
            p &= ~Permission.Write;
            Console.WriteLine($"쓰기 권한 제거 후 = {p}");

            // 5. 잘못된 비트 정리
            var dirty = (Permission)999; // 실수로 엉뚱한 값이 들어온 상황을 가정합니다.
            var clean = EnumUtils.NormalizeFlags(dirty);
            Console.WriteLine($"잘못된 값 999 → 정리 후 = {clean}");

            // 6. 상태 전이 엔진 사용
            var flow = OrderStatus.Pending;
            Console.WriteLine($"초기 상태 = {flow} ({flow.GetDescription()})");
            if (TryTransition(ref flow, OrderStatus.Paid, out var why1))
                Console.WriteLine($"전이 결과 = {flow} / {why1}");
            if (TryTransition(ref flow, OrderStatus.Shipped, out var why2))
                Console.WriteLine($"전이 결과 = {flow} / {why2}");
            else
                Console.WriteLine($"전이 실패 / 이유 = {why2}");
            if (TryTransition(ref flow, OrderStatus.Packed, out var why3))
                Console.WriteLine($"전이 결과 = {flow} / {why3}");
            else
                Console.WriteLine($"전이 실패 / 이유 = {why3}");

            // 7. JSON 직렬화에서 enum을 문자열로 다루기
            var order = new Order
            {
                Id = 42,
                Status = OrderStatus.Shipped,
                Perms = Permission.Read | Permission.Execute
            };

            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            jsonOptions.Converters.Add(new JsonStringEnumConverter()); // 숫자 대신 "Shipped"처럼 문자열로 저장합니다.

            string json = JsonSerializer.Serialize(order, jsonOptions);
            Console.WriteLine("직렬화된 JSON");
            Console.WriteLine(json);

            var back = JsonSerializer.Deserialize<Order>(json, jsonOptions);
            Console.WriteLine($"역직렬화 결과 = Id:{back!.Id}, Status:{back.Status}, Perms:{back.Perms}");

            Console.WriteLine("엔터를 누르면 종료합니다.");
            Console.ReadLine();
        }
    }
}

 
 
 */