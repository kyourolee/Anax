using UnityEngine;

public class Out : MonoBehaviour
{

    [SerializeField] private GameObject Ui;
    [SerializeField] private Sound_Manager sound_Manager;
    [SerializeField] private AudioClip clip;
    private void OnOut() 
    {
        Ui.SetActive(!Ui.activeSelf);
        sound_Manager.Play_SFX(clip);
    } 
        
    public void Game_out()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
