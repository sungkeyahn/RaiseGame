using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Shop : UI_Popup
{
    enum Buttons
    {
        RetuenBtn
    }
    enum GameObjects
    {
        Panel
    }
    public GameObject[] Cards=new GameObject[3];

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.RetuenBtn).gameObject, OnButtonClicked, Define.UIEvent.Click);
        GameObject StatCards = Get<GameObject>((int)GameObjects.Panel).gameObject;
        foreach (Transform child in StatCards.transform)
            GM.Resource.Destroy(child.gameObject);
        for (int i = 0; i < 3; i++)
        {
            Cards[i] = GM.UI.MakeSubUI<UI_StatCard>(StatCards.transform, "StatCard").gameObject;
        }
    }
    public void OnButtonClicked(PointerEventData data)
    {
        GM.Contents.GamePause(false);
        GM.UI.ClosePopupUI();
    }
}
