using UnityEngine;
using System.Collections;

public class UIMenu : UIScene {

	private UISceneWidget mButton_Role;
	private UISceneWidget mButton_Skill;

	protected override  void Start () {
		base.Start();
		mButton_Role = GetWidget("Button_Charactor");
		if(mButton_Role != null)
			mButton_Role.OnMouseClick = this.ButtonRoleOnClick;
		mButton_Skill = GetWidget("Button_Skill");
		if(mButton_Skill != null)
			mButton_Skill.OnMouseClick = this.ButtonSkillOnClick;
	}

	private void ButtonRoleOnClick(UISceneWidget eventObj)
	{
		UIManager.Instance.SetVisible(UIName.UIRole, true);
	}

	private void ButtonSkillOnClick(UISceneWidget eventObj)
	{
		UIManager.Instance.SetVisible(UIName.UISkillSelect, true);
	}
	

}
