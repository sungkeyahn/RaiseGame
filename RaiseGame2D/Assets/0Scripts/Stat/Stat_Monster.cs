using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_Monster : Stat
{
    /*몬스터의 스텟은 자신이 어떤 몬스터인지에 따라 다른 데이터 값을 가져오는 형식으로*/

    public int Level;
    public float MaxHP =20f;
    public float Attack;
    public float Speed=3.0f;
    public int DropGold;

    [SerializeField]
    public float CurrentHP;

    public override void Init()
    {
        CurrentHP = MaxHP;
        Attack = 5.0f;
        Speed = 3.0f;
        DropGold = 1;
    }
    public void Init(Data.Stat_Monster stat)
    {
        Level=stat.Level;
        MaxHP = stat.MaxHP;
        Attack = stat.Attack;
        Speed = stat.Speed;
        DropGold = stat.DropGold;
    }
    private void Awake()
    {
        Init();
    }
    public override bool TakeDamage(float damage)
    {
        if (CurrentHP <= 0)
        {
            return false;
        }
        CurrentHP -= Mathf.Clamp(damage, 0, CurrentHP);
        return true;
    }


}
