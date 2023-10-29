using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Title : UI_Scene
{
    enum Buttons
    {
        GameStartBtn, GameSettingBtn , GameExitBtn
    }
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.GameStartBtn).gameObject, OnGameStartBtnClicked, Define.UIEvent.Click);
        BindEvent(GetButton((int)Buttons.GameSettingBtn).gameObject, OnGameSettingBtnClicked, Define.UIEvent.Click);
        BindEvent(GetButton((int)Buttons.GameExitBtn).gameObject, OnGameExitBtnClicked, Define.UIEvent.Click);
    }
    public void OnGameStartBtnClicked(PointerEventData data)
    {
        //게임 씬으로 이동 
        GM.Scene.LoadScene(Define.Scene.Game);
        GM.Resource.Destroy(gameObject);
    }
    public void OnGameSettingBtnClicked(PointerEventData data)
    {
        //미구현 
    }
    public void OnGameExitBtnClicked(PointerEventData data)
    {
        //GM.Clear();
        Application.Quit();
    }

}
