using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerNote : MonoBehaviour
{
    private int count;

    void Start ()
    {
        count = 0;
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag ("NotePickup")) {
            other.gameObject.SetActive (false);
            count++;
        }
    }
}
