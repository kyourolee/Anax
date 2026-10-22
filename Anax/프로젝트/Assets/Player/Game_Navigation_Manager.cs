using UnityEngine;

public class Game_Navigation_Manager : MonoBehaviour
{
    [Header("Script")]
    [SerializeField] private GameObject Ui;
    [SerializeField] private Sound_Manager sound_Manager;
    private void OnOut() 
    {
        Ui.SetActive(!Ui.activeSelf);
        sound_Manager.Play_SFX(sound_Manager.sfx_data.Click_SFX);
    } 
        
    public void Game_out()
    {
        sound_Manager.Play_SFX(sound_Manager.sfx_data.Click_SFX);
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
