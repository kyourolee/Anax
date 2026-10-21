using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.Rendering.DebugUI.Table;

public class Command : MonoBehaviour
{
    [Header("Script")]
    [SerializeField] private Grid_Manager grid_Manager;
    [SerializeField] private Player_Input Player_Input;
    [SerializeField] private Selection selection;

    [Header("Component")]
    [SerializeField] private Camera Cam;
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap ground;

    public void command()
    {
        Vector2 Mouse_Pos = Mouse.current.position.ReadValue();
        Vector3 Pos = Cam.ScreenToWorldPoint(Mouse_Pos);
        Vector3Int Cell_Pos = grid.WorldToCell(Pos);
        if (grid_Manager.GetLevel(Cell_Pos) < 0) return;

        Vector3 target = grid.GetCellCenterWorld(Cell_Pos);
        Vector2 WorldPos = new Vector2(target.x, target.y);
        command_AI(WorldPos);
    }

    private void command_AI(Vector2 target)
    {
        float spacing = grid.cellSize.x;
        int cols = Mathf.CeilToInt(Mathf.Sqrt(selection.selected.Count));

        for (int i = 0  ; i < selection.selected.Count; i++)
        {
            Ai_StateManager StateManager = selection.selected[i].GetComponent<Ai_StateManager>();
            RaycastHit2D ray = Physics2D.Raycast(target, Vector2.zero, 0f, StateManager.data.Layer);
            StateManager.IsWork = false;

            if (StateManager.IsWork && StateManager.data.type != AI_Type.Worker) continue;
            if (ray)
            {
                if (ray.collider.gameObject.layer == 8)
                {
                    GameObject Work_Target = ray.collider.gameObject;
                    Building_Controller building_Script = Work_Target.GetComponent<Building_Controller>();

                    if (i >= building_Script.Work_Cell.Count) continue;
                    if (!grid_Manager.Check_Cell(building_Script.Work_Cell[i]))
                    {
                        StateManager.ChangeState(StateManager.Idle_state);
                        continue;
                    }

                    StateManager.Target_Pos = building_Script.Work_Cell[i];
                    StateManager.Work_Target = building_Script.gameObject;
                    StateManager.ChangeState(StateManager.move_state);

                    if (StateManager.data.type != AI_Type.Worker) continue;
                    StateManager.IsWork = true;
                    continue;
                }
                if (ray.collider.gameObject.layer == 11)
                {
                    GameObject Work_Target = ray.collider.gameObject;
                    Resource_Controller Resource_Script = Work_Target.GetComponent<Resource_Controller>();

                    if (i >= Resource_Script.Work_Cell.Count) continue;
                    if (!grid_Manager.Check_Cell(Resource_Script.Work_Cell[i]))
                    {
                        StateManager.ChangeState(StateManager.Idle_state);
                        continue;
                    }

                    StateManager.Target_Pos = Resource_Script.Work_Cell[i];
                    StateManager.Work_Target = Resource_Script.gameObject;
                    StateManager.ChangeState(StateManager.move_state);

                    if (StateManager.data.type != AI_Type.Worker) continue;
                    StateManager.IsWork = true;
                    continue;
                }
            }
            else
            {
                StateManager.Target_Pos = target;
                StateManager.ChangeState(StateManager.move_state);
                continue;
            }
        }
    }
}
