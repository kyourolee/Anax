using UnityEngine;

[CreateAssetMenu(fileName = "Building_Data", menuName = "RTS/ Building_Data")]
public class Building_Data : ScriptableObject
{
    public Vector2 Size;
    public LayerMask Layer;

    public int Need_Wood;
    public int Need_Rock;
    public int Need_Food;
    public int Need_Gold;
    public int Need_Wine;
    public int Get_Population;

    public GameObject building;
    public Sprite building_Sprite;
}
