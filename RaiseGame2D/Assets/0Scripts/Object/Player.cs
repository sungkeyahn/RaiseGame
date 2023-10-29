using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    SpriteRenderer sprite;

    //�Է�
    public Vector2 inputVec;
   
    //����
    Rigidbody2D rigid;

    //�ִ�
    Animator anim;

    //����
    public Stat_Player stat;
    float speed;

    //����
    public bool isDead=false;
 
    void Awake()
    {
        Application.targetFrameRate = 60;
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stat = GetComponent<Stat_Player>();
        stat.OnHPZeroEvent -= Dead;
        stat.OnHPZeroEvent += Dead;
    }
    private void Start()
    {
        GM.UI.ShowSceneUI<UI_HUD>(transform,"HUD");
        stat.Init(GM.Data.PlayerStat);
    }
    void LateUpdate()//�������� �������� ȣ��
    {
        if (isDead) { return; }
        if (inputVec != Vector2.zero)
            sprite.flipX = inputVec.x < 0;
    }
    void FixedUpdate()//�������� ��꿡 ȣ��
    {
        if (isDead) return;
        Vector2 moveVec = inputVec * speed * Time.fixedDeltaTime;
        if (rigid)
        {
            rigid.MovePosition(rigid.position + moveVec);

            speed = inputVec == Vector2.zero ? 0 : stat.Speed;
            anim.SetFloat("Speed", speed);
        }
    }
    void OnMove(InputValue value)//InputSystem�� �Լ�
    {
        //InputSystem��������Ʈ�� PlayerInput�� �׼� Move�� �ĺ��� ���� normalized ���־ �ʿ����
        inputVec = value.Get<Vector2>();
    }
    public void Dead()
    {
        isDead = true;
        anim.SetTrigger("Dead");
        rigid.isKinematic = isDead;
        StartCoroutine(Die());
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(2.0f);
        GM.UI.ShowPopupUI<UI_Dead>(null, "DeadUI");
    }

}
