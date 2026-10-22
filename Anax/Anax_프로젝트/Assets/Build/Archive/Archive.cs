using UnityEngine;

public class Archive : MonoBehaviour
{
    Building_Controller Building_Controller;
    bool Is_act;
    void LateUpdate()
    {
        Building_Controller = GetComponent<Building_Controller>();
        if (Building_Controller.isBuilt && !Is_act)
        {
            Is_act = true;
            Science_Manager.Science_Rate += 5;
        }
    }

    public void Del()
    {
        Building_Controller = GetComponent<Building_Controller>();
        if (Building_Controller.isBuilt)
        {
            Science_Manager.Science_Rate -= 5;
        }
    }
}
