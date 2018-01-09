using UnityEngine;
using System.Collections;
using DevelopEngine;

public class GameMain : MonoSingleton<GameMain> {

	public readonly string configPath = "/config.txt";
	AsyncOperation async;

	IEnumerator Start () {
		//限制游戏帧数
		Application.targetFrameRate = 45;
		//读取配置文件
		Configuration.LoadConfig(configPath);
		while(!Configuration.IsDone)
			yield return null;
		SkillManager.Instance.InitSkill();//初始化技能
		DontDestroyOnLoad(gameObject);
		//切换场景
		StartCoroutine(LoadScene(
			Configuration.GetContent("Scene","LoadGameStart")));
	}

	//加载进度条场景
	IEnumerator Load()
	{
		async = Application.LoadLevelAsync(
			Configuration.GetContent("Scene","Loading"));
		yield return async;
	}

	//加载UI场景
	public IEnumerator LoadScene(string uiScene)
	{
		Global.Contain3DScene = false;
		Global.LoadUIName = uiScene;
		yield return StartCoroutine(Load());
	}

	//加载3D+UI场景
	public IEnumerator LoadScene(string uiScene, string scene)
	{
		Global.Contain3DScene = true;
		Global.LoadUIName = uiScene;
		Global.LoadSceneName = scene;
		yield return StartCoroutine(Load());
	}
	

}
