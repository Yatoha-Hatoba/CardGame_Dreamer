using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fade
{
    public static event Action TextFadeEnd;
    
    public static IEnumerator TextColor_Fade(float StartFadeAlpha, float EndFadeAlpha, float StartFadeDuration,
        float StayTime,float EndFadeDuration, TextMeshProUGUI FadeText,bool TextEnabled)
    {

        Color StartColor = new Color(FadeText.color.r, FadeText.color.g, FadeText.color.b, StartFadeAlpha);
        Color EndColor = new Color(FadeText.color.r, FadeText.color.g, FadeText.color.b,EndFadeAlpha);

        FadeText.color = StartColor;

        float value = 0.0f;

        while (value <= 1.0f)
        {
            value += Time.deltaTime / StartFadeDuration;

            Debug.Log(value);
            FadeText.color = Color.Lerp(StartColor, EndColor, value);

            yield return null;
        }

        FadeText.color = EndColor;

        yield return new WaitForSeconds(StayTime);
        
        value = 0.0f;

        while (value <= 1.0f)
        {
            value += Time.deltaTime / EndFadeDuration;

            Debug.Log("After : " + value);
            FadeText.color = Color.Lerp(EndColor, StartColor, value);

            yield return null;
        }
        
        FadeText.color = StartColor;

        FadeText.enabled = TextEnabled;

        TextFadeEnd?.Invoke();
    }
    
    public static IEnumerator ImageColor_Fade(float StartFadeAlpha,float EndFadeAlpha,float FadeDuration,Image FadeImage)
    {
        Color color = FadeImage.color;
        color.a = StartFadeAlpha;
        FadeImage.color = color;

        float value = 0.0f;

        while(value <= 1.0f)
        {
            value += Time.deltaTime / FadeDuration;

            float a = Mathf.Lerp(StartFadeAlpha, EndFadeAlpha, value);
            color = FadeImage.color;
            color.a = a;
            FadeImage.color = color;

            yield return null;
        }

        color = FadeImage.color;
        color.a = EndFadeAlpha;
        FadeImage.color = color;
    }
    
    // 子供オブジェクトのSetActiveを使用しない場合はChildAdaptionはfalseにする
    public static IEnumerator UIopen_Fade(RectTransform OpenUIRect,UI_STATE state,float Duration,bool ChildAdaption)
    {
        float value = 0.0f;

        RectTransform UIRect = OpenUIRect;
        float SizeHeight = UIRect.sizeDelta.y;
        float y = 0.0f;

        if (ChildAdaption)
        {
            foreach (Transform child in OpenUIRect.transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        switch (state)
        {
            case UI_STATE.OPEN:
                UIRect.sizeDelta = new Vector2(UIRect.sizeDelta.x, 0.0f);
                break;
            
            case UI_STATE.CLOSE:
                break;
        }
        
        while (value < 1.0f)
        {
            value += Time.deltaTime / Duration;

            switch (state)
            {
                case UI_STATE.OPEN:
                    y = Mathf.Lerp(0f,SizeHeight, value);
                    break;
                case UI_STATE.CLOSE:
                    y = Mathf.Lerp(SizeHeight, 0f, value);
                    break;
            }
            
            UIRect.sizeDelta = new Vector2(UIRect.sizeDelta.x, y);

            yield return null;
            
        }
        
        UIRect.sizeDelta = new Vector2(UIRect.sizeDelta.x, SizeHeight);
        switch (state)
        {
            case UI_STATE.OPEN:
                if (ChildAdaption)
                {
                    foreach (Transform child in OpenUIRect.transform)
                    {
                        child.gameObject.SetActive(true);
                    }
                }

                break;
        }
    }
}
