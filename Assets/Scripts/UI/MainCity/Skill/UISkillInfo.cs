using UnityEngine;
using System.Collections;

public class UISkillInfo : MonoBehaviour {

	private UISprite mSprite_Icon;	//技能icon
	private UILabel mLabel_Name; 	//技能名称
	private UILabel mLabel_Lv;			//技能等级
	private GameObject mSprite_Light;	//蓝色光圈
	public int index;		//索引

	public void InitWidgets()
	{
		mSprite_Icon = Global.FindChild<UISprite>(transform,"Sprite_Ico");
		mLabel_Name = 
			Global.FindChild<UILabel>(transform,"Label_SkillName");
		mLabel_Lv = Global.FindChild<UILabel>(transform,"Label_Level");
		mSprite_Light = Global.FindChild(transform,"Sprite_Light");
	}

	//设置icon
	public void SetSkillIcon(string icon)
	{
		if(mSprite_Icon != null)
			mSprite_Icon.spriteName = icon;
	}

	//获取icon
	public string GetSkillName()
	{
//		if(mSprite_Icon != null)
			return mSprite_Icon.spriteName;
	}

	//设置技能等级
	public void SetSkillLevel(int lv)
	{
		if(mLabel_Lv != null)
			mLabel_Lv.text = lv.ToString();
	}

	//设置技能名称
	public void SetSkillName(string name)
	{
		if(mLabel_Name != null)
			mLabel_Name.text = name;
	}

	//设置蓝色光圈显隐
	public void SetLight(bool isLight)
	{
		if(mSprite_Light != null)
			mSprite_Light.SetActive(isLight);
	}

}
