using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.IO;
using DevelopEngine;

public class OperatingDB : MonoSingleton<OperatingDB> {

	public DbAccess db;	//数据库
	string appDBPath;	//数据库路径

	//打开数据库
	public void CreateDataBase()
	{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
		appDBPath = Application.streamingAssetsPath + "/ARPG.db";
#elif UNITY_ANDROID || UNITY_IPHONE
		appDBPath = Application.persistentDataPath + "/ARPG.db";
		if(!File.Exists(appDBPath))
		{
			StartCoroutine(CopyDataBase());
		}
#endif
		db = new DbAccess ("URI=file:" + appDBPath);
	}

	//拷贝数据库
	IEnumerator CopyDataBase()
	{
		string loadPath = string.Empty;
#if UNITY_ANDROID
		loadPath = "jar:file://" + Application.dataPath + "!/assets" + "/ARPG.db";
#elif UNITY_IPHONE
		loadPath = Application.dataPath + "/Raw" + "/ARPG.db";
#endif
		WWW www = new WWW (loadPath);
		yield return www;
		File.WriteAllBytes(appDBPath, www.bytes);
	}

	//获取名称
	public void GetName()
	{
		CreateDataBase();
		SqliteDataReader name = db.Select("T_Account","AccountID","1");
		while(name.Read())
		{
//			CharacterTemplate.Instance.name = name[1].ToString();
			CharacterTemplate.Instance.name = 
				name.GetValue(name.GetOrdinal("AccountName")).ToString();
		}
		db.CloseSqlConnection();
	}

}
