using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public RectTransform UIParent;
    
    public GameObject Main;
    public GameObject SelectLevel;
    public GameObject GameUI;
    // public Board Boards;
    public int currentLevel;
    void Start()
    {
        // Main.SetActive(true);
        // SelectLevel.SetActive(false);
    }

    public void GameStart()
    {
        // Main.SetActive(false);
        // SelectLevel.SetActive(true);
        UIParent.DOAnchorPos(new Vector2(0, 1080),1f);
    }

    public void LevelSelect(int i)
    {
        Board.Instance.LoadData($"Level{i}");
        currentLevel = i;
        UIParent.DOAnchorPos(new Vector2(0, 2160),1f);
    }

    public void NextLevel()
    {
        currentLevel++;
        LevelSelect(currentLevel);
    }
    public void Reload()
    {
        LevelSelect(currentLevel);
    }
}
