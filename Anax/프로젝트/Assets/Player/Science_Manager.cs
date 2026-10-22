using TMPro;
using UnityEngine;

public class Science_Manager : MonoBehaviour
{
    [Header("Script")]
    [SerializeField] private Game_Manager manager;
    //지금 연구중인 태크트리
    public TechTree_Manager TechTree_Manager;

    [Header("Component")]
    [SerializeField] private TextMeshProUGUI Text;

    [Header("Science_Rate")]
    public static float Science_Rate;

    void LateUpdate()
    {
        if (TechTree_Manager == null) return;

        TechTree_Manager.Get_Point(Science_Rate * Time.deltaTime);
        Text.text = $"{TechTree_Manager.data.name} : {TechTree_Manager.data.Max_Science_Point} : {Mathf.FloorToInt( TechTree_Manager.Current_Point)}";
    }
}
