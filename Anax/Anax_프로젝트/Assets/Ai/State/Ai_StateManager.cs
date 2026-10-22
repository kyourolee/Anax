using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.SocialPlatforms;

public class Ai_StateManager : MonoBehaviour
{
    [Header ("State_Setting")]
    public iAIState CurrentState;
    public Ai_Type_State Current_Work_State;
    public Idle_State Idle_state = new Idle_State();
    public Move_State move_state = new Move_State();
    public Work_State work_State = new Work_State();

    [Header("Script, Component")]
    public Grid_Manager grid_Manger;
    public Health_Controller Health;
    public Ai_Data data;
    public Transform Ai_Transform;
    public GameObject Work_Target;
    public GameObject In_work_Target;

    [Header("Move_State")]
    public Vector3 Target_Pos;
    public Transform Ai_Sprite;
    public int pathNum;

    [Header("anim")]
    public Animator anim;
    public bool IsMove;
    public bool Is_Selected;
    public bool IsWork;

    public List<Vector3Int> A = new List<Vector3Int> ();

    private void Awake()
    {
        MonoBehaviour[] components = GetComponents<MonoBehaviour>();
        
        foreach  (MonoBehaviour component in components)
        {
            if (component is Ai_Type_State state)
            {
                Current_Work_State = state;
            }
        }

        Ai_Transform = transform;
        Ai_Sprite.localPosition = new Vector3(0.5f, 0.8f, 0f);

        ChangeState(Idle_state);
    }

    void Update()
    {
        anim.SetBool("Move", IsMove);
        anim.SetBool("Work", IsWork && !IsMove);   
        CurrentState?.Update_State(this);
    }

    public void ChangeState(iAIState newState)
    {
        CurrentState?.Exit_State(this);
        CurrentState = newState;
        CurrentState.Enter_State(this);
    }

    public void Update_Work()
    {
        Current_Work_State?.Update_State(this);
    }
}
    