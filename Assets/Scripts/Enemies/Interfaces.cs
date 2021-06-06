using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    
}

public interface IPlayer
{
    
}

public interface IEntity
{
    Transform Transform { get; }
    EntityData Data { get; }


    void TakeDamage(IEntity owner, int damage);
}

[Serializable]
public class EntityData
{
    public int MaxLife;
    public int CurrentLife;
    public float AttackDistance;
    public float AttackTime;
    public int AttackDamage;
    public float Speed;
}


