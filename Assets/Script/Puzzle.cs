using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Puzzle : MonoBehaviour
{
    // public RectTransform rectTransform;
    public PuzzleData data = new PuzzleData();
    private RaycastHit _hitLayerMask;
    private RaycastHit _hit;
    private Vector3 _zeroPos;
    private readonly Queue<Grid> _selectedGrids = new Queue<Grid>();
    public Rotate rotate = Rotate._0;
    public Type type;
    public Sprite s1;
    public Sprite s2;
    public SpriteRenderer renderer;
    private void Start()
    {
        _zeroPos = transform.position;
    }

    private void Update()
    {
        if (isDrag)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(vector3.x, vector3.y, transform.position.z);
            // if (Input.GetMouseButtonDown(1))
            // {
            //     OnRightClick();
            // }
            int layerMask2 = 1 << LayerMask.NameToLayer("Grid");
            if (Physics.Raycast(ray, out _hit, Mathf.Infinity, layerMask2))
            {
                transform.position = _hit.collider.transform.position;
                _grid = _hit.collider.transform.GetComponent<Grid>();
            }
            else
            {
                _grid = null;
            }
        }
    }

    void OnRightClick()
    {
        Debug.Log("Pressed right Click");
        transform.Rotate(new Vector3(0, 0, 90));
        switch (rotate)
        {
            case Rotate._0:
                rotate = Rotate._90;
                break;
            case Rotate._90:
                rotate = Rotate._180;
                break;
            case Rotate._180:
                rotate = Rotate._270;
                break;
            case Rotate._270:
                rotate = Rotate._0;
                break;
        }
    }
    private Grid _grid;
    
    public void OnMouseDrag()
    {
        if (Camera.main != null)
        {
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Debug.DrawLine(ray.origin, ray.direction * 1000, Color.green);

            // int layerMask = 1 << LayerMask.NameToLayer("Puzzle");
            // if (Physics.Raycast(ray, out _hitLayerMask, Mathf.Infinity, layerMask))
            // {
            //     var transform1 = transform;
            //     float z = transform1.position.z;
            //     transform1.position = new Vector3(_hitLayerMask.point.x, _hitLayerMask.point.y, z);
            //
            //     if (Input.GetMouseButtonDown(1))
            //     {
            //         OnRightClick();
            //     }
            // }

            // int layerMask2 = 1 << LayerMask.NameToLayer("Grid");
            // if (Physics.Raycast(ray, out _hit, Mathf.Infinity, layerMask2))
            // {
            //     transform.position = _hit.collider.transform.position;
            //     _grid = _hit.collider.transform.GetComponent<Grid>();
            // }
            // else
            // {
            //     _grid = null;
            // }
        }
    }
    public bool Evaluate(Grid grid)
    {
        if (grid != null)
        {
            int ax = grid.x - data.centerX;
            int ay = grid.y - data.centerY;
            for (int x = 0; x < data.sizeX; x++)
            {
                for (int y = 0; y < data.sizeY; y++)
                {
                    if ((GetArray()[x, y] == "O" || GetArray()[x, y] == "C"))
                    {
                        if (x + ax >= Board.Instance.x || x + ax < 0)
                            return false;
                        if (y + ay >= Board.Instance.y || y + ay < 0)
                            return false;
                        if (Board.Instance.Grids[x + ax, y + ay].status == "V" || Board.Instance.Grids[x + ax, y + ay].status == "X")
                            return false;
                    }
                }
            }
        }
        return true;
    }

    private bool isDrag = false;
    public void OnMouseDown()
    {
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << LayerMask.NameToLayer("Puzzle");
            if (Physics.Raycast(ray, out _hitLayerMask, Mathf.Infinity, layerMask))
            {
                switch (type)
                {
                    case Type.dol :
                        GameManager.instance._soundManager.Play($"VFX/Pigeon_{Random.Range(1,9)}");
                        break;
                    case Type.ddong :
                        GameManager.instance._soundManager.Play($"VFX/Dog_{Random.Range(1,8)}");
                        break;
                    case Type.bak :
                        GameManager.instance._soundManager.Play($"VFX/Bat_{Random.Range(1,4)}");
                        break;
                    case Type.ju :
                        GameManager.instance._soundManager.Play($"VFX/Fox_{Random.Range(1,7)}");
                        break;
                    case Type.se :
                        GameManager.instance._soundManager.Play($"VFX/Cat_{Random.Range(1,4)}");
                        break;
                    case Type.go :
                        GameManager.instance._soundManager.Play($"VFX/Gorani_{Random.Range(1,4)}");
                        break;
                }

                renderer.sprite = s2;
                isDrag = true;
                while (_selectedGrids.Count > 0)
                {
                    _selectedGrids.Dequeue().status = "O";
                }
            }
        }
    }
    public void OnMouseUp()
    {
        isDrag = false;
        renderer.sprite = s1;
        if (_grid != null)
        {
            if (Evaluate(_grid))
            {
                int ax = _grid.x - data.centerX;
                int ay = _grid.y - data.centerY;
                for (int x = 0; x < data.sizeX; x++)
                {
                    for (int y = 0; y < data.sizeY; y++)
                    {
                        if (GetArray()[x, y] == "O" || GetArray()[x, y] == "C")
                        {
                            // x + ax
                            Debug.Log(x + ax);
                            Debug.Log(y + ay);
                            Board.Instance.Grids[x + ax, y + ay].status = "V";
                            _selectedGrids.Enqueue(Board.Instance.Grids[x + ax, y + ay]);
                        }
                    }
                }
                Board.Instance.IsClear();
            }
            else
            {
                transform.position = _zeroPos;
            }
        }
        else
        {
            OnRightClick();
        }
    }

    

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (rotate == Rotate._0 || rotate == Rotate._180)
            Gizmos.DrawWireCube(transform.position, new Vector3(data.sizeX, data.sizeY));
        if (rotate == Rotate._90 || rotate == Rotate._270)
            Gizmos.DrawWireCube(transform.position, new Vector3(data.sizeY, data.sizeX));
    }
    public enum Rotate
    {
        _0,
        _90,
        _180,
        _270
    }
    string[,] GetArray()
    {
        switch (rotate)
        {
            case Rotate._0:
                return data.GridArray;
            case Rotate._90:
                return data.GridArray90;
            case Rotate._180:
                return data.GridArray180;
            case Rotate._270:
                return data.GridArray270;
            default:
                return data.GridArray;
        }
    }
    
    public enum Type
    {
        dol,
        ddong,
        bak,
        ju,
        se,
        go
    }
}