using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    
}

public interface IPlayer
{
    
}

public interface IAlive
{
    Transform Transform { get; }
    UnitData Data { get; }


    void TakeDamage(IAlive owner, int damage);
}

[Serializable]
public class UnitData
{
    public int MaxLife;
    public int CurrentLife;
    public float AttackDistance;
    public float AttackTime;
    public int AttackDamage;
}