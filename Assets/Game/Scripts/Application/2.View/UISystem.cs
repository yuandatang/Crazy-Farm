using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISystem : View
{
    #region 常量
    #endregion

    #region 事件
    public Button btnResume;
    public Button btnRestart;
    public Button btnSelect;
    #endregion

    #region 字段
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_Sytem; }
    }
    #endregion

    #region 方法
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public override void RegisterEvents()
    {
        this.AttentionEvents.Add(Consts.E_ShowSystem);
    }

    #endregion

    #region Unity回调
    #endregion
    #region 事件回调

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_ShowSystem:
                Show();
                break;
        }
    }

    public void OnResumeClick()
    {
        Time.timeScale = 1;
        Hide();
    }

    public void OnRestartClick()
    {
        Time.timeScale = 1;
        GameModel gm = GetModel<GameModel>();

        StartLevelArgs e = new StartLevelArgs();
        e.LevelIndex = gm.PlayLevelIndex;
        SendEvent(Consts.E_StartLevel, e);
    }

    public void OnSelectClick()
    {
        Time.timeScale = 1;
        Game.Instance.LoadScene(3);
        Hide();
    }
    #endregion

    #region 帮助方法
    #endregion
}
