using UnityEngine;

[CreateAssetMenu(fileName = "AI_Data" , menuName = "RTS/ AI_Data")]

public class Ai_Data : ScriptableObject
{
    public AI_Type type;
    public LayerMask Layer;
    public GameObject Ai_Object;

    [Header("Audio_setting")]
    public AudioClip Ready;
}
