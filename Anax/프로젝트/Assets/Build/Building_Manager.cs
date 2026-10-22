using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Building_Manager : MonoBehaviour
{
    [Header("Script")]
    [SerializeField] private Game_Manager game_manager;
    [SerializeField] private Resource_Manager resource_manager;
    [SerializeField] private Grid_Manager grid_Manager;
    [SerializeField] private Sound_Manager sound_Manager;

    [Header("Component")]
    private Building_Data building_data;
    private GameObject Current_Building;
    private Vector2 building_Tile;
    public Camera Cam;
    public Grid grid;

    [Header("Preview_Component")]
    [SerializeField] private GameObject Build_Preview;
    private Vector3Int Preview_Cell_Pos;

    [Header("Preview_Color")]
    [SerializeField] private Color32 Can_Color;
    [SerializeField] private Color32 Cant_Color;

    [Header("audio")]
    [SerializeField] private AudioClip build;
    [SerializeField] private AudioClip Cancel;

    public bool Build_mode = false;

    private void Update()
    {
        if (!Build_mode) return;

        Preview_Cell_Pos = Check_Pos();
        Build_Preview.transform.position = grid.GetCellCenterWorld(Preview_Cell_Pos);

        SpriteRenderer renderer = Build_Preview.GetComponent<SpriteRenderer>();
        renderer.color =  grid_Manager.Can_Occupy(Preview_Cell_Pos, building_Tile) ? Can_Color : Cant_Color;
    }

    public void Get_Data(Building_Data data)
    {
        Build_mode = !Build_mode;
        building_data = data;

        if (!Build_mode)
        {
            Build_Preview.SetActive(false);
            return;
        }

        SpriteRenderer renderer = Build_Preview.GetComponent<SpriteRenderer>();
        renderer.sprite = building_data.building_Sprite;
        building_Tile = building_data.Size;
        Current_Building = building_data.building;

        Build_Preview.SetActive(true);
    }
    private void OnClick()
    {
        if (!Build_mode) return;
        Build_mode = false;

        Vector3Int Cell_Pos = Check_Pos();

        if (!grid_Manager.Can_Occupy(Cell_Pos, building_Tile))
        {
            Cancel_build();
            sound_Manager.Play_SFX(Cancel);
            return;
        }
        if (!Check_Resource())
        {
            Cancel_build();
            sound_Manager.Play_SFX(Cancel);
            return;
        }

        resource_manager.Set_wood(-building_data.Need_Wood);
        resource_manager.Set_Rock(-building_data.Need_Rock);
        resource_manager.Set_Food(-building_data.Need_Food);
        resource_manager.Set_Gold(-building_data.Need_Gold);
        resource_manager.Set_Wine(-building_data.Need_Wine);

        Building(Cell_Pos);
        Cancel_build();
    }

    private Vector3Int Check_Pos()
    {
        Vector2 Mouse_Pos = Mouse.current.position.ReadValue();
        Vector3 Pos = Cam.ScreenToWorldPoint(Mouse_Pos);
        return grid.WorldToCell(Pos);
    }

    private bool Check_Resource()
    {
        if (building_data.Need_Wood > resource_manager.Wood) return false;
        if (building_data.Need_Rock > resource_manager.Rock) return false;
        if (building_data.Need_Food > resource_manager.Food) return false;
        return true;
    }

    private void Cancel_build()
    {
        Current_Building = null;
        Build_Preview.gameObject.SetActive(false);

        SpriteRenderer renderer = Build_Preview.GetComponent<SpriteRenderer>();
        renderer.sprite = null;
    }

    private void Building(Vector3Int Cell_Pos)
    {
        grid_Manager.Occupy(Cell_Pos, building_Tile);

        Vector3 pos = grid.GetCellCenterWorld(Cell_Pos);
        GameObject building = Instantiate(Current_Building, pos, Quaternion.identity);

        Building_Controller building_Scrit = building.GetComponent<Building_Controller>();
        building_Scrit.game_manager = game_manager;
        building_Scrit.grid_Manager = grid_Manager;
        building_Scrit.Set_Work_Cell(building_data);

        sound_Manager.Play_SFX(build);
        Current_Building = null;
    }    
}
