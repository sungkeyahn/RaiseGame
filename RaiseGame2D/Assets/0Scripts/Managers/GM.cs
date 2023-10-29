using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    static GM instance;
    static GM Instance { get { Init(); return instance; } }

    PoolManager _pool = new PoolManager();
    public static PoolManager Pool { get { return Instance._pool; } }

    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource {get { return Instance._resource; } }

    ContentsManager _contents = new ContentsManager();
    public static ContentsManager Contents { get { return Instance._contents; } }

    UIManager _uI = new UIManager();
    public static UIManager UI { get { return Instance._uI; } }

    MySceneManager _scene = new MySceneManager();
    public static MySceneManager Scene { get { return Instance._scene; } }

    DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }

    static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("@GameManager");
            if (go == null)
            {
                go = new GameObject { name = "@GameManager" };
                go.AddComponent<GM>();
            }
            DontDestroyOnLoad(go);
            instance = go.GetComponent<GM>();

           instance._data.Init();
           instance._pool.Init();
        }
    }
    
    void Start()
    {
        Init();
    }
    void Update()
    {
        if (Contents.isStart && !Contents.isPause&&Contents.GetPlayer())
        {
            Contents.SetTime(Time.deltaTime);
        }
    }
    public static void Clear()
    {
        Pool.Clear();
        Contents.Clear();
        UI.Clear();
        Scene.Clear();
    }
}
