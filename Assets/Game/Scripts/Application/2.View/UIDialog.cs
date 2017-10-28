using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class UIDialog : View{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	public Image background;
	public Image monster;
	public Image carrot;
	public Text textleft;
	public Text textright;
	int current_stage;
	public event EventHandler<EventArgs> OnClick;
	#endregion
	//testtest
	#region 属性
	public override string Name
	{
		get { return Consts.V_Dialog; }
	}


	#endregion

	#region 方法
	#endregion

	#region Unity回调
	void Awake()
	{
		monster.gameObject.SetActive(true);
		carrot.gameObject.SetActive (true);
		textright.gameObject.SetActive (false);
		textleft.text = "Little carrot, I am going to eat you!";
		current_stage = 0;
		OnClick += Map_OnTileClick;
	}

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			EventArgs e = new EventArgs ();
			if (OnClick != null)
				OnClick (this, e);
		}
	}

	#endregion

	#region 事件回调


	public override void RegisterEvents()
	{
		AttentionEvents.Add(Consts.E_Dialog);
	}

	public override void HandleEvent(string eventName, object data)
	{
		switch (eventName)
		{
			
		}
	}
	void Map_OnTileClick(object sender, EventArgs e)
	{
		current_stage += 1;
		if (current_stage == 1) {
			textleft.gameObject.SetActive (false);
			textright.gameObject.SetActive (true);
			textright.text = "You cannot beat me, I will defense myself use the most powerful weapon!";
		} else if (current_stage == 2) {
			textleft.gameObject.SetActive (true);
			textleft.text = "It's so funny";
			textright.gameObject.SetActive (false);
		} else if (current_stage == 3) {
			textright.gameObject.SetActive (true);
			textleft.gameObject.SetActive (false);
			textright.text = "We will see who is stronger!";
		} else {
			StartLevelArgs ee = new StartLevelArgs()
			{
				LevelIndex = 0
			};
			SendEvent(Consts.E_StartLevel, ee);
		}
	}		
	#endregion

	#region 帮助方法
	#endregion
}
