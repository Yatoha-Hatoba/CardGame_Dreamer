using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;

public class MainCardManager : MonoBehaviour, IcardManager,Iinitialize
{
    public static MainCardManager Instance;

    [SerializeField, Header("初期化優先順位_デフォルト - 0 -)")]
    private int _InitPriority = 0;
    public int InitPriority { get { return _InitPriority; } private set { _InitPriority = value; } }

    private Dictionary<Type, IcardManager> Managers = new Dictionary<Type, IcardManager>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public bool Initialize()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        Type[] managerTypes = assembly.GetTypes().
            Where(type => typeof(IcardManager).IsAssignableFrom(type) &&
            type.IsClass &&
            !type.IsAbstract)
            .ToArray();

        foreach (Type type in managerTypes)
        {
            var components = FindObjectsByType(type, FindObjectsSortMode.None);

            foreach (var component in components)
            {
                if (component is IcardManager manager)
                {
                    Managers.Add(type, manager);
                }
            }
        }

        Debug.Log("MainManager初期化終了！");
        return true;
    }

    public T GetManager<T>() where T : class, IcardManager
    {
        if (Managers.TryGetValue(typeof(T), out IcardManager manager))
        {
            return manager as T;
        }

        Debug.LogError($"渡されたManagerは設定されていません : {typeof(T).Name}");
        return null;
    }
}