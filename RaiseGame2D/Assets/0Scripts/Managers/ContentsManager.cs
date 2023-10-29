using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ContentsManager
{
    public Action<int> OnSpawnEvent;

    GameObject _player;
    HashSet<GameObject> _monsters = new HashSet<GameObject>();

    //Data... �ֹ߼�
    public bool isPause= true;
    public bool isStart= false;
    int killScore=0; //ų��
    int GoldScore=0;//�������� ��� 
    float PlayTime=0; //���� ���� �ð� 

    public void StartGame(bool isStart) //���ӽ��� ��ư�� ���ε�� �Լ��ε�?
    {
        this.isStart = isStart;
        isPause = !isStart;
        if (isStart)
        {
            _player = GM.Contents.Spawn("Object/Player");
            GameObject camera = GameObject.Find("CMvcam1");
            camera.GetComponent<CinemachineVirtualCamera>().Follow = _player.transform;

            GameObject go = new GameObject { name = "Spawner" };
            Spawner spawner = Utill.GetOrAddComponent<Spawner>(go);
            spawner.SetKeepMonsterCount(50);
        }
        else
        { GM.Pool.Clear();}

    }
    
    public void GamePause(bool isPause)
    {
        this.isPause = isPause;
        if (isPause == true)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
    public GameObject GetPlayer() { return _player; }
    public GameObject Spawn(string path, Transform parent = null)
    {
        GameObject go = GM.Resource.Instantiate(path, parent);

        if (go.GetComponent<Monster>())
        {
            _monsters.Add(go);
            if (OnSpawnEvent != null)
                OnSpawnEvent.Invoke(1);
        }
        if (go.GetComponent<Player>())
            _player = go;
                      
        return go;
    }
    public void Despawn(GameObject go)
    {
         //���⵵ ���� ���Ѿ� �ǰ�
         //�����۵� ���� ���Ѿ� �ǰ� 
        if (go.GetComponent<Monster>())
        {
            if (_monsters.Contains(go))
            {
                _monsters.Remove(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(-1);
            }
        }
        if (go.GetComponent<Player>())
        {
            if (_player == go)
                _player = null;
        }
        GM.Resource.Destroy(go);
    }

    public void SetGold(int gold)
    {
        GoldScore = gold;
        GetPlayer().GetComponentInChildren<UI_HUD>().SetGoldScore(GoldScore);
    }
    public int GetGold() { return GoldScore;}
    public void SetTime(float time)
    {
        if (_player.GetComponentInChildren<UI_HUD>())
        {
            PlayTime += time;
            _player.GetComponentInChildren<UI_HUD>().SetTimer(PlayTime);
        }
    }
    public void SetKill(int kill)
    {
        killScore = kill;
        GetPlayer().GetComponentInChildren<UI_HUD>().SetKillScore(killScore);
    }
    public int GetKill() { return killScore; }


    public void Clear()
    {
        //�������� �� �� �ֹ߼� �����Ϳ� �����Ͽ� ����
    }

}
