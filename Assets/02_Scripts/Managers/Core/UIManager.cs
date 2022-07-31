using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 10;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public void MakeSubItemAsync<T>(Transform parent = null, string key = null, Action<T> completed = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(key))
            key = typeof(T).Name;

        key = $"Prefabs/UI/SubItem/{key}";
        Managers.ResourceA.InstantiateAsync(key, null, (go) =>
        {
            if (parent != null)
                go.transform.SetParent(parent);

            completed?.Invoke(go.GetOrAddComponent<T>());
        });
    }

    public void ShowSceneUIAsync<T>(string key = null, Action<T> completed = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(key))
            key = typeof(T).Name;

        key = $"Prefabs/UI/Scene/{key}";
        Managers.ResourceA.InstantiateAsync(key, null, (go) =>
        {
            T sceneUI = go.GetOrAddComponent<T>();
            _sceneUI = sceneUI;
            go.transform.SetParent(Root.transform);
            completed?.Invoke(sceneUI);
        });
    }

    public void ShowPopupUIAsync<T>(string key = null, Action<T> completed = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(key))
            key = typeof(T).Name;

        key = $"Prefabs/UI/Popup/{key}";
        Managers.ResourceA.InstantiateAsync(key, null, (go) =>
        {
            T popup = go.GetOrAddComponent<T>();
            _popupStack.Push(popup);
            go.transform.SetParent(Root.transform);
            completed?.Invoke(popup);
        });
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }
        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.ResourceA.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }
}
