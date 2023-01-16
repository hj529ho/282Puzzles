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
    public string[,] GridArray;
    public string[,] GridArray90;
    public string[,] GridArray180;
    public string[,] GridArray270;
    public int sizeX;
    public int sizeY;
    public int centerX;
    public int centerY;
}
