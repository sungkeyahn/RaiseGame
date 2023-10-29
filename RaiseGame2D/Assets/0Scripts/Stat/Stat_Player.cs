using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_Player : Stat
{
    [SerializeField]
    float maxHP;
    [SerializeField]
    float attack;
    [SerializeField]
    float defense;
    [SerializeField]
    float speed;
    [SerializeField]
    float attackCoolTime;
    public float MaxHP { get { return maxHP; } set { maxHP = value; GM.Data.PlayerStat.MaxHP = MaxHP; } }
    public float Attack { get { return attack; } set { attack = value; GM.Data.PlayerStat.Attack = Attack; } }
    public float Defense { get { return defense; } set { defense = value; GM.Data.PlayerStat.Defense = Defense; } }
    public float Speed { get { return speed; } set { speed = value; GM.Data.PlayerStat.Speed = Speed; } }
    public float AttackCoolTime { get { return attackCoolTime; } set { attackCoolTime = value; GM.Data.PlayerStat.AttackCoolTime = AttackCoolTime; } }

    [SerializeField]
    public float CurrentHP;
    public override void Init()
    {}
    public void Init(Data.PlayerStatData stat)
    {
        MaxHP = stat.MaxHP;
        Attack = stat.Attack;
        Defense = stat.Defense;
        Speed = stat.Speed;
        AttackCoolTime = stat.AttackCoolTime;
        CurrentHP = MaxHP;
    }
    public override bool TakeDamage(float damage)
    {
        if (CurrentHP <= 0)
        {
            if (OnHPZeroEvent.Target != null)
                OnHPZeroEvent.Invoke();
            return false;
        }
        CurrentHP -= Mathf.Clamp(damage-Defense, 0 , CurrentHP);
        return true;
    }
  
}
