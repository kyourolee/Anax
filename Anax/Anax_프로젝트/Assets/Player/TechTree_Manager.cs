using UnityEngine;
using UnityEngine.UI;

public class TechTree_Manager : MonoBehaviour
{
    public Game_Manager game_Manager;
    public Science_Manager manager;
    public Science_data data;
    public float Current_Point;
    public bool Is_Researched;

    public GameObject[] Need_TechTree;
    public Image image;
    public AudioClip clip;

    public void Awake()
    {
        image = GetComponent<Image>();
    }
    public void Can_Science()
    {
        if (Is_Researched) return;
        for (int i = 0; i < Need_TechTree.Length; i++)
        {
            TechTree_Manager Need_TechTree_script = Need_TechTree[i].GetComponent<TechTree_Manager>();

            if (!Need_TechTree_script.Is_Researched)
            {
                game_Manager.sound_Manager.Play_SFX(clip);
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

        game_Manager.sound_Manager.Play_SFX(manager.Researched_Sfx);
        Color color = new Color32(70, 88, 128, 255);
        image.color = color;

        Is_Researched = true;
    }
}
