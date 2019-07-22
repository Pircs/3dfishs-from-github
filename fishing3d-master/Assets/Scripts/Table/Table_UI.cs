using UnityEngine;
using System.Collections;
using System;

public class Record_UI : Record
{
    public string name;
    public string prefabName;
    public string scriptName;
}

public class Table_UI : GameTable
{
    private enum UIRecordField
    {
        Id = 0,
        Name,
        PrefabName,
        ScriptName,
    }

    public override void Destroy()
    {
        recordsList.Clear();
        recordsList = null;
    }

    public override void AddValue(int index, int column, string values)
    {
        Record_UI record = null;
        if (recordsList.Count <= index)
        {
            record = new Record_UI();
            recordsList.Add(record);
        }
        else
            record = recordsList[index] as Record_UI;

        switch ((UIRecordField)column)
        {
            case UIRecordField.Id:
                break;
            case UIRecordField.Name:
                record.name = values;
                break;
            case UIRecordField.PrefabName:
                record.prefabName = values;
                break;
            case UIRecordField.ScriptName:
                record.scriptName = values;
                break;
        }
    }

    public Record GetRecord(string name)
    {
        foreach (Record record in recordsList)
        {
            if ((record as Record_UI).name == name)
                return record;
        }

        return null;
    }
}
