using UnityEngine;
using System.Collections;

public class HeroPanel : MonoBehaviour
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	Skill1Icon m_Skill1Icon;
	Skill2Icon m_Skill2Icon;
	#endregion

	#region 属性
	#endregion

	#region 方法
	public void Show(GameModel gm, Vector3 createPosition)
	{
		transform.position = createPosition;

		m_Skill1Icon.Load(gm, createPosition);
		m_Skill2Icon.Load(gm, createPosition);

		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
	#endregion

	#region Unity回调
	void Awake()
	{
		m_Skill1Icon = GetComponentInChildren<Skill1Icon>();
		m_Skill2Icon = GetComponentInChildren<Skill2Icon>();
	}
	#endregion

	#region 帮助方法
	#endregion
}