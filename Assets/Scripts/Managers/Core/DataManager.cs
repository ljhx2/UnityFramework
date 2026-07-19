using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();

    private ResourceManagerAddressable _resourceManager;

    public void Init(ResourceManagerAddressable resourceManager)
    {
        _resourceManager = resourceManager;

        LoadJson<Data.StatData, int, Data.Stat>("StatData", (loader) =>
        {
            StatDict = loader.MakeDict();
        });
    }

    private void LoadJson<Loader, Key, Value>(string key, Action<Loader> completed = null) where Loader : ILoader<Key, Value>
    {
        key = $"Data/{key}";
        _resourceManager.LoadAsync<TextAsset>(key, (textAsset) =>
        {
            completed?.Invoke(JsonUtility.FromJson<Loader>(textAsset.text));
        });
        
    }
}
