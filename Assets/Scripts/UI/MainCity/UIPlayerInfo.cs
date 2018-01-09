using UnityEngine;
using System.Collections;

public class UIPlayerInfo : UIScene {

	private UILabel mLabel_Name;
	private UILabel mLabel_Lv;
	private UILabel mLabel_Hp;
	private UILabel mLabel_Mp;

	protected override void Start () {
		base.Start();
		mLabel_Name = Global.FindChild<UILabel>(transform, "Label_PlayerName");
		mLabel_Lv = Global.FindChild<UILabel>(transform, "Label_Level");
		mLabel_Hp = Global.FindChild<UILabel>(transform, "Label_HP");
		mLabel_Mp = Global.FindChild<UILabel>(transform, "Label_Magic");
		SetRoleInfo();
	}

	public void SetRoleInfo()
	{
		if(mLabel_Name != null)
			mLabel_Name.text = CharacterTemplate.Instance.name;
		if(mLabel_Lv != null)
			mLabel_Lv.text = CharacterTemplate.Instance.lv.ToString();
		if(mLabel_Hp != null)
			mLabel_Hp.text = CharacterTemplate.Instance.maxHp.ToString();
		if(mLabel_Mp != null)
			mLabel_Mp.text = CharacterTemplate.Instance.maxMp.ToString();
	}

	

}
