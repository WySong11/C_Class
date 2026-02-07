using System;
using static System.Console;
using System.Collections.Generic;

public enum ItemType
{
    Weapon,
    Armor,
    Potion
}

public class Program
{
    
    Dictionary<ItemType, List<Item>> inventory = new()
    {
        { ItemType.Weapon, new List<Item>() },
        { ItemType.Armor, new List<Item>() },
        { ItemType.Potion, new List<Item>() }
    };

    static void Main(string[] args)
    {
        WriteLine("Hello from TestClass Program!");
    }
}

public class Item
{
    public ItemType itemtype;
    public string Name { get; set; }
}

public class Weapon : Item
{
}

public class Armor : Item
{
}

public class Potion : Item
{
}