using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public SFX_data sfx_data;
    public void Play_SFX(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.PlayOneShot(clip);
    }
}
