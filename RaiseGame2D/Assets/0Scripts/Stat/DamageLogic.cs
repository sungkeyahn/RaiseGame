using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLogic : MonoBehaviour
{
    /*데미지를 가하는 기능을 수행하는 스크립트
    지금은 충돌 시  데미지를 주는 방식이지만 
    개편시 조건을 충돌에서 그냥 데미지를 주는 함수로 개편이후
    자식 클래스로 여러종류의 데미지 전달 방식을 설정것으로 개편 예정
    */
    float damage =0;
    int attackAbleLayers;
    public Action OnAttackEvent;
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public void AddAttackAbleTag(int addLayer)
    {
        attackAbleLayers = (attackAbleLayers | addLayer);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //레이어를 통한 피격 판정 판별 
        if (collision.gameObject.layer == attackAbleLayers)
        {
            Stat targetStat = collision.gameObject.GetComponent<Stat>();
            if (targetStat)
            {
                targetStat.TakeDamage(damage);
                if (OnAttackEvent != null)
                    OnAttackEvent.Invoke();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == attackAbleLayers)
        {
            Stat targetStat = collision.gameObject.GetComponent<Stat>();
            if (targetStat)
            {
                targetStat.TakeDamage(damage);
                if (OnAttackEvent != null)
                    OnAttackEvent.Invoke();
            }
        }
    }
}
