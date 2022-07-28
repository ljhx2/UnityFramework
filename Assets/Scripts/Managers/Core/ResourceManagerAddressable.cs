using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManagerAddressable : MonoBehaviour
{
    private Dictionary<string, AsyncOperationHandle> _loadedResourceHandle = new Dictionary<string, AsyncOperationHandle>();


    public AsyncOperationHandle<T> Load<T>(string key, Action<T> completed = null) where T : UnityEngine.Object
    {
        if (_loadedResourceHandle.ContainsKey(key))
        {
            AsyncOperationHandle<T> handle = _loadedResourceHandle[key].Convert<T>();
            completed?.Invoke(handle.Result);
            return handle;
        }
        else
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
            handle.Completed += (result) =>
            {
                if (result.Status == AsyncOperationStatus.Succeeded)
                {
                    _loadedResourceHandle.Add(key, handle);
                    completed?.Invoke(result.Result);
                }
                else
                {
                    Debug.LogError($"Load Resource key:{key} result:{result.Status.ToString()}");
                }
            };
            return handle;
        }
    }

    public AsyncOperationHandle<GameObject> Instantiate(string key, Transform parent = null, Action<GameObject> complete = null)
    {
        if (_loadedResourceHandle.ContainsKey(key))
        {
            AsyncOperationHandle<GameObject> handle = _loadedResourceHandle[key].Convert<GameObject>();
            GameObject loadObject = handle.Result;
            GameObject instance = UnityEngine.Object.Instantiate(loadObject, parent);
            complete?.Invoke(instance);
            return handle;
        }
        else
        {
            AsyncOperationHandle<GameObject> handle = Load<GameObject>(key, (loadObject) =>
            {
                GameObject instance = UnityEngine.Object.Instantiate(loadObject, parent);
                complete?.Invoke(instance);
            });
            return handle;
        }
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        UnityEngine.Object.Destroy(go);
    }

    public void Clear()
    {
        foreach (var kv in _loadedResourceHandle)
        {
            Addressables.Release(kv.Value);
        }
        _loadedResourceHandle.Clear();
    }
}
