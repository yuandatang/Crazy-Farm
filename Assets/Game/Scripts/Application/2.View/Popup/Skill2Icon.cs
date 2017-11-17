using UnityEngine;
using System.Collections;

public class Skill2Icon : MonoBehaviour
{
	SpriteRenderer m_Render;
	Vector3 m_CreatePosition;
	GameModel m_gm;
	void Awake()
	{
		m_Render = GetComponent<SpriteRenderer>();
	}

	public void Load(GameModel gm, Vector3 createPostion)
	{
		m_gm = gm;
	}

	void OnMouseDown()
	{
		//		if (!m_gm.Dazhao) {
		//			return;
		//		}
		//点击冷冻技能

		GameObject[] objects = GameObject.FindGameObjectsWithTag("Monster");
		foreach (GameObject go in objects) {
			Monster monster = go.GetComponent<Monster> ();
			//被冷冻的效果
			monster.Slowdown ();
		}
		SendMessageUpwards("Fadazhao",  SendMessageOptions.DontRequireReceiver);

		return;
	}
}