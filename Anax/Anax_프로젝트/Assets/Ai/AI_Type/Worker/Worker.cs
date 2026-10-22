using UnityEngine;
using System.Collections;

public class Worker : MonoBehaviour
{
    public bool Can_Work = true;

    public bool Axe;
    public bool PickAxe;
    public bool Hammer;

    public int Work_Tick()
    {
        if (Can_Work)
        {
            StartCoroutine(Work());
            return 25;
        }
        else return 0;
    }

    private IEnumerator Work()
    {
        Can_Work = false;
        int a = gameObject.layer == 6 ? Game_Manager.Ai_Power : 0;
        yield return new WaitForSeconds(Mathf.Abs(4 - a));
        Can_Work = true;
    }
}
