using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    /**
     * This class is used by LightNoteInteraction to determine the active sprite based on the note configured in JSON.
     */
    public class NotesSpritesReferences
    {
        private static Dictionary<string, int> notesToSprite;

        private static Sprite[] resourcesSprites;

        public static Sprite GetSpriteFromNote (Note note)
        {
            // Parse as "C#" instead of "C" and "#" in separate properties.
            string finalNote = note.note + note.alter;

            // Retrieve sprite children name from dictionary.
            int spriteNumber = GetNotesToSprite () [finalNote];

            return GetResourcesSprites () [spriteNumber];
        }

        private static Dictionary<string, int> GetNotesToSprite ()
        {
            if (null == notesToSprite) {
                notesToSprite = new Dictionary<string, int> ();
                notesToSprite.Add ("A", 0); // 01-purpleish-blue
                notesToSprite.Add ("A#", 1); // 02-purple
                notesToSprite.Add ("Bb", 1); // 02-purple
                notesToSprite.Add ("B", 2); // 03-pink
                notesToSprite.Add ("C", 3); // 04-red
                notesToSprite.Add ("C#", 4); // 05-orange
                notesToSprite.Add ("Db", 4); // 05-orange
                notesToSprite.Add ("D", 5); // 06-lightorange
                notesToSprite.Add ("D#", 6); // 07-yellow
                notesToSprite.Add ("Eb", 6); // 07-yellow
                notesToSprite.Add ("E", 7); // 08-greenish-yellow
                notesToSprite.Add ("F", 8); // 09-green
                notesToSprite.Add ("F#", 9); // 10-teal
                notesToSprite.Add ("Gb", 9); // 10-teal
                notesToSprite.Add ("G", 10); // 11-lightblue
                notesToSprite.Add ("Ab", 11); // 12-blue
            }

            return notesToSprite;
        }

        private static Sprite[] GetResourcesSprites ()
        {
            if (null == resourcesSprites) {
                resourcesSprites = Resources.LoadAll<Sprite> ("Sprites/PicksColored");
            }

            return resourcesSprites;
        }
    }
}
