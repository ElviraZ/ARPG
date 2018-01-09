using UnityEngine;
using System.Collections;
using DevelopEngine;

public class UIBattleOver : UIScene {

	private UISceneWidget mButton_Closed;

	protected override void Start () {
		base.Start();
		mButton_Closed = GetWidget("Button_Closed");
		if(mButton_Closed != null)
			mButton_Closed.OnMouseClick = this.ButtonCloseOnClick;
	}

	private void ButtonCloseOnClick(UISceneWidget eventObj)
	{
		StartCoroutine(GameMain.Instance.LoadScene(
			Configuration.GetContent("Scene","LoadUIMainCity"),
			Configuration.GetContent("Scene","LoadMainCity")));
	}
	

}
