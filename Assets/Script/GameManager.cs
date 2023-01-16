using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Main;
    public GameObject SelectLevel;
    public GameObject GameUI;
    public Board Boards;
    public int currentLevel;
    void Start()
    {
        Main.SetActive(true);
        SelectLevel.SetActive(false);
    }

    public void GameStart()
    {
        Main.SetActive(false);
        SelectLevel.SetActive(true);
    }

    public void LevelSelect(int i)
    {
        Boards.LoadData($"Level{i}");
        currentLevel = i;
        SelectLevel.SetActive(false);
        GameUI.SetActive(true);
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
