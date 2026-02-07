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

        bool m_loop = true; // 메인 루프 제어 변수

        do
        {
            Write("Add, List, Quit: ");
            
            string input = ReadLine(); // 사용자 입력 받기

            WriteLine(); // 빈 줄 출력

            switch(input.ToLower())
            {
                case "add":
                    Write("Item Type (Weapon, Armor, Potion): ");
                    
                    string typeInput = ReadLine(); // 아이템 종류 입력 받기

                    if(Enum.TryParse<ItemType>(typeInput, true, out ItemType itemType ))
                    {
                        Write("Item Name: ");
                        string itemName = ReadLine(); // 아이템 이름 입력 받기
                        Item newItem = itemType switch
                        {
                            ItemType.Weapon => new Weapon { type = itemType, name = itemName },
                            ItemType.Armor => new Armor { type = itemType, name = itemName },
                            ItemType.Potion => new Potion { type = itemType, name = itemName },
                            _ => null
                        };

                        if (newItem != null)
                        {
                            inventory[itemType].Add(newItem); // 인벤토리에 새 아이템 추가
                            WriteLine($"{itemType} '{itemName}' added to inventory.");
                        }
                    }
                    else
                    {
                        WriteLine("Invalid item type.");

                    }
                    break;

                case "list":
                    foreach(var category in inventory)
                    {
                        WriteLine($"{category.Key}s:");

                        if(category.Value.Count == 0)
                        {
                            WriteLine(" - (none)");
                            continue;
                        }

                        foreach (var item in category.Value)
                        {
                            //Item tt = item as Item;

                            WriteLine($" - {item.name}");
                        }
                    }
                    break;

                case "quit":
                    m_loop = false;
                    break;  
            }

            WriteLine(); // 빈 줄 출력

        } while (m_loop);
    }
    
}

public class Item
{
    public ItemType type; // 아이템 종류
    public string name;   // 아이템 이름

    public Item() { }

    public Item(ItemType type, string name)
    {
        this.type = type;
        this.name = name;
    }
}

public class Weapon : Item, IGetName
{
    public int damage; // 무기 공격력
 
    public Weapon() { }

    public Weapon(string name, int damage) : base(ItemType.Weapon, name)
    {
        this.damage = damage;
    }

    public void GetName()
    {
        WriteLine("111");
    }
}

public class Armor : Item, IGetName
{
    public int defense; // 방어구 방어력

    public Armor() { }

    public Armor(string name, int defense) : base(ItemType.Armor, name)
    {
        this.defense = defense;
    }

    public void GetName()
    {
    }
}

public class Potion : Item, IGetName
{
    public int healAmount; // 포션 회복량

    public Potion() { }

    public Potion(string name, int healAmount) : base(ItemType.Potion, name)
    {
        this.healAmount = healAmount;
    }

    public void GetName()
    {
        WriteLine(name);
    }
}

public interface IGetName
{
       void GetName();
}

public class Shop
{
       public List<Item> itemsForSale; // 판매 중인 아이템 목록
    public Shop()
    {
        itemsForSale = new List<Item>();
    }
    public void AddItemForSale(Item item)
    {
        itemsForSale.Add(item);
    }


}

/*public class npc : Shop, Item
{
    public string npcName;
    public npc(string name)
    {
        npcName = name;
    }
    public void DisplayItemsForSale()
    {
        Console.WriteLine($"{npcName}'s Items for Sale:");
        foreach (var item in itemsForSale)
        {
            Console.WriteLine($" - {item.name} ({item.type})");
        }
    }
}*/

public interface IUsable
{
    void Use();
}

public interface IEquipable
{
    int durability { get; set; }
    void Equip();
}

public class UsablePotion : Potion, IUsable
{
    public UsablePotion(string name, int healAmount) : base(name, healAmount) { }
    public void Use()
    {
        Console.WriteLine($"{name} used! Healed for {healAmount} HP.");
    }
}

public class EquipableArmor : IUsable, IEquipable
{
    public int durability { get; set; }

    public void Use()
    {
    }
    public void Equip()
    {
    }
}