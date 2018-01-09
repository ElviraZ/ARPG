using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

	public UIProgressBar mProgress;	//进度条
	AsyncOperation async;

	IEnumerator Start () {
		//如果加载的场景有3d场景
		if(Global.Contain3DScene)
		{
			//先加载3d场景
			async = Application.LoadLevelAsync(Global.LoadSceneName);
			//然后追加UI场景
			async = Application.LoadLevelAdditiveAsync(Global.LoadUIName);
		}
		//如果没有3d场景
		else
		{
			//只加载UI场景
			async = Application.LoadLevelAsync(Global.LoadUIName);
		}
		yield return async;
	}

	void Update () {
		mProgress.value = async.progress;	//更新进度条
	}
}
