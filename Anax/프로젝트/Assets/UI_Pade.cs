using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Pade : MonoBehaviour
{
    [Header("Ui")]
    [SerializeField] private GameObject Start_Panel;
    [SerializeField] private Image Panel;

    [Header("float,Int,bool")]
    public bool Fade_end;

    public void Pade_in()
    {
        Start_Panel.SetActive(true);
        StartCoroutine(Pade_in_Coroutine());
    }

    IEnumerator Pade_in_Coroutine()
    {
        Color Alpa = Panel.color;
        Fade_end = false;
        float time = 0f;

        while (Alpa.a < 1f)
        {
            time += Time.deltaTime / 0.5f;
            Alpa.a = Mathf.Lerp(0, 1, time);
            Panel.color = Alpa;
            yield return null;
        }

        time = 0f;
        Fade_end = true;
        yield return null;
    }

    public void Pade_Out()
    {
        Start_Panel.SetActive(true);
        StartCoroutine(Pade_Out_Coroutine());
    }

    IEnumerator Pade_Out_Coroutine()
    {
        Color Alpa = Panel.color;
        Fade_end = false;
        float time = 0f;

        while (Alpa.a > 0)
        {
            time += Time.deltaTime / 0.5f;
            Alpa.a = Mathf.Lerp(1, 0, time);
            Panel.color = Alpa;
            yield return null;
        }

        time = 0f;
        Fade_end = true;
        Start_Panel.SetActive(false);

        yield return null;
    }
}