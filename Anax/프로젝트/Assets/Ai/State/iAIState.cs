using UnityEngine;


public interface iAIState
{
    void Enter_State(Ai_StateManager StateManger);
    void Update_State(Ai_StateManager StateManger);
    void Exit_State(Ai_StateManager StateManger);
}

