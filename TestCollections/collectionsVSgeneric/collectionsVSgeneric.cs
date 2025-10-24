using System;
using static System.Console;
using System.Collections;
using System.Collections.Generic;

namespace collectionsVSgeneric
{
    public class collectionsVSgeneric
    {
        static void Main()
        {
            Console.WriteLine("=== System.Collections vs System.Collections.Generic 비교 ===");
            Console.WriteLine();

            Section_ArrayList_vs_List();
            Section_Hashtable_vs_Dictionary();
            Section_Queue();
            Section_Stack();
            Section_SortedList();
            Section_GenericsOnly_HashSet();

            Console.WriteLine();
            Console.WriteLine("요약: 제네릭 컬렉션은 캐스팅이 필요 없고 컴파일 단계에서 타입을 확인해 줍니다.");
            Console.WriteLine("요약: 비제네릭 컬렉션은 다양한 형식을 담을 수 있지만 캐스팅/박싱이 필요하고 런타임 오류가 나기 쉽습니다.");
        }

        // 1) ArrayList(비제네릭) vs List<int>(제네릭)
        static void Section_ArrayList_vs_List()
        {
            Console.WriteLine("— ArrayList vs List<int>");
            // 비제네릭: 서로 다른 형식을 한 곳에 담을 수 있음. (object로 보관)
            ArrayList al = new ArrayList();
            al.Add(10);            // int → object로 박싱
            al.Add("hello");       // string
            al.Add(3.14);          // double

            Console.WriteLine($"ArrayList[0] 실제 형식: {al[0].GetType().Name}");
            Console.WriteLine($"ArrayList[1] 실제 형식: {al[1].GetType().Name}");

            int a0 = (int)al[0];   // 언박싱 + 캐스팅
            Console.WriteLine($"al[0] + 5 = {a0 + 5}");

            try
            {
                // 잘못된 캐스팅: 문자열을 int로 변환하려고 해서 런타임 오류
                int wrong = (int)al[1];
                Console.WriteLine(wrong);
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"잘못된 캐스팅 예외(비제네릭): {ex.GetType().Name}");
            }

            // 제네릭: 한 가지 타입만 담도록 "컴파일 시점"에 제한 (타입 안전)
            List<int> list = new List<int>();
            list.Add(10);
            list.Add(20);
            // list.Add("hello"); // 컴파일 오류: string은 List<int>에 넣을 수 없음
            int sum = 0;
            foreach (var n in list) sum += n;
            Console.WriteLine($"List<int> 합계 = {sum}");
            Console.WriteLine();
        }

        // 2) Hashtable(비제네릭) vs Dictionary<string,int>(제네릭)
        static void Section_Hashtable_vs_Dictionary()
        {
            Console.WriteLine("— Hashtable vs Dictionary<string,int>");
            // 비제네릭: 키/값이 object이므로, 서로 다른 형식을 섞어 넣어도 컴파일은 됨
            Hashtable ht = new Hashtable();
            ht["Alice"] = 20;
            ht["Bob"] = 25;
            ht[123] = "Oops"; // 키 타입 섞기 (int 키), 값 타입 섞기 (string 값)

            foreach (DictionaryEntry e in ht)
                Console.WriteLine($"Hashtable 항목: {e.Key} -> {e.Value}");

            try
            {
                // "Alice"의 값을 int로 캐스팅 (성공)
                int alice = (int)ht["Alice"];
                Console.WriteLine($"Alice 나이 = {alice}");

                // "Oops"는 string이라서 int로 캐스팅하면 런타임 오류
                int wrong = (int)ht[123];
                Console.WriteLine(wrong);
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"잘못된 캐스팅 예외(Hashtable): {ex.GetType().Name}");
            }

            // 제네릭: 키/값 타입을 명확히 지정 → 섞어서 넣을 수 없음
            var ages = new Dictionary<string, int>();
            ages["Alice"] = 20;
            ages["Bob"] = 26;
            // ages[123] = "Oops"; // 컴파일 오류: 키/값 타입이 맞지 않음

            if (ages.TryGetValue("Bob", out int bobAge))
                Console.WriteLine($"Dictionary Bob 나이 = {bobAge}");

            foreach (var kv in ages)
                Console.WriteLine($"Dictionary 항목: {kv.Key} -> {kv.Value}");

