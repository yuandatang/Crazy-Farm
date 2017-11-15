using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFind {

    
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
                if(!open.Contains(current))
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
        for(int i=0; i<paths.Count; i++)
        {
            Debug.Log(paths[i].X + "," + paths[i].Y);
        }
        return paths;

    }
    
}
