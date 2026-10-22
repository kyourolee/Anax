using UnityEngine;

public class Object_Transparency : MonoBehaviour
{
    [SerializeField]private SpriteRenderer spriteRenderer;
    private int Count;

    private void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.gameObject.layer != 6) return;
        Count++;

        spriteRenderer =  gameObject.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;
    }

    private void OnTriggerExit2D(Collider2D Coll)
    {
        if (Coll.gameObject.layer != 6) return;
        Count = Mathf.Max(0, Count - 1);

        if (Count == 0)
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            Color color = spriteRenderer.color;
            color.a = 1f;
            spriteRenderer.color = color;
        }
    }
}
