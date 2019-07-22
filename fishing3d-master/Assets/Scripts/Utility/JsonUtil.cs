using UnityEngine;
using System.Collections;
using System.IO;

public class JsonUtil
{
    private JsonUtil() { }
    private static JsonUtil mInstance;
    public static JsonUtil GetInstance()
    {
        if (mInstance == null)
        {
            mInstance = new JsonUtil();
        }
        return mInstance;
    }

    public static void Save(string jsonStr,string filepath)
    {
        FileStream fs = new FileStream(filepath, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        sw.Write(jsonStr);
        sw.Flush();
        fs.Close();
    }
}
