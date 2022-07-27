using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingScene : BaseScene
{
    [SerializeField] TextMeshProUGUI _loadingMessage;

    public override IEnumerator Co_InitAsync()
    {
        yield return base.Co_InitAsync();
        SceneType = Define.Scene.Loading;
    }
    void Start()
    {
        Managers.Scene.SceneLoadProgress += LoadingProgress;

    }
    private void OnDestroy()
    {
        Managers.Scene.SceneLoadProgress -= LoadingProgress;
    }

    private void LoadingProgress(float obj)
    {
        _loadingMessage.text = $"Loading {obj * 100f}%";
        Debug.Log($"Loading {obj * 100f}%");
    }
}
