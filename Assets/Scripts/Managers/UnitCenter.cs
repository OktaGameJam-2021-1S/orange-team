using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCenter : Singleton<UnitCenter>
{
    public List<IEntity> UnitList = new List<IEntity>();

    public void Register(IEntity unit)
    {
        UnitList.Add(unit);
    }
    public void Unregister(IEntity unit)
    {
        UnitList.Remove(unit);
    }
}
