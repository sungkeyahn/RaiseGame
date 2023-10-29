using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    SpriteRenderer sprite;

    //물리
    Rigidbody2D rigid;
    Collider2D col;

    //애님
    Animator anim;

    //스텟
    Stat_Monster stat;
    
    //상태 
    bool isLife;

    //공격 
    DamageLogic damageLogic;

    //피격 
    WaitForFixedUpdate wait=new WaitForFixedUpdate();

    //플레이어 
    Transform target;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        stat = GetComponent<Stat_Monster>();
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        damageLogic = GetComponent<DamageLogic>();
        isLife = true;
    }
    private void Start()
    {
        target = GM.Contents.GetPlayer().transform;
        damageLogic.SetDamage(stat.Attack);
        damageLogic.AddAttackAbleTag(6); //6번 레이어 == 플레이어 
        stat.Init(GM.Data.SkeletonStatDict[2]);
    }
    private void OnEnable()
    {
        isLife = true;
        rigid.simulated = true;
        col.enabled = true;
        stat.CurrentHP = stat.MaxHP;
        anim.SetBool("Dead", false);
    }
    void LateUpdate()
    {
        if (!isLife) return;

        if (sprite)
            sprite.flipX = target.position.x - transform.position.x < 0;
    }
    void FixedUpdate()
    {
        if (!isLife || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        Vector2 dir = (Vector2)target.position - rigid.position;
        Vector2 moveVector = dir.normalized * stat.Speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + moveVector);
        rigid.velocity = Vector2.zero;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLife) return;

        if (stat.CurrentHP <= 0)
        {
            isLife = false;
            rigid.simulated = false;
            col.enabled = false;
            anim.SetBool("Dead", true);
        }
        else
            Hit();
    }
    void Hit()
    {
        anim.SetTrigger("Hit");
        StartCoroutine(KnockBack());
    }
    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos=GM.Contents.GetPlayer().transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized*3,ForceMode2D.Impulse);
    }
    void Dead()
    {
        if (gameObject.name == "BonusMon")
            DropTheShop();
        else
            DropGold(stat.DropGold);

        GM.Contents.SetKill(GM.Contents.GetKill()+1);
        GM.Contents.Despawn(gameObject);
    }
    void DropGold(int amountofGold)
    {
        GameObject ob = GM.Contents.Spawn("Object/Gold");
        Gold gold = ob.GetComponent<Gold>();
        gold.amountofGold = amountofGold;
        ob.transform.position = transform.position;
    }
    void DropTheShop()
    {
        GameObject ob = GM.Contents.Spawn("Object/Shop");
        Shop shop = ob.GetComponent<Shop>();
        
        ob.transform.position = transform.position;
    }
}
