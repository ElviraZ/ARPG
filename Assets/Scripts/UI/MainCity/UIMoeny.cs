using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;

public class UIMoeny : UIScene {

	private UILabel mLabel_Money1;
	private UILabel mLabel_Money2;

	protected override void Start () {
		base.Start();
		mLabel_Money1 = Global.FindChild<UILabel>(transform,"Label_Money1Value");
		mLabel_Money2 = Global.FindChild<UILabel>(transform,"Label_Money2Value");
		InitMoney();
	}

	public void SetGold(int gold)
	{
		if(mLabel_Money1 != null)
			mLabel_Money1.text = gold.ToString();
	}

	void InitMoney()
	{
		//读取数据库
		OperatingDB.Instance.CreateDataBase();
		SqliteDataReader money = 
			OperatingDB.Instance.db.Select("T_Money","AccountID","1");
		while(money.Read())
		{
//			CharacterTemplate.Instance.gold = int.Parse(money[1].ToString());
			SetGold(int.Parse(money[1].ToString()));
		}
		OperatingDB.Instance.db.CloseSqlConnection();
	}

//	public void SetDiamond(int diamond)
//	{
//
//	}
	

}
