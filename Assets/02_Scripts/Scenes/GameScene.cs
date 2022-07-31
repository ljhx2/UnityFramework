using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public override IEnumerator Co_InitAsync()
    {
        yield return base.Co_InitAsync();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUIAsync<UI_Inven>();

        Dictionary<int, Data.Stat> dick = Managers.Data.StatDict;

        gameObject.GetOrAddComponent<CursorController>();

        Managers.Game.SpawnAsync(Define.WorldObject.Player, "UnityChan", null, (player) =>
        {
            Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);
        });
        
        Managers.Game.SpawnAsync(Define.WorldObject.Monster, "Monster");

        yield break;
    }

    public override void Clear()
    {
        Debug.Log("GameScene Clear!");
    }


}
