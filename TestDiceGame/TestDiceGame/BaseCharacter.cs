using System;
using System.Threading;

public class Stats
{
    public int Health;

    public int MaxHealth;

    public int AttackPower;

    public int AttackSpeed;

    public Stats() { }

    public Stats(int health, int attackPower, int attackSpeed)
    {
        Health = health;
        MaxHealth = health;
        AttackPower = attackPower;
        AttackSpeed = attackSpeed;
    }
}

// 공격 이벤트 인자
public class AttackEventArgs : EventArgs
{
    // 공격자
    public int AttackerID;

    // 공격 대상
    public int TargetID;

    // 입힌 데미지
    public int Damage;

    public AttackEventArgs(int attackerID, int targetID, int damage)
    {
        AttackerID = attackerID;
        TargetID = targetID;
        Damage = damage;
    }
}

// 체력 변경 이벤트 인자
public class HealthChangedEventArgs : EventArgs
{
    // 캐릭터 ID
    public int CharacterID;

    // 변경된 체력
    public int Health;

    public HealthChangedEventArgs(int characterID, int newHealth)
    {
        CharacterID = characterID;
        Health = newHealth;
    }
}

// 공격 받을 수 있는 인터페이스
public interface IAttackable
{
    void TakeDamage(int damage);
}

public class BaseCharacter : IAttackable
{
    public int ID;

    public string Name;

    public Stats CharacterStats;

    public int TargetID;

    private event Action<AttackEventArgs> OnAttackEvent;

    private event Action<HealthChangedEventArgs> OnHealthChangedEvent;

    // 자동 공격 타이머
    private Timer autoAttackTimer;

    public BaseSkill Skill;

    // 스킬 사용 관련 변수
    protected int attackCountSinceLastSkill = 0;

    // 마지막 스킬 사용 시간
    protected DateTime lastSkillTime = DateTime.MinValue;

    // 스킬 보유 여부
    public bool IsHaveSkill => Skill != null;


    // 생성자
    public BaseCharacter()
    {
        ID = 0;
        Name = "DefaultName";
        CharacterStats = new Stats();
        TargetID = -1;
    }

    // 생성자 오버로드
    public BaseCharacter(int id, string name, Stats stats)
    {
        ID = id;
        Name = name;
        CharacterStats = stats;
        TargetID = -1;
    }

    // 생성자 오버로드
    public BaseCharacter(int id, string name, int health, int attackPower, int attackSpeed)
    {
        ID = id;
        Name = name;
        CharacterStats = new Stats(health, attackPower, attackSpeed);
        TargetID = -1;
    }

    // 소멸자
    ~BaseCharacter()
    {
        // 타이머 해제
        if ( autoAttackTimer != null )
        {
            autoAttackTimer.Dispose();
        }        
    }

    public void SetLevelData(int level)
    {
        CharacterStats.Health += level * 10; // 레벨당 체력 10 증가
        CharacterStats.AttackPower += level * 5; // 레벨당 공격력 5 증가
        CharacterStats.AttackSpeed += level * 300; // 레벨당 공격 속도 300 증가 (더 느리게 공격)

        string logMessage = $"{Name} has been leveled up to level {level}! New Health: {CharacterStats.Health}, " +
            $"New Attack Power: {CharacterStats.AttackPower}, New Attack Speed: {CharacterStats.AttackSpeed} ms";

        SaveLog.WriteLog(logMessage);
    }

    // 공격 목표 설정
    public void SetTargetID(int targetID)
    {
        TargetID = targetID;
    }

    // 공격 받았을 때
    public void TakeDamage(int damage)
    {
        CharacterStats.Health -= damage;
        if ( CharacterStats.Health < 0 ) CharacterStats.Health = 0;

        string logMessage = $"{Name} took {damage} damage! Remaining Health: {CharacterStats.Health}";
        SaveLog.WriteLog(logMessage, ConsoleColor.Cyan);        

        // 체력 변경 이벤트 발생
        OnHealthChangedEvent?.Invoke(new HealthChangedEventArgs(ID, CharacterStats.Health));
    }

