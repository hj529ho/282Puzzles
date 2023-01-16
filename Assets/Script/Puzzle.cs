using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle : MonoBehaviour
{
    // public RectTransform rectTransform;
    public PuzzleData Data = new PuzzleData();
    private RaycastHit hitLayerMask;
    private RaycastHit hit;
    private Board _board;
    private Vector3 zeroPos;

    private Queue<Grid> VGrids = new Queue<Grid>();

    private void Start()
    {
        _board = GameObject.Find("GameObject").GetComponent<Board>();
        zeroPos = transform.position;
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(ray.origin, ray.direction * 1000, Color.green);

        int layerMask = 1 << LayerMask.NameToLayer("Puzzle");
        if (Physics.Raycast(ray, out hitLayerMask, Mathf.Infinity, layerMask))
        {
            float z = this.transform.position.z;
            transform.position = new Vector3(hitLayerMask.point.x, hitLayerMask.point.y, z);

            if (Input.GetMouseButtonDown(1))
            {
                OnRightClick();
            }
        }

        int layerMask2 = 1 << LayerMask.NameToLayer("Grid");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask2))
        {
            transform.position = hit.collider.transform.position;
            _grid = hit.collider.transform.GetComponent<Grid>();
        }
        else
        {
            _grid = null;
        }
    }

    public bool Evaluate(Grid grid)
    {
        if (grid != null)
        {
            int ax = grid.x - Data.centerX;
            int ay = grid.y - Data.centerY;
            for (int x = 0; x < Data.sizeX; x++)
            {
                for (int y = 0; y < Data.sizeY; y++)
                {
                    if ((GetArray()[x, y] == "O" || GetArray()[x, y] == "C"))
                    {
                        if (x + ax >= _board.x || x + ax < 0)
                            return false;
                        if (y + ay >= _board.y || y + ay < 0)
                            return false;
                        if (_board.Grids[x + ax, y + ay].status == "V" || _board.Grids[x + ax, y + ay].status == "X")
                            return false;
                    }
                }
            }
        }
        return true;
    }

    public void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << LayerMask.NameToLayer("Puzzle");
        if (Physics.Raycast(ray, out hitLayerMask, Mathf.Infinity, layerMask))
        {
            while (VGrids.Count > 0)
            {
                VGrids.Dequeue().status = "O";
            }
        }
    }

    public void OnMouseUp()
    {
        if (_grid != null)
        {
            if (Evaluate(_grid))
            {
                int ax = _grid.x - Data.centerX;
                int ay = _grid.y - Data.centerY;
                for (int x = 0; x < Data.sizeX; x++)
                {
                    for (int y = 0; y < Data.sizeY; y++)
                    {
                        if (GetArray()[x, y] == "O" || GetArray()[x, y] == "C")
                        {
                            // x + ax
                            Debug.Log(x + ax);
                            Debug.Log(y + ay);
                            _board.Grids[x + ax, y + ay].status = "V";
                            VGrids.Enqueue(_board.Grids[x + ax, y + ay]);
                        }
                    }
                }
            }
            else
            {
                //제자리로
                transform.position = zeroPos;
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (rotate == Rotate._0 || rotate == Rotate._180)
            Gizmos.DrawWireCube(transform.position, new Vector3(Data.sizeX, Data.sizeY));
        if (rotate == Rotate._90 || rotate == Rotate._270)
            Gizmos.DrawWireCube(transform.position, new Vector3(Data.sizeY, Data.sizeX));
    }

    public Rotate rotate = Rotate._0;

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
                return Data.gridArray;
            case Rotate._90:
                return Data.gridArray90;
            case Rotate._180:
                return Data.gridArray180;
            case Rotate._270:
                return Data.gridArray270;
            default:
                return Data.gridArray;
        }
    }
}


