using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{

    public override IEnumerator Co_InitAsync()
    {
        yield return base.Co_InitAsync();

        SceneType = Define.Scene.Login;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }
}
