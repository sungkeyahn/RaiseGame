using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Dead : UI_Popup
{
    enum Buttons
    {
        TitleBtn
    }
    enum GameObjects
    {
        KillScore
    }
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.TitleBtn).gameObject, OnButtonClicked, Define.UIEvent.Click);
        Get<GameObject>((int)GameObjects.KillScore).GetComponentInChildren<Text>().text = $"Score:{GM.Contents.GetKill()}";
    }
    public void OnButtonClicked(PointerEventData data)
    {
        GM.Data.SaveJson();
        GM.UI.ClosePopupUI(this);
        GM.Scene.LoadScene(Define.Scene.Title);
    }
}
