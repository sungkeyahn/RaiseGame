using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*������ ���� ������Ʈ���� �ڽ��� �������� �޴� ������ �˾Ƽ� ������ �� �ֵ��� �ϴ� �θ�Ŭ����*/

public abstract class Stat : MonoBehaviour
{    
    public Action OnHPZeroEvent;
    public abstract void Init();
    public abstract bool TakeDamage(float damage);
}
