using UnityEngine;

public interface Ai_Type_State
{
    void Enter_State(Ai_StateManager StateManger);
    void Update_State(Ai_StateManager StateManger);
    void Exit_State(Ai_StateManager StateManger);
}
