using UnityEngine;
using System.Collections;
using ARPGSimpleDemo.Skill;
using ARPGSimpleDemo.Character;

public class UIBattleOption : UIScene {

	private UISceneWidget mButton_Attack;	//普通攻击
	private UICD[] cdGrid;		//技能按钮
	private float[] cdTime;		//技能冷却时间
	private float[] nowTime;	//cd
	private UISprite[] skillSprite;	//360切片图片，cd
	GameObject player;			//玩家
	private CharacterSkillManager chSkill;
	private CharacterInputController chInput;
	bool isPress;

	protected override void Start () {
		base.Start();
		cdGrid = GetComponentsInChildren<UICD>();
		skillSprite = new UISprite[cdGrid.Length] ;
		cdTime = new float[cdGrid.Length] ;
		nowTime = new float[cdGrid.Length] ;
		player = GameObject.FindGameObjectWithTag("Player");
		chSkill = player.GetComponent<CharacterSkillManager>();
		chInput = player.GetComponent<CharacterInputController>();

		for(int i = 0; i< cdGrid.Length; i++)
		{
			UISceneWidget mButton_Skill = cdGrid[i].GetComponent<UISceneWidget>();
			if(mButton_Skill != null)
				mButton_Skill.OnMouseClick = this.ButtonSkillOnClick;
			cdGrid[i].InitWidgets();
			cdGrid[i].index = i;
			cdGrid[i].SetIcon(SkillManager.Instance.mSkillInfo[i].skillIcon);
			GetSkillCD(SkillManager.Instance.mSkillInfo[i].id, out cd, out mp);
			cdGrid[i].GetComponent<UISceneWidget>().Throughtime = cd;
			cdTime[i] = nowTime[i] = cd;
			skillSprite[i] = cdGrid[i].mSprite_CD;
			cdGrid[i].costSp = mp;
		}

		mButton_Attack = GetWidget("BaseSkill");
		if(mButton_Attack != null)
			mButton_Attack.OnMousePress = this.ButtonAttackOnPress;

	}

	private void ButtonAttackOnPress(UISceneWidget eventObj, bool isDown)
	{
		isPress = isDown;
	}


	//设置冷却时间
	private void SetCoolTime()
	{
		for(int i = 0; i < skillSprite.Length; i++)
		{
			if(cdGrid[i].isSelect)
			{
				if(nowTime[i] > 0)
				{
					nowTime[i] -= Time.deltaTime;
					skillSprite[i].fillAmount = nowTime[i] / cdTime[i];
				}
				else
				{
					cdGrid[i].isSelect = false;
					nowTime[i] = cdTime[i];
				}
			}
		}
	}

	protected override void Update () {
		base.Update();
		SetCoolTime();
		if(isPress)
			chInput.On_ButtonDown(mButton_Attack.name);
	}

	private void ButtonSkillOnClick(UISceneWidget eventObj)
	{
		if(player.GetComponent<PlayerStatus>().SP < 
		   eventObj.GetComponent<UICD>().costSp) return;
		UICD uicd = eventObj.GetComponent<UICD>();
		uicd.isSelect = true;
		if(SkillManager.Instance.mSkillInfo[uicd.index].id != 0)
		{
			Debug.Log("释放技能");
			chInput.On_ButtonDown("Skill" + 
			                      SkillManager.Instance.mSkillInfo[uicd.index].id);
		}
	}

	float cd,mp;
	//获取cd时间，获取魔法消耗
	private void GetSkillCD(int skillId, out float cd, out float mp)
	{
		cd = chSkill.skills[skillId + 2].coolTime;
		mp = chSkill.skills[skillId + 2].costSP;
	}



}
