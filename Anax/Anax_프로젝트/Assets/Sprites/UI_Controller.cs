using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public GameObject[] Target_Ui;
    public void Show()
    { 
        for (int i = 0; i < Target_Ui.Length; i++)
        {
            GameObject Current_UI = Target_Ui[i];
            
            if (Current_UI.activeSelf == true)
            {
                Current_UI.SetActive(false);
            }
            else Current_UI.SetActive(true);
        }

    }

    public void Hide()
    {
        for (int i = 0; i < Target_Ui.Length; i++)
        {
            GameObject Current_UI = Target_Ui[i];

            if (Current_UI.activeSelf == false)
            {
                Current_UI.SetActive(true);
            }
            else Current_UI.SetActive(false);
        }
    }
}
