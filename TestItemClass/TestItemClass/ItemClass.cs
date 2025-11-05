/// <summary>
/// 아이템의 종류를 나타내는 열거형입니다.
/// 코드 전반에서 아이템 분류(Armor/Weapon/Potion)를 명확히 하기 위해 사용합니다.
/// </summary>
public enum ItemType { Armor, Weapon, Potion }

/// <summary>
/// 모든 아이템의 공통 기능을 제공하는 추상 기본 클래스입니다.
/// - 이름 검증/정리
/// - 아이템 타입 추상 속성
/// - 기본 ToString() 출력
/// </summary>
public abstract class Item
{
    /// <summary>
    /// 아이템 이름(읽기 전용).
    /// 생성자에서 null/빈값을 검사하고 필요시 "Unknown"으로 대체합니다.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 해당 인스턴스의 구체적인 ItemType(Armor/Weapon/Potion)을 파생 클래스에서 구현합니다.
    /// </summary>
    public abstract ItemType Type { get; }

    /// <summary>
    /// 생성자: 이름을 받아 공백/빈값을 정리합니다.
    /// string.IsNullOrWhiteSpace 체크로 입력 유효성 보장.
    /// </summary>
    /// <param name="name">아이템 이름(사용자 입력 가능)</param>
    protected Item(string name)
    {
        // 이름이 null 또는 공백이면 "Unknown"으로 기본화,
        // 그렇지 않으면 앞뒤 공백을 잘라서 저장.
        Name = string.IsNullOrWhiteSpace(name) ? "Unknown" : name.Trim();
    }

    /// <summary>
    /// ToString은 간단한 정보(종류 | 이름)를 반환합니다.
    /// 프로그램 출력 용도로만 사용하므로 포맷은 고정폭 정렬 등 복잡한 처리를 하지 않습니다.
    /// </summary>
    public override string ToString()
    {
        // 예: "Armor | Chainmail"
        return Type + " | " + Name;
    }
}

/// <summary>
/// 방어구(Armor) 클래스: Item을 상속하고 방어력(Defense) 속성을 추가합니다.
/// sealed로 더 이상 상속되지 않도록 고정합니다.
/// </summary>
public sealed class Armor : Item
{
    /// <summary>방어력 값(읽기 전용)</summary>
    public int Defense { get; }

    /// <summary>
    /// 생성자: 이름과 선택적 방어력 값을 받습니다.
    /// 기본 방어력은 0입니다.
    /// </summary>
    public Armor(string name, int defense = 0) : base(name) { Defense = defense; }

    /// <summary>ItemType을 Armor로 고정합니다.</summary>
    public override ItemType Type => ItemType.Armor;
}

/// <summary>
/// 무기(Weapon) 클래스: Item을 상속하고 공격력(Attack) 속성을 추가합니다.
/// </summary>
public sealed class Weapon : Item
{
    /// <summary>공격력 값(읽기 전용)</summary>
    public int Attack { get; }

    /// <summary>생성자: 이름과 선택적 공격력(기본 0)</summary>
    public Weapon(string name, int attack = 0) : base(name) { Attack = attack; }

    /// <summary>ItemType을 Weapon으로 고정합니다.</summary>
    public override ItemType Type => ItemType.Weapon;
}

/// <summary>
/// 포션(Potion) 클래스: Item을 상속하고 회복량(Heal) 속성을 추가합니다.
/// </summary>
public sealed class Potion : Item
{
    /// <summary>회복량(읽기 전용)</summary>
    public int Heal { get; }

    /// <summary>생성자: 이름과 선택적 회복량(기본 0)</summary>
    public Potion(string name, int heal = 0) : base(name) { Heal = heal; }

    /// <summary>ItemType을 Potion으로 고정합니다.</summary>
    public override ItemType Type => ItemType.Potion;
}