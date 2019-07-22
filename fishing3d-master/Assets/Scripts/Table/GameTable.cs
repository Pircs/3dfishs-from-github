using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Record
{ 
}

public abstract class GameTable
{
    public string name;
    public List<Record> recordsList = new List<Record>();
    public abstract void AddValue(int index, int type, string values);
    public abstract void Destroy();
    public static GameTable Clone(Type type)
    {
        GameTable tab = type.Assembly.CreateInstance(type.Name) as GameTable;
        return tab;
    }
}
