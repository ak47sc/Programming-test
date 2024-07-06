using UnityEngine;

// Scriptable Object holding obstacle Data
public class ObstacleData : ScriptableObject
{
    public bool[] obstacles = new bool[100];

    //Function to update obstacle info in SO
    public void Configure(bool[][] obstacle)
    {
        int idx = 0;
        for(int i = 0; i < obstacle.Length; i++)
        {
            for(int j = 0; j < obstacle[i].Length; j++)
            { 
                obstacles[idx++] = obstacle[j][i];
            }
        }
    }
}
