using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HandsCardManager : MonoBehaviour,Iinitialize
{
    [SerializeField, Header("初期化優先順位_デフォルト - 0 -)")]
    private int _InitPriority = 0;
    public int InitPriority { get { return _InitPriority; } private set { _InitPriority = value; } }

    [SerializeField, Header("カードプレハブ")]
    private Image CardPrefab;

    // プレイヤー手札Transform
    private RectTransform PlayerHandsRect;
    // エネミー手札Transform
    private RectTransform EnemyHandsRect;

    // 現在のプレイヤー手札リスト
    private List<Image> PlayerHandsList = new();
    // 現在のエネミー手札リスト
    private List<Image> EnemyHandsList = new();

    // ■■■　デバッグインプット　■■■ //
    public void OnDebug_Drow(InputAction.CallbackContext context)
    {
        if(!context.started)
        {
            return;
        }

        PlayerDrow();

    }

    public bool Initialize()
    {
        PlayerHandsRect = GameObject.FindWithTag("Hands/Player").GetComponent<Image>().rectTransform;
        EnemyHandsRect = GameObject.FindWithTag("Hands/Enemy").GetComponent<Image>().rectTransform;

        Debug.Log(PlayerHandsRect);
        Debug.Log(EnemyHandsRect);

        return true;
    }

    public void PlayerDrow()
    {
        Image GenerateCard = Instantiate(CardPrefab, PlayerHandsRect);
        PlayerHandsList.Add(GenerateCard.GetComponent<Image>());
        HandsOrganize(CHARA_TYPE.PLAYER);
        Debug.Log("生成されました！");
    }

    private void HandsOrganize(CHARA_TYPE Ctype)
    {
        RectTransform OrganizeHand = null;
        int setNum = -999;
        switch(Ctype)
        {
            case CHARA_TYPE.PLAYER:
                OrganizeHand = PlayerHandsRect;
                setNum = PlayerHandsList.Count / 2;
                break;
            case CHARA_TYPE.ENEMY:
                OrganizeHand = EnemyHandsRect;
                setNum = EnemyHandsList.Count / 2;
                break;
        }


        foreach(RectTransform rect in OrganizeHand)
        {
            rect.anchoredPosition = new Vector3(-50f * setNum, 0, 1);
            setNum--;
        }
    }
}
