using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Utill.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = order;
            order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null) root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    int order = 0;
    Stack<UI_Popup> popupStack = new Stack<UI_Popup>();
    public T ShowPopupUI<T>(Transform parent = null,string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;

        GameObject go = GM.Resource.Instantiate($"UI/{name}");
        go.AddComponent<T>();
        T popup = go.GetComponent<T>();
        popupStack.Push(popup);

        if (parent == null)
            go.transform.SetParent(Root.transform);
        else
            go.transform.SetParent(parent);
            return popup;
    }
    public void ClosePopupUI()
    {
        if (popupStack.Count == 0) return;

        UI_Popup popup = popupStack.Pop();
        GM.Resource.Destroy(popup.gameObject);
        popup = null;
        order--;
    }
    public void ClosePopupUI(UI_Popup popup)
    {
        if (popupStack.Count == 0) return;

        if (popupStack.Peek() != popup) return;

        ClosePopupUI();
    }
    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
            ClosePopupUI();
    }

    UI_Scene sceneUI = null;
    public T ShowSceneUI<T>(Transform parent = null,string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;

        GameObject go = GM.Resource.Instantiate($"UI/{name}");
        go.AddComponent<T>();
        T scene = go.GetComponent<T>();
        sceneUI = scene;

        if (parent == null)
            go.transform.SetParent(Root.transform);
        else
            go.transform.SetParent(parent);

        return scene;
    }

    public T MakeSubUI<T>(Transform parent, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GM.Resource.Instantiate($"UI/{name}");

        if (parent != null)
            go.transform.SetParent(parent);
        go.AddComponent<T>();
        T scene = go.GetComponent<T>();
        return scene;
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GM.Resource.Instantiate($"UI/{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        go.AddComponent<Canvas>();
        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        go.AddComponent<T>();
        T ui = go.GetComponent<T>();
        return ui;
    }

    public void Clear()
    {
        
    }
}
