using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Analytics.IAnalytic;

public class Unit_Spawner : MonoBehaviour
{
    [SerializeField] private Building_Controller controller;
    [SerializeField] private Resource_Manager Resource_Manager;
    [SerializeField] private Sound_Manager Sound_manager;
    [SerializeField] private Coroutine Worker_SpawnCoroutine;

    [SerializeField] private int Cost;
    [SerializeField] private int Spawn_Time;

    [SerializeField] private GameObject Spawn_Ui;

    private void Get_Component()
    {
        controller = gameObject.GetComponent<Building_Controller>();
        Resource_Manager = controller.game_manager.resource_manager;
        Sound_manager = controller.game_manager.sound_Manager;
    }

    public void Click()
    {
        Get_Component();
        if (!controller.isBuilt)
        {
            Sound_manager.Play_SFX(Sound_manager.sfx_data.Cancel_SFX);
            return;
        }

        Spawn_Ui.SetActive(true);
    }

    public void Spawn_Worker(int Number)
    {
        Get_Component();
        if (Resource_Manager.Food < Cost || Resource_Manager.population < 3|| Worker_SpawnCoroutine != null)
        {
            Sound_manager.Play_SFX(Sound_manager.sfx_data.Cancel_SFX);
            return;
        }    

        Worker_SpawnCoroutine = StartCoroutine(Spawn_Coroutine(Number));
        Resource_Manager.Set_Food(-Cost);
        Resource_Manager.Set_Population(-3);
    }

    private IEnumerator Spawn_Coroutine(int Number)
    {
        yield return new WaitForSeconds(Spawn_Time);

        GameObject New_ai = Instantiate(controller.ai_data[Number].Ai_Object , controller.Spawn_Cell.transform.position , Quaternion.identity);
        Ai_StateManager state_manager = New_ai.GetComponent<Ai_StateManager>();
        state_manager.grid_Manger = Grid_Manager.Instance;

        Worker_SpawnCoroutine = null;
    }

    public void Play_sfx()
    {
        Get_Component();
        Sound_manager.Play_SFX(Sound_manager.sfx_data.Click_SFX);
    }
}
