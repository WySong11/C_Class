using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class LectureCollection
{
    public enum E_Type
    {
        Player = 1001,
        Enemy,
        NPC,

        Warrior = 2001,
        Wizard,
        Archer,

        MAX,
    };


    static void Main(string[] args)
    {
        int[] nums;

        // Array 선언 및 리터럴 초기화
        int[] numbers = [1, 2, 3, 4, 5];

        foreach (var number in numbers)
        {
            Console.WriteLine(number);
        }

        Console.WriteLine();

        // Array 선언 및 크기 지정
        // 기본값으로 초기화됨 (int의 경우 0)
        int[] numbers2 = new int[5];

        for (int i = 0; i < numbers2.Length; i++)
        {
            Console.WriteLine(numbers2[i]);
        }

        // 다차원 배열 선언 및 초기화
        // 2행 3열 배열
        int[,] ints = { { 1, 2, 3 }, { 4, 5, 6 } };

        Console.WriteLine();

        for (int i = 0; i < ints.GetLength(0); i++) // 행
        {
            for (int j = 0; j < ints.GetLength(1); j++) // 열
            {
                Console.Write(ints[i, j] + " ");
            }
            Console.WriteLine();
        }

        // 가변 배열 (Jagged Array) 선언 및 초기화
        int[][] jagged = new int[3][]
        {
            new int[] {1, 2},
            new int[] {3, 4, 5},
            new int[] {6, 7, 8, 9}
        };

        Console.WriteLine();

        // jagged.Length : 행의 수
        for (int i = 0; i < jagged.Length; i++)
        {
            // jagged[i].Length : 각 행의 열의 수
            for (int j = 0; j < jagged[i].Length; j++)
            {
                Console.Write(jagged[i][j] + " ");
            }
            Console.WriteLine();
        }

        jagged[0] = new int[] { 10, 20, 30 }; // 첫 번째 행을 새로운 배열로 변경


        jagged[1][2] = 99; // 두 번째 행의 세 번째 요소 변경

        int i_index = 2;
        int j_index = 3;

        // [3][4] : 0,1,2 => Length : 3 / 0,1,2,3 => Length : 4
        // Index out of bounds 체크
        // Length 보다 작은지 확인

        if (i_index < jagged.Length && j_index < jagged[i_index].Length)
        {
            Console.WriteLine($"\nValue at jagged[{i_index}][{j_index}]: {jagged[i_index][j_index]}");
        }
        else
        {
            Console.WriteLine($"\nIndex out of bounds for jagged[{i_index}][{j_index}]");
        }

        //jagged[5][5] = 100; // Index out of bounds 예외 발생

        ///////////////////////////////////////////////////////////////
        /// System.Collections 

        // 비제네릭 컬렉션
        // Object 타입을 저장
        // ArrayList : 크기 제한이 없는 배열
        // HashTable : 키-값 쌍으로 데이터를 저장하는 컬렉션
        // Queue : FIFO(First In First Out) 방식으로 데이터를 저장하는 컬렉션
        // Stack : LIFO(Last In First Out) 방식으로 데이터를 저장하는 컬렉션
        // SortedList : 키-값 쌍으로 데이터를 저장하며, 키를 기준으로 정렬되는 컬렉션

        ArrayList arrayList = new ArrayList();
        arrayList.Add(1);
        arrayList.Add("Hello");
        arrayList.Add(3.14);

        foreach (var item in arrayList)
        {
            Console.WriteLine(item);
        }

        Hashtable ht = new Hashtable();
        ht["name"] = "Alice";
        ht["age"] = 30;
        ht[12] = "Number Twelve";
        ht[3.14] = "Pi";

        Console.WriteLine($"\nName: {ht["name"]}, Age: {ht["age"]}");
        Console.WriteLine($"Key 12: {ht[12]}, Key 3.14: {ht[3.14]}");

        Queue queue = new Queue();

        // Enqueue : 데이터 추가
        queue.Enqueue(1);
        queue.Enqueue(2);

        // Dequeue : 데이터 제거 및 반환
        queue.Dequeue(); // 1
        queue.Dequeue(); // 2


        Stack stack = new Stack();
        // Push : 데이터 추가
        stack.Push(1);
        stack.Push(2);

        // Pop : 데이터 제거 및 반환
        stack.Pop(); // 2
        stack.Pop(); // 1


        ///////////////////////////////////////////////////////////////
        /// System.Collections.Generic
        /// 

        // List<T> : 가장 많이 사용되는 제네릭 컬렉션. 동적 배열
        // Dictionary<T, T> : 키-값 쌍으로 데이터를 저장하는 제네릭 컬렉션. 많이 사용됨.
        // Queue<T> : 제네릭 큐
        // Stack<T> : 제네릭 스택
        // SortedList<TKey, TValue> : 키-값 쌍으로 데이터를 저장하며, 키를 기준으로 정렬되는 제네릭 컬렉션
        // HashSet<T> : 중복되지 않는 요소들의 집합을 저장하는 제네릭 컬렉션
        // LinkedList<T> : 양방향 연결 리스트를 구현한 제네릭 컬렉션

        List<int> list = new List<int>();

        // int 타입만 허용
        // Add 메서드
        // 요소 추가
        list.Add(1);
        list.Add(2);

        //list.Add(3.14); // 컴파일 오류 발생 (int 타입만 허용)

        foreach (var item in list)
        {
            Console.WriteLine(item);
        }

        list.Clear(); // 모든 요소 제거

        list.Add(10);
        list.Add(20);

        // Remove 메서드
        list.Remove(10); // 값으로 요소 제거

        // 리스트에서 0번째 인덱스 요소 제거   
        list.RemoveAt(0);

        // list.Count : 리스트의 요소 개수
        if ( list.Count > 5)
        {
            list.RemoveAt(5); // Index out of bounds 예외 발생
        }

        // 값이 리스트에 존재하는지 확인
        if (  list.Contains(20) == true )
        {
            list.Remove(20);            
        }

        // RemoveRange : 특정 범위의 요소 제거
        list.RemoveRange(0, list.Count); 

        // AddRange : 다른 컬렉션의 모든 요소 추가
        list.AddRange(list);

        // Insert : 특정 위치에 요소 삽입
        //list.Insert(1, 100); // 0번째 인덱스에 100 삽입

        // InsertRange : 특정 위치에 다른 컬렉션의 모든 요소 삽입
        list.InsertRange(0, new List<int> { 1, 2, 3 } );

        int value = list.Find( (item) => item == 2 ); // 값이 2인 요소 반환

        int index = list.FindIndex( (item) => item == 2 ); // 값이 2인 요소의 인덱스 반환

        list.Sort(); // 오름차순 정렬

        list.Sum(); // 모든 요소의 합계 반환

        list.Average(); // 모든 요소의 평균 반환

        list.Reverse(); // 리스트 뒤집기

        // 내림차순 정렬
        list.Sort( (a, b) => b.CompareTo(a) );

        list.Min(); // 최소값 반환
                    
        list.Max(); // 최대값 반환

        List<List<int>> list2 = new List<List<int>>();

        ////////////////////////////////////////////////////
        /// Dictionary<TKey, TValue>
        // Key-Value 쌍으로 데이터를 저장
        // Key는 중복 불가
        // Value는 중복 가능

        Dictionary<string, int> dict = new Dictionary<string, int>();

        Dictionary<int, string> dict2 = new Dictionary<int, string>();

        Dictionary<int, List<string>> dict3 = new Dictionary<int, List<string>>();

        // 요소 추가
        dict.Add("one", 1);

        // 인덱서를 사용한 요소 추가
        dict["two"] = 2; 

        // 요소 접근
        int val = dict["one"];

        Console.WriteLine($"\nKey 'one' has value: {val}");

        // 요소 제거
        if( dict.Remove("two") == true )
        {
            Console.WriteLine("Key 'two' removed from the dictionary.");
        }

        // Key 존재 여부 확인
        if (dict.ContainsKey("one"))
        {
            Console.WriteLine("Key 'one' exists in the dictionary.");
        }

        if (dict.ContainsKey("two"))
        {
        }

        // Value 존재 여부 확인
        if ( dict.ContainsValue(1) == true )
        {
            Console.WriteLine("Value 1 exists in the dictionary.");
        }

        // TryGetValue : Key가 존재하면 Value를 반환하고, 존재하지 않으면 기본값 반환
        if ( dict.TryGetValue("one", out int value_one) == true )
        {
            Console.WriteLine($"Key 'one' has value: {value_one}");
        }
        else
        {
            Console.WriteLine("Key 'one' does not exist in the dictionary.");
        }

        dict.Clear(); // 모든 요소 제거

        int dictCount = dict.Count; // 요소 개수 반환

        dict["one"] = 1;
        dict["two"] = 2;
        dict["three"] = 3;

        // Key-Value 쌍 반복
        foreach ( var kvp in dict )
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }

        // ToList : Dictionary를 List로 변환
        // KeyValuePair 리스트로 변환
        var dictAsList = dict.ToList();

        Console.WriteLine();

        foreach ( var kvp in dictAsList )
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }

        // Key 값만 리스트로 변환
        var keysList = dict.Keys.ToList();

        Console.WriteLine();

        foreach (var key in keysList)
        {
            Console.WriteLine($"Key: {key}");
        }

        // Value 값만 리스트로 변환
        var valuesList = dict.Values.ToList();

        Console.WriteLine();

        foreach (var value_item in valuesList)
        {
            Console.WriteLine($"Value: {value_item}");
        }

        /////////////////////////////////////////////////////////////////////////////////
        ///

        // System.Collections
        // 비제네릭 컬렉션
        // ArrayList: 동적 배열, 크기가 자동으로 조정됨
        // Queue: FIFO(First In First Out) 방식의 큐
        // Stack: LIFO(Last In First Out) 방식의 스택
        // Hashtable: 키-값 쌍으로 데이터를 저장하는 해시 테이블
        // SortedList: 키-값 쌍으로 데이터를 저장하며, 키를 기준으로 정렬된 리스트

        arrayList = new ArrayList();

        // Add 메서드: ArrayList에 요소를 추가
        // Remove 메서드: ArrayList에서 요소를 제거
        // Contains 메서드: ArrayList에 특정 요소가 있는지 확인
        // Clear 메서드: ArrayList의 모든 요소를 제거
        // Count 속성: ArrayList의 요소 개수를 반환
        // Capacity 속성: ArrayList의 현재 용량을 반환
        // IndexOf 메서드: ArrayList에서 특정 요소의 인덱스를 반환. 찾으면 Index , 못 찾으면 -1
        // Insert 메서드: ArrayList의 특정 인덱스에 요소를 삽입
        // Sort 메서드: ArrayList의 요소를 정렬
        // Reverse 메서드: ArrayList의 요소 순서를 반대로 변경
        // ToArray 메서드: ArrayList의 요소를 배열로 반환
        // TrimToSize 메서드: ArrayList의 용량을 현재 요소 개수에 맞게 조정
        // GetRange 메서드: ArrayList의 특정 범위의 요소를 반환
        // Clone 메서드: ArrayList의 얕은 복사본을 반환
        // CopyTo 메서드: ArrayList의 요소를 배열로 복사
        // ForEach 메서드: ArrayList의 각 요소에 대해 지정된 작업을 수행
        // ToString 메서드: ArrayList의 문자열 표현을 반환
        // Equals 메서드: ArrayList와 다른 객체를 비교
        // GetHashCode 메서드: ArrayList의 해시 코드를 반환

        // ArrayList에 요소 추가
        // 1 : int 타입 추가
        // Boxing: 값 형식(int)을 참조 형식(object)으로 변환하여 ArrayList에 저장
        arrayList.Add(1);

        // "Hello" : string 타입 추가
        // Boxing: 값 형식이 아닌 참조 형식(string)을 그대로 ArrayList에 저장
        arrayList.Add("Hello");

        // 3.14f : float 타입 추가
        // Boxing: 값 형식(float)을 참조 형식(object)으로 변환하여 ArrayList에 저장
        arrayList.Add(3.14f);

        arrayList.Add(20);

        // ArrayList의 요소 출력
        for (int i = 0; i < arrayList.Count; i++)
        {
            Console.WriteLine($"Element at index {i}: {arrayList[i]}");
        }

        // var 키워드: 변수의 타입을 컴파일러가 자동으로 추론하도록 하는 기능
        foreach (var item in arrayList)
        {
            Console.WriteLine($"Element: {item}");
        }

        int number1 = (int)arrayList[0];
        int number2 = (int)arrayList[3];

        Console.WriteLine(number1);
        Console.WriteLine(number2);

        // RemoveAt : Index 를 삭제
        arrayList.RemoveAt(0);
        PrintArraylist(arrayList);

        // Remove : 값을 찾아서 삭제
        arrayList.Remove(20);
        PrintArraylist(arrayList);

        /////////////////////////////////////////////////////////////
        // Queue
        // FIFO : First In , First Out 선입선출

        // Instance 생성
        Queue que = new Queue();

        // Enqueue : 값 삽입
        que.Enqueue(1);
        que.Enqueue(2);
        que.Enqueue(3);

        PrintQueue(que);

        Console.WriteLine();

        /////////////////////////////////////////////////////////////
        // Stack
        // LIFO : Last In , First Out 후입선출

        Stack sta = new Stack();

        // Push : 값 삽입
        sta.Push(1);
        sta.Push(2);
        sta.Push(3);

        PrintStack(sta);

        Console.WriteLine();

        ///////////////////////////////////////////////////
        // Hashtable
        // Key 와 Value 를 한 쌍으로 저장

        ht = new Hashtable();



        ht.Add(E_Type.Player, "Player");
        ht.Add(E_Type.Enemy, "Enemy");
        ht.Add(E_Type.NPC, "NPC");

        Console.WriteLine(ht[1001]);
        Console.WriteLine(ht[1002]);
        Console.WriteLine(ht[1003]);

        ht.Add("Warrior", E_Type.Warrior);
        ht.Add("Wizard", E_Type.Wizard);
        ht.Add("Archer", E_Type.Archer);

        foreach (var item in ht)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();

        foreach (DictionaryEntry item in ht)
        {
            Console.WriteLine($"Key -> {item.Key} , Value -> {item.Value}");
        }

        ///////////////////////////////////////////////////////////
        // Collections.Generic

        // List<T> : 가장 많이 사용하는 동적 배열
        // Dictionary<T, T> : Key - Value 쌍으로 데이터를 저장
        // Queue<T> : FIFO
        // Stack<T> : LIFO
        // HashSet<T> : 중복되지 않는 요소들만 저장

        // Type 을 지정해서 Instance 생성
        List<int> intList = new List<int>();

        // 지정된 Type 만 입력 가능
        // Boxing , Unboxing 이 없음
        intList.Add(0);
        intList.Add(1);
        intList.Add(2);

        //intList.Add(3.14f);
        //intList.Add("string");

        Console.WriteLine();

        PrintList(intList);

        intList.RemoveAt(0);

        PrintList(intList);

        intList[0] = 10;

        PrintList(intList);

        intList.Remove(10);

        PrintList(intList);

        // Clear : 모든 항목들을 삭제
        intList.Clear();

        PrintList(intList);

        /*        for(E_Type t = E_Type.Player ; t<E_Type.MAX;  )
                {
                    intList.Add((int)t);
                }*/

        foreach (E_Type t in Enum.GetValues<E_Type>())
        {
            if (t == E_Type.MAX) break;

            intList.Add((int)t);
        }

        PrintList(intList);

        RemoveList(ref intList, (int)E_Type.NPC);

        PrintList(intList);

        RemoveList(ref intList, 1005);

        PrintList(intList);

        // Find : 값을 찾아서 Value반환
        val = intList.Find(x => x == 10);

        // FindIndex : 값을 찾아서 Index 반환
        index = intList.FindIndex(x => x == 10);

        // 오름차순 정렬
        intList.Sort();

        PrintList(intList);

        // 내림차순 정렬
        intList.Sort((a, b) => b.CompareTo(a));

        PrintList(intList);

        // 모든 요소의 합계 반환
        Console.WriteLine("Sum => " + intList.Sum());

        // 모든 요소의 평균
        Console.WriteLine("Average => " + intList.Average());

        intList.Reverse();

        PrintList(intList);

        Console.WriteLine("Min => " + intList.Min());
        Console.WriteLine("Max => " + intList.Max());
    }


    static void RemoveList(ref List<int> list, int value)
    {
        // Contains : 리스트에 값이 있는지 확인하는 변수
        // Indexof : 위치를 찾아준다. 없으면 -1

        if (list.Contains(value) == true)
        {
            list.Remove(value);
        }
        else
        {
            Console.WriteLine("Not Find Value -> " + value);
        }
    }

    static void PrintList(List<int> list)
    {
        foreach (int item in list)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();
    }


    static void PrintStack(Stack stack)
    {
        int count = stack.Count;

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine("Stack Value -> " + stack.Pop());

            Console.WriteLine("Stack Count -> " + stack.Count);
        }
    }

    static void PrintQueue(Queue queue)
    {
        int count = queue.Count;

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine("Queue Value -> " + queue.Dequeue());

            Console.WriteLine("Queue Count -> " + queue.Count);
        }
    }

    static void PrintArraylist(ArrayList inarrayList)
    {
        foreach (var item in inarrayList)
        {
            Console.WriteLine($"Element: {item}");
        }
        Console.WriteLine();
    }
}
