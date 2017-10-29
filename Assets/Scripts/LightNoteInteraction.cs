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

    private SpriteRenderer _spriteRenderer;
    private Sprite _spriteWhenInactive;

    private float _baseTimeLeft;
    private float _timeLeft;

    private bool _active = true;

    private NotesSequence _sequence { get; set; }

    public void SetSequence (NotesSequence sequence)
    {
        _sequence = sequence;
    }

    public void SetActiveSpriteFromNote (Note note)
    {
        spriteWhenActive = NotesSpritesReferences.GetSpriteFromNote (note);
    }

    // Use this for initialization
    // Made public so it's accessible programmatically without using Start();
    public void StartInteraction ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer> ();
        _spriteWhenInactive = _spriteRenderer.sprite;

        _baseTimeLeft = duration + (duration / delayTolerance);
    }

    // Update is called once per frame
    void Update ()
    {
        Debug.DrawRay (transform.position, transform.rotation * Vector3.right, Color.red);

        if (_active) {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft < 0) {
                _spriteRenderer.sprite = _spriteWhenInactive;
            }
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag ("Player")) {
            Debug.Log ("Collision, here's the sequence:");
            Debug.Log (_sequence);
            // Player entered in the note.
            _active = true;
            _timeLeft = _baseTimeLeft;
            _spriteRenderer.sprite = spriteWhenActive;
        }
    }
}
