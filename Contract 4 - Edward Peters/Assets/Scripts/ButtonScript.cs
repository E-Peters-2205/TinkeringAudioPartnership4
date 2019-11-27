using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    public void PressButton()
    {
        playerScript.DoSomething();
        playerScript.someOtherBool = !playerScript.someOtherBool;
        playerScript.counter++;
    }
 
}
