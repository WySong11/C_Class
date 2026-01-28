using System;                      // C# 기본 기능(자료형, Enum, Random 등)을 쓰기 위한 네임스페이스
using static System.Console;       // Console.WriteLine 같은 걸 "Console." 없이 바로 쓰기 위해 static import
using System.Collections.Generic;  // Dictionary, List 같은 컬렉션 자료구조를 쓰기 위한 네임스페이스

// 아이템 종류를 "정해진 값"으로 만들기 위한 enum(열거형)
// 문자열로 관리하면 오타가 나기 쉬운데, enum은 선택지가 고정되어 안전해요.
public enum ItemType
{
    Weapon,  // 무기
    Armor,   // 방어구
    Potion   // 포션
}

public class Program
{
    // 프로그램 시작점(Entry Point)
    static void Main(string[] args)
    {
        // 인벤토리를 "아이템 종류(ItemType) -> 그 종류의 아이템 목록(List<Item>)" 형태로 저장하기 위해 Dictionary 사용
        // 예: inventory[ItemType.Weapon] = 무기 목록
        Dictionary<ItemType, List<Item>> inventory = new()
        {
            // 무기 목록을 비어있는 리스트로 초기화
            { ItemType.Weapon, new List<Item>() },

            // 방어구 목록을 비어있는 리스트로 초기화
            { ItemType.Armor, new List<Item>() },

            // 포션 목록을 비어있는 리스트로 초기화
            { ItemType.Potion, new List<Item>() }
        };

        // 프로그램을 계속 반복할지 결정하는 플래그(스위치)
        // true면 반복 계속, false면 반복 종료
        bool m_loop = true;

        // do-while: 최소 1번은 실행하고, 조건이 true인 동안 계속 반복
        do
        {
            // 사용자에게 명령어 입력 안내를 출력
            Write("Add, List, Exit : ");

            // 사용자가 입력한 문자열을 읽어옴
            string input = ReadLine();

            // 보기 좋게 한 줄 띄움
            WriteLine();

            // 입력을 소문자로 바꿔서 비교하면, Add / ADD / add 모두 같은 명령으로 처리 가능
            switch (input.ToLower())
            {
                case "add":
                    {
                        // 아이템 타입 입력 받기
                        Write("Item Type (Weapon, Armor, Potion): ");

                        // 타입 문자열 입력 받기
                        string typeInput = ReadLine();

                        // Enum.TryParse:
                        // - 사용자가 입력한 문자열이 ItemType에 있는 값인지 검사하고
                        // - 성공하면 itemType 변수에 변환된 enum 값을 넣어줌
                        // true(대소문자 무시) 옵션을 줘서 "weapon"도 OK
                        if (Enum.TryParse<ItemType>(typeInput, true, out ItemType itemType))
                        {
                            // 아이템 이름 입력 받기
                            Write("Item Name: ");

                            // 이름 문자열 입력 받기
                            string itemName = ReadLine();

                            // switch 식(switch expression):
                            // itemType에 따라 다른 "파생 클래스" 객체를 만들어줌
                            // (Weapon / Armor / Potion)
                            Item newItem = itemType switch
                            {
                                // 무기면 Weapon 객체 생성
                                // - itemtype 필드에 타입 저장
                                // - Name 프로퍼티에 이름 저장
                                ItemType.Weapon => new Weapon { itemtype = itemType, Name = itemName },

                                // 방어구면 Armor 객체 생성
                                ItemType.Armor => new Armor { itemtype = itemType, Name = itemName },

                                // 포션이면 Potion 객체 생성
                                ItemType.Potion => new Potion { itemtype = itemType, Name = itemName },

                                // 혹시 모를 예외(여기서는 사실상 안 들어옴)
                                _ => null
                            };

                            // newItem이 null이 아니면 정상적으로 생성된 것
                            if (newItem != null)
                            {
                                // inventory에서 해당 타입의 리스트를 꺼내서(newItemType)
                                // 그 리스트에 아이템을 추가
                                inventory[itemType].Add(newItem);

                                // 사용자에게 추가 완료 메시지 출력
                                WriteLine($"{itemType} '{itemName}' added to inventory.");
                            }
                        }
                        else
                        {
                            // Enum 변환에 실패했다는 뜻(예: "Wepon" 같은 오타)
                            WriteLine("Invalid item type.");
                        }
                    }
                    break;

                case "list":
                    {
                        // inventory는 (ItemType -> List<Item>) 쌍들의 묶음
                        // foreach로 하나씩 꺼내서 출력
                        foreach (var category in inventory)
                        {
                            // category.Key : ItemType (Weapon/Armor/Potion)
                            // category.Value : 그 타입의 아이템 리스트(List<Item>)
                            // "Weapons:"처럼 보이게 s를 붙여 출력
                            WriteLine($"{category.Key}s:");

                            // 해당 타입의 리스트가 비어있으면 안내 출력
                            if (category.Value.Count == 0)
                            {
                                WriteLine(" - (none)");
                                continue; // 다음 타입으로 넘어감
                            }

                            // 리스트에 들어있는 아이템들을 하나씩 출력
                            foreach (var item in category.Value)
                            {
                                // 아이템 이름 출력
                                WriteLine($" - {item.Name}");
                            }
                        }
                    }
                    break;

                case "exit":
                    // 반복 종료 스위치를 false로 바꿔서 do-while 탈출
                    m_loop = false;
                    break;

                default:
                    // add/list/exit가 아닌 다른 입력이 들어왔을 때
                    WriteLine("Unknown command.");
                    break;
            }

            // 한 번의 명령 처리 후 보기 좋게 한 줄 띄움
            WriteLine();

        } while (m_loop); // m_loop가 true인 동안 계속 반복
    }
}

// 모든 아이템의 공통 부모 클래스
public class Item
{
    // 아이템 타입을 저장하는 필드
    // (Weapon/Armor/Potion)
    public ItemType itemtype;

    // 아이템 이름을 저장하는 프로퍼티
    // { get; set; } 은 읽기/쓰기가 가능한 자동 구현 프로퍼티
    public string Name { get; set; }

    // 기본 생성자
    // new Item() 할 때 호출됨
    public Item() { }

    // 타입과 이름을 받아서 초기화하는 생성자
    public Item(ItemType type, string name)
    {
        itemtype = type; // 필드에 타입 저장
        Name = name;     // 프로퍼티에 이름 저장
    }
}

// 무기 클래스: Item을 상속받음(Weapon is-a Item)
public class Weapon : Item
{
    // 기본 생성자
    public Weapon() { }

    // 부모(Item)의 생성자를 호출해서 초기화하는 생성자
    public Weapon(ItemType type, string name) : base(type, name)
    {
        // base(type, name)이 부모 생성자 호출이므로
        // 여기서 따로 쓸 내용이 없으면 비워둬도 됨
    }
}

// 방어구 클래스
public class Armor : Item
{
    public Armor() { }

    public Armor(ItemType type, string name) : base(type, name)
    {
    }
}

// 포션 클래스
public class Potion : Item
{
    public Potion() { }

    public Potion(ItemType type, string name) : base(type, name)
    {
    }
}
