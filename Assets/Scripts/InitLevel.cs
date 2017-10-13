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

    // Use this for initialization
    void Start ()
    {
        string filePath = Path.Combine (Application.streamingAssetsPath, levelName + ".json");

        if (File.Exists (filePath)) {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText (filePath); 
            Debug.Log (dataAsJson);

            // Deserialize
            Level level = JsonUtility.FromJson<Level> (dataAsJson);
            Debug.Log (level.name);
            Debug.Log (level.notes);
            //Debug.Log (level.notes [0].delay);
            //Debug.Log (level.notes [0].x);
            //Debug.Log (level.notes [0].y);
            //Debug.Log (level.notes [0].sequenceNumber);

        } else {
            Debug.LogError ("Cannot load game data!");
        }
    }

    private void CreateObject (ArrayList config)
    {
        Instantiate (
            noteObjectToCreate, 
            new Vector3 (this.rp (), 1f, this.rp ()),
            new Quaternion (this.ra (), this.ra (), this.ra (), 0)
        );
    }

    // Random position
    private float rp ()
    {
        return Random.Range (-8, 8);
    }

    // Random angle
    private float ra ()
    {
        return Random.Range (-180, 180);
    }
}
