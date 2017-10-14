using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Note = Model.Note;

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

    public void SetActiveSpriteFromNote (Note note)
    {
        Dictionary<string, int> noteToSprite = new Dictionary<string, int> ();
        noteToSprite.Add ("A", 0); // 01-purpleish-blue
        noteToSprite.Add ("A#", 1); // 02-purple
        noteToSprite.Add ("Bb", 1); // 02-purple
        noteToSprite.Add ("B", 2); // 03-pink
        noteToSprite.Add ("C", 3); // 04-red
        noteToSprite.Add ("C#", 4); // 05-orange
        noteToSprite.Add ("Db", 4); // 05-orange
        noteToSprite.Add ("D", 5); // 06-lightorange
        noteToSprite.Add ("D#", 6); // 07-yellow
        noteToSprite.Add ("Eb", 6); // 07-yellow
        noteToSprite.Add ("E", 7); // 08-greenish-yellow
        noteToSprite.Add ("F", 8); // 09-green
        noteToSprite.Add ("F#", 9); // 10-teal
        noteToSprite.Add ("Gb", 9); // 10-teal
        noteToSprite.Add ("G", 10); // 11-lightblue
        noteToSprite.Add ("G#", 11); // 12-blue
        noteToSprite.Add ("Ab", 11); // 12-blue

        // Parse as "C#" instead of "C" and "#" in separate properties.
        string finalNote = note.note + note.alter;

        // Retrieve sprite children name from dictionary.
        int spriteNumber = noteToSprite [finalNote];

        Sprite[] sprites = Resources.LoadAll<Sprite> ("Sprites/PicksColored");

        spriteWhenActive = sprites [spriteNumber];
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
            // Player entered in the note.
            _active = true;
            _timeLeft = _baseTimeLeft;
            _spriteRenderer.sprite = spriteWhenActive;
        }
    }
}
