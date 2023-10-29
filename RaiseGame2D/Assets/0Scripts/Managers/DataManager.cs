using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    string filePath;

    Data.SkeletonStatData SkeletonDefaultData;
    Data.ZombieStatData ZombieDefaultData;
    Data.EventStatData EventDefaultData;

    public Data.PlayerStatData PlayerStat = new Data.PlayerStatData();
    public Dictionary<int, Data.Stat_Monster> SkeletonStatDict { get; private set; } = new Dictionary<int, Data.Stat_Monster>();
    public Dictionary<int, Data.Stat_Monster> ZombieStatDict { get; private set; } = new Dictionary<int, Data.Stat_Monster>();
    public Dictionary<int, Data.Stat_Monster> EventStatDict { get; private set; } = new Dictionary<int, Data.Stat_Monster>();

    void DefaultDataSetting()
    {
        SkeletonStatDict.Add(1, new Data.Stat_Monster(1, 20, 5, 2, 1));
        SkeletonStatDict.Add(2, new Data.Stat_Monster(2, 25, 10, 3, 2));
        SkeletonStatDict.Add(3, new Data.Stat_Monster(3, 30, 15, 4, 3));

        ZombieStatDict.Add(1, new Data.Stat_Monster(1, 20, 5, 4, 1));
        ZombieStatDict.Add(2, new Data.Stat_Monster(2, 25, 10, 5, 2));
        ZombieStatDict.Add(3, new Data.Stat_Monster(3, 30, 15, 5, 3));

        EventStatDict.Add(1, new Data.Stat_Monster(1, 20, 5, 1, 0));
        EventStatDict.Add(2, new Data.Stat_Monster(2, 25, 10, 1, 0));
        EventStatDict.Add(3, new Data.Stat_Monster(3, 30, 15, 1, 0));

        string json1 = JsonUtility.ToJson(SkeletonStatDict, true);
        File.WriteAllText(Path.Combine(filePath + "SkeletonStatData.json"), json1);
        string json2 = JsonUtility.ToJson(ZombieStatDict, true);
        File.WriteAllText(Path.Combine(filePath + "ZombieStatData.json"), json2);
        string json3 = JsonUtility.ToJson(EventStatDict, true);
        File.WriteAllText(Path.Combine(filePath + "EventStatData.json"), json3);
    }

    public void Init()
    {
        filePath = " /Android/data/com.DefaultCompany.TEST(RaiseGame2D)/files";//    Application.persistentDataPath; //"/data/data/com.DefaultCompany.TEST(RaiseGame2D)/files";//
        PlayerStat = LoadJson(Path.Combine(filePath +"PlayerStatData.json"));
        SkeletonStatDict = LoadJson<Data.SkeletonStatData, int, Data.Stat_Monster>(Path.Combine(filePath + "SkeletonStatData.json")).MakeDict();
        ZombieStatDict = LoadJson<Data.ZombieStatData, int, Data.Stat_Monster>(Path.Combine(filePath + "ZombieStatData.json")).MakeDict();
        EventStatDict = LoadJson<Data.EventStatData, int, Data.Stat_Monster>(Path.Combine(filePath + "EventStatData.json")).MakeDict();
    }
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        if (!File.Exists(path))
            DefaultDataSetting();
           
        string text = File.ReadAllText(path);
        return JsonUtility.FromJson<Loader>(text);
    }
 
    //플레이어용  Save Load 메서드
    //...
    public void SaveJson()
    {
        Data.PlayerStatData saveData = new Data.PlayerStatData();
        saveData = PlayerStat;
        string json = JsonUtility.ToJson(saveData,true);
        string path = Path.Combine(filePath + "PlayerStatData.json");
        File.WriteAllText(path, json);
    }
    public Data.PlayerStatData LoadJson(string path)
    {
        Data.PlayerStatData saveData = new Data.PlayerStatData();
        if (!File.Exists(path))
        {
            saveData.Attack = 10;
            saveData.AttackCoolTime = 1.0f;
            saveData.MaxHP = 200;
            saveData.Speed = 10;
            saveData.Defense = 1;
            PlayerStat = saveData;
            SaveJson();
        }
        else
        {
            string text = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<Data.PlayerStatData>(text);
            if (saveData != null)
                PlayerStat = saveData;     
        }
        return saveData;
    }


}
