using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Fish))]
public class FishPathEditor : Editor {

	void OnSceneGUI()
	{
		//Handles.DrawBezier(Vector3.zero,100*Vector3.one,new Vector3(0,0,0),new Vector3(0,90,0),Color.red,null,2);
	}

	public override void OnInspectorGUI()
	{
		Fish fish = (Fish)target;

        //if (fish.FishPathData == null)
        //{
        //    fish.FishPathData = ScriptableObject.CreateInstance<FishPath>();
        //    fish.FishPathData.isNewPath = true;
        //    EditorUtility.SetDirty(fish);
        //}

		EditorGUILayout.BeginHorizontal();
		fish.Speed = EditorGUILayout.FloatField("Speed",fish.Speed);
		EditorGUILayout.EndHorizontal();

        if (fish.FishPathData != null)
        {
            EditorGUILayout.BeginHorizontal();
            fish.FishPathData.renderPath = EditorGUILayout.Toggle("Render Path", fish.FishPathData.renderPath);
            EditorGUILayout.EndHorizontal();
            if (fish.FishPathData.renderPath)
            {
                GUILayout.Space(10);
                fish.FishPathData.lineColour = EditorGUILayout.ColorField("Line Colour", fish.FishPathData.lineColour);
                GUILayout.Space(5);
            }
        }


        GUILayout.Space(5);
        if (GUILayout.Button("Load"))
        {
            string filepath = EditorUtility.OpenFilePanel("Load", Application.dataPath + "/Resources/Pathes/", "bytes");
            if (filepath.Length > 0)
            {
                fish.FishPathData = PathConfigManager.GetInstance().Load(filepath);
                fish.FishPathData.FileName = Path.GetFileName(filepath);
                EditorUtility.SetDirty(fish);
            }
        }

        GUILayout.Space(5);
        if (GUILayout.Button("Save"))
        {
            string savepath = EditorUtility.SaveFilePanel("Save", Application.dataPath + "/Resources/Pathes/", fish.FishPathData.FileName, "bytes");
            if (savepath.Length > 0)
            {
                PathConfigManager.GetInstance().Save(savepath, fish.FishPathData);
                AssetDatabase.Refresh();
                fish.FishPathData.FileName = Path.GetFileName(savepath);
            }
        }

        if (fish.FishPathData != null)
        {
            int numberOfControlPoints = fish.FishPathData.numberOfControlPoints;
            if (numberOfControlPoints > 0)
            {

                GUILayout.Space(5);
                if (GUILayout.Button("Reset Path"))
                {
                    if (EditorUtility.DisplayDialog("Resetting path?", "Are you sure you want to delete all control points?", "Delete", "Cancel"))
                    {
                        fish.FishPathData.ResetPath();
                        fish.FishPathData = null;
                        EditorUtility.SetDirty(fish);
                        return;
                    }
                }

                GUILayout.Space(10);
                GUILayout.Box(EditorGUIUtility.whiteTexture, GUILayout.Height(2), GUILayout.Width(Screen.width - 20));
                GUILayout.Space(3);

                //			//scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
                for (int i = 0; i < numberOfControlPoints; i++)
                {
                    FishPathControlPoint point = fish.FishPathData.controlPoints[i];
                    point.highLight = GUILayout.Toggle(point.highLight, "HighLight");
                    point.color = EditorGUILayout.ColorField("Line Colour", point.color);

                    point.mTime = EditorGUILayout.FloatField("Time", point.mTime);
                    point.mSpeedScale = EditorGUILayout.FloatField("SpeedScale", point.mSpeedScale);
                    point.mRotationChange = EditorGUILayout.Vector2Field("RotationChange", point.mRotationChange);

                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Delete"))
                    {
                        fish.FishPathData.DeletePoint(i);
                        numberOfControlPoints = fish.FishPathData.numberOfControlPoints;
                        if (numberOfControlPoints == 0)
                        {
                            fish.FishPathData = null;
                        }
                        EditorUtility.SetDirty(fish);
                        return;
                    }

                    if (GUILayout.Button("Add New Point After"))
                    {
                        fish.FishPathData.AddPoint(i + 1);
                        EditorUtility.SetDirty(fish);
                    }

                    EditorGUILayout.EndHorizontal();

                    GUILayout.Space(7);
                    GUILayout.Box(EditorGUIUtility.whiteTexture, GUILayout.Height(2), GUILayout.Width(Screen.width - 25));
                    GUILayout.Space(7);
                }
                //			//EditorGUILayout.EndScrollView();
            }
        }
        else
        {
            GUILayout.Space(5);
            if (GUILayout.Button("Add New Path"))
            {
                //Undo.RegisterSceneUndo("Create a new Camera Path point");
                fish.FishPathData = ScriptableObject.CreateInstance<FishPath>();
                fish.FishPathData.AddPoint();
                EditorUtility.SetDirty(fish);
                fish.FishPathData.isNewPath = true;
            }
        }
		
		if(GUI.changed)
		{
            if (fish.FishPathData)
                fish.FishPathData.CaculateFinePoints();
            //EditorUtility.SetDirty(fish);
            //EditorApplication.SaveScene();
            EditorApplication.MarkSceneDirty();
		}
	}
}
