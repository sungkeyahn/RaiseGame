using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLogic : MonoBehaviour
{
    /*�������� ���ϴ� ����� �����ϴ� ��ũ��Ʈ
    ������ �浹 ��  �������� �ִ� ��������� 
    ����� ������ �浹���� �׳� �������� �ִ� �Լ��� ��������
    �ڽ� Ŭ������ ���������� ������ ���� ����� ���������� ���� ����
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
        //���̾ ���� �ǰ� ���� �Ǻ� 
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
