using UnityEngine;
using System.Collections;
using DevelopEngine;
using Mono.Data.Sqlite;

public class UIRoleInfo : UIScene {

	private UISceneWidget mButton_Create;
	private UILabel mLabel_Name;
	private UILabel mLabel_Weapon;
	private UILabel mLabel_Job;
	private UILabel mLabel_Des;

	protected override void Start () {
		base.Start();
		InitWidget();
		SetJobInfo("2");
		}
	
	//初始化窗口部件
	void InitWidget()
	{
		mButton_Create = GetWidget("Button_Creat");
		if(mButton_Create != null)
			mButton_Create.OnMouseClick = this.ButtonCreateOnClick;
		mLabel_Name = Global.FindChild<UILabel>(transform,"Label_RoleName");
		mLabel_Weapon = Global.FindChild<UILabel>(transform,"Label_Weapon");
		mLabel_Job = Global.FindChild<UILabel>(transform,"Label_Job");
		mLabel_Des = Global.FindChild<UILabel>(transform,"Label_Des");
		if(mLabel_Name != null)
			mLabel_Name.text = CharacterTemplate.Instance.name;
	}

	private void ButtonCreateOnClick(UISceneWidget eventObj)
	{
		Debug.Log("创建角色!");
		CreateSaveData(CharacterTemplate.Instance.jobId);
		//进入游戏主城
		StartCoroutine(GameMain.Instance.LoadScene(
			Configuration.GetContent("Scene","LoadUIMainCity"),
			Configuration.GetContent("Scene","LoadMainCity")));
	}

	//设置角色信息
	public void SetJobInfo(string id)
	{
		CharacterTemplate.Instance.jobId = int.Parse(id);
		if(mLabel_Job != null)
			mLabel_Job.text = Configuration.GetContent("Job","Job" + id);
		if(mLabel_Weapon != null)
			mLabel_Weapon.text = Configuration.GetContent("Job","Weapon" + id);
		if(mLabel_Des != null)
			mLabel_Des.text = Configuration.GetContent("Job","Description" + id);
	}

	//生成游戏存档
	void CreateSaveData(int jobId)
	{
		OperatingDB.Instance.CreateDataBase();
		//写入游戏账号
		OperatingDB.Instance.db.UpdateInto("T_Account", new string[] {"AccountName"}
		,new string[] {"'" + CharacterTemplate.Instance.name + "'"},"AccountID","1");
		SqliteDataReader sqReader = 
			OperatingDB.Instance.db.Select("T_Job","JobID",jobId.ToString());
		while(sqReader.Read())
		{
			int i = 0;
			CharacterTemplate.Instance.jobId = int.Parse(sqReader[i].ToString());
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
		OperatingDB.Instance.db.InsertInto("T_Character", new string[] {"1",
			CharacterTemplate.Instance.jobId.ToString(),
			CharacterTemplate.Instance.lv.ToString(),
			CharacterTemplate.Instance.expCur.ToString(),
			CharacterTemplate.Instance.force.ToString(),
			CharacterTemplate.Instance.intellect.ToString(),
			CharacterTemplate.Instance.attackSpeed.ToString(),
			CharacterTemplate.Instance.maxHp.ToString(),
			CharacterTemplate.Instance.maxMp.ToString(),
			CharacterTemplate.Instance.damageMax.ToString(),
			"'" + CharacterTemplate.Instance.jobModel.ToString() + "'"
		});
		OperatingDB.Instance.db.UpdateInto("T_Money",new string[] {"Gold","Diamond"}
			,new string[] {"0","0"}, "AccountID", "1");
		OperatingDB.Instance.db.CloseSqlConnection();
	}


}
