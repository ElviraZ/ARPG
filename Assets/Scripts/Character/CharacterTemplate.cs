using UnityEngine;
using System.Collections;
using DevelopEngine;

public class CharacterTemplate : MonoSingleton<CharacterTemplate> {

	public string name;	//名称
	public int jobId;			//职业ID
	public int accountId;	//账号ID
	public int characterId;//角色ID
	public int lv = 1;			//等级
	public int expCur = 1;	//经验
	public int force;			//力量
	public int intellect;		//智力
	public int attackSpeed = 5;//攻击速度
	public int maxHp;			//血量
	public int maxMp;		//魔法
	public int damageMax;//攻击
	public string jobModel;//角色模型

	public int diamond;		//钻石
	public int gold;				//金币

}