    public void StartAutoAttackTimer()
    {
        // 이미 타이머가 존재하면 리턴
        if (autoAttackTimer != null) return;

        // 자동 공격 타이머 시작
        autoAttackTimer = new Timer(t =>
        {
            // 죽었으면 자동 공격 중지
            if (IsAlive() == false)
            {
                if (autoAttackTimer != null)
                {
                    autoAttackTimer.Dispose();
                }
                return;
            }

            OnAutoAttckTick();

        }, null, (int)CharacterStats.AttackSpeed, (int)CharacterStats.AttackSpeed);
    }

    public bool IsAlive()
    {
        return CharacterStats.Health > 0;
    }

    // 공격력 계산
    // 공격력은 기본 공격력의 70% ~ 130% 사이의 랜덤 값으로 결정
    public int GetAttackPower()
    {
        double min = CharacterStats.AttackPower * 0.7;
        double max = CharacterStats.AttackPower * 1.3;

        // random 고유 시드 생성
        Random rand = new Random(Guid.NewGuid().GetHashCode());

        return Math.Max(1, rand.Next((int)min, (int)max +1));
    }

    // 공격 이벤트 핸들러 추가
    public void AddOnAttackEvent(Action<AttackEventArgs> handler)
    {
        if(handler == null ) return;
        OnAttackEvent += handler;
    }

    // 체력 변경 이벤트 핸들러 추가
    public void AddOnHealthChangedEvent(Action<HealthChangedEventArgs> handler)
    {
        if (handler == null) return;
        OnHealthChangedEvent += handler;
    }

    public void StopAutoAttackTimer()
    {
        if (autoAttackTimer != null)
        {
            autoAttackTimer.Dispose();
            autoAttackTimer = null;
        }
    }
    public void ClearEvents()
    {
        OnAttackEvent = null;
        OnHealthChangedEvent = null;
    }

    public bool IsSkillUable()
    {
        if(IsHaveSkill == false) return false;

        if(IsAlive() == false) return false;

        // 쿨타임 체크
        var noew = DateTime.Now;

        // 쿨타임이 지나지 않았으면 스킬 사용 불가
        if ( (noew - lastSkillTime).TotalSeconds < Skill.CooldownSeconds )
        {
            return false;
        }

        // 조건 체크
        switch(Skill.Condition)
        {
            case UseSkillCondition.Always:
                return true;
            case UseSkillCondition.HealthRatio:
                {
                    double healthRatio = ( (float)CharacterStats.Health / (float)CharacterStats.MaxHealth ) * 100;
                    if ( healthRatio <= Skill.ConditionValue )
                    {
                        return true;
                    }
                }
                break;
            case UseSkillCondition.AttackCount:
                {
                    if( attackCountSinceLastSkill >= Skill.ConditionValue )
                    {
                        return true;
                    }
                }
                break;
        }

        return false;
    }

    public virtual void UseSkill()
    {
        // 스킬 사용 시간 기록
        lastSkillTime = DateTime.Now;

        // 공격 카운트 초기화
        attackCountSinceLastSkill = 0;


    }

    public void HealPercentage(float percentage)
    {
        int healAmount = (int)( CharacterStats.MaxHealth * (percentage / 100f) );

        CharacterStats.Health += healAmount;
        if ( CharacterStats.Health > CharacterStats.MaxHealth )
        {
            CharacterStats.Health = CharacterStats.MaxHealth;
        }

        string logMessage = Skill.UseSkillMessage();
        SaveLog.WriteLog(logMessage, ConsoleColor.Magenta);
        // 체력 변경 이벤트 발생
        OnHealthChangedEvent?.Invoke(new HealthChangedEventArgs(ID, CharacterStats.Health));
    }

