using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GGDok.SaveManager;
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
    private SaveManager _saveManager = new SaveManager();

    private SaveDataPuzzle _saveDataPuzzle;
    public List<Sprite> Sprites;
    public SaveDataPuzzle SaveData
    {
        get
        {
            if (_saveDataPuzzle == null)
            {
                if (!_saveManager.CheckSaveData(typeof(SaveDataPuzzle)))
                {
                    _saveDataPuzzle = new SaveDataPuzzle() { ClearBoard = 0, GottenPuzzle = 0 };
                    _saveManager.Save(_saveDataPuzzle);
                }
                else
                {
                    Debug.Log("LoadData");
                    SaveDataPuzzle dataPuzzle = (SaveDataPuzzle)_saveManager.Load(typeof(SaveDataPuzzle));
                    _saveDataPuzzle = dataPuzzle;
                    Debug.Log(dataPuzzle.ClearBoard);
                }
            }
            return _saveDataPuzzle;
        }
    }

    public Sprite ClearIamge;
    public Sprite CurrentImage;
    public Sprite LockedImage;
    void Start()
    {
        PuzzleLocked();
    }

    public void PuzzleLocked()
    {
        for (int i = 0; i < SaveData.ClearBoard; i++)
        {
            selectButtons[i].GetComponent<Image>().sprite = ClearIamge;
            selectButtons[i].GetComponentInChildren<Text>().enabled = true;
        }
        for (int i = SaveData.ClearBoard; i < 80; i++)
        {
            selectButtons[i].GetComponent<Image>().sprite =LockedImage;
            selectButtons[i].GetComponentInChildren<Text>().enabled = false;
        }
        if (SaveData.ClearBoard < 80)
        {
            selectButtons[SaveData.ClearBoard].GetComponent<Image>().sprite = CurrentImage;
            selectButtons[SaveData.ClearBoard].GetComponentInChildren<Text>().enabled = true;
        }
    }

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _soundManager.Init(BGM,SFX);
            _soundManager.Play("BGM",Define.Sound.BGM);
            _saveManager.Init();
        
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
        Debug.Log(SaveData.ClearBoard);
        if (SaveData.ClearBoard + 1 < i)
        {
            Debug.Log("asdfasdf");
            return;
        }
        Board.Instance.LoadData($"Level{i}");
        currentLevel = i;
        ChapterText.text = $"레벨 {currentLevel}";
        UIParent.DOAnchorPos(new Vector2(0, 2160),1f);
    }
    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel < 81)
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

    public void GottenPuzzle(int id)
    {
        _saveDataPuzzle.GottenPuzzle = id;
        _saveManager.Save(_saveDataPuzzle);
    }

    public void SaveClearData()
    {
        if (SaveData.ClearBoard < currentLevel)
        {
            _saveDataPuzzle.ClearBoard = currentLevel;
            _saveManager.Save(new SaveDataPuzzle() { ClearBoard = currentLevel, GottenPuzzle = 0 });
        }

        PuzzleLocked();
    }
}
