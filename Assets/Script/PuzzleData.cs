using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

[System.Serializable]
public class PuzzleData
{
    public int id;
    public string size;
    public List<string> grid;
    public string[,] gridArray;
    public string[,] gridArray90;
    public string[,] gridArray180;
    public string[,] gridArray270;
    public int sizeX;
    public int sizeY;
    public int centerX;
    public int centerY;
}
