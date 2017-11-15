using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Monster : Role
{
    #region 常量
    public const float CLOSED_DISTANCE = 0.1f;
    float slowdownrate = 0.2f;
    #endregion

    #region 事件
    public event Action<Monster> Reached;
    #endregion

    #region 字段
    public MonsterType MonsterType = MonsterType.Monster0;//怪物类型
    float m_MoveSpeed;//移动速度（米/秒）
    Vector3[] m_Path = null; //路径拐点
    List<Vector3> final_Path = null; // 最终A*算法后的路径
    int m_PointIndex = -1; //当前拐点索引
    bool m_IsReached = false;//是否到达终点
    int m_Price;//gold
    float T = 0;
    bool slowdown = false;
    Map m_map = null; 
    GameObject slow;
    #endregion

    #region 属性
    public int Price
    {
        get { return m_Price; }
        set { m_Price = value; }
    }
    public float MoveSpeed
    {
        get { return m_MoveSpeed; }
        set { m_MoveSpeed = value; }
    }

    #endregion

    #region 方法
    //放慢速度
    public void Slowdown()
    {
        if (this.slowdown)
        {
            T = Time.time;
            return;
        }
            

        this.slow = Game.Instance.ObjectPool.Spawn("Slowdebuff");
        Vector3 pos1 = transform.position;
        pos1.y = pos1.y - 0.2f;
        slow.transform.position = pos1;
        this.MoveSpeed = this.MoveSpeed * this.slowdownrate;
        T = Time.time;
        this.slowdown = true;

    }

    public void Load(Vector3[] path)
    {
        m_Path = path;
        MoveNext();
    }

    bool HasNext()
    {
        return (m_PointIndex + 1) < (m_Path.Length - 1);
    }

    void MoveTo(Vector3 position)
    {
        transform.position = position;
    }

    void MoveNext()
    {
        if (!HasNext())
            return;

        if (m_PointIndex == -1)
        {
            //刚刚出来，那就放置到起点位置
            m_PointIndex = 0;
            MoveTo(m_Path[m_PointIndex]);
        }
        else
        {
            //不然就指定下一个目标位置
            m_PointIndex++;
        }
    }
    #endregion

    #region Unity回调
    void Update()
    {
        //到达了终点
        if (m_IsReached)
            return;
        //当前位置
        Vector3 pos = transform.position;
        //目标位置
        Vector3 dest = m_Path[m_PointIndex + 1];
        //计算距离
        float dis = Vector3.Distance(pos, dest);

        
        /* 利用A*算法寻路
        List<Tile> curPath = new List<Tile>();
        int x = (int)pos.x;
        int y = (int)pos.y;
        Tile start = new Tile(x, y);
        x = (int)dest.x;
        y = (int)dest.y;
        Tile end = new Tile(x, y);
        curPath = PathFind.FindPath(start, end);
        m_map = GetComponent<Map>();
        for (int j = 0; j < curPath.Count; j++)
        {
            pos = m_map.GetPosition(curPath[j]);
            dest = m_map.GetPosition(curPath[j + 1]);
            Debug.Log(pos.x + "," + pos.y);
            Debug.Log(dest.x + "," + dest.y);
            dis = Vector3.Distance(pos, dest);
            if (dis <= CLOSED_DISTANCE)
            {
                //到达拐点
                MoveTo(dest);

                if (HasNext())
                    MoveNext();
                else
                {
                    //到达终点
                    m_IsReached = true;

                    //触发到达终点事件
                    if (Reached != null)
                        Reached(this);
                }
            }
            else
            {
                //移动的单位方向
                Vector3 direction = (dest - pos).normalized;

                //帧移动(米/帧 =  米/秒  * Time.deltaTime)
                transform.Translate(direction * m_MoveSpeed * Time.deltaTime);
            }
        }
        */
        
        if (this.slow == true && this.slow.activeSelf)
        {   
            Vector3 pos1 = transform.position;
            pos1.y = pos1.y - 0.2f;
            this.slow.transform.position = pos1;
       }

        if (this.slowdown && (Time.time - T) >= 3.0f)
        {
            Game.Instance.ObjectPool.Unspawn(slow);
            this.MoveSpeed = this.MoveSpeed / this.slowdownrate;
            this.slowdown = false;

        }
        
        if (dis <= CLOSED_DISTANCE)
        {
            //到达拐点
            MoveTo(dest);

            if (HasNext())
                MoveNext();
            else
            {
                //到达终点
                m_IsReached = true;

                //触发到达终点事件
                if (Reached != null)
                    Reached(this);
            }
        }
        else
        {
            //移动的单位方向
            Vector3 direction = (dest - pos).normalized;

            //帧移动(米/帧 =  米/秒  * Time.deltaTime)
            transform.Translate(direction * m_MoveSpeed * Time.deltaTime);
        }
        
    }


    public static List<Tile> FindPath(Tile start, Tile end)
    {
        Debug.Log("i'm in pathfinding");

        List<Tile> open = new List<Tile>();
        List<Tile> close = new List<Tile>();
        List<Tile> paths = new List<Tile>();
        open.Add(start);

        while (open.Count > 0)
        {

            close.Add(open[0]);
            Tile pendingTile = open[0];
            open.RemoveAt(0);

            for (int i = 0; i < pendingTile.Count; i++)
            {
                Tile current = pendingTile[i];
                if (current == null || current.Equals(start) || !current.CanHold || close.Contains(current))
                {
                    continue;
                }
                int h;
                int g;
                int f;

                //Up Right Down Left
                g = pendingTile.G + 10;
                Debug.Log(current.Pos.X + "," + current.Pos.Y);
                h = (System.Math.Abs(end.Pos.X - current.Pos.X) + System.Math.Abs(end.Pos.Y - current.Pos.Y)) * 10;
                f = h + g;
                if (!open.Contains(current))
                {
                    current.F = f;
                    current.G = g;
                    current.H = h;
                    current.Parent = pendingTile;
                    open.Add(current);
                }
                else
                {
                    if (f < current.F || current.F == 0)
                    {
                        current.G = g;
                        current.H = h;
                        current.F = f;
                        current.Parent = pendingTile;
                        open.Add(current);
                    }
                }

                if (current.Equals(end))
                {
                    Tile path = end;
                    while (path.Parent != null)
                    {
                        paths.Add(path);
                        path = path.Parent;
                    }
                    paths.Reverse();
                    open.Clear();
                    //VisualizePath(paths);
                    return paths;
                }
            }

            open = open.OrderBy(item => item.F).ToList();
        }
        for (int i = 0; i < paths.Count; i++)
        {
            Debug.Log(paths[i].X + "," + paths[i].Y);
        }
        return paths;

    }

    #endregion

    #region 事件回调
    public override void OnSpawn()
    {
        base.OnSpawn();

        MonsterInfo info = Game.Instance.StaticData.GetMonsterInfo((int)MonsterType);
        this.MaxHp = info.Hp;
        this.Hp = info.Hp;
        this.MoveSpeed = info.MoveSpeed;
        this.Price = info.Price;
        this.slowdown = false;
    }

    public override void OnUnspawn()
    {
        base.OnUnspawn();

       if (this.slowdown)
       {
            Game.Instance.ObjectPool.Unspawn(this.slow);
        }
        this.slow = null;
        this.m_Path = null;
        this.m_PointIndex = -1;
        this.m_IsReached = false;
        this.m_MoveSpeed = 0;
        this.Reached = null;
    }
    #endregion

    #region 帮助方法
    #endregion
}       