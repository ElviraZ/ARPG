using UnityEngine;
using System.Collections;

public class UIBattleHead : UIScene {
	public static UIBattleHead Instance;

	private UILabel mLabel_Name;
	private UILabel mLabel_Lv;
	private UILabel mLabel_Hp;
	private UILabel mLabel_Mp;
	private UISlider mSlider_Hp;
	private UISlider mSlider_Mp;

	void Awake()
	{
		Instance = this;
	}

	protected override void Start () {
		base.Start();
		mLabel_Name = Global.FindChild<UILabel>(transform,"Label_PlayerName");
		mLabel_Lv = Global.FindChild<UILabel>(transform,"Label_Level");
		mLabel_Hp = Global.FindChild<UILabel>(transform,"Label_HP");
		mLabel_Mp = Global.FindChild<UILabel>(transform,"Label_Magic");
		mSlider_Hp = Global.FindChild<UISlider>(transform,"Bar_HP");
		mSlider_Mp = Global.FindChild<UISlider>(transform,"Bar_Magic");
		InitRoleInfo();
	}

	public void SetHp(float curHp, float maxHp)
	{
		if(mSlider_Hp != null)
			mSlider_Hp.value = curHp / maxHp;
		if(mLabel_Hp != null)
			mLabel_Hp.text = curHp + "/" + maxHp;
	}

	public void SetMp(float curMp, float maxMp)
	{
		if(mSlider_Mp != null)
			mSlider_Mp.value = curMp / maxMp;
		if(mLabel_Mp != null)
			mLabel_Mp.text = curMp + "/" + maxMp;
	}

	void InitRoleInfo()
	{
		if(mLabel_Name != null)
			mLabel_Name.text = CharacterTemplate.Instance.name;
		if(mLabel_Lv != null)
			mLabel_Lv.text = CharacterTemplate.Instance.lv.ToString();
		SetHp(CharacterTemplate.Instance.maxHp,
		      CharacterTemplate.Instance.maxHp);
		SetMp(CharacterTemplate.Instance.maxMp,
		      CharacterTemplate.Instance.maxMp);
	}

}
