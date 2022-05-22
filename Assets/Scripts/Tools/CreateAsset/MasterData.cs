using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;
using System;
using MiManchi.MiBaseClass;

public partial class MasterData
{
	public static MasterData Instance = new MasterData();
	public enum DataTables
	{
		LocalizeCommonControllerData,
		LocalizeRolesData,
		LocalizeSkillsData,
	}

	public ref readonly T GetTableData<T>(ulong key)
	{
		var table = typeof(T).Name;
		var type = GetType().GetField(table, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (type == null)
        {
			Debug.Log($"<color=#FF0000> Absent, Please Check Script Exists,  Return Null </color>");
			//return null;
        }
		var typeValue = type.GetValue(this);
        if (typeValue == null)
        {
			Debug.Log($"<color=#FF0000>Parameter : {table}, Absent, Please Check MasterData Exists Parameter,  Return Null</color>");
			//return null;
		}
		var getValueMethod = typeValue.GetType().GetMethod("TryGetValue");
		var getContenMethod = typeValue.GetType().GetMethod("ContainsKey");

        if (!(bool)getContenMethod.Invoke(typeValue, new object[] { key }))
        {
			Debug.Log($"<color=#FF0000> Table : {table}  Absent Key: {key} </color>");
			//return null;
        }
		var obj = Activator.CreateInstance(Type.GetType(table.ToString()));
		var parameters = new object[] { key, obj };
		getValueMethod.Invoke(typeValue, parameters);
		obj = parameters[1];

		T[] t = new T[] { (T)obj };
		return ref t[0];
	}
	public static ref readonly int GetTableData<T>() where T : BaseData
	{

		int[] nums = { 1,2,3};

		return ref nums[1];
	}

}