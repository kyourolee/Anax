using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Controller : MonoBehaviour
{
    public Ai_StateManager stateManager;
    public int Max_Hp;
    public int Current_Hp;
    bool Enemy_Ai;

    [SerializeField] private Slider Hp_slider;

    public void Awake()
    {
        Update_HP_ui();
    }

    public void Get_Damage(int Value)
    {
        Current_Hp = Mathf.Max(0, Current_Hp - Value);
        Update_HP_ui();

        if (Current_Hp == 0)
        {
            attack_Manager.Enemy_List.Remove(gameObject);
            Game_Manager.Ai_Unit.Remove(gameObject);
            Destroy(gameObject);
        }

        if (Current_Hp <= Max_Hp)
        {
            StartCoroutine(heal());
        }
    }

    private IEnumerator heal()
    {
         while (Current_Hp > 0 && Current_Hp < Max_Hp)
        {
            yield return new WaitForSeconds(5);
            Current_Hp = Mathf.Min(Current_Hp + 5, Max_Hp);
            Update_HP_ui();
        } 
    }

    private void Update_HP_ui()
    {
        Hp_slider.maxValue = Max_Hp;
        Hp_slider.value = Current_Hp;
    }
}
