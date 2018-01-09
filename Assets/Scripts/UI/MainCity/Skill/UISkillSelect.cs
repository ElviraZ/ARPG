using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ARPGSimpleDemo.Skill;
using ARPGSimpleDemo.Character;

public class UISkillSelect : UIScene {

	private UISceneWidget mButton_Closed;	//	关闭按钮
	private UILabel mLabel_SkillDes;					//技能描述
	private UISkillStatus[] skillStatus;	//按钮
	private UISkillInfo[] skillInfo;			//开关
	private List<SkillData> skillData;
	public int skillId;		//技能ID
	bool isSelect = false;

    protected override	void Start () {
		base.Start();
		mButton_Closed = GetWidget("Button_Closed");
		if(mButton_Closed != null)
			mButton_Closed.OnMouseClick = this.ButtonClosedOnClick;
		mLabel_SkillDes = 
			Global.FindChild<UILabel>(transform,"Label_SkillDes");
		skillStatus = GetComponentsInChildren<UISkillStatus>();
		skillInfo = GetComponentsInChildren<UISkillInfo>();
//		Debug.Log("skillStatus.Length:"  + skillStatus.Length);
//		Debug.Log("skillInfo.Length:" + skillInfo.Length);

		for(int i = 0;  i < skillStatus.Length;  i++)
		{
			UISceneWidget mButton_Skill = 
				skillStatus[i].GetComponent<UISceneWidget>();
			if(mButton_Skill != null)
				mButton_Skill.OnMouseClick = this.ButtonSkillOnClick;
			skillStatus[i].InitWidgets();	//初始化按钮
			skillStatus[i].SetIcon(SkillManager.Instance.mSkillInfo[i].skillIcon);
			skillStatus[i].index = i;	//设置索引
		}

		skillData = GameObject.FindGameObjectWithTag("Player").
			GetComponent<CharacterSkillManager>().skills;

		for(int i = 0; i < skillInfo.Length; i++)
		{
			UISceneWidget mButton_Skill = 
				skillInfo[i].GetComponent<UISceneWidget>();
			if(mButton_Skill != null)
				mButton_Skill.OnMouseClick = this.SkillOnClick;
			skillInfo[i].InitWidgets();
			skillInfo[i].SetLight(false);
			skillInfo[i].SetSkillIcon(skillData[i + 3].skillIcon);
			skillInfo[i].SetSkillName(skillData[i + 3].name);
			skillInfo[i].SetSkillLevel(skillData[i + 3].level);
			skillInfo[i].index = i;
		}

		SetLight(false);
	}

	private void SkillOnClick(UISceneWidget eventObj)
	{
		isSelect = true;
		SetLight(true);
		UISkillInfo si = eventObj.GetComponent<UISkillInfo>();
		skillInfo[si.index].SetLight(true);
		skillId = si.index + 1;
		SetSkillDes(skillData[si.index + 3].description);
	}

	private void ButtonSkillOnClick(UISceneWidget eventObj)
	{
		if(!isSelect) return;
		SetLight(false);
		isSelect = false;
		SetSkillDes(string.Empty);
		for(int i = 0; i < skillInfo.Length; i++)
		{
			skillInfo[i].SetLight(false);
		}
		UISkillStatus ss = eventObj.GetComponent<UISkillStatus>();
		SetSkillIcon(ss.index, skillId);
	}

	private void SetSkillIcon(int index, int id)
	{
		SkillInfo si = new SkillInfo ();
		si.id = id;
		si.skillIcon = skillInfo[id - 1].GetSkillName();
		SkillManager.Instance.mSkillInfo.SetValue(si,index);
		skillStatus[index].SetIcon(
			SkillManager.Instance.mSkillInfo[index].skillIcon);
		//过滤技能
		for(int i = 0;  i < skillStatus.Length; i++)
		{
			if(skillStatus[i].GetIcon() == 
			   SkillManager.Instance.mSkillInfo[index].skillIcon &&
			   skillStatus[i] != skillStatus[index])
			{
				SkillManager.Instance.mSkillInfo[i].id = 0;
				SkillManager.Instance.mSkillInfo[i].skillIcon = "HeiSeChenDi";
				skillStatus[i].SetIcon("HeiSeChenDi");
			}
		}
	}
	
	//设置技能描述
	public void SetSkillDes(string des)
	{
		if(mLabel_SkillDes != null)
			mLabel_SkillDes.text = des;
	}

	private void ButtonClosedOnClick(UISceneWidget eventObj)
	{
		SetVisible(false);
	}

	//按钮蓝色光圈显隐
	void SetLight(bool isLight)
	{
		for(int i = 0;  i < skillStatus.Length;  i++)
		{
			skillStatus[i].SetLight(isLight);
		}
	}

}
