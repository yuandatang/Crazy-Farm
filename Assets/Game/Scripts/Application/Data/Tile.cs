using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//格子信息
public class Tile
{
    public int X;
    public int Y;
    public bool CanHold; //是否可以放置塔 isWall = !CanHold
    public object Data; //格子所保存的数据
    public bool _isTower = false;
	public bool isCarrot = false;

    public Tile(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public override string ToString()
    {
        return string.Format("[X:{0},Y:{1},CanHold:{2}]",
            this.X,
            this.Y,
            this.CanHold
            );
    }

    public const int _count = 4; //一开始可以走四个方向
    public Tile _parent;
    public int _g;
    public int _f;
    public int _h;

    public Tile _down;
    public Tile _up;
    public Tile _right;
    public Tile _left;
    public bool _isWall;
    public Point _pos;


    public int Count
    {
        get
        {
            return _count;
        }
    }
    public bool isWall
    {
        get
        {
            return !CanHold;
        }
        set
        {
            _isWall = value;
        }
    }
    public bool isTower
    {
        get
        {
            return _isTower;
        }
        set
        {
            _isTower = value;
        }
    }
    public Tile Left
    {
        get
        {
            return _left;
        }
        set
        {
            _left = value;
        }
    }
    public Tile Right
    {
        get
        {
            return _right;
        }
        set
        {
            _right = value;
        }
    }
    public Tile Up
    {
        get
        {
            return _up;
        }
        set
        {
            _up = value;
        }
    }
    public Tile Down
    {
        get
        {
            return _down;
        }
        set
        {
            _down = value;
        }
    }
    public int F
    {
        get
        {
            return _f;
        }
        set
        {
            _f = value;
        }
    }
    public int G
    {
        get
        {
            return _g;
        }
        set
        {
            _g = value;
        }
    }
    public int H
    {
        get
        {
            return _h;
        }
        set
        {
            _h = value;
        }
    }
    public Tile Parent
    {
        get
        {
            return _parent;
        }
        set
        {
            _parent = value;
        }
    }

    public Point Pos
    {
        get
        {
            return _pos;
        }
        set
        {
            _pos = value;
        }
    }


    public Tile(Tile left, Tile right, Tile down, Tile up)
    {
        this.Left = left;
        this.Right = right;
        this.Down = down;
        this.Up = up;
       
    }

    private Tile SwitchTileProp(int index, Tile value = null)
    {
        switch (index)
        {
            case 0:
                //Up
                return ReturnTileProp(ref _up, value);
            
            case 1:
                //Right
                return ReturnTileProp(ref _right, value);
            
            case 2:
                //Down
                return ReturnTileProp(ref _down, value);
           
            case 3:
                //Left
                return ReturnTileProp(ref _left, value);
           
        }
        return null;
    }

    private Tile ReturnTileProp(ref Tile Prop, Tile value = null)
    {
        if (value == null)
        {
            return Prop;
        }
        else
        {
            Prop = value;
            return null;
        }
    }

    public Tile this[int index]
    {
        get
        {
            return SwitchTileProp(index);
        }
        set
        {
            SwitchTileProp(index, value);
        }
    }

}
