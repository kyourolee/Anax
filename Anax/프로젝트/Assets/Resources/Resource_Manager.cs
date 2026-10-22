using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class Resource_Manager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private Map_Data data;

    [Header("script")]
    [SerializeField] private Game_Manager game_Manager;

    [Header("Resource")]
    public int Wood;
    public int Rock;
    public int Food;
    public int Gold;
    public int Wine;
    public int population;

    [Header("Ui")]
    [SerializeField] private TMP_Text wood_Text;
    [SerializeField] private TMP_Text rock_Text;
    [SerializeField] private TMP_Text food_Text;
    [SerializeField] private TMP_Text gold_Text;
    [SerializeField] private TMP_Text wine_Text;
    [SerializeField] private TMP_Text population_Text;

    public void Awake()
    {
        Wood = data.Start_Wood;
        Rock = data.Start_Rock;
        Food = data.Start_Food;
        Gold = data.Start_Gold;
        Wine = data.Start_Wine;

        Set_wood(0);Set_Rock(0);Set_Food(0);Set_Gold(0);Set_Wine(0);Set_Population(0);
    }

    public void Set_wood(int Value)
    {
        Wood = Mathf.Clamp(Wood + Value, 0, 500);
        wood_Text.text = $"Wood : {Wood}";
    }

    public void Set_Rock(int Value)
    {
        Rock = Mathf.Clamp(Rock + Value, 0, 500);
        rock_Text.text = $"Rock : {Rock}";
    }

    public void Set_Food(int Value)
    {
        Food = Mathf.Clamp(Food + Value, 0, 500);
        food_Text.text = $"Food : {Food}";
    }

    public void Set_Gold(int Value)
    {
        Gold = Mathf.Clamp(Gold + Value, 0, 500);
        gold_Text.text = $"Gold : {Gold}";
    }

    public void Set_Wine(int Value)
    {
        Wine = Mathf.Clamp(Wine + Value, 0, 500);
        wine_Text.text = $"Wine : {Wine}";
    }

    public void Set_Population(int Value)
    {
        population = Mathf.Clamp(population + Value, 0, 500);
        population_Text.text = $"Population : {population}";
    }
}
