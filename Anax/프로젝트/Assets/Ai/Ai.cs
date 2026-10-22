using UnityEngine;

public class Ai : MonoBehaviour
{
    [Header("Script")]
    private Ai_StateManager stateManager;

    [Header("ui")]
    [SerializeField] private GameObject Select_box;
    void LateUpdate()
    {
        stateManager = GetComponent<Ai_StateManager>();
        Select_box.SetActive(stateManager.Is_Selected);
    }
}
