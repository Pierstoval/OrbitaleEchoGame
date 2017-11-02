using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Level = Model.Level;
using Note = Model.Note;
using NotesSequence = Model.NotesSequence;

public class InitLevel : MonoBehaviour
{
    public GameObject noteObjectToCreate;

    public string levelName;

    private Note previousNote;

    private NotesSequence sequence;

    // Use this for initialization
    void Awake ()
    {
        string filePath = "Levels/" + levelName;

        sequence = new NotesSequence ();

        // Read the json from the file into a string
        TextAsset dataAsJson = Resources.Load<TextAsset> (filePath);

        if (dataAsJson) {
            // Deserialize
            Level level = JsonUtility.FromJson<Level> (dataAsJson.text);

            foreach (Note note in level.notes) {
                if (null != previousNote) {
                    var x = previousNote.duration * Mathf.Cos (previousNote.angle * Mathf.Deg2Rad);
                    var y = previousNote.duration * Mathf.Sin (previousNote.angle * Mathf.Deg2Rad);
                    note.x = previousNote.x + x;
                    note.y = previousNote.y + y;
                }
                CreateObject (note);
                previousNote = note;
            }

            previousNote = null;
        } else {
            Debug.LogError ("Cannot load game data!");
        }
    }

    void CreateObject (Note note, Note previousNote = null)
    {
        GameObject noteGameObject = 
            Instantiate (
                noteObjectToCreate,
                new Vector2 (note.x, note.y),
                Quaternion.Euler (0f, 0f, note.angle)
            );

        sequence.AddNoteObject (noteGameObject, note);

        LightNoteInteraction sc = noteGameObject.GetComponent<LightNoteInteraction> ();
        sc.sequenceNumber = note.sequenceNumber;
        sc.duration = note.duration;
        sc.SetActiveSpriteFromNote (note);
        sc.StartInteraction ();
        sc.SetSequence (sequence);
    }
}
