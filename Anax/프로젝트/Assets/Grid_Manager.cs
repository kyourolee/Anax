using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid_Manager : MonoBehaviour
{
    public static Grid_Manager Instance;
    public HashSet<Vector3Int> occuied = new HashSet<Vector3Int>();

    [Header("TileMap")]
    [SerializeField] private Tilemap[] ground;
    [SerializeField] private Tilemap stair;
    public Grid grid;

    public void Awake()
    {
        Instance = this;
        if (grid == null) grid = GetComponent<Grid>();
    }
    public bool Check_Cell(Vector3Int cell_Pos)
    {
        if (occuied.Contains(cell_Pos)) return false;
        if (GetLevel(cell_Pos) < 0) return false;
        return true;
    }

    public bool CanMove(Vector3Int Cell , Vector3Int target_Cell)
    {
        if (!Check_Cell(target_Cell)) return false;

        int Currnet_Cell = GetLevel(Cell);
        int Next_Cell = GetLevel(target_Cell);

        if (Currnet_Cell < 0 || Next_Cell < 0) return false;
        if (Currnet_Cell == Next_Cell) return true;

        if (Mathf.Abs(Currnet_Cell - Next_Cell) != 1) return false;
        if (Cell.x != target_Cell.x && Cell.y != target_Cell.y) return false;
        return stair.HasTile(Cell) || stair.HasTile(target_Cell);
    }    

    public int GetLevel(Vector3Int Cell)
    {
        for (int i = 0; i < ground.Length; i++)
            if (ground[i].HasTile(Cell)) return i;
        return -1;
    }

    public bool Can_Occupy(Vector3Int cell_Pos, Vector2 size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if (!Check_Cell(cell_Pos + new Vector3Int(x, y, 0)))
                    return false;
            }
        }
        return true;
    }

    public void Occupy(Vector3Int cell_Pos , Vector2 size)
    {
        for (int x = 0; x <  size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                occuied.Add(cell_Pos + new Vector3Int(x, y, 0));
            }
        }
    }

    public void Release(Vector3Int cell_Pos, Vector2 size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                occuied.Remove(cell_Pos + new Vector3Int(x, y, 0));
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.yellow;
        foreach (var cell in occuied)
        {
            Vector3 center = grid.GetCellCenterWorld(cell);
            Gizmos.DrawCube(center, grid.cellSize * 0.9f);
        }
    }
}
