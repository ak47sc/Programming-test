using UnityEngine;
using UnityEditor;

//Custom Editor tool for creating obstacles
public class CreateObstacle : EditorWindow 
{
    bool[][] obstacles = new bool[10][];
    Vector2 scrollPos;
    
    [MenuItem("Tools/Obstacle spawner")]
    public static void ShowWindow()
    {
        GetWindow(typeof(CreateObstacle));
    }

    //initializing obstacles value with default values and if SO present then populating it with the present data
    private void Awake()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i] = new bool[10];
            for (int j = 0; j < obstacles[i].Length; j++)
            {
                obstacles[i][j] = false;
            }
        }
        ObstacleData obstacleData = AssetDatabase.LoadAssetAtPath<ObstacleData>("Assets/ScriptableObject/ObstacleData.asset");
        if(obstacleData != null )
        {
            int idx = 0;
            for(int i = 0;i<obstacles.Length;i++)
            {
                for (int j = 0; j < obstacles[i].Length; j++)
                {
                    obstacles[j][i] = obstacleData.obstacles[idx++];
                }
            }
        }
        
    }

    //GUI function for editor UI
    public void OnGUI()
    {
        GUILayout.Label("Create Obstacles", EditorStyles.boldLabel);
        scrollPos =  EditorGUILayout.BeginScrollView(scrollPos,GUILayout.Width(500),GUILayout.Height(250));
        EditorGUILayout.BeginHorizontal();
        for(int i = 0; i < obstacles.Length; i++)
        {
            EditorGUILayout.BeginVertical();
            for(int j = obstacles[i].Length - 1; j >=0 ; j--)
            {
                obstacles[i][j] = EditorGUILayout.Toggle(obstacles[i][j]);
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();

        if(GUILayout.Button("Create Obstacles"))
        {
            SaveObstacleData(obstacles);
        }
    }

    //Function to save new obstacle data in the SO
    private void SaveObstacleData(bool[][] obstacles)
    {
        ObstacleData saveObj = CreateInstance<ObstacleData>();
        AssetDatabase.CreateAsset(saveObj, "Assets/ScriptableObject/ObstacleData.asset");
        saveObj.Configure(obstacles);
        EditorUtility.SetDirty(saveObj);
        AssetDatabase.SaveAssets();
    }
}
