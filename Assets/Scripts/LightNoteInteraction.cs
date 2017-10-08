using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightNoteInteraction : MonoBehaviour
{
    // The number in the notes sequence.
    public int sequenceNumber = 0;

    // Delay the player has to respect to activate this note, in seconds.
    public float delayToActivate = 0.4f;

    // Percentage of the delay value we accept as offset.
    // The user can benefit from this percentage if he's a bit late or a bit fast when triggering the note.
    [Range (0, 100)]
    public int delayTolerance = 20;

    // The sprite to use when the note becomes active.
    // The inactive one should correspond to the one present in the sprite renderer when the object is constructed.
    public Sprite spriteWhenActive;

    private SpriteRenderer _spriteRenderer;
    private Sprite _spriteWhenActive;
    private Sprite _spriteWhenInactive;
    private float _baseTimeLeft;
    private float _timeLeft;
    private float _delayToActivate;
    private float _delayTolerance;
    private int _sequenceNumber;
    private bool _active = true;

    // Use this for initialization
    void Start ()
    {
        _sequenceNumber = sequenceNumber;

        _spriteRenderer = GetComponent<SpriteRenderer> (); 
        _spriteWhenInactive = _spriteRenderer.sprite;
        _spriteWhenActive = spriteWhenActive;

        _delayToActivate = delayToActivate;
        _delayTolerance = (float)delayTolerance;
        _baseTimeLeft = _delayToActivate + (_delayToActivate / _delayTolerance);
    }

    // Update is called once per frame
    void Update ()
    {
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
            // Player entered in the note.
            _active = true;
            _timeLeft = _baseTimeLeft;
            _spriteRenderer.sprite = _spriteWhenActive;
        }
    }
}
