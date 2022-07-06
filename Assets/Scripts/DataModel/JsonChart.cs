using System.Collections;
using System.Collections.Generic;

namespace COJTRhythmGame.JsonUtil
{
    [System.Serializable]
    public struct JsonChart
    {
        [System.Serializable]
        public struct Note
        {
            public string length;
            public string[] tone;
            public string[] option;
            public bool istuplet;
        }
        public string title;
        public float difficulty;
        public string path;
        public int bpm;
        public string beat;
        public Note[] notes;
    }
}
