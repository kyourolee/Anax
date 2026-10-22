using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.GraphicsBuffer;

public class Worker_Work_State : MonoBehaviour , Ai_Type_State
{
    public void Enter_State(Ai_StateManager StateManager)
    {
        StateManager.In_work_Target = StateManager.Work_Target;
        
        if (StateManager.Work_Target != null)
        {
            Worker Work_script = StateManager.transform.GetComponent<Worker>();

            if (StateManager.In_work_Target.gameObject.layer == 8)
            {
                Building_Controller building_Script = StateManager.In_work_Target.GetComponent<Building_Controller>();

                SpriteRenderer Ai_sprite = StateManager.Ai_Sprite.GetComponent<SpriteRenderer>();
                Ai_sprite.flipX = StateManager.transform.position.x > StateManager.Work_Target.transform.position.x;
                Work_script.Hammer = true;

                StateManager.anim.SetBool("Hammer", Work_script.Hammer);
                return;
            }
            if (StateManager.In_work_Target.gameObject.layer == 11)
            {
                Resource_Controller resource_script = StateManager.In_work_Target.GetComponent<Resource_Controller>();

                SpriteRenderer Ai_sprite = StateManager.Ai_Sprite.GetComponent<SpriteRenderer>();
                Ai_sprite.flipX = StateManager.transform.position.x > StateManager.Work_Target.transform.position.x;

                if (StateManager.In_work_Target.CompareTag("Tree"))
                {
                    Work_script.Axe = true;
                    StateManager.anim.SetBool("Axe", Work_script.Axe);
                    return;
                }
                if (StateManager.In_work_Target.CompareTag("Rock"))
                {
                    Work_script.PickAxe = true;
                    StateManager.anim.SetBool("PickAxe", Work_script.PickAxe);
                    return;
                }
            }
        }
        else
        {
            StateManager.ChangeState(StateManager.Idle_state);
        }
    }
    
    public void Update_State(Ai_StateManager StateManager)
    {
        Worker Work_script = StateManager.transform.GetComponent<Worker>();

        if (StateManager.In_work_Target.layer == 8)
        {
            Building_Controller Building_Script = StateManager.In_work_Target.GetComponent<Building_Controller>();
            if (Building_Script.Building_HP == Building_Script.Max_Building_HP)
            {
                StateManager.ChangeState(StateManager.Idle_state);
                StateManager.IsWork = false;
                return;
            }
            int Tick = Work_script.Work_Tick();
            Building_Script.Building_HP += Tick;
            Building_Script.Building_HP = Mathf.Clamp(Building_Script.Building_HP, 0, Building_Script.Max_Building_HP);
        }
        if (StateManager.In_work_Target.layer == 11)
        {
            Resource_Controller resource_Script = StateManager.In_work_Target.GetComponent<Resource_Controller>();

            if (resource_Script.Resource_HP == 0)
            {
                StateManager.ChangeState(StateManager.Idle_state);
                StateManager.IsWork = false;
                return;
            }
            int Tick = Work_script.Work_Tick();
            resource_Script.Decrease_Resource(Tick);
        }    
    }

    public void Exit_State(Ai_StateManager StateManager)
    {
        Worker Work_script = StateManager.transform.GetComponent<Worker>();
        StateManager.IsWork = false;

        Work_script.Axe = false;
        StateManager.anim.SetBool("Axe", Work_script.Axe);

        Work_script.PickAxe = false;
        StateManager.anim.SetBool("PickAxe", Work_script.PickAxe);

        Work_script.Hammer = false;
        StateManager.anim.SetBool("Hammer", Work_script.Hammer);
        StateManager.Work_Target = null;
    }
}
