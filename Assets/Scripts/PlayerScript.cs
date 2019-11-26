using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public bool someBool;
    public bool someOtherBool;
    public int counter = 0;

    public void DoSomething()
    {
        Debug.Log("Doing something!");
        someBool = !someBool;
    }

}
