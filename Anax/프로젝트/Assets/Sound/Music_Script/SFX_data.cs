using UnityEngine;

[CreateAssetMenu(fileName = "SFX_data", menuName = "RTS/SFX_data")]
public class SFX_data : ScriptableObject
{
    [Header("UI_SFX")]
    public AudioClip Click_SFX;
    public AudioClip Cancel_SFX;
    public AudioClip Research_Complete_SFX;

    [Header("Building_SFX")]
    public AudioClip Build_SFX;
}
