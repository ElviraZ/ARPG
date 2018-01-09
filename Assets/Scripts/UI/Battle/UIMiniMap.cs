using UnityEngine;
using System.Collections;

public class UIMiniMap : UIScene {

	private UISceneWidget mButton_Closed;
	private UILabel mLabel_Time;

	protected override void Start () {
		base.Start();
		mButton_Closed = GetWidget("Button_Exit");
		if(mButton_Closed != null)
			mButton_Closed.OnMouseClick = this.ButtonClosedOnClick;
		mLabel_Time = Global.FindChild<UILabel>(transform,"Label_Time");
		InvokeRepeating("TimerUpdate",1,1);
	}

	public void SetTime(int time)
	{
		if(mLabel_Time != null)
			mLabel_Time.text = Global.GetMinuteTime(time);
	}

	void TimerUpdate()
	{
		if(Time.timeSinceLevelLoad > 300)
		{
			//显示失败界面
			UIManager.Instance.SetVisible(UIName.UIBattleOver, true);
		}
		else
		{
			SetTime((int)Time.timeSinceLevelLoad);
		}
	}

	private void ButtonClosedOnClick(UISceneWidget eventObj)
	{
		UIManager.Instance.SetVisible(UIName.UIPopup, true);
	}
	

}
