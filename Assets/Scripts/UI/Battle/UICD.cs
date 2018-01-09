using UnityEngine;
using System.Collections;

public class UICD : MonoBehaviour {

	private UISprite mSprite_Icon;	//技能icon
	public UISprite mSprite_CD;		//技能冷却
	public int index;
	public bool isSelect;
	public float costSp;

	public void InitWidgets()
	{
		mSprite_Icon = Global.FindChild<UISprite>(transform,"Sprite_Ico");
		mSprite_CD = Global.FindChild<UISprite>(transform,"Sprite_CD");
	}

	public void SetIcon(string icon)
	{
		if(mSprite_Icon != null)
			mSprite_Icon.spriteName = icon;
	}

}
