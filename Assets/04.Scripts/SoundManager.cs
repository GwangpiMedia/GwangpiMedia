using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    private Dictionary<string, AudioClip> DicbgmClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> DicsfxClips = new Dictionary<string, AudioClip>();

    [System.Serializable]
    public struct NamedAudioClip
    {
        public string name;
        public AudioClip clip;
    }

    public NamedAudioClip[] bgmClipList;
    public NamedAudioClip[] sfxClipList;

    private Coroutine currentBGMCoroutiune;

    public override void Awake()
    {
        base.Awake();
        InitializeAudioClips();
    }
    void InitializeAudioClips()
    {
        foreach (var bgm in bgmClipList)
        {
            if (!DicbgmClips.ContainsKey(bgm.name))
            {
                DicbgmClips.Add(bgm.name, bgm.clip);
            }
        }
        foreach (var sfx in sfxClipList)
        {
            if (!DicsfxClips.ContainsKey(sfx.name))
            {
                DicsfxClips.Add(sfx.name, sfx.clip);
            }
        }
    }

    public void PlayBGM(string name, float fadeDuration = 1.0f)
    {
        if (DicbgmClips.ContainsKey(name))
        {
            // if(currentBGMCoroutiune != null)
            // {
            //     StopCoroutine(currentBGMCoroutiune);
            // }

            // currentBGMCoroutiune = StartCoroutine(FadeOutBGM(fadeDuration, () =>
            // {
            //     bgmSource.spatialBlend = 0f;
            //     bgmSource.clip = DicbgmClips[name];
            //     bgmSource.Play();
            //     currentBGMCoroutiune = StartCoroutine(FadeInBGM(fadeDuration));
            // }));

            bgmSource.clip = DicbgmClips[name];
            bgmSource.Play();
        }
    }
    public void PlaySFX(string name, Vector3 position) //위치 지정해야 할 때 쓰는 거 
    {
        if (DicsfxClips.ContainsKey(name))
        {
            AudioSource.PlayClipAtPoint(DicsfxClips[name], position);
        }
        else
        {
            Debug.Log("클립 못 찾음");
        }
    }
    public void PlaySFX(string name) //메뉴 같은 거 효과음
    {
        if (DicsfxClips.ContainsKey(name))
        {
            sfxSource.spatialBlend = 0f;
            sfxSource.PlayOneShot(DicsfxClips[name]);
        }
    }

    public void StopBGM()
    {
        bgmSource?.Stop();
    }
    public void StopSFX()
    {
        sfxSource?.Stop();
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = Mathf.Clamp(volume, 0, 1);
    }
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp(volume, 0, 1);
    }

    private IEnumerator FadeOutBGM(float duration, Action OnFadeComplete)
    {
        float startVolume = bgmSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        bgmSource.volume = 0;
        OnFadeComplete?.Invoke(); //페이드아웃이 완료되면 다음 작업 실행
    }

    private IEnumerator FadeInBGM(float duration)
    {
        float startVolume = 0f;
        bgmSource.volume = 0f;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(startVolume, 1f, t / duration);
            yield return null;
        }
        bgmSource.volume = 1f;
    }
}