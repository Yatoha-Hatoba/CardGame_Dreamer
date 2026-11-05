using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;

public class Initializer : MonoBehaviour
{
    // 初期化するリスト
    private List<Iinitialize> TrueInitializeList = new List<Iinitialize>();

    // --- イベント関連 --- //
    public static event Action FinishInitialized;

    private void Start()
    {

        // アセンブリ(コンパイル後のコードをまとめたもの)からIinitializeを持つクラス型を探索する
        var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(Iinitialize).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in types)
        {
            // 探索したクラス型のコンポーネントを一括で受け取る
            var components = FindObjectsByType(type, FindObjectsSortMode.None);

            // 受け取ったコンポーネントの中にIinitializeがあった場合はリストに追加する
            foreach (var component in components)
            {
                if (component is Iinitialize Init)
                {
                    TrueInitializeList.Add(Init);
                }
            }
        }

        // 優先順位が高い初期化を前に持っていく
        TrueInitializeList.Sort((A, B) => B.InitPriority.CompareTo(A.InitPriority));

        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        foreach (Iinitialize I in TrueInitializeList)
        {
            yield return new WaitUntil(() => I.Initialize());
        }

        Debug.Log("初期化が全て終了しました");
        FinishInitialized?.Invoke();
    }
}

