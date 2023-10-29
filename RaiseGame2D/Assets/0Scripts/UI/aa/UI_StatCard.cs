using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StatCard : UI_Base
{
    enum Buttons
    {
        StatBuyBtn 
    }
    enum GameObjects
    {
        Price
    }
    int price;
    string type;
    float increase;
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.StatBuyBtn).gameObject, OnButtonClicked, Define.UIEvent.Click);
    }
    public void OnButtonClicked(PointerEventData data)
    {
        /*누르면   (돈 빠지고)  (스텟오르고) (상점 닫히고) */
        if (GM.Contents.GetGold()>=price)
        {
            GM.Contents.SetGold(GM.Contents.GetGold() - price);
            Stat_Player stat = GM.Contents.GetPlayer().GetComponent<Stat_Player>();
            switch (type)
            {
                case "MaxHP":
                    stat.MaxHP += increase;
                    break;
                case "Attack":
                    stat.Attack += increase;
                    break;
                case "Defense":
                    stat.Defense += increase;
                    break;
                case "Speed":
                    stat.Speed += increase;
                    break;
                case "AttackCoolTime":
                    stat.AttackCoolTime += increase;
                    break;
            }
            GM.Data.SaveJson();
            GM.Contents.GamePause(false);
            GM.UI.ClosePopupUI(GetComponentInParent<UI_Shop>());
        }

    }
    public void SetPirce(int price)
    {
        GetObject((int)GameObjects.Price).GetComponent<Text>().text = $"{price}";
    }
    public void SetStatType(string type)
    {   
        Get<Button>((int)Buttons.StatBuyBtn).gameObject.GetComponentInChildren<Text>().text = type;
    }
    public void SetStatInfo(string type,int price,float increase)
    {
        this.price = price;
        this.type= type;
        this.increase = increase;
    }
}
