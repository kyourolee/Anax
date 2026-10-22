using System.Collections;
using UnityEngine;

public class pasture : MonoBehaviour
{
    [SerializeField] private float Get_Food_Time;
    [SerializeField] private int Get_Food__Value;

    private Coroutine Food_Coroutine;
    private Building_Controller controller;

    private void Awake()
    {
        controller = GetComponent<Building_Controller>();   
    }
    private void LateUpdate()
    {
        if (!controller.isBuilt || Food_Coroutine != null) return;
        Food_Coroutine = StartCoroutine(Get_Food());
    }

    private IEnumerator Get_Food()
    {
        yield return new WaitForSeconds(Get_Food_Time);
        controller.game_manager.resource_manager.Set_Food(Get_Food__Value);
        Food_Coroutine = null;
    }    
        
}
