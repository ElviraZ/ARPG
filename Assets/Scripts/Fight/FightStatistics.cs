using UnityEngine;
using System.Collections;
using DevelopEngine;
using System.Collections.Generic;
using ARPGSimpleDemo.Character;
using ARPGSimpleDemo.Skill;
using Mono.Data.Sqlite;
using System;

public class FightStatistics : MonoSingleton<FightStatistics> {

	List<GameObject> enemyList;	//怪物列表
	SkillData sd;

	void Start () {
		enemyList = new List<GameObject> ();
		InitEnemy();
	}

	//初始化敌人技能
	void InitEnemy()
	{
		OperatingDB.Instance.CreateDataBase();
		SqliteDataReader enemy = 
			OperatingDB.Instance.db.Select("T_Scene","SceneName","11");
		int id = 0;
		while(enemy.Read())
		{
			id = enemy.GetInt32(enemy.GetOrdinal("Monster"));
		}
		SqliteDataReader skill = 
			OperatingDB.Instance.db.Select("T_MonsterSkill","ID",id.ToString());
		while(skill.Read())
		{
			int i = 1;
			sd = new SkillData ();
			Type t = typeof(SkillData);
			foreach(var item in t.GetProperties())
			{
				if(item.PropertyType.Equals(typeof(string)))
					item.SetValue(sd, skill[i].ToString(), null);
				else if(item.PropertyType.Equals(typeof(float)))
					item.SetValue(sd, float.Parse(skill[i].ToString()),null);
				else if(item.PropertyType.Equals(typeof(String[])))
				{
					string[] str = skill[i].ToString().Split(',');
					item.SetValue(sd, str, null);
				}
				else
					item.SetValue(sd,int.Parse(skill[i].ToString()), null);
				i++;
			}
		}
		OperatingDB.Instance.db.CloseSqlConnection();
		AddEnemy();
	}

	//添加敌人
	void AddEnemy()
	{
		var enemyObj = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(var enemy in enemyObj)
		{
			enemy.GetComponent<CharacterSkillManager>().skills.Add(sd);
			enemyList.Add(enemy);
		}
		Debug.Log("enemyList.Count:" + enemyList.Count);
	}

	//删除敌人
	public void DeleteEnemy(GameObject obj)
	{
		enemyList.Remove(obj);
		Debug.Log("enemyList.Count:" + enemyList.Count);
		if(enemyList.Count <= 0)
		{
			Debug.Log("游戏胜利！");
			StartCoroutine(wait(4));
		}
	}

	IEnumerator wait(float time)
	{
		GetResulte();
		yield return new WaitForSeconds(time);
		UIManager.Instance.SetVisible(UIName.UIBattleVictory, true);
	}

	int star;
	//获取胜利时间
	void GetResulte()
	{
		if(Time.timeSinceLevelLoad < 60)
			star = 3;
		else if(Time.timeSinceLevelLoad >=60 &&
		        Time.timeSinceLevelLoad < 120)
			star = 2;
		else if(Time.timeSinceLevelLoad >=120 &&
		        Time.timeSinceLevelLoad < 300)
			star = 1;
		GetResulteDB(star, 11);
	}

	int exp;
	int gold;
	void GetResulteDB(int star, int scene)
	{
		OperatingDB.Instance.CreateDataBase();
		SqliteDataReader sceneDR = OperatingDB.Instance.db.SelectWhere(
			"T_Scene", new string[] {"SceneExp" + star,"Gold" + star},
		new string[] {"SceneName"}, new string[] {"="}, 
		new string[] {scene.ToString()});
		while(sceneDR.Read())
		{
			exp = int.Parse(sceneDR[0].ToString());
			gold = int.Parse(sceneDR[1].ToString());
		}

		UIVictory.Instance.SetExp(exp);
		UIVictory.Instance.SetStar(star);

		//读取角色表
		SqliteDataReader sqReader = OperatingDB.Instance.db.Select(
			"T_Character","CharacterID","1");
		while(sqReader.Read())
		{
			int i = 0;
			CharacterTemplate.Instance.characterId = int.Parse(sqReader[i].ToString());
			CharacterTemplate.Instance.jobId = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.lv = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.expCur = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.force = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.intellect = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.attackSpeed = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.maxHp = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.maxMp = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.damageMax = int.Parse(sqReader[++i].ToString());
			CharacterTemplate.Instance.jobModel = sqReader[++i].ToString();
		}

		//获取当前等级经验上限
		SqliteDataReader lvExp = OperatingDB.Instance.db.Select("T_Exp","Lv",
		                                                        CharacterTemplate.Instance.lv.ToString());
		int maxExp = 0;
		while(lvExp.Read())
		{
			maxExp = int.Parse(lvExp[2].ToString());
		}

		CharacterTemplate.Instance.expCur += exp;
		if(CharacterTemplate.Instance.expCur > maxExp)
		{
			//升级
			CharacterTemplate.Instance.lv +=1;
			CharacterTemplate.Instance.maxHp += 1000;
			CharacterTemplate.Instance.maxMp += 500;
			CharacterTemplate.Instance.force += 5;
			CharacterTemplate.Instance.intellect += 5;
			CharacterTemplate.Instance.damageMax += 5;
			OperatingDB.Instance.db.UpdateInto("T_Character",new string[] {
				"Lv","ExpCur","Force","Intellect","MaxHP","MaxMP","DamageMax"},
			new string[] {
				CharacterTemplate.Instance.lv.ToString(),
				CharacterTemplate.Instance.expCur.ToString(),
				CharacterTemplate.Instance.force.ToString(),
				CharacterTemplate.Instance.intellect.ToString(),
				CharacterTemplate.Instance.maxHp.ToString(),
				CharacterTemplate.Instance.maxMp.ToString(),
				CharacterTemplate.Instance.damageMax.ToString()
			},"CharacterID","1");
		}
		else
		{
			OperatingDB.Instance.db.UpdateInto("T_Character",
			                                   new string[] {"ExpCur"},
			new string[] {CharacterTemplate.Instance.expCur.ToString()},
			"CharacterID","1");
		}

		SqliteDataReader goldInfo = 
			OperatingDB.Instance.db.Select("T_Money","AccountID","1");
		while(goldInfo.Read())
		{
			CharacterTemplate.Instance.gold = int.Parse(goldInfo[1].ToString());
		}
		CharacterTemplate.Instance.gold += gold;
		OperatingDB.Instance.db.UpdateInto("T_Money",new string[] {"Gold"},
		new string[] {CharacterTemplate.Instance.gold.ToString()},
		"AccountID","1");

		OperatingDB.Instance.db.CloseSqlConnection();
	}



}
