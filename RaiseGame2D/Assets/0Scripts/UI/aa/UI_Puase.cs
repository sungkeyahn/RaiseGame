using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Puase : UI_Popup
{
    enum Buttons
    {
        ReturnBtn, TitleBtn
    }
    enum GameObjects
    {
        StatInfoPanel, Info1, Info2, Info3, Info4, Info5, Info6
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        BindEvent(GetButton((int)Buttons.ReturnBtn).gameObject, OnReturnBtnClicked, Define.UIEvent.Click);
        BindEvent(GetButton((int)Buttons.TitleBtn).gameObject, OnTitleBtnClicked, Define.UIEvent.Click);
    }
    public void OnReturnBtnClicked(PointerEventData data)
    {
        GM.Contents.GamePause(false);
        GM.UI.ClosePopupUI();
    }
    public void OnTitleBtnClicked(PointerEventData data)
    {
        GM.Scene.LoadScene(Define.Scene.Title);
        GM.Contents.GamePause(false);
        GM.UI.ClosePopupUI();
    }
    public void SetStats(Stat_Player stat)
    {
        Get<GameObject>((int)GameObjects.Info1).GetComponentInChildren<Text>().text = $"MaxHP:{stat.MaxHP}";
        Get<GameObject>((int)GameObjects.Info2).GetComponentInChildren<Text>().text = $"Attack:{stat.Attack}";  
        Get<GameObject>((int)GameObjects.Info3).GetComponentInChildren<Text>().text = $"Defense:{stat.Defense}";  
        Get<GameObject>((int)GameObjects.Info4).GetComponentInChildren<Text>().text = $"Speed:{stat.Speed}";  
        Get<GameObject>((int)GameObjects.Info5).GetComponentInChildren<Text>().text = $"AtkSpeed:{stat.AttackCoolTime}"; 
    }

}
