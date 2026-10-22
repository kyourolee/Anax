using UnityEngine;

public class Palace : MonoBehaviour
{
    private GameObject TechTree_Ui;
    private Building_Controller controller;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip Cancel_clip;

    public void Open_TechTree()
    {        
        controller = GetComponent<Building_Controller>();
        TechTree_Ui = controller.game_manager.TechTree_ui;

        if (!controller.isBuilt)
        {
            controller.game_manager.sound_Manager.Play_SFX(Cancel_clip);
            return;
        }

        controller.game_manager.sound_Manager.Play_SFX(clip);
        TechTree_Ui.SetActive(true);
    }
}
