using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris : MonoBehaviour
{
    public List<GameObject> tetromino;
    private ObjectPool _pool;
    private Queue spawnQueue;
    void Start()
    {
        _pool = GetComponent<ObjectPool>();
    }

    public void SetSevenBag()
    {
        // tetromino.
    }

    public void Spawn()
    {
    }
}
