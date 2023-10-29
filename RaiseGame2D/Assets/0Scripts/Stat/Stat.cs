using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*스텟을 가진 오브젝트들이 자신이 데미지를 받는 로직을 알아서 구현할 수 있도록 하는 부모클래스*/

public abstract class Stat : MonoBehaviour
{    
    public Action OnHPZeroEvent;
    public abstract void Init();
    public abstract bool TakeDamage(float damage);
}
