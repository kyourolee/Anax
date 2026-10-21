using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Hoplite : MonoBehaviour
{
    public Ai_StateManager StateManager;
    public LayerMask Attack_Layer;
    public Coroutine Attack_Tick;

    [SerializeField] private float Attack_Time;
    [SerializeField] private Transform Attack_Pos;
    [SerializeField] private Vector2 Attack_Size;

    public bool IsAttack;
    public int Target_Layer_num;

    private void LateUpdate()
    {
        if (StateManager.Work_Target != null)
        {
            if (StateManager.Work_Target.layer != Target_Layer_num) return;
            Health_Controller target_health = StateManager.Work_Target.GetComponent<Health_Controller>();
            if (target_health.Current_Hp == 0)
            {
                StateManager.Work_Target = null;
                IsAttack = false;
                
            }
        }

        if (StateManager.Work_Target == null)
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 5f, Attack_Layer);
            if (!collider) return;
            StateManager.Work_Target = collider.gameObject;
            Health_Controller target_health = StateManager.Work_Target.GetComponent<Health_Controller>();
            if (target_health.Current_Hp == 0)
            {
                StateManager.Work_Target = null;
                IsAttack = false;


            }
        }

        if (StateManager.Work_Target == null) return;

        if (Physics2D.OverlapBox(Attack_Pos.position, Attack_Size,0f , Attack_Layer))
        {
            if (!IsAttack && Attack_Tick == null)
                Attack();
            return;
        }

        if (!IsAttack)
        {
            StateManager.Target_Pos = StateManager.Work_Target.transform.position;

            if (StateManager.CurrentState != StateManager.move_state)
            {
                StateManager.ChangeState(StateManager.move_state);
            }
        }
    }

    private void Attack()
    {
        IsAttack = true;
        StateManager.ChangeState(StateManager.Idle_state);
        StateManager.IsMove = false;
        StateManager.IsWork = false;

        StateManager.anim.SetTrigger("Attack");
        Attack_tick();

        Collider2D[] collider = Physics2D.OverlapBoxAll(Attack_Pos.position, Attack_Size, 0f, Attack_Layer);

        foreach(Collider2D Coll in collider)
        {
            if (Coll.gameObject != StateManager.Work_Target) continue;

            Health_Controller health = Coll.GetComponent<Health_Controller>();
            int a = gameObject.layer == 6 ? Game_Manager.Ai_Power : 0;
            health.Get_Damage(5 + a);
        }
    }
    public void Attack_tick()
    {
        if (Attack_Tick != null) return;
        Attack_Tick = StartCoroutine(Attack_tick_Coroutine());
    }

    private IEnumerator Attack_tick_Coroutine()
    {
        yield return new WaitForSeconds(Attack_Time);
        Attack_Tick = null;
        IsAttack = false;
    }


    private void OnDrawGizmos()
    {
       Gizmos.color = Color.yellow;
       Gizmos.DrawWireCube(Attack_Pos.position,Attack_Size);
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
