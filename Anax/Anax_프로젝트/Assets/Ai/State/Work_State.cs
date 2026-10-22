using UnityEngine;

public class Work_State : iAIState
{
    public void Enter_State(Ai_StateManager StateManager)
    {
        StateManager.Current_Work_State.Enter_State(StateManager);
    }

    public void Update_State(Ai_StateManager StateManager)
    {
        StateManager.Update_Work();
    }

    public void Exit_State(Ai_StateManager StateManager)
    {
        StateManager.Current_Work_State.Exit_State(StateManager);
    }
}
