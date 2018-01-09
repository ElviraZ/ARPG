using UnityEngine;
using System.Collections;
using ARPGSimpleDemo.Character;
using ARPGSimpleDemo.Skill;
using Mono.Data.Sqlite;
using System;

public class CharacterManager : MonoBehaviour {

	//动态加载模型
	void Awake()
	{
		GameObject player = Instantiate(Resources.Load(
			CharacterTemplate.Instance.jobModel),
                transform.position,transform.rotation) as GameObject;
		player.transform.parent = transform;
		InitRoleInfo();
		InitSkill(CharacterTemplate.Instance.jobId);
	}

	//初始化角色基础信息
	void InitRoleInfo()
	{
		PlayerStatus ps = GetComponent<PlayerStatus>();
		ps.level = CharacterTemplate.Instance.lv;
		ps.exp = CharacterTemplate.Instance.expCur;
		ps.force = CharacterTemplate.Instance.force;
		ps.intellect = CharacterTemplate.Instance.intellect;
		ps.attackSpeed = CharacterTemplate.Instance.attackSpeed;
		ps.HP = ps.MaxHP = CharacterTemplate.Instance.maxHp;
		ps.SP = ps.MaxSP = CharacterTemplate.Instance.maxMp;
		ps.damage = CharacterTemplate.Instance.damageMax;
	}

	//初始化技能
	void InitSkill(int jobId)
	{
		GetComponent<CharacterSkillManager>().skills.Clear();
		OperatingDB.Instance.CreateDataBase();
		SqliteDataReader skill = OperatingDB.Instance.db.ReadFullTable("T_Skill" + jobId);
		while(skill.Read())
		{
			int i = 1;
			SkillData sd = new SkillData ();
			Type t = typeof(SkillData);
			foreach(var item in t.GetProperties())
			{
				if(item.PropertyType.Equals(typeof(string)))	//字符串类型
					item.SetValue(sd,skill[i].ToString(),null);
				else if(item.PropertyType.Equals(typeof(float)))//浮点
					item.SetValue(sd, float.Parse(skill[i].ToString()),null);
				else if(item.PropertyType.Equals(typeof(string[])))
				{
					//字符串切割
					string[] str = skill[i].ToString().Split(',');
					Debug.Log("str.Length:" + str.Length);
					item.SetValue(sd, str, null);
				}
				else//整型，枚举
				{
					item.SetValue(sd, int.Parse(skill[i].ToString()),null);
				}
				i++;
			}
			GetComponent<CharacterSkillManager>().skills.Add(sd);
		}
		OperatingDB.Instance.db.CloseSqlConnection();
	}



}
