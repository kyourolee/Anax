using UnityEngine;
using System.Collections.Generic;

class Node
{
    public Vector3Int cell;
    public Node parent;
    public int g;
    public int h;
    public int f => g + h;
}

public static class Pathfinder
{
    public static List<Vector3Int> FindPath(Grid_Manager grid , Vector3Int start , Vector3Int end)
    {
        List<Node> open = new List<Node>();
        Dictionary<Vector3Int , Node> inpo = new Dictionary<Vector3Int, Node>();
        HashSet<Vector3Int> closed = new HashSet<Vector3Int>();
        Node Start_Path = new Node
        {
            cell = start,
            g = 0,
            h = GetDistance(start, end),
        };

        open.Add(Start_Path);
        inpo[start] = Start_Path;

        while (open.Count > 0)
        {
            Node current = open[0];

            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].f < current.f || (open[i].f == current.f && open[i].h < current.h))
                    current = open[i];
            }

            if (current.cell == end)
            {
                List<Vector3Int> path = new List<Vector3Int>();
                for (Node n = current; n != null; n = n.parent)
                    path.Add(n.cell);

                path.Reverse();
                return path;
            }

            open.Remove(current);
            closed.Add(current.cell);

            foreach (Vector3Int Direction in Cell)
            {
                bool Diagonal = Direction.x != 0 && Direction.y != 0;

                if (Diagonal && (!grid.Check_Cell(current.cell + new Vector3Int(Direction.x, 0))
                    || (!grid.Check_Cell(current.cell + new Vector3Int(0, Direction.y)))))
                    {
                    continue;
                }

                Vector3Int next = current.cell + Direction;

                if (!grid.CanMove(current.cell , next)) continue;

                if (closed.Contains(next)) continue;

                int G = current.g +(Diagonal ? 14 : 10);

                if (inpo.TryGetValue(next,out Node node))
                {
                    if (G >= node.g) continue;
                }
                else
                {
                    node = new Node
                    { 
                      cell = next,
                      h = GetDistance(next, end)
                    };

                    inpo[next] = node;
                    open.Add(node);
                }
                node.g = G;
                node.parent = current;
            }
        }
        return null;
    }

    static int GetDistance(Vector3Int start, Vector3Int end)
    {
        int X = Mathf.Abs(start.x - end.x);
        int Y = Mathf.Abs(start.y - end.y);

        int diagonal = Mathf.Min(X, Y);
        int straight = Mathf.Max(X, Y) - diagonal;

        return diagonal * 14 + straight * 10;
    }

    static readonly Vector3Int[] Cell =
    {
        new Vector3Int(1, 0) ,new Vector3Int(-1, 0),
        new Vector3Int(0, 1) ,new Vector3Int(0, -1),
        new Vector3Int(1, 1) ,new Vector3Int(1, -1),
        new Vector3Int(-1, 1),new Vector3Int(-1, -1),
    };
}
