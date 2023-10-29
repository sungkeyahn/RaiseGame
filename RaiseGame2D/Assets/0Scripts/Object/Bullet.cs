using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigid;
    DamageLogic damageLogic;
    float projectionvelocity = 15f;
    int per;
    public void Init(float damage,int per,Vector3 dir)
    {
       damageLogic.SetDamage(damage);
       this.per = per;
        if (per > -1)
            rigid.velocity = dir* projectionvelocity;
    }
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        damageLogic = GetComponent<DamageLogic>();
        damageLogic.AddAttackAbleTag(7);
    }
    private void Update()
    {
        Vector3 targetPos = GM.Contents.GetPlayer().transform.position;
        float dir = Vector3.Distance(targetPos, transform.position);
        if (dir > 20f)
        {
            rigid.velocity = Vector2.zero;
            GM.Contents.Despawn(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return;
        per--;
        if (per == -1)
        {
            rigid.velocity = Vector2.zero;
            GM.Contents.Despawn(gameObject);
        }
    }

}
