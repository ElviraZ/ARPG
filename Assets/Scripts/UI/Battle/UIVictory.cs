using UnityEngine;
using System.Collections;
using DevelopEngine;

public class UIVictory : UIScene {
	public static UIVictory Instance;

	private UISceneWidget mButton_Exit;
	private UILabel mLabel_Exp;
	private GameObject[] mStar;

	void Awake()
	{
		Instance = this;
	}

	protected override void Start () {
		base.Start();
		InitWidgets();
		SetStar(3);
	}

	void InitWidgets()
	{
		mButton_Exit = GetWidget("Button_Closed");
		if(mButton_Exit != null)
			mButton_Exit.OnMouseClick = this.ButtonExitOnClick;
		mLabel_Exp = 
			Global.FindChild<UILabel>(transform,"Label_GetExpValue");
		mStar = new GameObject[3] ;
		for(int i = 0; i < mStar.Length; i++)
		{
			mStar[i] = Global.FindChild(transform,"Sprite_Star" + (i+1));
		}
	}

	public void SetStar(int star)
	{
		if(star > 3 || star < 1)
		{
			for(int i = 0; i < 3; i++)
			{
				mStar[i].SetActive(false);
			}
		}
		else
		{
			for(int i = 0; i < 3; i++)
			{
				if(i < star)
					mStar[i].SetActive(true);
				else
					mStar[i].SetActive(false);
			}
		}
	}

	public void SetExp(int exp)
	{
		if(mLabel_Exp != null)
			mLabel_Exp.text = exp.ToString();
	}

	private void ButtonExitOnClick(UISceneWidget eventObj)
	{
		StartCoroutine(GameMain.Instance.LoadScene(
			Configuration.GetContent("Scene","LoadUIMainCity"),
			Configuration.GetContent("Scene","LoadMainCity")));
	}


}
