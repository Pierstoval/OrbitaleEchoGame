using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Note = Model.Note;

namespace Model
{
    /**
     * This object is created for each level via the "InitLevel" class.
     * The created instance is then available via the "LightNoteInteraction" class to check for the current sequence.
     */
    public class NotesSequence
    {
        private Dictionary<int, List<GameObject>> objectsList;

        public NotesSequence ()
        {
            objectsList = new Dictionary<int, List<GameObject>> ();
        }

        public void AddNoteObject (GameObject noteObject, Note note)
        {
            if (!objectsList.ContainsKey (note.sequenceNumber)) {
                objectsList [note.sequenceNumber] = new List<GameObject> ();
            }

            objectsList [note.sequenceNumber].Add (noteObject);
        }
    }
}

