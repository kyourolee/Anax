using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Scene : MonoBehaviour
{
    [SerializeField] private UI_Pade pade;
    public void Change_scene(int SceneNum)
    {
        pade.Pade_in();
        SceneManager.LoadScene(SceneNum);
    }
}
