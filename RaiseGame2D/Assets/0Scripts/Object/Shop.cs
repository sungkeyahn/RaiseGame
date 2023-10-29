using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    GameObject ShopUI;
    float [] sellStatAmountOfIncrease = {50f,5f,1f,3f,-0.1f}; //스텟 증가량 체,공,방,스,쿨
    float[] price  = new float[3];
    string[] typename = new string[3];

    void Start()
    {
        //등장할때 랜덤으로 스텟중 3개를 골라 상점의 품목으로 입점
        //가격은 플레이어 스텟 따라서 정해야 하지만 일단 x10원으로 가정 
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UseTheShop();
            GM.Contents.Despawn(gameObject);
        }
    }
    void UseTheShop()
    {
        ShopUI = GM.UI.ShowPopupUI<UI_Shop>(null, "ShopUI").gameObject;
        for (int i = 0; i < 3; i++)
        {
            int type = UnityEngine.Random.Range(0, 5);
            switch (type)
            {
                case 0:
                    price[i] = 1;
                    typename[i] = "MaxHP";
                    break;
                case 1:
                    price[i] = 1;
                    typename[i] = "Attack";
                    break;
                case 2:
                    price[i] = 1;
                    typename[i] = "Defense";
                    break;
                case 3:
                    price[i] = 1;
                    typename[i] = "Speed";
                    break;
                case 4:
                    price[i] = 1;
                    typename[i] = "AttackCoolTime";
                    break;
            }
            ShopUI.GetComponent<UI_Shop>().Cards[i].GetComponent<UI_StatCard>().SetStatType(typename[i]);
            ShopUI.GetComponent<UI_Shop>().Cards[i].GetComponent<UI_StatCard>().SetPirce((int)price[i]);
            ShopUI.GetComponent<UI_Shop>().Cards[i].GetComponent<UI_StatCard>().SetStatInfo(typename[i], (int)price[i], sellStatAmountOfIncrease[type]);
        }
        GM.Contents.GamePause(true);
    }

}
