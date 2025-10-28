using System;
using System.Collections.Generic;
using static System.Console;

namespace TestStruct
{
    //
    // 1) 위치를 담는 간단한 구조체 (가변)
    //    - 값 타입(value type)인 구조체로, 인스턴스가 복사되어 전달됩니다.
    //    - 이 예제는 의도적으로 '가변(mutable)' 구조체를 보여주기 위해 필드와 변경 메서드를 포함합니다.
    //    - 주의: 일반적으로 구조체는 '작고(권장 16바이트 이내), 불변(immutable)'으로 설계하는 것이 성능과 안전성에 유리합니다.
    //
    public struct Point2D
    {
        // 공개 필드(가변): 외부에서 직접 접근/변경 가능.
        // 필드가 공개되어 있고 변경 가능한 구조체는 예기치 않은 복사/수정 버그를 일으키기 쉽습니다.
        public int X;
        public int Y;

        public Point2D()
        {
            // 매개변수 없는 생성자는 C# 10.0부터 구조체에 허용됩니다.
            // 모든 필드를 명시적으로 초기화해야 합니다.
            X = 0;
            Y = 0;
        }

        // 사용자 정의 생성자: 모든 필드를 초기화합니다.
        // 구조체에 생성자를 정의하면 모든 코드 경로에서 모든 필드가 초기화되도록 신경 써야 합니다.
        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        // 가변 메서드: 현재 인스턴스의 값을 변경합니다.
        // 인스턴스를 값으로 전달하면 이 메서드는 '복사본'의 값을 변경합니다.
        // ref로 전달하면 원본을 직접 변경합니다(아래 예제 참조).
        public void MoveBy(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }

        // readonly 메서드: 이 메서드는 인스턴스를 변경하지 않음을 컴파일러에 보장합니다.
        // readonly로 표시하면 읽기 전용(특히 불변) 인스턴스에 대해 불필요한 방어적 복사를 피할 수 있습니다.
        public readonly double DistanceTo(Point2D other)
        {
            var dx = X - other.X;
            var dy = Y - other.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        // 디버깅/출력을 위한 ToString 재정의.
        // 구조체의 ToString 자체는 박싱(boxing)을 발생시키지 않지만,
        // 구조체를 object나 인터페이스로 취급하면 박싱이 발생합니다(성능 고려 필요).
        public override string ToString() => $"({X}, {Y})";
    }

    //
    // 2) 사각형 정보를 담는 구조체 (불변 스타일 권장)
    //    - 외부에서 변경할 수 없는 get 전용 프로퍼티로 구현되어 '읽기 전용'처럼 동작합니다.
    //    - 생성자로만 값을 설정하도록 하여 불변(immutable) 스타일을 구현했습니다.
    //    - 불변 구조체는 복사로 인한 부작용이 적고, 멀티스레드 환경에서도 안전합니다.
    //
    public struct Rectangle
    {
        // 읽기 전용 자동 프로퍼티: 생성자에서만 설정 가능
        public int Width { get; }
        public int Height { get; }

        // 생성자: 모든 읽기 전용 프로퍼티를 초기화
        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        // 계산 프로퍼티: Width와 Height에 기반한 파생 값
        // 불변 구조체에서는 계산 프로퍼티가 안전하게 사용될 수 있습니다.
        public int Area => Width * Height;

        public override string ToString() => $"{Width} x {Height}";
    }

    //
    // 3) 불변 구조체 예제 (권장 패턴)
    //    - readonly struct: 모든 인스턴스 멤버는 읽기 전용 이어야 하며, 박싱/부작용 위험이 줄어듭니다.
    //    - IEquatable<T> 구현: 값비교를 효율적으로 수행하도록 권장됩니다.
    //    - Deconstruct: 언패킹/패턴 매칭에 유용합니다.
    //
    public readonly struct ImmutablePoint : IEquatable<ImmutablePoint>
    {
        public int X { get; }
        public int Y { get; }

        public ImmutablePoint(int x, int y) => (X, Y) = (x, y);

        public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);

        public bool Equals(ImmutablePoint other) => X == other.X && Y == other.Y;

        public override bool Equals(object? obj) => obj is ImmutablePoint p && Equals(p);

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public static bool operator ==(ImmutablePoint a, ImmutablePoint b) => a.Equals(b);
        public static bool operator !=(ImmutablePoint a, ImmutablePoint b) => !a.Equals(b);

        public override string ToString() => $"[{X}, {Y}]";

        // 인터페이스용 데모 메서드(구현 예시)
        public string Describe() => $"ImmutablePoint X={X}, Y={Y}";
    }

