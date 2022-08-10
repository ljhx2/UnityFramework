using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Panel//UI_Popup
{
    enum GameObjects
    {
        GridPanel
    }

    private void Start()
    {
        Init();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log($"{gameObject.GetHashCode()} : sdfdsf");
        }
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = GetObject((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.ResourceA.Destroy(child.gameObject);

        //실제 인벤토리 정보를 참고해서
        Managers.UI.MakeSubItemAsync<UI_Inven_Item>(gridPanel.transform, null, (item) =>
        {
            item.SetInfo($"집행검0번");
            for (int i = 1; i < 8; i++)
            {
                int index = i;
                Managers.UI.MakeSubItemAsync<UI_Inven_Item>(gridPanel.transform, null, (item) =>
                {
                    item.SetInfo($"집행검{index}번");
                    if (index == 7)
                    {
                        IsLoaded = true;
                    }
                });
            }
        });
        gameObject.SetActive(false);
    }
}
