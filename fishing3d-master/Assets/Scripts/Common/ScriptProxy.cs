using UnityEngine;
using System.Collections;
using LuaInterface;
using LuaFramework;
public class ScriptProxy : MonoBehaviour {
	public string className;
	private LuaTable table;
	private LuaFunction update;

    public LuaTable Table
    {
        set { 
            table = value;
            update = table.GetLuaFunction("onUpdate");
        }
        get { return table; }
    }
	// Use this for initialization
    //it will be called in actorcontroller
    public void InitScript()
    {
        LuaManager luaMgr = AppFacade.Instance.GetManager<LuaManager>(ManagerName.Lua);
        LuaState state = luaMgr.GetState();
        LuaTable classT = state.GetTable(className);
        LuaFunction newFunc = classT.GetLuaFunction("new");
        object[] ret = newFunc.Call(gameObject);
        LuaTable instance = ret[0] as LuaTable;
        if(instance != null)
        {
            table = instance;
            LuaFunction onAwake = table.GetLuaFunction("OnAwake");
            if(onAwake != null)
            {
                onAwake.Call(gameObject);
                onAwake.Dispose();
            }
        }
        update = table.GetLuaFunction("onUpdate");
    }
	void Start () 
	{
		//CallLuaFunction("OnStart");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(update != null)
		{
			update.Call(table,Time.deltaTime);
		}
	}

	void OnDestroy()
	{
		if(update != null)
			update.Dispose();
	}

	public void CallLuaFunction(string name)
	{
		LuaFunction func = table.GetLuaFunction(name);
		if(func != null)
		{
			LuaAssist.CallLuaFunction(func,table);
			func.Dispose();
		}

	}

	public object[] CallLuaFunction(string name,params object[] args)
	{
		LuaFunction func = table.GetLuaFunction(name);
		object[] ret = null;
		if(func != null)
		{
			ret = func.Call(args);
			func.Dispose();
		}
		return ret;
	} 
		 
}
