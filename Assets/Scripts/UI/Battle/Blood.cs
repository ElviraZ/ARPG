using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour {

	public Transform target;	//血条挂点
	public GameObject prefab;	//血条预设
	GameObject blood;
	UIFollowTarget uiFollowTarget;
	UISlider bloodSlider;
	bool isBlood;

	void Start () {
		StartCoroutine(InitHUD());
	}

	IEnumerator InitHUD()
	{
		while(!isBlood)
		{
			HUDRoot.go = GameObject.FindGameObjectWithTag("HUD");
			if(HUDRoot.go == null)
				yield return new WaitForSeconds(0.1f);
			else
			{
				isBlood = true;
				//初始化血条
				InitBlood();
			}
		}
	}

	void InitBlood()
	{
		blood = NGUITools.AddChild(HUDRoot.go, prefab);
		uiFollowTarget = blood.AddComponent<UIFollowTarget>();
		uiFollowTarget.target = target;
		//设定3d相机、ui相机
		uiFollowTarget.gameCamera = Camera.main;
		uiFollowTarget.uiCamera = 
			blood.transform.parent.parent.parent.GetComponent<Camera>();
		bloodSlider = blood.GetComponentInChildren<UISlider>();
	}

	//设置血条
	public void SetBlood(float curhp, float maxhp)
	{
		if(curhp <= 0)
			Destroy(blood, 1);
		else
			bloodSlider.value = curhp	/ maxhp;
	}

}
