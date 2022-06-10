using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chart
{
    public struct Note
    {
        public string length;
        public string kind;
        public int[] tone;
        public double timing;
    };
    public float difficutly;
    public string path;
    public int bpm;
    public string beat;
    public Note[] notes;
}
