using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    SpriteRenderer sprite;

    //입력
    public Vector2 inputVec;
   
    //물리
    Rigidbody2D rigid;

    //애님
    Animator anim;

    //스텟
    public Stat_Player stat;
    float speed;

    //상태
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
    void LateUpdate()//프레임의 마지막에 호출
    {
        if (isDead) { return; }
        if (inputVec != Vector2.zero)
            sprite.flipX = inputVec.x < 0;
    }
    void FixedUpdate()//물리관련 계산에 호출
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
    void OnMove(InputValue value)//InputSystem의 함수
    {
        //InputSystem의컴포넌트인 PlayerInput의 액션 Move는 후보정 으로 normalized 들어가있어서 필요없음
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
