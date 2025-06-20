using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 타입을 명시하여 타입 안전성을 제공하는 제네릭 컬렉션.
// 컴파일 시 타입 검사가 이루어지므로 런타임에 타입 오류가 발생하지 않음.
// 박싱과 언박싱이 필요하지 않아 성능에 영향을 덜 받음.
public class CollectionsGeneric
{
    public void OnList()
    {
        // List<T> : 제네릭 컬렉션으로, 타입 안전성을 제공하며 동적으로 크기가 변할 수 있는 배열
        List<string> list = new List<string>();
        list.Add("Hello");
        list.Add("World");
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Count : List의 요소 개수
        Console.WriteLine($"Count: {list.Count}");

        // Remove : 특정 요소를 제거
        list.Remove("World");
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Insert : 특정한 위치에 요소를 삽입
        list.Insert(0, "Greetings");
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // AddRange : 다른 컬렉션의 요소를 추가
        List<string> anotherList = new List<string>() { "Apple", "Banana" };
        list.AddRange(anotherList);
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Contains : 특정 요소가 List에 포함되어 있는지 확인
        bool containsHello = list.Contains("Hello");
        Console.WriteLine($"Contains 'Hello': {containsHello}");
        Console.WriteLine();

        // RemoveRange : 특정 범위의 요소를 제거
        list.RemoveRange(1, 2); // Remove two items starting from index 1
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Clear : List의 모든 요소를 제거
        list.Clear();
        Console.WriteLine($"Count after Clear: {list.Count}");
        Console.WriteLine();

        // Sort : List의 요소를 정렬
        list.Add("Banana");
        list.Add("Apple");
        list.Add("Cherry");
        list.Sort();
        Console.WriteLine("Sorted List:");
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Find : 특정 조건을 만족하는 첫 번째 요소를 찾음
        list.Add("Avocado");
        list.Add("Blueberry");
        list.Add("Apricot");
        string? foundItem = list.Find(item => item.StartsWith('A'));
        if (foundItem != null)
        {
            Console.WriteLine($"Found item: {foundItem}");
        }
        Console.WriteLine();

        // FindAll : 특정 조건을 만족하는 모든 요소를 찾음
        List<string> foundItems = list.FindAll(item => item.StartsWith('B'));
        Console.WriteLine("Items starting with 'B':");
        foreach (var item in foundItems)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // IndexOf : 특정 요소의 인덱스를 반환
        int index = list.IndexOf("Apple");
        Console.WriteLine($"Index of 'Apple': {index}");
        Console.WriteLine();

        // Contains : 특정 요소가 List에 포함되어 있는지 확인
        bool containsBanana = list.Contains("Banana");
        Console.WriteLine($"Contains 'Banana': {containsBanana}");
        Console.WriteLine();
    }

    public void OnDictionary()
    {
        // Dictionary<TKey, TValue> : 키와 값의 쌍으로 데이터를 저장하는 제네릭 컬렉션
        Dictionary<int, string> dictionary = new Dictionary<int, string>();
        dictionary.Add(1, "One");
        dictionary.Add(2, "Two");
        dictionary.Add(3, "Three");
        // KeyValuePair<TKey, TValue> : 키와 값의 쌍을 나타내는 구조체
        foreach (KeyValuePair<int, string> kvp in dictionary)
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
        Console.WriteLine();
        // Count : Dictionary의 요소 개수
        Console.WriteLine($"Count: {dictionary.Count}");
        // Remove : 특정 키를 가진 요소를 제거
        dictionary.Remove(2);
        foreach (KeyValuePair<int, string> kvp in dictionary)
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
        Console.WriteLine();

        // ContainsKey : 특정 키가 Dictionary에 포함되어 있는지 확인
        bool containsKey = dictionary.ContainsKey(1);
        Console.WriteLine($"Contains key 1: {containsKey}");
        Console.WriteLine();

        // ContainsValue : 특정 값이 Dictionary에 포함되어 있는지 확인
        bool containsValue = dictionary.ContainsValue("Three");
        Console.WriteLine($"Contains value 'Three': {containsValue}");
        Console.WriteLine();

        // Clear : Dictionary의 모든 요소를 제거
        dictionary.Clear();
        Console.WriteLine($"Count after Clear: {dictionary.Count}");
    }

