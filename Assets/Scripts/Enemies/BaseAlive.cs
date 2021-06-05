using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class BaseAlive : MonoBehaviour, IAlive
{
    [SerializeField] protected EntityData EntityData;
    [SerializeField] protected Transform Healthbar;


    public EntityData Data => EntityData;
    public Transform Transform => transform;
    protected virtual void Start()
    {
        EntityData.CurrentLife = EntityData.MaxLife;
        SetHealth(EntityData.MaxLife);
    }

    protected void SetHealth(int health)
    {
        EntityData.CurrentLife = health;
        if (Healthbar != null)
        {
            var scale = Healthbar.transform.localScale;
            scale.x = (float)EntityData.CurrentLife / (float)EntityData.MaxLife;
            Healthbar.transform.localScale = scale;
        }
    }
    public void TakeDamage(IAlive owner, int damage)
    {
        SetHealth(EntityData.CurrentLife - damage);
    }

}


