using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    // Start is called before the first frame update
    private PuzzleGenerator Generator;
    public GameObject Grid;
    public List<GameObject> Pivots;
    public Grid[,] Grids;
    public int x;
    public GameObject Next;
    public int y;
    public Transform parent;
    void Start()
    {
        Generator = GetComponent<PuzzleGenerator>();
    }
    public void LoadData(string filename)
    {
        Next.SetActive(false);
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
        TextAsset asset = Resources.Load<TextAsset>($"BoardData/{filename}");
        BoardData data = JsonUtility.FromJson<BoardData>(asset.text);
        int k = 0;
        foreach (int id in data.puzzles)
        {
            GameObject go = Generator.GenerateById(id);
            go.transform.position = Pivots[k].transform.position;
            k++;
        }
        string[] size =  data.size.Split('x');

        x = int.Parse(size[0]);
        y = int.Parse(size[1]);
        
        string[,] board = new string[x, y];
        Grids = new Grid[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                board[i, j] = data.board[i+j*x];
                GameObject go = Instantiate(Grid,transform);
                go.GetComponent<Transform>().localPosition = new Vector3(i,j*-1,0);
                Grid grid = go.GetComponent<Grid>();
                Grids[i, j] = grid;
                grid.x = i;
                grid.y = j;
                grid.status = data.board[i + j * x];
            }
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition -= new Vector3(((float)x/2)-0.5f,((float)-y/2)+0.5f,0);
        }
    }

    public void Clear()
    {
        Next.SetActive(true);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,new Vector3(x,y));
    }
}
