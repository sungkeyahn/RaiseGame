using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_HUD : UI_Scene
{
    enum Buttons
    {
        MenuBtn
    }
    enum Texts
    {
        Time
    }
    enum GameObjects
    {
        HPBar,Kill,Gold,MenuBtn
    }

    Stat_Player stat;

    public override void Init()
    {
        stat=GM.Contents.GetPlayer().GetComponent<Stat_Player>();
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        BindEvent(GetButton((int)Buttons.MenuBtn).gameObject, OnButtonClicked, Define.UIEvent.Click);
    }
    private void Update()
    {
        float ratio = stat.CurrentHP / (float)stat.MaxHP;
        SetHpRatio(ratio);
    }
    public void SetTimer(float time)
    {
        int min = Mathf.FloorToInt(time / 60);
        int sec = Mathf.FloorToInt(time % 60);
        GetText((int)Texts.Time).GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}", min,sec);
    }
    public void SetKillScore(int kill)
    {
        GetObject((int)GameObjects.Kill).GetComponentInChildren<Text>().text = string.Format("{0:F0}", kill);
    }
    public void SetGoldScore(int gold)
    {
        GetObject((int)GameObjects.Gold).GetComponentInChildren<Text>().text = string.Format("{0:F0}",gold);
    }
    public void OnButtonClicked(PointerEventData data)
    {
        UI_Puase puase = GM.UI.ShowPopupUI<UI_Puase>(null, "GamePuaseUI");
        puase.SetStats(stat);
        GM.Contents.GamePause(true);
    }
    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponentInChildren<Slider>().value = ratio;
    }
}
