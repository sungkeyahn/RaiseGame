using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public int amountofGold; //°ñµå·® 

    private void LateUpdate()
    {
        Vector2 currentRotation = transform.rotation.eulerAngles;
        currentRotation.y += 90.0f * Time.deltaTime;
        transform.rotation = Quaternion.Euler(currentRotation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            PickUp();
    }
    void PickUp()
    {
        GM.Contents.SetGold(GM.Contents.GetGold()+amountofGold);
        //GM.Contents.GetPlayer().GetComponentInChildren<UI_HUD>().SetGoldScore(GM.Contents.GoldScore);
        GM.Contents.Despawn(gameObject);
    }
}
