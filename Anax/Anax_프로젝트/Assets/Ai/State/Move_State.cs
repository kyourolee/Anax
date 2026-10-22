using UnityEngine;

public class Move_State : iAIState
{
    public void Enter_State(Ai_StateManager StateManger)
    {
        StateManger.A = null;
        StateManger.pathNum = 0;
        Vector3Int StartPos = StateManger.grid_Manger.grid.WorldToCell(StateManger.transform.position);
        Vector3Int EndPos = StateManger.grid_Manger.grid.WorldToCell(StateManger.Target_Pos);

        StateManger.A = Pathfinder.FindPath(StateManger.grid_Manger, StartPos, EndPos);
    }

    public void Update_State(Ai_StateManager StateManager)
    {
        Transform Ai_Transform = StateManager.transform;

        if (StateManager.pathNum == StateManager.A.Count)
        {
            if (StateManager.IsWork)
            {
                StateManager.ChangeState(StateManager.work_State);
                return;
            }
            else StateManager.ChangeState(StateManager.Idle_state);
            return;
        }

        Vector3 a = StateManager.A[StateManager.pathNum];
        Vector2 target = new Vector2(a.x, a.y);

        SpriteRenderer Ai_sprite = StateManager.Ai_Sprite.GetComponent<SpriteRenderer>();
        Ai_sprite.flipX = StateManager.transform.position.x > a.x;

        float c = StateManager.gameObject.layer == 6 ? Game_Manager.Ai_Power : 0;
        Ai_Transform.position = Vector2.MoveTowards(Ai_Transform.position, target, (c + 3) * Time.deltaTime);
        StateManager.IsMove = true;

        if (Vector2.Distance(Ai_Transform.position, a) < 0.05f)
            StateManager.pathNum++;
    }

    public void Exit_State(Ai_StateManager StateManager)
    {
        StateManager.IsMove = false;
    }
}
