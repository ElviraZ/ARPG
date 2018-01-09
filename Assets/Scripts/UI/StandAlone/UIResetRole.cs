using UnityEngine;
using System.Collections;
using DevelopEngine;

public class UIResetRole : UIScene {

	private UISceneWidget mButton_Sure;
	private UISceneWidget mButton_Canel;
	private UILabel mLabel_Message;

	protected override void Start () {
		base.Start();
		mButton_Canel = GetWidget("Button_Cancel");
		if(mButton_Canel != null)
			mButton_Canel.OnMouseClick = this.ButtonCancelOnClick;
		mButton_Sure = GetWidget("Button_Sure");
		if(mButton_Sure != null)
			mButton_Sure.OnMouseClick = this.ButtonSureOnClick;
		mLabel_Message = Global.FindChild<UILabel>(transform,"Label_Message");
		if(mLabel_Message != null)
			mLabel_Message.text = Configuration.GetContent("StandAlone","Cover");
	}
	
	private void ButtonCancelOnClick(UISceneWidget eventObj)
	{
		SetVisible(false);
	}
	
	private void ButtonSureOnClick(UISceneWidget eventObj)
	{
		DeteleSaveData();
		UIManager.Instance.SetVisible(UIName.UIRoleName, true);
		SetVisible(false);
	}

	//删除游戏存档
	void DeteleSaveData()
	{
		OperatingDB.Instance.CreateDataBase();
		OperatingDB.Instance.db.UpdateInto("T_Account",new string[] {"AccountName"}
			,new string[] {"''"},"AccountID","1");
		OperatingDB.Instance.db.DeleteContents("T_Character");
		OperatingDB.Instance.db.UpdateInto("T_Money",new string[]{"Gold","Diamond"},
		new string[]{"0","0"},"AccountID","1");
		OperatingDB.Instance.db.CloseSqlConnection();
	}

}
