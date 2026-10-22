using UnityEngine;

public class Work_Cell : MonoBehaviour
{
    public bool Cell_Use;
    public GameObject Using_Ai;
    public GameObject resource_Object;

    private void LateUpdate()
    {
        if (Using_Ai == null) return;

        Ai_StateManager stateManager = Using_Ai.GetComponent<Ai_StateManager>();
        if (stateManager.Work_Target != resource_Object || !stateManager.IsWork)
        {
            Cell_Use = false;
            Using_Ai = null;
        }
    }
}
