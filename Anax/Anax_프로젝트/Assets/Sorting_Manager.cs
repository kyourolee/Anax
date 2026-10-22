using UnityEngine;

public class Sorting_Manager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void LateUpdate()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
        if (gameObject.layer == 6 || gameObject.layer == 7)
        {
            spriteRenderer.sortingOrder += 100;
        }
    }
}
