using System.ComponentModel;

class Abstract
{
    public int HP;
    public int Defense;
    public int Damage;

}

class Hero: Abstract
{
    Armor armor;
    Weapon weapon;
}

class Enemy : Abstract
{
    List<string> Type = new List<string>();
    public Enemy(int hp, int defense, int damage, List<string> type)
    {
        HP = hp;
        Defense = defense;
        Damage = damage;
        Type = type;
    }
}

class Armor
{
    public int Durability;
    public decimal ArmorDefense;
    public Armor(int durability, decimal armorDefense)
    {
        Durability = durability;
        ArmorDefense = armorDefense;
    }
}

class Weapon 
{
    public int Durability;
    public int Damage;
    public Weapon(int durability, int damage)
    {
        Durability = durability;
        Damage = damage;
    }

}



