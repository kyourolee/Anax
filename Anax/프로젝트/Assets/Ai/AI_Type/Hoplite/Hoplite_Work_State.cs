using UnityEngine;

public class Hoplite_Work_State : MonoBehaviour, Ai_Type_State
{
    public void Enter_State(Ai_StateManager StateManager)
    {

    }

    public void Update_State(Ai_StateManager StateManager)
    {
    }

    public void Exit_State(Ai_StateManager StateManager)
    {
        StateManager.IsWork = false;
        return;
    }
}
