using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    UI_Inven _inven = null;
    public override IEnumerator Co_InitAsync()
    {
        yield return base.Co_InitAsync();

        SceneType = Define.Scene.Game;

        //Managers.UI.ShowPanelUIAsync<UI_Inven>(fadeDuration: 0.2f, completed:(panel) => { _inven = (UI_Inven)panel; });
        Managers.UI.ShowPopupUIAsync<UI_Inven>(scaleDuration: 0.2f, completed: (panel) => { _inven = panel; });

        Dictionary<int, Data.Stat> dick = Managers.Data.StatDict;

        //Managers.Game.SpawnAsync(Define.WorldObject.Player, "UnityChan", null, (player) =>
        //{
        //    
        //});
        //
        //Managers.Game.SpawnAsync(Define.WorldObject.Enemy, "Monster");

        yield break;
    }

    public override void Clear()
    {
        Debug.Log("GameScene Clear!");
    }
}
