using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class BaseUnit : MonoBehaviour, IAlive
{
    [SerializeField] protected UnitData EntityData;
    [SerializeField] protected Transform Healthbar;

    protected UnitCenter UnitCenter;

    public UnitData Data => EntityData;
    public Transform Transform => transform;


    protected virtual void Start()
    {
        UnitCenter = FindObjectOfType<UnitCenter>();
        EntityData.CurrentLife = EntityData.MaxLife;
        SetHealth(EntityData.MaxLife);
        UnitCenter.Register(this);
    }
    private void OnDestroy()
    {
        UnitCenter.Unregister(this);
    }
    private void OnDisable()
    {
        // in case we use object pooling
        UnitCenter.Unregister(this);
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