    // 4) 'in' 파라미터 예제: 큰 구조체를 복사하지 않고 읽기 전용으로 전달할 때 사용
    public struct LargeStruct
    {
        // 필드가 많아 복사 비용이 증가한다는 가정의 예제
        public long A, B, C, D, E, F, G, H;

        public override string ToString() => $"LargeStruct({A},{B},{C},{D},{E},{F},{G},{H})";
    }

    // 5) ref return 예제: 배열/컬렉션 내부 요소에 대한 직접 참조를 반환하여 수정 가능
    public static class RefHelpers
    {
        public static ref Point2D GetRef(Point2D[] arr, int index) => ref arr[index];
    }

    class Program
    {
        // 값 타입 특성 확인용: 매개변수로 구조체를 받으면 '복사본'을 받습니다.
        // 내부에서 상태를 변경해도 호출자 측 원본에는 영향이 없습니다.
        static void TryMove(Point2D p)
        {
            // p는 호출자에서 전달된 복사본입니다.
            p.MoveBy(10, 0); // 복사본의 값만 변경됩니다.
            WriteLine($"TryMove 내부 p: {p}"); // 복사본 변경 결과 출력
        }

        // ref를 사용하면 '원본'을 참조로 받아 직접 수정할 수 있습니다.
        // ref 매개변수는 호출 시 변수 자체를 전달해야 하며, 복사가 일어나지 않습니다.
        static void MoveInPlace(ref Point2D p)
        {
            // ref로 전달된 p는 원본을 가리키므로 이 변경은 호출자에게 영향을 줍니다.
            p.MoveBy(10, 0); // 원본이 직접 바뀝니다.
            WriteLine($"MoveInPlace 내부 p: {p}");
        }

        // 'in' 파라미터 사용 예제 (읽기 전용 참조 전달 — 복사 방지)
        static void PrintLargeStruct(in LargeStruct ls)
        {
            // ls는 읽기 전용 참조로 전달되므로 값 변경 불가.
            WriteLine($"in 파라미터로 전달된 LargeStruct: {ls}");
        }

