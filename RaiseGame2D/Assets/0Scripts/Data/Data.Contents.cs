using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
	#region Stat
	[Serializable]
	public class PlayerStatData
	{
		public float MaxHP;
		public float Attack;
		public float Defense;
		public float Speed;
		public float AttackCoolTime;
	}
	[Serializable]
	public class Stat_Monster
	{
		public Stat_Monster() { }
		public Stat_Monster(int l,float hp, float atk, float speed, int gold)
		{
			Level = l;
			MaxHP = hp;
			Attack = atk;
			Speed = speed;
			DropGold = gold;
		}
		public int Level;
		public float MaxHP;
		public float Attack;
		public float Speed;
		public int DropGold;
	}
	[Serializable]
	public class SkeletonStatData : ILoader<int, Stat_Monster>
	{
		public List<Stat_Monster> stats = new List<Stat_Monster>();
		public Dictionary<int, Stat_Monster> MakeDict()
		{
			Dictionary<int, Stat_Monster> dict = new Dictionary<int, Stat_Monster>();
			foreach (Stat_Monster stat in stats)
				dict.Add(stat.Level, stat);
			return dict;
		}
	}
	[Serializable]
	public class ZombieStatData : ILoader<int, Stat_Monster>
	{
		public List<Stat_Monster> stats = new List<Stat_Monster>();
		public Dictionary<int, Stat_Monster> MakeDict()
		{
			Dictionary<int, Stat_Monster> dict = new Dictionary<int, Stat_Monster>();
			foreach (Stat_Monster stat in stats)
				dict.Add(stat.Level, stat);
			return dict;
		}
	}
	[Serializable]
	public class EventStatData : ILoader<int, Stat_Monster>
	{
		public List<Stat_Monster> stats = new List<Stat_Monster>();
		public Dictionary<int, Stat_Monster> MakeDict()
		{
			Dictionary<int, Stat_Monster> dict = new Dictionary<int, Stat_Monster>();
			foreach (Stat_Monster stat in stats)
				dict.Add(stat.Level, stat);
			return dict;
		}
	}
	#endregion
}

