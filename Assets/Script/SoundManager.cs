using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager
{
    public float[] volume = new float[(int)Define.Sound.MaxCOUNT];
    public bool[] isMute = new bool[(int)Define.Sound.MaxCOUNT];
    AudioSource[] _audioSource = new AudioSource[(int)Define.Sound.MaxCOUNT];
    private Slider _BGM;
    private Slider _SFX;
    public void Init(Slider BGM,Slider SFX)
    {
        GameObject root = GameObject.Find("@Sound");
        if (root != null) return;
        root = new GameObject("@Sound");
        Object.DontDestroyOnLoad(root);
        string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
        for(int i = 0; i <soundNames.Length-1;i++)
        {
            GameObject go = new GameObject(soundNames[i]);
            _audioSource[i] = go.AddComponent<AudioSource>();
            go.transform.parent = root.transform;
        }
        _audioSource[(int)Define.Sound.BGM].loop = true;
        Debug.Log("SoundManager Init");
        _audioSource[0].dopplerLevel = 0;
        _audioSource[1].dopplerLevel = 0;
        _audioSource[0].reverbZoneMix = 0;
        _audioSource[1].reverbZoneMix = 0;
        volume[0] = 0.5f;
        volume[1] = 0.5f;
        
        BGM.value = volume[0];
        SFX.value = volume[1];
    }

    private string lastPath = "";
    public void Play(string path, Define.Sound type = Define.Sound.SFX, float pitch = 1.0f)
    {
        if (type == Define.Sound.BGM)
        {
            if (lastPath == path)
            {
                return;
            }
            lastPath = path;
        }
        if(path.Contains("Sounds/") == false)
        {
            path = $"Sounds/{path}";
        }
        var audioClip = ResourceUtil.Load<AudioClip>(path);
        if (audioClip == null)
        {
            Debug.Log($"AudioClip Load Fail : {path}");
            return;
        }
        var audioSourceIndex = (int) type;
       
        var audioSource = _audioSource[audioSourceIndex];
        if (isMute[audioSourceIndex])
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = volume[audioSourceIndex];
        }
        audioSource.pitch = pitch;
        if(type == Define.Sound.BGM)
        {
            if (audioSource.isPlaying)
            {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(audioSource.DOFade(0, 1f)).AppendCallback(() =>
                {
                    audioSource.Stop();
                    audioSource.clip = audioClip;
                    audioSource.Play();
                }).Append(audioSource.DOFade(volume[audioSourceIndex], 0.3f));
            }
            else
            {
                audioSource.volume = 0;
                audioSource.clip = audioClip;
                audioSource.Play();
                audioSource.DOFade(volume[audioSourceIndex], 0.3f);
            }
        }
        else
        {
            audioSource.PlayOneShot(audioClip); 
        }
        Debug.Log($"Play Sound : {path} Type : {type.ToString()}");
    }

    public void PlaySceneChangeSound(string path)
    {
        if(path.Contains("Sounds/") == false)
        {
            path = $"Sounds/{path}";
        }
        
        var audioClip = ResourceUtil.Load<AudioClip>(path);
        if (audioClip == null)
        {
            Debug.Log($"AudioClip Load Fail : {path}");
            return;
        }
        var audioSource = _audioSource[(int)Define.Sound.SFX];
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    
    // public void BGM_Fadeout(float duration = 1)
    // {
    //     GameManager.Start_Coroutine(BGM_FadeoutCoroutine(duration));
    // }
    
    public IEnumerator BGM_FadeoutCoroutine(float duration)
    {
        
        AudioSource audioSource = _audioSource[(int)Define.Sound.BGM];
        lastPath = "";
        audioSource.DOFade(0, duration).OnComplete(() =>
        {
            audioSource.Stop();
        });
        
        yield return null;
    }
    
    public void ChangeVolume(float volume, Define.Sound type=Define.Sound.SFX)
    {
        if (!isMute[(int)type])
        {
            _audioSource[(int)type].volume = volume;
        }
        this.volume[(int)type] = volume;
    }
    
    public void Mute(bool ismute, Define.Sound type=Define.Sound.SFX)
    {
        isMute[(int)type] = ismute;
        if(ismute)
            _audioSource[(int)type].volume = 0;
        else
            _audioSource[(int)type].volume = volume[(int)type];
    }

    public void Stop(Define.Sound type)
    {
        _audioSource[(int)type].Stop();
    }
}
