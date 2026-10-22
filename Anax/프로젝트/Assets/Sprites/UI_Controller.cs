using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [Header("Ui")]
    public GameObject[] Target_Ui;

    public void Show_Hide()
    { 
        for (int i = 0; i < Target_Ui.Length; i++)
        {
            GameObject Current_UI = Target_Ui[i];
            Current_UI.SetActive(!Current_UI.activeSelf);
        }
    }
}
