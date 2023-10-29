using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    public abstract void Init();
    private void Awake()
    {
        Init();
    }
    
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    //���÷��� ����� Ȱ���� �Լ� 
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
		{
			if (typeof(T) == typeof(GameObject))
				objects[i] = Utill.FindChild(gameObject, names[i], true);
			else
				objects[i] = Utill.FindChild<T>(gameObject, names[i], true);

			if (objects[i] == null)
				Debug.Log($"Failed to bind({names[i]})");
		}
    }

    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] obs = null;
        if (_objects.TryGetValue(typeof(T), out obs) == false) 
            return null;

        return obs[index] as T;
    }
    //���ֻ���� TŸ���� ���ʸ� �Լ��� ����ϴ� �Լ��� ���� �ϳ� �� ����
    //�������̽��� �����ϰ� �������ν� ����� ���ݴ� ����
    protected GameObject GetObject(int index) { return Get<GameObject>(index); }
    protected Text GetText(int index) { return Get<Text>(index); }
    protected Button GetButton(int index) { return Get<Button>(index); }
    protected Image GetImage(int index) { return Get<Image>(index); }

    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UIEventHandler evt = Utill.GetOrAddComponent<UIEventHandler>(go);
        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnPointerClickHandler -= action;
                evt.OnPointerClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
            default:
                break;
        }
    }

}
