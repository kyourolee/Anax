using UnityEngine;
using UnityEngine.UI;

public class TechTree_Manager : MonoBehaviour
{
    [Header("Script")]
    private Game_Manager game_Manager;
    [SerializeField] private Science_Manager manager;
    private Sound_Manager Sound_Manager;

    [Header("Component, Data")]
    public GameObject[] Need_TechTree;
    public Science_data data;
    public Image image;

    [Header("int,float,bool")]
    public float Current_Point;
    public bool Is_Researched;

    public void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Can_Science()
    {
        image = GetComponent<Image>();
        if (Is_Researched) return;
        for (int i = 0; i < Need_TechTree.Length; i++)
        {
            TechTree_Manager Need_TechTree_script = Need_TechTree[i].GetComponent<TechTree_Manager>();

            if (!Need_TechTree_script.Is_Researched)
            {
                Sound_Manager.Play_SFX(Sound_Manager.sfx_data.Cancel_SFX);
                return;
            }
        }

        Image current_image = manager.TechTree_Manager.image; 
        Color color = new Color32(185, 157, 93, 255);
        Color end_color = new Color32(70, 88, 128, 255);
        current_image.color = manager.TechTree_Manager.Is_Researched ? end_color : color;

        manager.TechTree_Manager = this;
        Color nextcolor = new Color32(95, 169, 179, 255);
        image.color = nextcolor;
    }

    public void Get_Point(float Value)
    {
        if (Current_Point >= data.Max_Science_Point)
        {
            Current_Point = data.Max_Science_Point;
            Researched();
        }
        else if (!Is_Researched)
        {
            Current_Point += Value;
        }
    }   

    private void Researched()
    {
        if (Is_Researched) return;
        Game_Manager.Ai_Speed += data.Add_Ai_Speed;
        Game_Manager.Ai_Power += data.Add_Ai_Power;
        Game_Manager.Ai_Work += data.Add_Ai_Work;

        Sound_Manager.Play_SFX(Sound_Manager.sfx_data.Research_Complete_SFX);
        Color color = new Color32(70, 88, 128, 255);
        image.color = color;

        Is_Researched = true;
    }
}
