using System;
using System.Collections.Generic;
using System.Linq;

// 목표: System.Collections.Generic 네임스페이스의 주요 컬렉션들을 연습하고 이해합니다.
// System.Linq : Average 메서드를 사용하려면 필요합니다.

namespace CollectionsGeneric_Solutions
{
    public class CollectionsGeneric_Solutions
    {
        static void Main()
        {
            Console.WriteLine("=== System.Collections.Generic 연습 정답 ===");
            Console.WriteLine();

            Exercise_List_Solution();
            Exercise_Dictionary_Solution();
            Exercise_Queue_Solution();
            Exercise_Stack_Solution();
            Exercise_HashSet_Solution();
            Exercise_LinkedList_Solution();
            Exercise_SortedSet_Solution();
            Exercise_SortedDictionary_Solution();

            Console.WriteLine();
            Console.WriteLine("정답 실행 종료");
        }

        static void Exercise_List_Solution()
        {
            Console.WriteLine("— List<T> 정답");
            var scores = new List<int> { 90, 75, 88 };
            scores.Add(100);
            scores.Remove(75);
            scores.Sort();
            Console.WriteLine("정렬: " + string.Join(", ", scores));
            double avg = scores.Average();
            Console.WriteLine($"평균: {avg:F2}");
            Console.WriteLine();
        }

        static void Exercise_Dictionary_Solution()
        {
            Console.WriteLine("— Dictionary<TKey,TValue> 정답");
            var ages = new Dictionary<string, int>
            {
                ["Alice"] = 20,
                ["Bob"] = 25
            };
            ages["Carol"] = 23;
            ages["Bob"] = 26;

            if (ages.TryGetValue("Bob", out int bobAge))
                Console.WriteLine($"Bob -> {bobAge}");
            else
                Console.WriteLine("Bob -> 없음");

            foreach (var kv in ages)
                Console.WriteLine($"{kv.Key} -> {kv.Value}");
            Console.WriteLine();
        }

        static void Exercise_Queue_Solution()
        {
            Console.WriteLine("— Queue<T> 정답");
            var q = new Queue<string>();
            q.Enqueue("A");
            q.Enqueue("B");
            q.Enqueue("C");
            Console.WriteLine($"Peek = {q.Peek()}");
            Console.WriteLine($"Dequeue = {q.Dequeue()}");
            Console.WriteLine($"남은 개수 = {q.Count}");
            Console.WriteLine();
        }

        static void Exercise_Stack_Solution()
        {
            Console.WriteLine("— Stack<T> 정답");
            var st = new Stack<string>();
            st.Push("first");
            st.Push("second");
            st.Push("third");
            Console.WriteLine($"Peek = {st.Peek()}");
            Console.WriteLine($"Pop = {st.Pop()}");
            Console.WriteLine($"남은 개수 = {st.Count}");
            Console.WriteLine();
        }

        static void Exercise_HashSet_Solution()
        {
            Console.WriteLine("— HashSet<T> 정답");
            var set = new HashSet<int> { 1, 2, 2, 3 };
            Console.WriteLine("집합: " + string.Join(", ", set));
            Console.WriteLine($"2 포함? {set.Contains(2)}");

            var other = new HashSet<int> { 2, 3, 4 };
            var union = new HashSet<int>(set);
            union.UnionWith(other);
            Console.WriteLine("합집합: " + string.Join(", ", union));
            Console.WriteLine();
        }

        static void Exercise_LinkedList_Solution()
        {
            Console.WriteLine("— LinkedList<T> 정답");
            var ll = new LinkedList<string>();
            var head = ll.AddLast("head");
            ll.AddLast("tail");
            ll.AddBefore(head, "new head");
            foreach (var s in ll)
                Console.WriteLine(s);
            Console.WriteLine();
        }

        static void Exercise_SortedSet_Solution()
        {
            Console.WriteLine("— SortedSet<T> 정답");
            var fruits = new SortedSet<string> { "banana", "apple", "cherry", "apple" };
            Console.WriteLine(string.Join(", ", fruits));
            foreach (var f in fruits)
                if (f.StartsWith("b", StringComparison.OrdinalIgnoreCase))
                    Console.WriteLine($"b로 시작: {f}");
            Console.WriteLine();
        }

        static void Exercise_SortedDictionary_Solution()
        {
            Console.WriteLine("— SortedDictionary<TKey,TValue> 정답");
            var sd = new SortedDictionary<int, string>
            {
                [3] = "three",
                [1] = "one",
                [2] = "two"
            };
            foreach (var kv in sd)
                Console.WriteLine($"{kv.Key} : {kv.Value}");
            Console.WriteLine();
        }
    }
}



