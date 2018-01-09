using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using DevelopEngine;

public class UIStandAlone : UIScene {

	private UISceneWidget mButton_Start;
	private UISceneWidget mButton_Continue;

	protected override void Start () {
		base.Start();
		mButton_Start = GetWidget("Button_Start");
		if(mButton_Start != null)
			mButton_Start.OnMouseClick = this.ButtonStartOnClick;
		mButton_Continue = GetWidget("Button_Continue");
		if(mButton_Continue != null)
			mButton_Continue.OnMouseClick = this.ButtonContinueOnClick;
	}

	private void ButtonStartOnClick(UISceneWidget eventObj)
	{
//		UIManager.Instance.SetVisible(UIName.UIResetRole, true);
		GetRoleName();
	}

	private void ButtonContinueOnClick(UISceneWidget eventObj)
	{
//		UIManager.Instance.SetVisible(UIName.UIMessageBox, true);
		GetSaveData();
	}

	//获取游戏存档
	void GetSaveData()
	{
		OperatingDB.Instance.GetName();
		if(CharacterTemplate.Instance.name == string.Empty)
			UIManager.Instance.SetVisible(UIName.UIMessageBox, true);
		else
		{
			GetSave();
			//进入游戏主城
			StartCoroutine(GameMain.Instance.LoadScene(
				Configuration.GetContent("Scene","LoadUIMainCity"),
				Configuration.GetContent("Scene","LoadMainCity")));
		}
	}

	void GetSave()
	{
		OperatingDB.Instance.CreateDataBase();
		SqliteDataReader sqReader = 
			OperatingDB.Instance.db.Select("T_Character","CharacterID","1");
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
		OperatingDB.Instance.db.CloseSqlConnection();
	}


	//获取账号名称
	void GetRoleName()
	{
		OperatingDB.Instance.GetName();
		if(CharacterTemplate.Instance.name == string.Empty)
			UIManager.Instance.SetVisible(UIName.UIRoleName, true);
		else
			UIManager.Instance.SetVisible(UIName.UIResetRole, true);
	}



}
