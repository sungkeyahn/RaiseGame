using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utill 
{
    //��ƿ���Լ��� ����
    //�ڽ��� �ڽ��� ���������� ��ĵ�ϸ鼭 �̸��� �´� �ڽ��� �ִ��� Ȯ�����ְ� �ش� �ڽ��� ������Ʈ�� ��ȯ�� �ִ� �Լ�
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null) return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform tr = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || tr.name == name)
                {
                    T component = tr.GetComponent<T>();
                    if (component != null) return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        return null;
    }
    //GameObject�� ��ȯ���ִ� ����
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform tr = FindChild<Transform>(go, name, recursive);
        if (tr == null) return null;
        return tr.gameObject;
    }
    //������Ʈ �߰��� �ڵ�󿡼� �����ϰ� ������ ����ϴ� ��ƿ�Լ�
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T com = go.GetComponent<T>();
        if (com == null)
            com = go.AddComponent<T>();
        return com;
    }
}
