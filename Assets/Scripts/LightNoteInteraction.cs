using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Note = Model.Note;
using NotesSequence = Model.NotesSequence;
using NotesSpritesReferences = Model.NotesSpritesReferences;

public class LightNoteInteraction : MonoBehaviour
{
    // The number in the notes sequence.
    public int sequenceNumber = 0;

    // Delay the player has to respect to activate this note, in seconds.
    public float duration = 0.4f;

    // Percentage of the delay value we accept as offset.
    // The user can benefit from this percentage if he's a bit late or a bit fast when triggering the note.
    [Range (0, 100)]
    public int delayTolerance = 20;

    // The sprite to use when the note becomes active.
    // The inactive one should correspond to the one present in the sprite renderer when the object is constructed.
    public Sprite spriteWhenActive;

    private SpriteRenderer spriteRenderer;
    private Sprite spriteWhenInactive;

    private float baseTimeLeft;
    private float timeLeft;

    private bool active = true;

    private NotesSequence sequence { get; set; }

    public void SetSequence (NotesSequence notesSequence)
    {
        sequence = notesSequence;
    }

    public void SetActiveSpriteFromNote (Note note)
    {
        spriteWhenActive = NotesSpritesReferences.GetSpriteFromNote (note);
    }

    // Use this for initialization
    // Made public so it's accessible programmatically without using Start();
    public void StartInteraction ()
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        spriteWhenInactive = spriteRenderer.sprite;

        baseTimeLeft = duration + (duration / delayTolerance);
    }

    public void Activate ()
    {
        if (true == active) {
            return;
        }

        // Player entered in the note.
        active = true;
        timeLeft = baseTimeLeft;
        spriteRenderer.sprite = spriteWhenActive;
    }

    // Update is called once per frame
    void Update ()
    {
        Debug.DrawRay (transform.position, transform.rotation * Vector3.right, Color.red);

        if (active) {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0) {
                spriteRenderer.sprite = spriteWhenInactive;
                active = false;
                timeLeft = 0;
            }
        }
    }
}
