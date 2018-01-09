using UnityEngine;
using System.Collections;

public class UISelectRole : UIScene {

	private UISceneWidget[] mButton_Jobs;
	UIRoleInfo uiRoleInfo;

	protected override void Start () {
		base.Start();
		uiRoleInfo = UIManager.Instance.GetUI<UIRoleInfo>(UIName.UIRoleInfo);
		mButton_Jobs = GetComponentsInChildren<UISceneWidget>();
		for(int i = 0; i < mButton_Jobs.Length; i++)
		{
			mButton_Jobs[i].OnMouseClick = this.ButtonJobOnClick;
		}
	}

	private void ButtonJobOnClick(UISceneWidget eventObj)
	{
		string jobId = eventObj.name;
		jobId = jobId.Substring(jobId.Length - 1);
		uiRoleInfo.SetJobInfo(jobId);
	}
	

}
