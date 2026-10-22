using TMPro;
using UnityEngine;

public class Science_Manager : MonoBehaviour
{
    [SerializeField] private Game_Manager manager;
    public TechTree_Manager TechTree_Manager;
    public static float Science_Rate;

    [SerializeField] private TextMeshProUGUI Text;
    public AudioClip Researched_Sfx;

    void LateUpdate()
    {
        if (TechTree_Manager == null) return;

        TechTree_Manager.Get_Point(Science_Rate * Time.deltaTime);
        Text.text = $"{TechTree_Manager.data.name} : {TechTree_Manager.data.Max_Science_Point} : {Mathf.FloorToInt( TechTree_Manager.Current_Point)}";
    }
}
