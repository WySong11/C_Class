using System;
using static System.Console;
using System.Collections;


/*
System.Collections는 “제네릭이 아닌” 컬렉션들이 모여 있는 네임스페이스입니다.
박싱·언박싱과 형변환이 필요해서 초보자에게 타입 개념을 익히기에 좋지만 실무에서는 제네릭을 더 자주 씁니다.
아래 예제 하나로 ArrayList, Hashtable, Queue, Stack, SortedList, BitArray의 기본 사용법을 체험해 보세요.

ArryaList는 다양한 타입을 담을 수 있지만 꺼낼 때 캐스팅이 필요합니다.

Hashtable는 키-값 쌍을 저장하는 맵 구조로, 키와 값이 object 타입이라 안전한 캐스팅이 중요합니다.

Queue는 먼저 넣은 게 먼저 나오는 FIFO 구조이고, Stack은 나중에 넣은 게 먼저 나오는 LIFO 구조입니다.

Stack은 접시 쌓기처럼 Push와 Pop으로 동작합니다.

SortedList는 키가 자동으로 정렬되는 맵 구조로, 정렬된 맵이 필요할 때 간단히 쓸 수 있습니다.

BitArray는 불리언 비트 묶음에 대해 NOT·AND·OR 같은 비트 연산을 쉽게 수행할 수 있습니다.

ArrayList는 캐스팅이 필요하고 Hashtable은 키와 값이 object라서 안전한 캐스팅이 중요합니다.
Queue는 Enqueue와 Dequeue로 줄 서기처럼 동작하고 Stack은 Push와 Pop으로 접시 쌓기처럼 동작합니다.
SortedList는 키가 자동으로 정렬되므로 정렬된 맵이 필요할 때 간단히 쓸 수 있습니다.
BitArray는 불리언 비트 묶음에 대해 NOT·AND·OR 같은 비트 연산을 쉽게 보여줍니다.

*/

namespace ConsoleCollections
{
    public class ConsoleCollections
    {
        static void Main()
        {
            Console.WriteLine("=== ArrayList: 다양한 타입 담기 ===");
            ArrayList arr = new ArrayList();          // 크기가 자동으로 늘어나는 배열
            arr.Add(10);                               // int -> object로 박싱
            arr.Add("hello");                          // string
            arr.Add(3.14);                             // double
            foreach (var item in arr)
            {
                // 꺼낼 때는 원래 타입으로 캐스팅해야 안전합니다.
                if (item is int i) Console.WriteLine($"int: {i}");
                else if (item is string s) Console.WriteLine($"string: {s}");
                else Console.WriteLine($"other: {item}");
            }
            // 인덱스로 접근 후 캐스팅
            int first = (int)arr[0];                   // 언박싱 + 캐스팅
            Console.WriteLine($"첫 번째 값 + 5 = {first + 5}");

            Console.WriteLine();
            Console.WriteLine("=== Hashtable: 키-값 쌍 ===");
            Hashtable ht = new Hashtable();            // 키와 값이 object 형식
            ht["id"] = 101;
            ht["name"] = "Alice";
            ht["vip"] = true;
            foreach (DictionaryEntry entry in ht)
            {
                Console.WriteLine($"{entry.Key} = {entry.Value}");
            }
            Console.WriteLine($"name 키 존재? {ht.ContainsKey("name")}");
            Console.WriteLine($"값에 Alice 존재? {ht.ContainsValue("Alice")}");

            Console.WriteLine();
            Console.WriteLine("=== Queue: 먼저 넣은 게 먼저 나옴(FIFO) ===");
            Queue q = new Queue();
            q.Enqueue("A");
            q.Enqueue("B");
            q.Enqueue("C");
            Console.WriteLine($"Peek = {q.Peek()}");   // 맨 앞 확인만
            Console.WriteLine($"Dequeue = {q.Dequeue()}"); // 꺼내기
            Console.WriteLine($"남은 개수 = {q.Count}");

            Console.WriteLine();
            Console.WriteLine("=== Stack: 나중에 넣은 게 먼저 나옴(LIFO) ===");
            Stack st = new Stack();
            st.Push(100);
            st.Push(200);
            st.Push(300);
            Console.WriteLine($"Peek = {st.Peek()}");
            Console.WriteLine($"Pop = {st.Pop()}");
            Console.WriteLine($"남은 개수 = {st.Count}");

            Console.WriteLine();
            Console.WriteLine("=== SortedList: 키 기준 정렬되는 맵 ===");
            SortedList sl = new SortedList();          // 키가 자동 정렬
            sl.Add(3, "three");
            sl.Add(1, "one");
            sl.Add(2, "two");
            for (int i = 0; i < sl.Count; i++)
            {
                Console.WriteLine($"{sl.GetKey(i)} : {sl.GetByIndex(i)}");
            }

            Console.WriteLine();
            Console.WriteLine("=== BitArray: 비트 모음 ===");
            BitArray bits = new BitArray(new bool[] { true, false, true, false });
            Console.Write("원본: ");
            PrintBits(bits);
            bits.Not();                                // 비트 반전
            Console.Write("NOT: ");
            PrintBits(bits);
            BitArray other = new BitArray(new bool[] { true, true, false, false });
            Console.Write("AND: ");
            PrintBits(new BitArray(bits).And(other));  // AND 연산
            Console.Write("OR : ");
            PrintBits(new BitArray(bits).Or(other));   // OR  연산
        }

        static void PrintBits(BitArray b)
        {
            for (int i = 0; i < b.Length; i++)
                Console.Write(b[i] ? 1 : 0);
            Console.WriteLine();
        }
    }
}
