using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Ai : MonoBehaviour
{
    public attack_Manager attack_manager;
    public Health_Controller health;
    [SerializeField] private Ai_StateManager stateMananger;
    private Coroutine Can_search;

    public void LateUpdate()
    {
        if (Can_search != null || stateMananger.Work_Target == null || stateMananger.CurrentState == stateMananger.move_state) return;
        Can_search = StartCoroutine(search_Target());
    }
    private IEnumerator search_Target()
    {
        yield return new WaitForSeconds(4);

        if (stateMananger.Work_Target == null && Game_Manager.Ai_Unit.Count != 0)
        {
            int Current_ai = Game_Manager.Ai_Unit.Count;
            Vector3 target = Game_Manager.Ai_Unit[Random.Range(0, Current_ai)].transform.position;
            stateMananger.Target_Pos = target;
            stateMananger.ChangeState(stateMananger.move_state);
            Can_search = null;
        }
        else Can_search = null;
    }
}
