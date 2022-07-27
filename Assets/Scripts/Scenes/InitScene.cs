using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScene : BaseScene
{
    private void Awake()
    {
        Managers.Scene.CurrentScene = this;
    }
    public override IEnumerator Co_InitAsync()
    {
        yield return base.Co_InitAsync();

        SceneType = Define.Scene.Init;
    }

    void Start()
    {
        if (Managers.Scene.NextSceneType != Define.Scene.Unknown)
        {
            var nextSceneType = Managers.Scene.NextSceneType;
            Managers.Scene.FadeColor = Color.white;
            Managers.Scene.NextSceneType = Define.Scene.Unknown;
            Managers.Scene.SceneLoad(nextSceneType, true);
        }
        else
        {
            //Managers.Scene.SceneLoad();
        }
    }

}
