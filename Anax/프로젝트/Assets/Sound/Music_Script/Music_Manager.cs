using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Music_Manager : MonoBehaviour
{

    [Header("Music Setting")]
    [SerializeField] private Music_Data data;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Game_Manager game_manager;
    [SerializeField] private int Current_Music_Num;
    public bool IsMusic = true;
    [SerializeField] private bool IsChange;

    public bool menu;


    private void Awake()
    {
        if (menu)
        {
            Menu();
            return;
        }    
        if (!game_manager.live)
        {
            Dark_age();
        }
    }

    private IEnumerator Change_Music(AudioClip target_clip)
    {
        if (IsChange) yield break;
        IsChange = true;

        float time = 1;
        float Current_time = time;

        while (Current_time > 0f)
        {
            Current_time -= 0.5f * Time.deltaTime;
            audioSource.volume = Current_time;
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = null;
        audioSource.clip = target_clip;

        while (Current_time < time)
        {
            Current_time += 0.5f * Time.deltaTime;
            audioSource.volume = Current_time;
            yield return null;
        }

        audioSource.volume = 0.5f;
        IsChange = false;
        audioSource.Play();
    }

    public void Play_Greece()
    {
        audioSource.clip = data.Dark_age;
        audioSource.loop = false;

        StartCoroutine(Change_Music(data.Greece_Theme));
    }

    private void Dark_age()
    {
        audioSource.clip = data.Dark_age;
        audioSource.loop = true;

        StartCoroutine(Change_Music(data.Dark_age));
    }

    private void Menu()
    {
        audioSource.clip = data.Dark_age;
        audioSource.loop = true;

        StartCoroutine(Change_Music(data.Menu_Theme));
    }

    public void Attack()
    {
        audioSource.clip = data.Dark_age;
        audioSource.loop = true;

        StartCoroutine(Change_Music(data.Attack_Theme));
    }


}
