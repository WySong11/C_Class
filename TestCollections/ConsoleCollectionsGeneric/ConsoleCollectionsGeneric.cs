using System;
using static System.Console;
using System.Collections.Generic;

/*
System.Collections.Generic는 “제네릭 컬렉션”들이 모여 있는 네임스페이스입니다.
Generic 덕분에 타입 안정성이 높고 성능도 좋습니다.

List<T>는 순서가 있는 가변 배열로, 배열과 비슷하지만 크기가 자동으로 늘어납니다.

Dictionary<TKey,TValue>는 키-값 쌍을 저장하는 맵 구조로, 키와 값의 타입을 지정할 수 있습니다.

Queue<T>는 먼저 넣은 게 먼저 나오는 FIFO 구조이고, Stack<T>은 나중에 넣은 게 먼저 나오는 LIFO 구조입니다.

Stack<T>은 접시 쌓기처럼 Push와 Pop으로 동작합니다.

SortedSet<T>은 자동으로 정렬되고 중복이 없는 집합입니다.

SortedDictionary<TKey,TValue>는 키가 자동으로 정렬되는 맵 구조로, 정렬된 맵이 필요할 때 간단히 쓸 수 있습니다.

사용자 정의 타입도 제네릭 컬렉션에 담을 수 있습니다.

순서와 인덱스가 필요하면 List를 씁니다.
키로 빠르게 찾으려면 Dictionary를 씁니다.
줄 서기처럼 처리하면 Queue를 씁니다.
쌓았다가 역순으로 빼면 Stack을 씁니다.
중복 없이 포함 여부가 중요하면 HashSet을 씁니다.
앞뒤 노드 기준 삽입과 삭제가 많으면 LinkedList를 씁니다.
항상 정렬된 상태가 필요하면 SortedSet이나 SortedDictionary를 씁니다.
*/
namespace ConsoleCollectionsGeneric
{
    public class ConsoleCollectionsGeneric
    {
        static void Main()
        {
            Console.WriteLine("=== List<T>: 순서가 있는 가변 배열 ===");
            var scores = new List<int> { 90, 75, 88 };
            scores.Add(100);                              // 추가
            Console.WriteLine($"개수 = {scores.Count}, 첫 요소 = {scores[0]}");
            scores.Sort();                                // 정렬
            Console.WriteLine("정렬된 점수: " + string.Join(", ", scores));
            int firstOver90 = scores.Find(s => s >= 90);  // 조건으로 찾기
            Console.WriteLine($"90점 이상 첫 번째 = {firstOver90}");

            Console.WriteLine();
            Console.WriteLine("=== Dictionary<TKey,TValue>: 키-값 맵 ===");
            var ages = new Dictionary<string, int>
            {
                ["Alice"] = 20,
                ["Bob"] = 25
            };
            ages["Carol"] = 23;                           // 추가 또는 갱신
            if (ages.TryGetValue("Bob", out int bobAge))
                Console.WriteLine($"Bob = {bobAge}");
            foreach (var kv in ages)
                Console.WriteLine($"{kv.Key} -> {kv.Value}");
            ages.Remove("Alice");
            Console.WriteLine($"Alice 키 존재? {ages.ContainsKey("Alice")}");

            Console.WriteLine();
            Console.WriteLine("=== Queue<T>: 선입선출 FIFO ===");
            var q = new Queue<string>();
            q.Enqueue("A");
            q.Enqueue("B");
            q.Enqueue("C");
            Console.WriteLine($"Peek = {q.Peek()}");      // 맨 앞 보기
            Console.WriteLine($"Dequeue = {q.Dequeue()}");// 꺼내기
            Console.WriteLine($"남은 개수 = {q.Count}");

            Console.WriteLine();
            Console.WriteLine("=== Stack<T>: 후입선출 LIFO ===");
            var st = new Stack<string>();
            st.Push("first");
            st.Push("second");
            st.Push("third");
            Console.WriteLine($"Peek = {st.Peek()}");
            Console.WriteLine($"Pop = {st.Pop()}");
            Console.WriteLine($"남은 개수 = {st.Count}");

            Console.WriteLine();
            Console.WriteLine("=== HashSet<T>: 중복 없는 집합 ===");
            var set = new HashSet<int> { 1, 2, 2, 3 };    // 2는 중복이라 하나만 저장
            Console.WriteLine("집합: " + string.Join(", ", set));
            set.Add(3);                                   // 이미 있음 → 변화 없음
            Console.WriteLine($"2 포함? {set.Contains(2)}");

            Console.WriteLine();
            Console.WriteLine("=== LinkedList<T>: 양방향 연결 리스트 ===");
            var ll = new LinkedList<string>();
            var head = ll.AddLast("head");
            ll.AddLast("tail");
            ll.AddBefore(head, "new head");               // 노드 기준 삽입
            foreach (var s in ll)
                Console.WriteLine(s);

            Console.WriteLine();
            Console.WriteLine("=== SortedSet<T>: 자동 정렬 + 중복 제거 ===");
            var fruits = new SortedSet<string> { "banana", "apple", "cherry", "apple" };
            Console.WriteLine(string.Join(", ", fruits)); // apple, banana, cherry

            Console.WriteLine();
            Console.WriteLine("=== SortedDictionary<TKey,TValue>: 키 기준 자동 정렬 맵 ===");
            var sd = new SortedDictionary<int, string>();
            sd[3] = "three";
            sd[1] = "one";
            sd[2] = "two";
            foreach (var kv in sd)
                Console.WriteLine($"{kv.Key} : {kv.Value}");

            Console.WriteLine();
            Console.WriteLine("=== 사용자 정의 타입과 제네릭 ===");
            var students = new List<Student>
        {
            new Student { Id = 1, Name = "Mina" },
            new Student { Id = 2, Name = "Jisoo" },
            new Student { Id = 3, Name = "Hajin" },
        };
            // 조건에 맞는 학생들 찾기
            var idOver1 = students.FindAll(s => s.Id > 1);
            Console.WriteLine("Id > 1:");
            foreach (var s in idOver1) Console.WriteLine(s);

            // 빠른 조회를 위한 사전
            var byId = new Dictionary<int, Student>();
            foreach (var s in students) byId[s.Id] = s;
            Console.WriteLine($"Id=2 학생 이름 = {byId[2].Name}");

            Console.WriteLine();
            Console.WriteLine("끝!");
        }
    }

    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public override string ToString() => $"{Id}: {Name}";
    }
}
