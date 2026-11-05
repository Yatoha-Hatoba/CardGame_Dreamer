using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ParentLoadManager : MonoBehaviour
{
    public static ParentLoadManager Instance;

    private GameObject BlackFadeObject;
    private Image BlackFadeImage;

    private bool CardGameInitializeFinished = false;
    private void _CardGameInitializeFinished() { CardGameInitializeFinished = true; }

    // --- イベント関連 --- //
    public static event Action LoadFinished;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        BlackFadeObject = transform.Find("LoadCanvas/BlackFadeObject").gameObject;
        BlackFadeImage = BlackFadeObject.GetComponent<Image>();
        BlackFadeObject.SetActive(false);
    }

    public void LoadStart(string LoadSceneName)
    {
        BlackFadeObject.SetActive(true);
        StartCoroutine(LoadCoroutine(LoadSceneName));
    }

    private IEnumerator LoadCoroutine(string LoadSceneName)
    {
        yield return Fade.ImageColor_Fade(0.0f, 1.0f, 1.0f, BlackFadeImage);

        AsyncOperation operation = SceneManager.LoadSceneAsync(LoadSceneName);

        while (operation.progress < 0.9f)
        {
            Debug.Log("Load : 0%");
            yield return null;
        }

        Debug.Log("Load : 50%   / シーンデータ取得開始！");
        Initializer.FinishInitialized += _CardGameInitializeFinished;

        while(!CardGameInitializeFinished)
        {
            Debug.Log("Load : 50%");
            yield return null;
        }

        Debug.Log("Load : 100%");
        Initializer.FinishInitialized -= _CardGameInitializeFinished;

        yield return Fade.ImageColor_Fade(1.0f, 0.0f, 1.0f, BlackFadeImage);

        BlackFadeObject.SetActive(false);
        LoadFinished?.Invoke();
    }
}
