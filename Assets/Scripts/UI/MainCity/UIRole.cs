using UnityEngine;
using System.Collections;

public class UIRole : UIScene {

	private UILabel[] mRoleInfo;
	private UISceneWidget mButton_Closed;
	private SpinWithMouse[] player;

	protected override void Start () {
		base.Start();
		mRoleInfo = Global.FindChild(transform,"LableRoot").
									GetComponentsInChildren<UILabel>();
		mButton_Closed = GetWidget("Button_Closed");
		if(mButton_Closed != null)
			mButton_Closed.OnMouseClick = this.ButtonClosedOnClick;
		SetRoleInfo();
		player = Global.FindChild(transform, "RoleRoot").
							GetComponentsInChildren<SpinWithMouse>();
		foreach(var item in player)
		{
			item.gameObject.SetActive(false);
		}
		SetPlayer(CharacterTemplate.Instance.jobId);
	}

	void SetPlayer(int jobId)
	{
		player[jobId - 1].gameObject.SetActive(true);
	}

	private void ButtonClosedOnClick(UISceneWidget eventObj)
	{
		SetVisible(false);
	}

	public void SetRoleInfo()
	{
		int i = 0;
		mRoleInfo[i].text = CharacterTemplate.Instance.name;
		mRoleInfo[++i].text = CharacterTemplate.Instance.lv.ToString();
		mRoleInfo[++i].text = CharacterTemplate.Instance.maxHp.ToString();
		mRoleInfo[++i].text = CharacterTemplate.Instance.maxMp.ToString();
		mRoleInfo[++i].text = CharacterTemplate.Instance.expCur.ToString();
		mRoleInfo[++i].text = CharacterTemplate.Instance.force.ToString();
		mRoleInfo[++i].text = CharacterTemplate.Instance.intellect.ToString();
		mRoleInfo[++i].text = CharacterTemplate.Instance.attackSpeed.ToString();
		mRoleInfo[++i].text = CharacterTemplate.Instance.damageMax.ToString();
	}

}
