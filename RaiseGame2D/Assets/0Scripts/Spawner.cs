using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0;
    int _reserveCount = 0;

    [SerializeField]
    int _keepMonsterCount = 0;

    [SerializeField]
    Vector3 _spawnPos;
    [SerializeField]
    float _spawnRadius = 15.0f;
    [SerializeField]
    float _spawnTime = 5.0f;

    
    public void AddMonsterCount(int value) { _monsterCount += value; }
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }

    void Start()
    {
        GM.Contents.OnSpawnEvent -= AddMonsterCount;
        GM.Contents.OnSpawnEvent += AddMonsterCount;
    }

    void Update()
    {
        while (_reserveCount + _monsterCount < _keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }

    IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        yield return new WaitForSeconds(Random.Range(0, _spawnTime));
       
        GameObject obj;
        int random = Random.Range(0, 10);
        if (random == 1)
            obj = GM.Contents.Spawn("Object/BonusMon");
        else if (random % 2 == 0)
            obj = GM.Contents.Spawn("Object/Skeleton");
        else
            obj = GM.Contents.Spawn("Object/Zombie");
        

        Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius);
        Vector3 randPos = _spawnPos + randDir;
        randPos.z = 0f;
        obj.transform.position = randPos;
        
        _reserveCount--;
    }
}
