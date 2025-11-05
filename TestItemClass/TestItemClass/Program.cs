using System;
using System.Collections.Generic;

/// <summary>
/// 인벤토리 클래스:
/// - 모든 아이템 리스트(_all)
/// - 타입별 전용 리스트(_armors/_weapons/_potions)
/// 타입 기반 조회 및 출력이 빠르도록 별도 리스트를 유지합니다(간단한 캐시의 역할).
/// LINQ를 사용하지 않는 목적에 맞춰 수동으로 분류/추가합니다.
/// </summary>
public sealed class Inventory
{
    // 모든 아이템을 추가 순서대로 보관
    // readonly로 외부에서 변경 불가
    private readonly List<Item> _all = new List<Item>();

    // 타입별 전용 컬렉션(타입 검사 시 캐스팅된 구체 타입을 바로 저장)
    private readonly List<Armor> _armors = new List<Armor>();
    private readonly List<Weapon> _weapons = new List<Weapon>();
    private readonly List<Potion> _potions = new List<Potion>();

    /// <summary>
    /// 인벤토리에 아이템을 추가합니다.
    /// null 체크를 통해 예외를 방지하고, 타입에 따라 전용 리스트에도 추가합니다.
    /// </summary>
    /// <param name="item">추가할 아이템(Armor/Weapon/Potion)</param>
    public void Add(Item item)
    {
        if (item == null) return; // 안전성: null 입력 무시

        // 전체 목록에 항상 추가
        _all.Add(item);

        // 구체 타입 판별 후 해당 전용 리스트에 추가
        if (item is Armor a) _armors.Add(a);
        else if (item is Weapon w) _weapons.Add(w);
        else if (item is Potion p) _potions.Add(p);
    }

    /// <summary>전체 아이템 수를 반환합니다.</summary>
    public int CountAll() => _all.Count;

    /// <summary>모든 아이템을 종류별로 그룹화하여 출력합니다(Armor/Weapon/Potion 순).</summary>
    public void PrintAllGrouped()
    {
        PrintCategory("Armor", _armors);
        PrintCategory("Weapon", _weapons);
        PrintCategory("Potion", _potions);
    }

    /// <summary>지정한 ItemType에 해당하는 카테고리만 출력합니다.</summary>
    /// <param name="type">출력할 아이템 종류</param>
    public void PrintByType(ItemType type)
    {
        if (type == ItemType.Armor) PrintCategory("Armor", _armors);
        else if (type == ItemType.Weapon) PrintCategory("Weapon", _weapons);
        else if (type == ItemType.Potion) PrintCategory("Potion", _potions);
    }

    /// <summary>
    /// 범용 카테고리 출력 도우미(제네릭).
    /// - 제목을 출력하고, 목록이 비어있으면 "비어 있음" 표시.
    /// - 각 아이템의 Name만 출력(간단한 목록 형태).
    /// </summary>
    /// <typeparam name="T">Item을 상속한 구체 타입</typeparam>
    /// <param name="title">화면에 표시할 카테고리 제목</param>
    /// <param name="list">출력할 리스트</param>
    private void PrintCategory<T>(string title, List<T> list) where T : Item
    {
        Console.WriteLine("[" + title + "]");
        if (list.Count == 0)
        {
            // 항목이 없으면 별도 라인으로 표시
            Console.WriteLine(" - (비어 있음)");
            return;
        }

        // 단순히 Name만 출력(원한다면 ToString() 전부 출력하도록 변경 가능)
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine(" - " + list[i].Name);
        }
    }
}

public static class Program
{
    /// <summary>
    /// 콘솔 기반 간단한 인벤토리 데모 프로그램 진입점.
    /// 지원 명령:
    /// - add: 아이템 추가(종류/이름 입력)
    /// - list: 전체(종류별) 또는 특정 종류만(list armor|weapon|potion)
    /// - help: 도움말
    /// - quit/q/exit: 종료
    /// </summary>
    public static void Main()
    {
        var inv = new Inventory();

        Console.WriteLine("명령: add, list, list armor|weapon|potion, help, quit");
        Console.WriteLine("예시: add  → 종류와 이름을 순서대로 입력");

        while (true)
        {
            Console.Write("> ");
            var line = Console.ReadLine();

            // 빈 입력은 무시하고 프롬프트로 돌아감
            if (string.IsNullOrWhiteSpace(line)) continue;

            // 공백 기준 간단 분리(복수 공백 무시)
            var parts = SplitBySpace(line);
            // 명령어는 소문자로 표준화
            var cmd = parts[0].ToLowerInvariant();

            if (cmd == "add")
            {
                // add 명령 처리: 사용자에게 종류와 이름을 순서대로 묻고 추가
                HandleAdd(inv);
            }
            else if (cmd == "list")
            {
                if (parts.Length == 1)
                {
                    // 인벤토리가 비어있는지 확인하여 적절한 메시지 출력
                    if (inv.CountAll() == 0)
                    {
                        Console.WriteLine("인벤토리가 비어 있습니다");
                    }
                    else
                    {
                        // 전체 출력(종류별 그룹)
                        inv.PrintAllGrouped();
                    }
                }
                else
                {
                    // list 뒤에 종류가 붙은 경우: 종류 파싱 후 해당 종류만 출력
                    if (TryParseType(parts[1], out var t))
                    {
                        inv.PrintByType(t);
                    }
                    else
                    {
                        Console.WriteLine("종류는 armor|weapon|potion 중 하나여야 합니다");
                    }
                }
            }
            else if (cmd == "help")
            {
                // 도움말 출력
                PrintHelp();
            }
            else if (cmd == "quit" || cmd == "q" || cmd == "exit")
            {
                // 프로그램 종료
                return;
            }
            else
            {
                // 알 수 없는 명령어 처리
                Console.WriteLine("알 수 없는 명령입니다");
            }
        }
    }

