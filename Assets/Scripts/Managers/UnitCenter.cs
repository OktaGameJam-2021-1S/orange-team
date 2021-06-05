using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCenter : Singleton<UnitCenter>
{
    public List<IAlive> UnitList = new List<IAlive>();

    public void Register(IAlive unit)
    {
        UnitList.Add(unit);
    }
    public void Unregister(IAlive unit)
    {
        UnitList.Remove(unit);
    }
}
