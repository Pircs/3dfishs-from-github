using UnityEngine;
using System.Collections;
using LuaInterface;
using LuaFramework;
public class LuaAssist
{
	static public void CallLuaFunction(LuaFunction func)
	{
		func.BeginPCall();
		func.PCall();
		func.EndPCall();
	}

	static public void CallLuaFunction(LuaFunction func,object arg1)
	{
		func.BeginPCall();
		func.Push(arg1);
		func.PCall();
		func.EndPCall();
	}

	static public void CallLuaFunction(LuaFunction func,object arg1,object arg2)
	{
		func.BeginPCall();
		func.Push(arg1);
		func.Push(arg2);
		func.PCall();
		func.EndPCall();
	}

	static public void CallLuaFunction(LuaFunction func,object arg1,object arg2,object arg3)
	{
		func.BeginPCall();
		func.Push(arg1);
		func.Push(arg2);
		func.Push(arg3);
		func.PCall();
		func.EndPCall();
	}

	static public void CallLuaFunction(LuaFunction func,object arg1,object arg2,object arg3,object arg4)
	{
		func.BeginPCall();
		func.Push(arg1);
		func.Push(arg2);
		func.Push(arg3);
		func.Push(arg4);
		func.PCall();
		func.EndPCall();
	}
}
