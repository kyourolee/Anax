using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Building_Controller : MonoBehaviour
{
    [Header("Script")]
    public Grid_Manager grid_Manager;
    public Game_Manager game_manager;
    private Building_Data data;

    [Header("building_setting")]
    public bool isBuilt;
    public int Max_Building_HP;
    public int Building_HP;

    [Header("Work_cell")]
    [SerializeField] private GameObject[] Cell;
    public List<Vector3Int> Work_Cell = new List<Vector3Int>();
    public GameObject Spawn_Cell;

    [Header("Unit")]
    public List<GameObject> Worker = new List<GameObject>();
    public Ai_Data[] ai_data;

    [Header("Resource_Setting")]
    [SerializeField] private Slider slider;

    public void Update()
    {
        if (Max_Building_HP == Building_HP) isBuilt = true;
        if (isBuilt)
        {
            if (!game_manager.Building.Contains(gameObject))
            {
                Add_Current_building();
            }
            slider.gameObject.SetActive(false);
        }
        else
        {
            slider.maxValue = Max_Building_HP;
            slider.value = Building_HP;
            slider.gameObject.SetActive(true);
        }
    }

    public void Add_Current_building()
    {
        game_manager.Building.Add(gameObject);
        if (game_manager.Building.Count == 1) game_manager.music_Manager.Play_Greece();
        game_manager.resource_manager.Set_Population(data.Get_Population);
    }

    public void Set_Work_Cell(Building_Data Data)
    {
        data = Data;

        for (int i = 0; i < Cell.Length; i++)
        {
            Vector3Int Current_cell = grid_Manager.grid.WorldToCell(Cell[i].transform.position);
            if (!Work_Cell.Contains(Current_cell)) Work_Cell.Add(Current_cell);
        }
    }

    public void Del_building()
    {
        game_manager.resource_manager.Set_wood(data.Need_Wood);
        game_manager.resource_manager.Set_Rock(data.Need_Rock);
        game_manager.resource_manager.Set_Food(data.Need_Food);
        game_manager.resource_manager.Set_Gold(data.Need_Gold);
        game_manager.resource_manager.Set_Wine(data.Need_Wine);

        Vector3Int Pos = grid_Manager.grid.WorldToCell(transform.position);
        grid_Manager.Release(Pos, data.Size);
        Destroy(gameObject);
    }
}
