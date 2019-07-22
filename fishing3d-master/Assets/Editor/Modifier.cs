using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ModifyProperty))]
public class ArrayModifier : Editor
{
	public override void OnInspectorGUI()
	{
		ModifyProperty modifyproperty = (ModifyProperty)target;
		modifyproperty.modifyType = (ModifyProperty.ModifyType)EditorGUILayout.EnumPopup("ModifyType",modifyproperty.modifyType);
		if(modifyproperty.modifyType == ModifyProperty.ModifyType.Array)
		{
			modifyproperty.relativeOffset = EditorGUILayout.Vector3Field("Relative Offset", modifyproperty.relativeOffset);
			modifyproperty.count = EditorGUILayout.IntField("Count",modifyproperty.count);
		}
		else
		{
			modifyproperty.ringCenter = EditorGUILayout.Vector3Field("Ring Center", modifyproperty.ringCenter);
			modifyproperty.ringRadius = EditorGUILayout.FloatField("Radius",modifyproperty.ringRadius);
			modifyproperty.count = EditorGUILayout.IntField("Count",modifyproperty.count);
		}

		if (GUILayout.Button("Apply"))
		{
			modifyproperty.applyModify();
		}
	}
}
