using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireController : MonoBehaviour
{
    public GameObject animationObject;
    private GrowingCircleAnimation animationScript;

    void Start ()
    {
        animationScript = animationObject.GetComponent<GrowingCircleAnimation> ();
    }

    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.F)) {
            animationScript.Trigger ();
        }
    }
}
