using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : BaseScene
{
    public override void Clear()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Managers.Scene_Addressable.SceneLoadProgress += LoadingProgress;
    }
    private void OnDestroy()
    {
        Managers.Scene_Addressable.SceneLoadProgress -= LoadingProgress;
    }

    private void LoadingProgress(float obj)
    {
        Debug.Log($"Loading : {obj * 100f}");
    }
}
