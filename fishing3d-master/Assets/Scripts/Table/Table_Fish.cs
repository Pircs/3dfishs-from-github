using UnityEngine;
using System.Collections;
using System;

public class Record_Fish : Record
{
    public int id;
    public string name;
    public string prefabName;
    public int[] pathids;
    public int probablity;
    public string moveAnimationName;
    public string deadAnimationName;
}

public class Table_Fish : GameTable
{
    private enum FishRecordField
    {
        Id = 0,
        Name,
        PrefabName,
        PathIds,
        Probability,
        MoveAnimationName,
        DeadAnimationName
    }

    public override void Destroy()
    {
        recordsList.Clear();
        recordsList = null;
    }

    public override void AddValue(int index, int column, string values)
    {
        Record_Fish record = null;
        if (recordsList.Count <= index)
        {
            record = new Record_Fish();
            recordsList.Add(record);
        }
        else
            record = recordsList[index] as Record_Fish;

        switch ((FishRecordField)column)
        {
            case FishRecordField.Id:
                record.id = int.Parse(values);
                break;
            case FishRecordField.Name:
                record.name = values;
                break;
            case FishRecordField.PrefabName:
                record.prefabName = values;
                break;
            case FishRecordField.PathIds:
                string[] idstr = values.Split(',');
                record.pathids = new int[idstr.Length];
                for (int i = 0; i < idstr.Length; i ++ )
                {
                    record.pathids[i] = int.Parse(idstr[i]);
                }
                break;
            case FishRecordField.Probability:
                record.probablity = int.Parse(values);
                break;
            case FishRecordField.MoveAnimationName:
                record.moveAnimationName = values;
                break;
            case FishRecordField.DeadAnimationName:
                record.deadAnimationName = values;
                break;
        }
    }

    public Record GetRecord(int id)
    {
        foreach (Record record in recordsList)
        {
            if ((record as Record_Fish).id == id)
                return record;
        }
        return null;
    }
}
