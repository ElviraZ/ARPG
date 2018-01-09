using UnityEngine;
using System.Collections;
using DevelopEngine;

public class UIBattle : UIScene {

	private UISceneWidget mButton_Close;
	private UISceneWidget mButton_Stage;
	
	protected override  void Start () {
		base.Start();
		mButton_Close = GetWidget("Button_Closed");
		if(mButton_Close != null)
			mButton_Close.OnMouseClick = this.ButtonClosedOnClick;
		mButton_Stage = GetWidget("Button_Stage");
		if(mButton_Stage != null)
			mButton_Stage.OnMouseClick = this.ButtonStageOnClick;
	}
	
	private void ButtonClosedOnClick(UISceneWidget eventObj)
	{
		SetVisible(false);
	}
	
	private void ButtonStageOnClick(UISceneWidget eventObj)
	{
		//进入战斗
		StartCoroutine(GameMain.Instance.LoadScene(
			Configuration.GetContent("Scene","LoadUIBattle"),
			Configuration.GetContent("Scene","LoadBattle")));
	}
}
