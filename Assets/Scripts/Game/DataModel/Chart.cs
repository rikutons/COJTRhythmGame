using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chart
{
    public struct Note
    {
        public string length;
        public string kind;
        public string[] options;
        public int[] tone;
        public char[] pitches;
        public double timing;
        public bool isTuplet;
    };
    public string title;
    public float difficutly;
    public string path;
    public int bpm;
    public string beat;
    public Note[] notes;
    public float barInterval;
}
