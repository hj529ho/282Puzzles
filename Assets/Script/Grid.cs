using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Grid : MonoBehaviour
{
    public int x;
    public int y;
    public string status;

    private void Update()
    {
        if (status == "X")
        {
            GetComponent<Image>().color = Color.clear;
        }
    }
}