        static void Main()
        {
            WriteLine("=== 구조체는 '값 타입'입니다. 복사본과 원본이 분리됩니다. ===");

            var p1 = new Point2D(1, 2);
            var p2 = p1;                 // 값 복사 발생: p2는 p1의 완전한 복사본
            p2.MoveBy(5, 0);             // 복사본만 이동 — p1에는 영향 없음

            WriteLine($"원본 p1: {p1}"); // (1, 2)
            WriteLine($"복사본 p2: {p2}"); // (6, 2)

            WriteLine();
            WriteLine("=== 메서드 인자 전달 방식 비교 ===");

            var p3 = new Point2D(0, 0);
            TryMove(p3);                  // 값 전달: 내부에서 수정해도 원본 p3에는 변화 없음
            WriteLine($"TryMove 호출 후 p3: {p3}"); // (0, 0)

            MoveInPlace(ref p3);          // 참조 전달: 원본 p3가 수정됨
            WriteLine($"MoveInPlace 호출 후 p3: {p3}"); // (10, 0)

            WriteLine();
            WriteLine("=== 불변 스타일 구조체 예시 ===");

            var rect = new Rectangle(3, 4);   // 생성자로만 값 설정 (불변 스타일)
            WriteLine($"사각형: {rect}, 넓이: {rect.Area}"); // 3 x 4, 12

            WriteLine();
            WriteLine("=== default 값 확인 ===");

            // 구조체의 default 초기화: 모든 필드는 0 또는 해당 타입의 기본값으로 초기화됩니다.
            // 즉, Point2D의 경우 X=0, Y=0이 됩니다.
            Point2D p0 = default;
            WriteLine($"default Point2D: {p0}"); // (0, 0)

            WriteLine();
            WriteLine("=== 추가 예제: readonly, in, ref return, equality, deconstruct ===");

            // readonly 메서드 예제: 불필요한 복사 방지
            var pa = new Point2D(0, 0);
            var pb = new Point2D(3, 4);
            WriteLine($"거리(pa -> pb): {pa.DistanceTo(pb):F2}"); // readonly 메서드 호출

            // ImmutablePoint 사용: IEquatable 구현, Deconstruct, 값 비교
            var ip1 = new ImmutablePoint(2, 3);
            var ip2 = new ImmutablePoint(2, 3);
            WriteLine($"ImmutablePoint 동치 비교: {ip1 == ip2}"); // true
            var (dx, dy) = ip1; // Deconstruct 지원 (튜플 문법)
            WriteLine($"Deconstruct 결과: dx={dx}, dy={dy}");

            // 'in' 파라미터: 큰 구조체를 복사하지 않고 전달
            var large = new LargeStruct { A = 1, B = 2, C = 3, D = 4, E = 5, F = 6, G = 7, H = 8 };
            PrintLargeStruct(in large);

            // ref return: 배열 내부 요소를 직접 참조로 받아 변경
            var arr = new Point2D[] { new Point2D(0, 0), new Point2D(1, 1) };
            ref var itemRef = ref RefHelpers.GetRef(arr, 1);
            WriteLine($"배열 원소(수정 전): {arr[1]}");
            itemRef.MoveBy(100, 0); // 참조를 통해 원본 배열 요소 직접 수정
            WriteLine($"배열 원소(수정 후): {arr[1]}");

            // 박싱(boxing)과 인터페이스 호출 관련: 구조체를 object나 인터페이스 타입으로 취급하면 박싱 발생
            // ImmutablePoint는 readonly struct이지만, object로 캐스팅하면 여전히 박싱됩니다.
            object boxed = ip1; // 박싱 발생
            WriteLine($"박싱된 값: {boxed}");

            WriteLine();
            WriteLine("=== 컬렉션(List / Dictionary)와 구조체 사용 예제 ===");

            // 1) List<T>에 가변 구조체를 넣고 수정하는 방법 (주의: 인덱서로 바로 수정하면 복사본을 수정하게 됨)
            var pointList = new List<Point2D>
            {
                new Point2D(1, 1),
                new Point2D(2, 2)
            };

            WriteLine("pointList 원본:");
            foreach (var v in pointList) WriteLine(v);

            // 잘못된 직관(인덱서 반환값은 값이므로 직접 수정하면 실제 리스트 요소는 바뀌지 않음):
            // pointList[0].MoveBy(10, 0); // -> (컴파일러가 허용하지 않거나, 허용하더라도 복사본만 변경)

            // 올바른 수정 방법: 요소를 꺼내어 수정한 뒤 다시 넣기
            var temp = pointList[0];
            temp.MoveBy(10, 0);
            pointList[0] = temp; // 수정된 값을 다시 저장해야 실제 컬렉션 요소가 바뀝니다.

            WriteLine("pointList 수정 후:");
            foreach (var v in pointList) WriteLine(v);

            // 2) Immutable 구조체를 List에 넣는 경우: 요소 교체 방식으로 값 변경
            var immList = new List<ImmutablePoint> { new ImmutablePoint(5, 5) };
            WriteLine($"immList[0] 원본: {immList[0]}");
            // 불변이므로 값 변경은 교체(assignment)로 처리
            immList[0] = new ImmutablePoint(6, 6);
            WriteLine($"immList[0] 교체 후: {immList[0]}");

            WriteLine();
            // 3) Dictionary에서 구조체를 키로 사용
            // 권장: 키는 불변(Immutable) 구조체를 사용하고, 적절한 Equals/GetHashCode 구현이 필요함.
            var dict = new Dictionary<ImmutablePoint, string>();
            dict[new ImmutablePoint(1, 1)] = "A";
            dict[new ImmutablePoint(2, 2)] = "B";

            WriteLine($"dict[ImmutablePoint(1,1)] = {dict[new ImmutablePoint(1, 1)]}"); // 정상 조회

            // 4) 가변 구조체를 키로 사용하는 경우 주의점 시연
            var dictMutable = new Dictionary<Point2D, string>();
            var key = new Point2D(5, 5);
            dictMutable[key] = "original";

            WriteLine($"dictMutable에 추가한 키: {key}, ContainsKey(key) = {dictMutable.ContainsKey(key)}"); // true

            // 로컬 변수 key를 변경하면(복사본 변경) 조회가 실패할 수 있음 — 키 자체가 사본으로 Dictionary에 저장되어 있음
            key.MoveBy(1, 0);
            WriteLine($"로컬 key 수정 후: {key}, ContainsKey(key) = {dictMutable.ContainsKey(key)}"); // 보통 false
            WriteLine($"원래 좌표로 찾기: dictMutable.ContainsKey(new Point2D(5,5)) = {dictMutable.ContainsKey(new Point2D(5, 5))}"); // true

            // 요약: mutable struct를 키로 쓰면 혼동을 일으킬 수 있으니, 가능한 불변 구조체를 키로 사용하거나 참조 타입 키를 사용하세요.
        }
    }
}
