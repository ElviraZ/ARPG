using UnityEngine;
using System.Collections;

public class UIOther : UIScene {

	private UISceneWidget mButton_Battle;

	protected override void Start () {
		base.Start();
		mButton_Battle = GetWidget("Button_Battle");
		if(mButton_Battle != null)
			mButton_Battle.OnMouseClick = this.ButtonBattleOnClick;
	}

	private void ButtonBattleOnClick(UISceneWidget eventObj)
	{
		//选择关卡
		UIManager.Instance.SetVisible(UIName.UIBattle, true);
	}
	

}
