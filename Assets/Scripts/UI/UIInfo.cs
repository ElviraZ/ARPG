using UnityEngine;
using System.Collections;

public class UIInfo : MonoBehaviour {

	private UIManager uiManager;

	void Start () {
		Object obj = FindObjectOfType(typeof(UIManager));
		if(obj != null)
			uiManager = obj as UIManager;
		if(uiManager == null)
		{
			GameObject manager = new GameObject ("UIManager");
			uiManager = manager.AddComponent<UIManager>();
		}
		uiManager.InitializeUIs();//初始化UIManager
		uiManager.SetGameStartVisible(true);//显示开始游戏场一级界面
		uiManager.SetStandAloneVisible(true);
		uiManager.SetCreateRoleVisible(true);
		uiManager.SetMainCityVisible(true);
		uiManager.SetBattleVisisble(true);
	}
	

}
