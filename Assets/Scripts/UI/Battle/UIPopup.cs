﻿using UnityEngine;
using System.Collections;
using DevelopEngine;

public class UIPopup : UIScene {

	private UISceneWidget mButton_Sure;
	private UISceneWidget mButton_Canel;
	private UILabel mLabel_Message;
	private UILabel mLabel_Tips;

	protected override void Start () {
		base.Start();
		mButton_Sure = GetWidget("Button_Sure");
		if(mButton_Sure != null)
			mButton_Sure.OnMouseClick = this.ButtonSureOnClick;
		mButton_Canel = GetWidget("Button_Cancel");
		if(mButton_Canel != null)
			mButton_Canel.OnMouseClick = this.ButtonCanelOnClick;
		mLabel_Message = Global.FindChild<UILabel>(transform,"Label_Message");
		mLabel_Tips = Global.FindChild<UILabel>(transform,"Label_Tips");
		mLabel_Message.text = Configuration.GetContent("Battle","Message1");
		mLabel_Tips.text = Configuration.GetContent("Battle","Message2");
	}

	private void ButtonSureOnClick(UISceneWidget eventObj)
	{
		//进入主城
		StartCoroutine(GameMain.Instance.LoadScene(
			Configuration.GetContent("Scene","LoadUIMainCity"),
			Configuration.GetContent("Scene","LoadMainCity")));
	}
	
	private void ButtonCanelOnClick(UISceneWidget eventObj)
	{
		SetVisible(false);
	}
}
