using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int, Data.Stat > dick = Managers.Data.StatDict;

        gameObject.GetOrAddComponent<CursorController>();
    }

    public override void Clear()
    {
        Debug.Log("GameScene Clear!");
    }


}