    protected void RaiseAttack(int damage , bool isSkill = false)
    {
        if(IsAlive() == false) return;

        if(isSkill == true)
        {
            string logMessage = Skill.UseSkillMessage();
            SaveLog.WriteLog(logMessage, ConsoleColor.Magenta);
        }
        else
        {
            string logMessage = $"{Name} 이(가) {damage} 데미지를 입혔습니다!";
            SaveLog.WriteLog(logMessage, ConsoleColor.Red);
        }

        OnAttackEvent?.Invoke(new AttackEventArgs(ID, TargetID, damage));
    }

    protected virtual void OnAutoAttckTick()
    {
        if (IsAlive() == false) return;

        attackCountSinceLastSkill++;

        if (IsSkillUable() == true)
        {
            UseSkill();
        }
        else
        {
            int damage = GetAttackPower();
            RaiseAttack(damage);
        }
    }
}

public class PlayerCharacter : BaseCharacter
{
    public PlayerCharacter() : base()
    {
    }

    public PlayerCharacter(int id, string name, Stats stats) 
        : base(1000, "Player", new Stats(80, 10, 1000))
    {
        Skill = new BlowSkill();
    }

    public PlayerCharacter(int id, string name, int health, int attackPower, int attackSpeed)
        : base(1000, "Player", 80, 10, 1000)
    {
        Skill = new BlowSkill();
    }

    ~PlayerCharacter()
    {
    }
}

public class EnemyCharacter : BaseCharacter
{
    public EnemyCharacter() : base()
    {
    }

    public EnemyCharacter(int id, string name, Stats stats)
        : base(2000, "Enemy", new Stats(100, 7, 1000))
    {
        Skill = new HealSkill();
    }

    public EnemyCharacter(int id, string name, int health, int attackPower, int attackSpeed)
        : base(2000, "Enemy", 100, 7, 1000)
    {
        Skill = new HealSkill();
    }

    ~EnemyCharacter()
    {
    }
}

public class WarriorClass : BaseCharacter
{
    public WarriorClass() : base()
    {
    }
    public WarriorClass(int id, string name, Stats stats)
        : base(id, name, stats)
    {
        Skill = new BlowSkill();
    }
    public WarriorClass(int id, string name, int health, int attackPower, int attackSpeed)
        : base(id, name, health, attackPower, attackSpeed)
    {
        Skill = new BlowSkill();
    }
    ~WarriorClass()
    {
    }

    public override void UseSkill()
    {
        base.UseSkill();

        // 강타 스킬 사용
        int damage = (int)(GetAttackPower() * Skill.SkillMoltiplier);
        RaiseAttack(damage, true);
    }
}

public class  MageClass : BaseCharacter
{
    public MageClass() : base()
    {
    }

    public MageClass(int id, string name, Stats stats)
        : base(id, name, stats)
    {
        Skill = new FireballSkill();
    }

    public MageClass(int id, string name, int health, int attackPower, int attackSpeed)
        : base(id, name, health, attackPower, attackSpeed)
    {
        Skill = new FireballSkill();
    }

    ~MageClass()
    {
    }

    public override void UseSkill()
    {
        base.UseSkill();

        // 스킬 사용
        int damage = (int)(GetAttackPower() * Skill.SkillMoltiplier);
        RaiseAttack(damage, true);
    }
}

public class HealerClass : BaseCharacter
{
    public HealerClass() : base()
    {
    }

    public HealerClass(int id, string name, Stats stats)
        : base(id, name, stats)
    {
        Skill = new HealSkill();
    }

    public HealerClass(int id, string name, int health, int attackPower, int attackSpeed)
        : base(id, name, health, attackPower, attackSpeed)
    {
        Skill = new HealSkill();
    }

    ~HealerClass()
    {
    }

    public override void UseSkill()
    {
        base.UseSkill();

        HealPercentage(Skill.SkillMoltiplier);
    }
}