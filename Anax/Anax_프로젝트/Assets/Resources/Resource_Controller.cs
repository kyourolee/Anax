using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource_Controller : MonoBehaviour
{
    [SerializeField] private Resource_Manager resourceManager;
    [SerializeField] private Grid_Manager grid_Manager;
    [SerializeField] private Resource_Data data;

    [Header("Work_cell")]
    public List<Vector3Int> Work_Cell = new List<Vector3Int>();
    [SerializeField] private GameObject[] Cell;

    [Header("Worker")]
    public List<GameObject> Worker = new List<GameObject>();

    [Header("Resource_Setting")]
    [SerializeField] private Slider slider;
    public int Resource_HP;

    private void Awake()
    {
        Vector3Int Pos = grid_Manager.grid.WorldToCell(transform.position);
        grid_Manager.Occupy(Pos, data.Cell_Size);

        for (int i = 0; i < Cell.Length; i++)
        {
            Vector3Int Current_cell = grid_Manager.grid.WorldToCell( Cell[i].transform.position);
            if (!Work_Cell.Contains (Current_cell)) Work_Cell.Add(Current_cell); 
        }

        Resource_HP = data.Resource_HP;

        slider.maxValue = data.Resource_HP;
        slider.value = data.Resource_HP;
    }

    public void Decrease_Resource(int Value)
    {
        if (Value == 0) return;

        Resource_HP = Mathf.Max(0, Resource_HP -= Value);
        slider.gameObject.SetActive(true);
        slider.value = Resource_HP;

        if (Resource_HP == 0)
        {
            gameObject.SetActive(false);
            slider.gameObject.SetActive(false);

            Vector3Int Pos = grid_Manager.grid.WorldToCell(transform.position);
            grid_Manager.Release(Pos, data.Cell_Size);
        }

        resourceManager.Set_wood(data.Add_Wood);
        resourceManager.Set_Rock(data.Add_Rock);
        resourceManager.Set_Food(data.Add_Food);
        resourceManager.Set_Gold(data.Add_Gold);
        resourceManager.Set_Wine(data.Add_Wine);
    }
}