            Console.WriteLine();
        }

        // 3) Queue(비제네릭) vs Queue<string>(제네릭)
        static void Section_Queue()
        {
            Console.WriteLine("— Queue vs Queue<string>");
            // 비제네릭 큐: 다양한 타입 혼합 가능 → 꺼낼 때 캐스팅 필요
            Queue q = new Queue();
            q.Enqueue("A");
            q.Enqueue("B");
            q.Enqueue(100); // 다른 타입
            Console.WriteLine($"Queue.Peek() = {q.Peek()}");

            try
            {
                string s1 = (string)q.Dequeue(); // "A"
                string s2 = (string)q.Dequeue(); // "B"
                                                 // 다음은 int 이므로 string으로 캐스팅 시 런타임 오류
                string s3 = (string)q.Dequeue();
                Console.WriteLine($"{s1}, {s2}, {s3}");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"잘못된 캐스팅 예외(Queue): {ex.GetType().Name}");
            }

            // 제네릭 큐: 특정 타입만 허용 → 컴파일 단계에서 타입 체크
            var q2 = new Queue<string>();
            q2.Enqueue("A");
            q2.Enqueue("B");
            // q2.Enqueue(100); // 컴파일 오류
            Console.WriteLine($"Queue<string>.Peek() = {q2.Peek()}");
            Console.WriteLine($"Queue<string>.Dequeue() = {q2.Dequeue()}");
            Console.WriteLine($"남은 개수 = {q2.Count}");
            Console.WriteLine();
        }

        // 4) Stack(비제네릭) vs Stack<int>(제네릭)
        static void Section_Stack()
        {
            Console.WriteLine("— Stack vs Stack<int>");
            Stack st = new Stack();
            st.Push(1);
            st.Push("two"); // 다른 타입
            st.Push(3);
            Console.WriteLine($"Stack.Peek() = {st.Peek()}");

            try
            {
                int a = (int)st.Pop();     // 3 OK
                int b = (int)st.Pop();     // "two" → InvalidCastException
                Console.WriteLine($"{a}, {b}");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"잘못된 캐스팅 예외(Stack): {ex.GetType().Name}");
            }

            var st2 = new Stack<int>();
            st2.Push(1);
            st2.Push(2);
            st2.Push(3);
            Console.WriteLine($"Stack<int>.Peek() = {st2.Peek()}");
            Console.WriteLine($"Stack<int>.Pop() = {st2.Pop()}");
            Console.WriteLine($"남은 개수 = {st2.Count}");
            Console.WriteLine();
        }

        // 5) SortedList(비제네릭) vs SortedList<int,string>(제네릭)
        static void Section_SortedList()
        {
            Console.WriteLine("— SortedList vs SortedList<int,string>");
            // 비제네릭 SortedList: 키가 object라 서로 다른 비교 불가 타입을 섞으면 런타임 오류
            SortedList sl = new SortedList();
            sl.Add(3, "three");
            sl.Add(1, "one");
            sl.Add(2, "two");
            try
            {
                sl.Add("4", "four"); // 다른 타입의 키 삽입 → 정렬 비교 불가로 예외
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"정렬/비교 문제 예외(SortedList 비제네릭): {ex.GetType().Name}");
            }
            for (int i = 0; i < sl.Count; i++)
            {
                Console.WriteLine($"{sl.GetKey(i)} : {sl.GetByIndex(i)}");
            }

            // 제네릭 SortedList<TKey,TValue>: 키 타입 고정 → 비교/정렬 안전
            var sl2 = new SortedList<int, string>();
            sl2.Add(3, "three");
            sl2.Add(1, "one");
            sl2.Add(2, "two");
            foreach (var kv in sl2)
                Console.WriteLine($"SortedList<int,string> {kv.Key} : {kv.Value}");
            Console.WriteLine();
        }

        // 6) 제네릭에만 있는 예: HashSet<T>
        static void Section_GenericsOnly_HashSet()
        {
            Console.WriteLine("— HashSet<int> (제네릭 전용 예)");
            var set = new HashSet<int> { 1, 2, 2, 3 };
            Console.WriteLine("중복 제거된 집합: " + string.Join(", ", set));
            var other = new HashSet<int> { 2, 3, 4 };
            set.UnionWith(other);
            Console.WriteLine("합집합 결과: " + string.Join(", ", set));

            // 비제네릭에 정확히 대응하는 집합 타입은 없음.
            // 흉내 내려면 ArrayList로 Contains 체크 후 넣는 식으로 직접 구현해야 하므로 코드가 길고 느려지기 쉽습니다.
            Console.WriteLine();
        }
    }
}