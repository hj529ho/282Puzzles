using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public RectTransform UIParent;
    
    public GameObject Main;
    public GameObject SelectLevel;
    public GameObject GameUI;
    // public Board Boards;
    public int currentLevel;

    public List<Button> selectButtons;
    void Start()
    {
        // Main.SetActive(true);
        // SelectLevel.SetActive(false);

        // foreach (Button button in selectButtons)
        // {
        //     button.enabled = false;
        // }
    }
    
    

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
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
        if (currentLevel < 32)
        {
            LevelSelect(currentLevel);
        }

    }
    public void Reload()
    {
        LevelSelect(currentLevel);
    }

    public RectTransform ChapterUI;

    private int CurrentChapter = 0;
    public void NextChapter()
    {
        CurrentChapter++;
        ChapterUI.DOAnchorPos(new Vector2(CurrentChapter * -1920, ChapterUI.anchoredPosition.y), 1);
    }
    public void PrevChapter()
    {
        CurrentChapter--;
        ChapterUI.DOAnchorPos(new Vector2(CurrentChapter * -1920, ChapterUI.anchoredPosition.y), 1);
    }


    public RectTransform MainMenu;
    
    public void OnCollection()
    {
        MainMenu.DOAnchorPos(new Vector2(1920, MainMenu.anchoredPosition.y),1f);
    }

    public void OnMainBack()
    {
        MainMenu.DOAnchorPos(new Vector2(0, MainMenu.anchoredPosition.y),1f);
    }

    public void OnSetting()
    {
        MainMenu.DOAnchorPos(new Vector2(-1920, MainMenu.anchoredPosition.y),1f);
    }
    
}
