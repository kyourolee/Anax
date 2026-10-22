using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;

public class attack_Manager : MonoBehaviour
{
    public Game_Manager game_manager;
    public Music_Manager music_manager;
    [SerializeField] private GameObject Enemy_Ai;
    [SerializeField] private GameObject[] Spawn_Pos;

    private int Power;
    private int Speed;

    private int Enemy_Count = 1;

    public bool IsAttack;
    public static List<GameObject> Enemy_List = new List<GameObject>();

    public GameObject Attack_Ui;

    public void OnAttack()
    {
        if (IsAttack) return;

        music_manager.Attack();
        Attack_Ui.SetActive(true);
        Vector3 Spawn_pos = Spawn_Pos[Random.Range(0, Spawn_Pos.Length)].transform.position;
        IsAttack = true;

        for (int i = 0; i < Enemy_Count; i++)
        {
            GameObject Current_ai = Instantiate(Enemy_Ai, Spawn_pos, Quaternion.identity);
            Ai_StateManager StateManager = Current_ai.GetComponent<Ai_StateManager>();
            Enemy_Ai enemy = Current_ai.GetComponent<Enemy_Ai>();
            StateManager.grid_Manger = Grid_Manager.Instance;
            enemy.attack_manager = this;

            GameObject Target = Game_Manager.Ai_Unit[Random.Range(0, Game_Manager.Ai_Unit.Count)];
            StateManager.Target_Pos = Target.transform.position;
            Enemy_List.Add(Current_ai);
            
            StateManager.ChangeState(StateManager.move_state);
        }

        Enemy_Count += 1;
    }

    public void LateUpdate()
    {
        if (!IsAttack) return;

        if (Enemy_List.Count <= 0)
        {
            IsAttack = false;
            Attack_Ui.SetActive(false);
            music_manager.Play_Greece();
            return;
        }
    }
}
