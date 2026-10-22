using UnityEngine;

[CreateAssetMenu(fileName = "Map_Data", menuName = "RTS/ Map_Data")]

public class Map_Data : ScriptableObject
{
    public Vector2 Map_Limit_Pos_X;
    public Vector2 Map_Limit_Pos_Y;

    public int Start_Wood;
    public int Start_Rock;
    public int Start_Food;
    public int Start_Gold;
    public int Start_Wine;
}

