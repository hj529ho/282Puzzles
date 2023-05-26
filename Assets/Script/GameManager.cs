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

    public Animator animalAnimator;
    public SoundManager _soundManager = new SoundManager();
    public Slider BGM;
    public Slider SFX;


    public Text ChapterText;
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
            _soundManager.Init(BGM,SFX);
            _soundManager.Play("BGM",Define.Sound.BGM);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void GOMain()
    {
        // Main.SetActive(false);
        // SelectLevel.SetActive(true);
        animalAnimator.SetTrigger("Main");
        UIParent.DOAnchorPos(new Vector2(0, 0),1f);
    }

    public void GameStart()
    {
        // Main.SetActive(false);
        // SelectLevel.SetActive(true);
        animalAnimator.SetTrigger("Hide");
        UIParent.DOAnchorPos(new Vector2(0, 1080),1f);
    }
    public void LevelSelect(int i)
    {
        Board.Instance.LoadData($"Level{i}");
        currentLevel = i;
        ChapterText.text = $"레벨 {currentLevel}";
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