    /// <summary>
    /// add 명령 처리기:
    /// 1) 종류 입력(armor|weapon|potion) - TryParseType로 검증
    /// 2) 이름 입력 - Item 하위 타입 인스턴스를 생성하여 Inventory에 추가
    /// </summary>
    /// <param name="inv">대상 인벤토리</param>
    private static void HandleAdd(Inventory inv)
    {
        Console.Write("종류(armor|weapon|potion): ");
        var tStr = Console.ReadLine();

        // 종류 문자열을 안전하게 파싱. 실패하면 사용자에게 알리고 종료.
        if (!TryParseType(tStr, out var t))
        {
            Console.WriteLine("종류 입력이 올바르지 않습니다");
            return;
        }

        Console.Write("이름: ");
        var name = Console.ReadLine();

        // 파싱 결과에 따라 적절한 구체 타입 인스턴스 생성
        Item item;
        if (t == ItemType.Armor) item = new Armor(name);
        else if (t == ItemType.Weapon) item = new Weapon(name);
        else item = new Potion(name);

        inv.Add(item);
        // 추가된 아이템 정보 간단 출력
        Console.WriteLine("추가됨 → " + item.ToString());
    }

    /// <summary>
    /// 문자열을 ItemType으로 변환 시도합니다.
    /// 지원되는 포맷: armor|a, weapon|w, potion|p (대소문자 무시, 앞뒤 공백 무시)
    /// </summary>
    /// <param name="s">입력 문자열</param>
    /// <param name="t">파싱된 ItemType (성공 시 설정)</param>
    /// <returns>파싱 성공 여부</returns>
    private static bool TryParseType(string s, out ItemType t)
    {
        // 기본값 설정(실패 시 의미 없음)
        t = ItemType.Armor;

        if (string.IsNullOrWhiteSpace(s)) return false;

        // 공백 제거 후 소문자화하여 비교를 단순화
        var v = s.Trim().ToLowerInvariant();

        if (v == "armor" || v == "a") { t = ItemType.Armor; return true; }
        if (v == "weapon" || v == "w") { t = ItemType.Weapon; return true; }
        if (v == "potion" || v == "p") { t = ItemType.Potion; return true; }

        // 매칭 실패
        return false;
    }

    // 공백 분리기(간단 구현)
    // 설명:
    // - 입력 문자열을 순회하며 연속된 공백을 하나로 취급하고,
    // - 공백으로 구분된 토큰을 List<string>에 추가하여 최종 배열을 반환합니다.
    // - 일반적으로 string.Split을 사용할 수 있지만 이 구현은 연속된 공백을 무시하고
    //   빈 토큰을 생성하지 않는 점이 목적입니다.
    private static string[] SplitBySpace(string input)
    {
        var temp = new List<string>();
        var current = string.Empty;

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (char.IsWhiteSpace(c))
            {
                // 공백을 만나면 지금까지 모은 단어를 리스트에 추가(비어있지 않을 때만)
                if (current.Length > 0)
                {
                    temp.Add(current);
                    current = string.Empty;
                }
            }
            else
            {
                // 공백이 아니면 현재 단어에 문자 추가
                current += c;
            }
        }

        // 루프 종료 후 마지막 단어가 남아있다면 추가
        if (current.Length > 0) temp.Add(current);

        return temp.ToArray();
    }

    /// <summary>
    /// 도움말 출력(사용 가능한 명령 목록)
    /// </summary>
    private static void PrintHelp()
    {
        Console.WriteLine("add  → 아이템 추가");
        Console.WriteLine("list → 모든 아이템을 종류별로 표시");
        Console.WriteLine("list armor|weapon|potion → 해당 종류만 표시");
        Console.WriteLine("quit → 프로그램 종료");
    }
}