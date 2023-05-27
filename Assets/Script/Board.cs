using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private static Board instance = null;
    private ObjectPool _pool;
    public List<Grid> _spawned = new List<Grid>();
    public List<GameObject> _spawnedPuzzle = new List<GameObject>();
    
    private PuzzleGenerator Generator;
    public GameObject Grid;
    public List<GameObject> Pivots;
    public Grid[,] Grids;
    public int x;
    public GameObject Next;
    public int y;
    public Transform parent;
    public ParticleSystem particle;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static Board Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    
    void Start()
    {
        Generator = GetComponent<PuzzleGenerator>();
        _pool = GetComponent<ObjectPool>();
        _pool.Init(Grid,100);
    }
    public void LoadData(string filename)
    {
        Next.SetActive(false);
        _spawned.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            _pool.Despawn(transform.GetChild(i).gameObject);
        }

        foreach (GameObject go in _spawnedPuzzle)
        {
            parent.DetachChildren();
            Destroy(go);
        }
        TextAsset asset = Resources.Load<TextAsset>($"BoardData/{filename}");
        BoardData data = JsonUtility.FromJson<BoardData>(asset.text);
        foreach (int id in data.puzzles)
        {
            GameObject go = Generator.GenerateById(id);
            _spawnedPuzzle.Add(go);
            go.transform.position = Pivots[parent.childCount-1].transform.position;
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
                GameObject go = _pool.Spawn();
                go.GetComponent<Transform>().localPosition = new Vector3(i,j*-1,0);
                Grid grid = go.GetComponent<Grid>();
                _spawned.Add(grid);
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
    public void IsClear()
    {
        int a = 0;
        foreach (Grid grid in _spawned)
        {
            Debug.Log(grid.status);
            if (grid.status == "O")
            {
                a++;
            }
        }
        if (a == 0)
        {
            Clear();
            particle.Play();
        }

    }
    public void Clear()
    {
        Debug.Log($"Clear!! {GameManager.instance.currentLevel}");
        GameManager.instance.SaveClearData();
        Next.SetActive(true);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,new Vector3(x,y));
    }
}
