using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickController : MonoBehaviour
{
    private CircleOnClickAnimation animation;

    void Start ()
    {
        animation = GetComponent<CircleOnClickAnimation> ();
    }
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.F)) {
            Debug.Log ("\"F\" key was pressed");
        }
    }
}
