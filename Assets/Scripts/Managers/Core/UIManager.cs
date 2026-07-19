using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
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
*/

public class UIManager : MonoBehaviour
{
    private Dictionary<string, UI_Panel> _panelDict = new Dictionary<string, UI_Panel>();
    //private Dictionary<string, UI_Popup> _popupDict = new Dictionary<string, UI_Popup>();
    private Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();


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
    public Transform PanelRoot
    {
        get
        {
            Transform panel = Root.transform.Find("@Panel_Root");
            if (panel == null)
            {
                panel = new GameObject { name = "@Panel_Root" }.transform;
                panel.SetParent(Root.transform);
                panel.SetAsFirstSibling();
            }
                
            return panel;
        }
    }
    public Transform PopupRoot
    {
        get
        {
            Transform popup = Root.transform.Find("@Popup_Root");
            if (popup == null)
            {
                popup = new GameObject { name = "@Popup_Root" }.transform;
                popup.SetParent(Root.transform);
                popup.SetAsLastSibling();
            }
            return popup;
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

    public void ShowPanelUIAsync<T>(string key = null, float fadeDuration = 0f, Action<T> completed = null) where T : UI_Panel
    {
        if (string.IsNullOrEmpty(key))
            key = typeof(T).Name;

        key = $"Prefabs/UI/Panel/{key}";
        if (_panelDict.ContainsKey(key))
        {
            UI_Panel panel = _panelDict[key];
            //panel.transform.SetParent(PanelRoot);
            completed?.Invoke(panel as T);
            _panelDict[key].Show(fadeDuration, null);
        }
        else
        {
            StartCoroutine(Co_ShowPanel<T>(key, fadeDuration, completed));
        }
    }
    IEnumerator Co_ShowPanel<T>(string key, float fadeDuration = 0f, Action<T> completed = null) where T : UI_Panel
    {
        var handle = Managers.ResourceA.InstantiateAsync(key, PanelRoot);
        yield return handle;

        GameObject instance = handle.Result;
        T panelUI = instance.GetOrAddComponent<T>();
        _panelDict.Add(key, panelUI);
        
        while (panelUI.IsLoaded == false)
        {
            yield return null;
        }
        
        completed?.Invoke(panelUI);
        panelUI.Show(fadeDuration, null);
    }
    public void HidePanelUI(UI_Panel panel, float fadeDuration = 0f, Action<UI_Panel> completed = null)
    {
        panel.Hide(fadeDuration, completed);
    }
    public void HidePanelUI(string key, float fadeDuration = 0f, Action<UI_Panel> completed = null)
    {
        key = $"Prefabs/UI/Panel/{key}";
        if (_panelDict.ContainsKey(key))
        {
            HidePanelUI(_panelDict[key], fadeDuration, completed);
        }
    }

    public void ShowPopupUIAsync<T>(string key = null, float scaleDuration = 0f, Action<T> completed = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(key))
            key = typeof(T).Name;

        key = $"Prefabs/UI/Popup/{key}";
        StartCoroutine(Co_ShowPopup<T>(key, scaleDuration, completed));
    }
    IEnumerator Co_ShowPopup<T>(string key, float scaleDuration = 0f, Action<T> completed = null) where T : UI_Popup
    {
        var handle = Managers.ResourceA.InstantiateAsync(key, PopupRoot);
        yield return handle;

        GameObject instance = handle.Result;
        T popup = instance.GetComponent<T>();
        _popupStack.Push(popup);

        while (popup.IsLoaded == false)
        {
            yield return null;
        }
        
        completed?.Invoke(popup);
        popup.Show(scaleDuration);
    }

    public void ClosePopupUI(UI_Popup popup, float scaleDuration = 0f, Action completed = null)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopupUI(scaleDuration, completed);
    }

    public void ClosePopupUI(float scaleDuration = 0f, Action completed = null)
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        popup.Hide(scaleDuration, (popup) => 
        {
            Managers.ResourceA.Destroy(popup.gameObject);
            completed?.Invoke();
        });
    }

    public void CloseAllPopupUI(float scaleDuration = 0f)
    {
        while (_popupStack.Count > 0)
            ClosePopupUI(scaleDuration);
    }

    public void CloseAllPanelUI()
    {
        foreach (var kv in _panelDict)
        {
            Managers.ResourceA.Destroy(kv.Value.gameObject);
        }
        _panelDict.Clear();
    }


    public void Clear()
    {
        CloseAllPanelUI();
        CloseAllPopupUI();
    }
}