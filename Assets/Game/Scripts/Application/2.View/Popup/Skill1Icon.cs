using UnityEngine;      
using System.Collections;     
      
public class Skill1Icon : MonoBehaviour       
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
        if(m_gm.Gold <20)
        {
            string path = "Res/Roles/Slow/snail";
            m_Render.sprite = Resources.Load<Sprite>(path);
              
        }
  }       
      
  void OnMouseDown()
  {

        if (m_gm.Gold >= 20)
        {
            m_gm.Gold -= 20;
        }
        else{
            return;
        }
      GameObject[] objects = GameObject.FindGameObjectsWithTag("Monster");        
      foreach (GameObject go in objects) {        
          Monster monster = go.GetComponent<Monster>();       
          //被冷冻的效果        
          monster.Slowdown();     
      }       
      SendMessageUpwards("Fadazhao", SendMessageOptions.DontRequireReceiver);     
      
      
      return;     
  }       
} 