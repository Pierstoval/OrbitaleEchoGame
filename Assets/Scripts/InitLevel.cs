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

            // Deserialize
            Level level = JsonUtility.FromJson<Level> (dataAsJson);

            // Todo: create the game objects \o/

        } else {
            Debug.LogError ("Cannot load game data!");
        }
    }
}