    public void OnQueue()
    {
        // Queue<T> : 선입선출(FIFO) 방식의 제네릭 컬렉션
        Queue<string> queue = new Queue<string>();
        queue.Enqueue("First");
        queue.Enqueue("Second");
        queue.Enqueue("Third");
        foreach (var item in queue)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();
        // Count : Queue의 요소 개수
        Console.WriteLine($"Count: {queue.Count}");
        // Dequeue : 큐에서 가장 먼저 들어온 요소를 제거하고 반환
        string dequeuedItem = queue.Dequeue();
        Console.WriteLine($"Dequeued: {dequeuedItem}");
        foreach (var item in queue)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();
        // Peek : 큐에서 가장 먼저 들어온 요소를 제거하지 않고 반환
        string peekedItem = queue.Peek();
        Console.WriteLine($"Peeked: {peekedItem}");
    }

    public void OnStack()
    {
        // Stack<T> : 후입선출(LIFO) 방식의 제네릭 컬렉션
        Stack<string> stack = new Stack<string>();
        stack.Push("First");
        stack.Push("Second");
        stack.Push("Third");
        foreach (var item in stack)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Count : Stack의 요소 개수
        Console.WriteLine($"Count: {stack.Count}");
        Console.WriteLine();

        // Pop : 스택에서 가장 마지막에 들어온 요소를 제거하고 반환
        string poppedItem = stack.Pop();
        Console.WriteLine($"Popped: {poppedItem}");
        foreach (var item in stack)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Peek : 스택에서 가장 마지막에 들어온 요소를 제거하지 않고 반환
        string peekedItem = stack.Peek();
        Console.WriteLine($"Peeked: {peekedItem}");
    }

    public void OnHashSet()
    {
        // HashSet<T> : 중복되지 않는 요소를 저장하는 제네릭 컬렉션
        HashSet<string> hashSet = new HashSet<string>();
        hashSet.Add("Apple");
        hashSet.Add("Banana");
        hashSet.Add("Cherry");
        foreach (var item in hashSet)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Count : HashSet의 요소 개수
        Console.WriteLine($"Count: {hashSet.Count}");
        Console.WriteLine();

        // Remove : 특정 요소를 제거
        hashSet.Remove("Banana");
        foreach (var item in hashSet)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Contains : 특정 요소가 HashSet에 포함되어 있는지 확인
        bool containsApple = hashSet.Contains("Apple");
        Console.WriteLine($"Contains 'Apple': {containsApple}");
        Console.WriteLine();
    }

    public void OnSortedSet()
    {
        // SortedSet<T> : 정렬된 중복되지 않는 요소를 저장하는 제네릭 컬렉션
        SortedSet<int> sortedSet = new SortedSet<int>();
        sortedSet.Add(3);
        sortedSet.Add(1);
        sortedSet.Add(2);
        foreach (var item in sortedSet)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Count : SortedSet의 요소 개수
        Console.WriteLine($"Count: {sortedSet.Count}");
        Console.WriteLine();

        // Remove : 특정 요소를 제거
        sortedSet.Remove(2);
        foreach (var item in sortedSet)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Contains : 특정 요소가 SortedSet에 포함되어 있는지 확인
        bool containsOne = sortedSet.Contains(1);
        Console.WriteLine($"Contains 1: {containsOne}");
        Console.WriteLine();
    }

    public void OnLinkedList()
    {
        // LinkedList<T> : 노드 기반의 양방향 연결 리스트
        LinkedList<string> linkedList = new LinkedList<string>();
        linkedList.AddLast("First");
        linkedList.AddLast("Second");
        linkedList.AddLast("Third");
        foreach (var item in linkedList)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Count : LinkedList의 요소 개수
        Console.WriteLine($"Count: {linkedList.Count}");
        Console.WriteLine();

        // Remove : 특정 요소를 제거
        linkedList.Remove("Second");
        foreach (var item in linkedList)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        // Find : 특정 요소를 찾아 LinkedListNode<T> 반환
        var foundNode = linkedList.Find("First");
        if (foundNode != null)
        {
            Console.WriteLine($"Found: {foundNode.Value}");
        }
        Console.WriteLine();
    }
}