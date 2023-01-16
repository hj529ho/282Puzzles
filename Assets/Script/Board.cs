using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    // Start is called before the first frame update

    public string[,] _board;
    public GameObject Grid;
    int x;
    int y;
    public Puzzle Puzzle;
    void Start()
    {
        _board = LoadData("Data");
        GetComponent<RectTransform>();
        Evaluate(Puzzle, 7, 9);
        
    }
    string[,] LoadData(string filename)
    {
        TextAsset asset = Resources.Load<TextAsset>($"BoardData/{filename}");
        BoardData data = JsonUtility.FromJson<BoardData>(asset.text);

        string[] size =  data.size.Split('x');

        x = int.Parse(size[0]);
        y = int.Parse(size[1]);
        
        string[,] board = new string[x, y];
        
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                board[i, j] = data.board[i+j*10];
                GameObject go = Instantiate(Grid,transform);
                go.GetComponent<RectTransform>().anchoredPosition = new Vector2(i*80,j*-80);
                Grid grid = go.GetComponent<Grid>();
                grid.x = i;
                grid.y = j;
                grid.status = data.board[i + j * 10];
            }
        }
        
        Debug.Log(board[6,6]);
        return board;
    }
    public void Evaluate(Puzzle puzzle, int x, int y)
    {
        //중간체크
        switch (puzzle.rotate)
        {
            case Puzzle.Rotate._0:
                break;
            case Puzzle.Rotate._90:
                break;
            case Puzzle.Rotate._180:
                break;
            case Puzzle.Rotate._270:
                break;
        }
    }
}
