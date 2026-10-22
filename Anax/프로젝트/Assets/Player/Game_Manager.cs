using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class Game_Manager : MonoBehaviour
{
    [Header("Script")]
    public Resource_Manager resource_manager;
    public Music_Manager music_Manager;
    public Sound_Manager sound_Manager;
    public GameObject TechTree_ui;
    public attack_Manager attack_Manager;
    public Science_Manager science_Manager;
    public UI_Pade Pade_script;
    public TechTree_Manager Tech;
    public Music_Manager Music_Manager;

    [Header("Game")]
    public bool live;
    private bool Game_End;

    [Header("Day")]
    public int day;
    public int One_day_time;
    public TextMeshProUGUI day_text;
    private Coroutine day_Coroutine;

    [Header("Research Bonuses")]
    public static int Ai_Speed;
    public static int Ai_Power;
    public static int Ai_Work;

    [Header("ingame_building")]
    public List<GameObject> Building = new List<GameObject>();

    [Header("ingame_Ai")]
    public static List<GameObject> Ai_Unit = new List<GameObject>();
    public List<GameObject> Start_Ai_Unit = new List<GameObject>();

    [Header("Ui")]
    public GameObject deffect;
    public GameObject victory;

    public void Awake()
    {
        for (int i = 0; i < Start_Ai_Unit.Count;i++)
        {
            Ai_Unit.Add(Start_Ai_Unit[i]);
        }

        Pade_script.Pade_Out();

        Ai_Power = 0;
        Ai_Speed = 0;
        Ai_Work = 0;
    }

    public void LateUpdate()
    {
        if (Game_End) return;
        if (Ai_Unit.Count == 0 || day >= 200)
        {
            Game_End = true;
            Defeat();
        }
        if (Tech.Is_Researched || resource_manager.population >= 450)
        {
            Game_End = true;
            Victory();
        }

        if (day_Coroutine == null)
        {
            day_Coroutine = StartCoroutine(Day());
        }    
    }

    private void Defeat()
    {
        deffect.SetActive(true);
        Music_Manager.Attack();
        
    }

    private void Victory()
    {
        victory.SetActive(true);
        Music_Manager.Play_Greece();
    }

    private IEnumerator Day()
    {
        yield return new WaitForSeconds(One_day_time);

        if (Game_End) yield break;  
        day += 1;
        day_text.text = $"Day :{day}";

        if (day >= 3 && Random.Range(0, 15) == 0)
        {
            attack_Manager.OnAttack();
        }
        day_Coroutine = null;
    }
}
