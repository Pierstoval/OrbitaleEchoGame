using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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
        string filePath = Path.Combine (Application.streamingAssetsPath, levelName + ".json");

        if (File.Exists (filePath)) {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText (filePath);

            // Deserialize
            Level level = JsonUtility.FromJson<Level> (dataAsJson);

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
