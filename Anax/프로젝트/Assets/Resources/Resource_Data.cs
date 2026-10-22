using UnityEngine;

[CreateAssetMenu(fileName = "Resource_Data", menuName = "RTS/ Resource_Data")]

public class Resource_Data : ScriptableObject
{
    public Vector2 Cell_Size;

    public int Resource_HP;

    public int Add_Wood;
    public int Add_Rock;
    public int Add_Food;
    public int Add_Gold;
    public int Add_Wine;
}
