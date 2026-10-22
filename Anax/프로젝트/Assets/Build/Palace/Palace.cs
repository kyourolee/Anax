using UnityEngine;

public class Palace : MonoBehaviour
{
    [Header("Script")]
    private Building_Controller controller;

    [Header("Ui")]
    private GameObject TechTree_Ui;

    public void Open_TechTree()
    {        
        controller = GetComponent<Building_Controller>();
        Sound_Manager sound_Manager;
        sound_Manager = controller.game_manager.sound_Manager;
        TechTree_Ui = controller.game_manager.TechTree_ui;

        if (!controller.isBuilt)
        {
            sound_Manager.Play_SFX(sound_Manager.sfx_data.Cancel_SFX);
            return;
        }
        sound_Manager.Play_SFX(sound_Manager.sfx_data.Click_SFX);
        TechTree_Ui.SetActive(true);
    }
}
