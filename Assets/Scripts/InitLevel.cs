using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Level = Model.Level;
using Note = Model.Note;

public class InitLevel : MonoBehaviour
{
    public GameObject noteObjectToCreate;

    public string levelName;

    private Note previousNote;

    // Use this for initialization
    void Start ()
    {
        string filePath = "Levels/" + levelName;

        // Read the json from the file into a string
        TextAsset dataAsJson = Resources.Load<TextAsset> (filePath);

        if (dataAsJson) {
            // Deserialize
            Level level = JsonUtility.FromJson<Level> (dataAsJson.text);

            foreach (Note note in level.notes) {
                if (null != previousNote) {
                    note.x = previousNote.x;
                    note.y = previousNote.y * 2;
                }
                CreateObject (note);
                previousNote = note;
            }

            // Todo: create the game objects \o/

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

        LightNoteInteraction sc = noteGameObject.GetComponent<LightNoteInteraction> ();
        sc.sequenceNumber = note.sequenceNumber;
        sc.duration = note.duration;
        sc.SetActiveSpriteFromNote (note);
        sc.StartInteraction ();
    }
}
