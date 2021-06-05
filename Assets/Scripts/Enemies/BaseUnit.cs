using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class BaseUnit : MonoBehaviour, IEntity
{
    [SerializeField] protected EntityData EntityData;
    [SerializeField] protected Transform Healthbar;
    protected UnitCenter UnitCenter;
    public EntityData Data => EntityData;
    public Transform Transform => transform;

    protected virtual void Start()
    {
        UnitCenter = FindObjectOfType<UnitCenter>();
        if(UnitCenter)
            UnitCenter.Register(this);
        EntityData.CurrentLife = EntityData.MaxLife;
        SetHealth(EntityData.MaxLife);
        
    }
    private void OnDestroy()
    {
        if (UnitCenter)
            UnitCenter.Unregister(this);
    }
    private void OnDisable()
    {
        // in case we use object pooling
        if (UnitCenter)
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
    public void TakeDamage(IEntity owner, int damage)
    {
        SetHealth(EntityData.CurrentLife - damage);
    }

}


