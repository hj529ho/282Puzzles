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
        data.GridArray = new string[data.sizeX,data.sizeY];
        data.GridArray90 = new string[data.sizeY,data.sizeX];
        data.GridArray180 = new string[data.sizeX,data.sizeY];
        data.GridArray270 = new string[data.sizeY,data.sizeX];
        for (int i = 0; i < data.sizeX; i++)
        {
            for (int j = 0; j < data.sizeY; j++)
            {
                data.GridArray[i, j] = data.grid[i+j*data.sizeX];
                if (data.grid[i + j * data.sizeX] == "C")
                {
                    data.centerX = i;
                    data.centerY = j;
                }
            }
        }

        data.GridArray90 = Rotate(data.GridArray,data.sizeX,data.sizeY);
        data.GridArray180 = Rotate(data.GridArray90,data.sizeY,data.sizeX);
        data.GridArray270 = Rotate(data.GridArray180,data.sizeX,data.sizeY);
        
        GameObject go = Resources.Load<GameObject>($"Prefab/puzzle{id}");
        GameObject spawned = Instantiate(go);
        spawned.GetComponent<Puzzle>().data = data;
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
