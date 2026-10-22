using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_View : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Map_Data map_Data;
    [SerializeField] private Camera Cam;

    [Header("View Setting")]
    [SerializeField] private Vector3 View_Pos;
    [SerializeField] private float View_X;
    [SerializeField] private float View_Y;
    private float acceleration = 4.5f;

    [Header("Zoom Setting")]
    [SerializeField] private float Current_Zoom;
    [SerializeField] private float Min_Zoom_Value;
    [SerializeField] private float Max_Zoom_Value;

    private void Update()
    {
        View();
    }
    private void OnView(InputValue Value)
    {
        View_Pos = Value.Get<Vector2>();
    }

    private void View()
    {

        float X = View_Pos.x != 0 ? Mathf.Sign(View_Pos.x) : 0f;
        float Y = View_Pos.y != 0 ? Mathf.Sign(View_Pos.y) : 0f;

        View_X = Mathf.MoveTowards(View_X, X, acceleration * Time.deltaTime);
        View_Y = Mathf.MoveTowards(View_Y, Y, acceleration * Time.deltaTime);

        Vector3 Pos = new Vector3 (View_X, View_Y, 0f);
        if (Pos.sqrMagnitude > 1f)
        Pos.Normalize();

        Vector3 Limit_Pos = transform.position + Pos;
        
        Limit_Pos.x = Mathf.Clamp(Limit_Pos.x, map_Data.Map_Limit_Pos_X.x, map_Data.Map_Limit_Pos_X.y);
        Limit_Pos.y = Mathf.Clamp(Limit_Pos.y, map_Data.Map_Limit_Pos_Y.x, map_Data.Map_Limit_Pos_Y.y);
        
            

        transform.position = Limit_Pos;
    }    
    private void OnZoom(InputValue Value)
    {
        Current_Zoom -= Value.Get<float>();
        Current_Zoom = Mathf.Clamp(Current_Zoom, Min_Zoom_Value, Max_Zoom_Value);

        Cam.orthographicSize = Current_Zoom;
    }
}
