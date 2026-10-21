using UnityEngine;

public class Ai : MonoBehaviour
{
    private Ai_StateManager stateManager;
    [SerializeField] private GameObject Select_box;
    void LateUpdate()
    {
        stateManager = GetComponent<Ai_StateManager>();


        if (stateManager.Is_Selected)
        {
            Select_box.SetActive(true);
        }
        else Select_box.SetActive(false);
    }
}
