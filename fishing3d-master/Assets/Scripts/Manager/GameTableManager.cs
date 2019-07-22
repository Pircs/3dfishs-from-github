using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameTableManager
{
    private static GameTableManager mInstance = null;

    private Dictionary<string, GameTable> mTableMap = new Dictionary<string, GameTable>();

    private GameTableManager()
    {

    }

    public static GameTableManager GetInstance()
    {
        if (mInstance == null)
            mInstance = new GameTableManager();

        return mInstance;
    }

    public void Initialize()
    {
        LoadAllTable();
    }

    public GameTable GetTable(string name)
    {
        GameTable table = null;
        mTableMap.TryGetValue(name, out table);
        return table;
    }

    private void LoadAllTable()
    {
        mTableMap.Clear();
        LoadTable("table_ui", typeof(Table_UI));
        LoadTable("table_fish", typeof(Table_Fish));
    }

    private bool LoadTable(string tablename, Type type)
    {
        if (mTableMap.ContainsKey(tablename) == true)
        {
            Debug.Log("Load table is error!!! Had a file! Name=" + tablename);
            return false;
        }

        TextAsset o = ResourcesManager.GetInstance().LoadLocalAsset("Tables/" + tablename) as TextAsset;

        if (o == null)
        {
            Debug.Log("Load table is error!!!Read File is error! File=" + tablename);
            return false;
        }

        GameTable tab = GameTable.Clone(type);
        tab.name = o.name;

        ReadTable(o.text, ref tab);

        mTableMap.Add(tab.name, tab);
        return true;
    }

    public void ReadTable(string text, ref GameTable tab)
    {
        if (tab == null)
            return;
        string[] lines = text.Split('\n');

        int row = 0;
        for (int index = 0; index < lines.Length; index++)
        {
            //第一行为表头
            if (index == 0)
            {
                continue;
            }
            string str = lines[index];
            //空行或者注释行
            if (str.Length < 1 || str[0] == '#')
                continue;

            string[] array = str.Split('\t');

            if (array.Length == 0)
                continue;

            for (int ndx = 0; ndx < array.Length; ndx++)
            {
                string strField = array[ndx];
                tab.AddValue(row, ndx, strField.Trim());
            }
            row++;
        }
    }
}
