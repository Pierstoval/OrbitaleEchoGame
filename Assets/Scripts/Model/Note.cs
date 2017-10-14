using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class Note
    {
        public string note;

        // Can be "", "#" or "b"
        public string alter;

        public int octave;

        public float duration;

        public int sequenceNumber;

        public float angle;

        public float x;

        public float y;
    }
}
