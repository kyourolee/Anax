using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Selection : MonoBehaviour
{
    [SerializeField] private Player_Input Player_Input;
    [SerializeField] private Sound_Manager sound_manger;
    [SerializeField] private Camera Cam;
    [SerializeField] private LayerMask Ai_Layer;
    public List<Transform> selected = new List<Transform>();

    public bool IsSelect;
    [Header ("SelectPos")]
    public Vector2 Single_SelectPos;
    public Vector2 Start_SelectPos;
    public Vector2 End_SelectPos;

    public RectTransform Select_Box;

    public void LateUpdate()
    {
        if (Player_Input.Shift && Player_Input.Click)
        {
            if (!IsSelect)
            {
                Start_SelectPos = Mouse.current.position.ReadValue();
                Select_Box.gameObject.SetActive(true);
                IsSelect = true;
            }
            End_SelectPos = Mouse.current.position.ReadValue();

            Vector2 Current_SelectPos = Mouse.current.position.ReadValue();
            Vector2 box_size = new Vector2(Mathf.Abs(Start_SelectPos.x - Current_SelectPos.x), Mathf.Abs(Start_SelectPos.y - Current_SelectPos.y));
            Vector2 Center = (Start_SelectPos + Current_SelectPos) / 2f;

            Select_Box.position = Center;
            Select_Box.sizeDelta = box_size;
        }
        else if (IsSelect)
        {
            IsSelect = false;
            Select_Box.gameObject.SetActive(false);
            Multi_Select();
            return;
        }
    }

    public void Multi_Select()
    {
        Vector2 Start = Cam.ScreenToWorldPoint(Start_SelectPos);
        Vector2 End = Cam.ScreenToWorldPoint(End_SelectPos);

        Vector2 center = (Start + End) * 0.5f;
        Vector2 size = new Vector2(Mathf.Abs(Start.x - End.x), Mathf.Abs(Start.y - End.y));
        Collider2D[] collider = Physics2D.OverlapBoxAll(center, size , 0f , Ai_Layer);

        if (collider == null) return;
        foreach (Collider2D coll in collider)
        {
            Ai_StateManager StateManager = coll.gameObject.GetComponent<Ai_StateManager>();
            if (!selected.Contains(coll.gameObject.transform))
            {
                selected.Add(coll.gameObject.transform);
                StateManager.Is_Selected = true;
            }
            else
            {
                selected.Remove(coll.gameObject.transform);
                StateManager.Is_Selected = false;
            }
        }
    }

    public void Single_Select()
    {
        Vector2 Pos = Mouse.current.position.ReadValue();
        Single_SelectPos = Cam.ScreenToWorldPoint(Pos);

        Collider2D coll = Physics2D.OverlapPoint(Single_SelectPos,Ai_Layer);
        if (coll != null)
        {
            Ai_StateManager StateManager = coll.gameObject.GetComponent<Ai_StateManager>();
            if (!selected.Contains(coll.gameObject.transform))
            {
                selected.Add(coll.gameObject.transform);
                StateManager.Is_Selected = true;
            }
            else
            {
                selected.Remove(coll.gameObject.transform);
                StateManager.Is_Selected = false;
            }
        }
    }
}
