using UnityEngine;
using System.Collections;

public class UISkillStatus : MonoBehaviour {

	private UISprite mSprite_Icon;	//技能icon
	private GameObject mSprite_Light;//蓝色光圈
	public int index;	//索引

	public void InitWidgets()
	{
		mSprite_Icon = Global.FindChild<UISprite>(transform,"Sprite_Ico");
		mSprite_Light = Global.FindChild(transform, "Sprite_Light");
	}

	//设置icon
	public void SetIcon(string icon)
	{
		if(mSprite_Icon != null)
			mSprite_Icon.spriteName = icon;
	}

	//获取icon
	public string GetIcon()
	{
		return mSprite_Icon.spriteName;
	}

	public void SetLight(bool isLight)
	{
		if(mSprite_Light != null)
			mSprite_Light.SetActive(isLight);
	}

}
