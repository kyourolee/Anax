using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player_Input : MonoBehaviour
{
    [SerializeField] private Command command;
    [SerializeField] private Selection selection;
    public bool Click;
    public bool Shift;
    public bool Ctrl;

    private void OnClick(InputValue Value)
    {
        Click = Value.isPressed;

        if(Value.isPressed && !Shift)
        {
            selection.Single_Select();
            return;
        }
    }    

    private void OnRightClick(InputValue Value)
    {
        command.command();
    }

    private void OnShift(InputValue Value)
    {
        Shift = Value.isPressed;
    }

    private void OnCtrl(InputValue Value)
    {
        if (Click) return;

        foreach (Transform Ai in selection.selected)
        {
            Ai_StateManager StateManager = Ai.GetComponent<Ai_StateManager>();
            StateManager.Is_Selected = false;
        }
        selection.selected.Clear();
    }    
}
