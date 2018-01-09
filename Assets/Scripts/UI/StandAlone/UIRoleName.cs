using UnityEngine;
using System.Collections;
using DevelopEngine;

public class UIRoleName : UIScene {

	private UISceneWidget mButton_Exit;
	private UISceneWidget mButton_Sure;
	private UIInput mInput_Name;

	protected override void Start () {
		base.Start();
		mButton_Exit = GetWidget("Button_Exit");
		if(mButton_Exit != null)
			mButton_Exit.OnMouseClick = this.ButtonExitOnClick;
		mButton_Sure = GetWidget("Button_OK");
		if(mButton_Sure != null)
			mButton_Sure.OnMouseClick = this.ButtonSureOnClick;
		mInput_Name = Global.FindChild<UIInput>(transform,"Input_Name");
	}
	
	private void ButtonExitOnClick(UISceneWidget eventObj)
	{
		SetVisible(false);
	}
	
	private void ButtonSureOnClick(UISceneWidget eventObj)
	{
		CharacterTemplate.Instance.name = mInput_Name.value;
		Debug.Log("进入创建角色场景！");
		StartCoroutine(GameMain.Instance.LoadScene(
			Configuration.GetContent("Scene","LoadCreatRole")));
	}

}
