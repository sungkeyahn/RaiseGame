using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //��ġ
    //���� -0.3,-0.275
    //������ 0.3 -0.275

    Player player;

    //���� Ž��
    float ScanRange=10f;
    RaycastHit2D[] targets;
    Transform nearestTarget;
    
    //�߻� ����
    float timer;
    bool isTarget;

    void Start()
    {
        player = GM.Contents.GetPlayer().GetComponent<Player>();
    }
    void Update()
    {
        if (player.isDead) return;
        timer += Time.deltaTime;
        if (player.stat.AttackCoolTime < timer) 
        {
            timer = 0f;
            if (isTarget)
                FireBullte();
        }
    }
    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, ScanRange,Vector2.zero,0, LayerMask.GetMask("Monster"));
        isTarget = ScansMonster(100f);
    }
    bool ScansMonster(float Range)
    {
        Transform result = null;
        float diff = Range;

        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);
            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        if (result == null)
        {
            nearestTarget = null;
            return false;
        }
        else
        {
            nearestTarget = result;
            return true;
        }
    }
    void FireBullte()
    {
        GameObject bullet = GM.Contents.Spawn("Object/Bullet",transform);
        Vector3 dir = nearestTarget.position - transform.position;
        dir = dir.normalized;
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(player.stat.Attack,0,dir);
    }

}
