using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireController : MonoBehaviour
{
    public GameObject animationObject;
    public KeyCode keyCode;

    private GrowingCircleAnimation animationScript;
    private GameObject[] notes;
    private Collider2D circleCollider;

    void Start ()
    {
        animationScript = animationObject.GetComponent<GrowingCircleAnimation> ();
        circleCollider = GetComponent<Collider2D> ();
        notes = GameObject.FindGameObjectsWithTag ("NotePickup");
    }

    void Update ()
    {
        if (Input.GetKeyDown (keyCode)) {
            animationScript.Trigger ();
            CheckOverlappingNotes ();
        }
    }

    void CheckOverlappingNotes ()
    {
        foreach (GameObject note in notes) {
            if (note.GetComponent<Collider2D> ().IsTouching (circleCollider)) {
                note.GetComponent<LightNoteInteraction> ().Activate ();
            }
        }
    }
}
