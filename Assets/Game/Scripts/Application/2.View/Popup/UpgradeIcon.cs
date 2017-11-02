using UnityEngine;
using System.Collections;

public class UpgradeIcon : MonoBehaviour
{
    SpriteRenderer m_Render;
    Tower m_Tower;
    bool m_Enough = false;
    void Awake()
    {
        m_Render = GetComponent<SpriteRenderer>();
    }

    public void Load(GameModel gm, Tower tower)
    {
        m_Tower = tower;
        m_Enough = gm.Gold >= tower.BasePrice;
        //图标
        TowerInfo info = Game.Instance.StaticData.GetTowerInfo(tower.ID);
        string path = "Res/Roles/" + ((tower.IsTopLevel || !m_Enough) ? info.DisabledIcon : info.NormalIcon);
        m_Render.sprite = Resources.Load<Sprite>(path);
    }

    void OnMouseDown()
    {
        if (m_Tower.IsTopLevel || !m_Enough)
            return;
        UpgradeTowerArgs e = new UpgradeTowerArgs()
        {
            tower = m_Tower
        };
        SendMessageUpwards("UpgradeTower", e, SendMessageOptions.DontRequireReceiver);
    }
}