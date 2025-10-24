using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionsGeneric_Exercises
{
    public class CollectionsGeneric_Exercises
    {
        static void Main()
        {
            Console.WriteLine("=== System.Collections.Generic 연습 ===");
            Console.WriteLine("각 함수를 실행하며 TODO를 채워보세요.");
            Console.WriteLine();

            Exercise_List();
            Exercise_Dictionary();
            Exercise_Queue();
            Exercise_Stack();
            Exercise_HashSet();
            Exercise_LinkedList();
            Exercise_SortedSet();
            Exercise_SortedDictionary();

            Console.WriteLine();
            Console.WriteLine("연습 종료");
        }

        // List<T>
        static void Exercise_List()
        {
            Console.WriteLine("— List<T> 연습");

            // 목표: 점수 리스트를 만들고, 추가/삭제/정렬/평균을 구합니다.
            // TODO: 아래 scores에 90, 75, 88을 넣고 100을 추가하세요.
            var scores = new List<int>();
            // 힌트: Add 사용

            // TODO: 75를 제거하세요.
            // 힌트: Remove 사용

            // TODO: 오름차순 정렬하고 콘솔에 "정렬: ..." 형태로 출력하세요.
            // 힌트: Sort, string.Join

            // TODO: 평균을 double로 구해 "평균: ..." 출력하세요.
            // 힌트: scores.Average() 또는 합/Count

            Console.WriteLine();
        }

        // Dictionary<TKey,TValue>
        static void Exercise_Dictionary()
        {
            Console.WriteLine("— Dictionary<TKey,TValue> 연습");

            // 목표: 학생 이름을 키, 점수를 값으로 관리합니다.
            // TODO: ages 딕셔너리를 만들고 "Alice"=20, "Bob"=25를 넣으세요.
            var ages = new Dictionary<string, int>();

            // TODO: "Carol"=23을 추가하고, "Bob" 값을 26으로 갱신하세요.
            // 힌트: 인덱서 사용 ages["키"]=값

            // TODO: "Bob"의 값을 안전하게 꺼내 출력하세요. 없으면 "없음" 출력
            // 힌트: TryGetValue

            // TODO: 모든 항목을 "이름 -> 나이" 형식으로 출력하세요.
            // 힌트: foreach (var kv in ages)

            Console.WriteLine();
        }

        // Queue<T>
        static void Exercise_Queue()
        {
            Console.WriteLine("— Queue<T> 연습");

            // 목표: 프린터 대기열처럼 선입선출 동작을 이해합니다.
            // TODO: 문서 "A","B","C"를 큐에 넣고 맨 앞 항목을 Peek로 확인하세요.
            var q = new Queue<string>();

            // TODO: 하나 Dequeue 하여 출력하고, 남은 개수를 출력하세요.
            // 힌트: Dequeue, Count

            Console.WriteLine();
        }

        // Stack<T>
        static void Exercise_Stack()
        {
            Console.WriteLine("— Stack<T> 연습");

            // 목표: 되돌리기(Undo)처럼 후입선출 동작을 이해합니다.
            // TODO: "first","second","third"를 스택에 Push 하세요.
            var st = new Stack<string>();

            // TODO: 맨 위를 Peek로 확인하고 Pop으로 하나 꺼낸 뒤, 남은 개수를 출력하세요.
            // 힌트: Peek, Pop, Count

            Console.WriteLine();
        }

        // HashSet<T>
        static void Exercise_HashSet()
        {
            Console.WriteLine("— HashSet<T> 연습");

            // 목표: 중복 제거와 집합 연산을 경험합니다.
            // TODO: 1,2,2,3을 넣어 중복이 제거되는지 확인하세요.
            var set = new HashSet<int>();

            // TODO: 2가 포함되어 있는지 출력하세요.
            // 힌트: Contains

            // TODO: 다른 집합 other={2,3,4}와의 합집합(UnionWith) 결과를 출력하세요.
            var other = new HashSet<int> { 2, 3, 4 };

            Console.WriteLine();
        }

        // LinkedList<T>
        static void Exercise_LinkedList()
        {
            Console.WriteLine("— LinkedList<T> 연습");

            // 목표: 노드 기준으로 삽입/삭제를 경험합니다.
            // TODO: "head"와 "tail"을 추가하고, head 앞에 "new head"를 삽입하세요.
            var ll = new LinkedList<string>();

            // TODO: 모든 항목을 순서대로 출력하세요.
            // 힌트: foreach

            Console.WriteLine();
        }

        // SortedSet<T>
        static void Exercise_SortedSet()
        {
            Console.WriteLine("— SortedSet<T> 연습");

            // 목표: 자동 정렬 + 중복 제거를 확인합니다.
            // TODO: "banana","apple","cherry","apple"을 추가하고 전체를 출력하세요.
            var fruits = new SortedSet<string>();

            // TODO: "b"로 시작하는 항목만 출력해 보세요.
            // 힌트: foreach와 StartsWith

            Console.WriteLine();
        }

        // SortedDictionary<TKey,TValue>
        static void Exercise_SortedDictionary()
        {
            Console.WriteLine("— SortedDictionary<TKey,TValue> 연습");

            // 목표: 키가 자동으로 정렬되는 맵을 사용합니다.
            // TODO: 3->"three", 1->"one", 2->"two"를 넣고 전체를 키 순서로 출력하세요.
            var sd = new SortedDictionary<int, string>();

            Console.WriteLine();
        }
    }
}
