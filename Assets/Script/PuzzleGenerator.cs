using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGenerator : MonoBehaviour
{
    public void GenerateById(int id)
    {
        TextAsset asset = Resources.Load<TextAsset>($"Puzzle/puzzle{id}");
        string json = asset.text;
        PuzzleData data = JsonUtility.FromJson<PuzzleData>(json);
        string[] xy;
        xy = data.size.Split('x');
        data.sizeX = int.Parse(xy[0]);
        data.sizeY = int.Parse(xy[1]);
        data.gridArray = new string[data.sizeX,data.sizeY];
        data.gridArray90 = new string[data.sizeY,data.sizeX];
        data.gridArray180 = new string[data.sizeX,data.sizeY];
        data.gridArray270 = new string[data.sizeY,data.sizeX];
        for (int i = 0; i < data.sizeX; i++)
        {
            for (int j = 0; j < data.sizeY; j++)
            {
                data.gridArray[i, j] = data.grid[i+j*data.sizeX];
                if (data.grid[i + j * data.sizeX] == "C")
                {
                    data.centerX = i;
                    data.centerY = j;
                }
            }
        }

        data.gridArray90 = Rotate(data.gridArray,data.sizeX,data.sizeY);
        data.gridArray180 = Rotate(data.gridArray90,data.sizeY,data.sizeX);
        data.gridArray270 = Rotate(data.gridArray180,data.sizeX,data.sizeY);
        
        GameObject go = Resources.Load<GameObject>($"Prefab/puzzle{id}");
        GameObject spawned = Instantiate(go);
        spawned.GetComponent<Puzzle>().Data = data;
    }

    string[,] Rotate(string[,] arr,int sizeX, int sizeY)
    {
        string[,] rer = new string[sizeX, sizeY];
        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                rer[i,j] = arr[sizeX - j -1,i];
            }
        }
        return rer;
    }
}
