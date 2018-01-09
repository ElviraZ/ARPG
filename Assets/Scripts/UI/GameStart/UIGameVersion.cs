using UnityEngine;
using System.Collections;
using DevelopEngine;

public class UIGameVersion : UIScene  {

	private UISceneWidget mButton_StandAlone;	//单击按钮
	private UISceneWidget mButton_Online;		//网络按钮

	protected override void Start () {
		base.Start();
		mButton_StandAlone = GetWidget("Button_StandAlone");
		if(mButton_StandAlone != null)
			mButton_StandAlone.OnMouseClick = this.ButtonStandAloneOnClick;
		mButton_Online = GetWidget("Button_Online");
		if(mButton_Online != null)
			mButton_Online.OnMouseClick = this.ButtonOnlineOnClick;
	}

	private void ButtonOnlineOnClick(UISceneWidget eventObj)
	{
		Debug.Log("网络游戏！");
	}

	private void ButtonStandAloneOnClick(UISceneWidget eventObj)
	{
		Debug.Log("单机游戏！");
		//进入单机游戏场景
		StartCoroutine(GameMain.Instance.LoadScene(
			Configuration.GetContent("Scene","LoadStandAlone")));
	}

}
