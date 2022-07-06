using System;
using System.Text.RegularExpressions;
using UnityEngine;
using COJTRhythmGame.JsonUtil;

public class JsonReader : MonoBehaviour
{
    static public Chart Read(string jsonFileName)
    {
        string data = Resources.Load<TextAsset>("Charts/" + jsonFileName).text;
        JsonChart jsonChart = JsonUtility.FromJson<JsonChart>(data);
        return JsonChartToChart(jsonChart);
    }

    static private Chart JsonChartToChart(JsonChart jsonChart)
    {
        Chart chart = new Chart();
        
        chart.difficutly = jsonChart.difficulty;
        chart.path = jsonChart.path;
        chart.bpm = jsonChart.bpm;
        chart.title = jsonChart.title;
        string[] values = jsonChart.beat.Split('/');
        int upper = int.Parse(values[0]);
        int lower = int.Parse(values[1]);
        chart.notes = new Chart.Note[jsonChart.notes.Length];
        double timing = 0;
        for (int i = 0; i < jsonChart.notes.Length; i++)
        {
            JsonChart.Note n = jsonChart.notes[i];
            chart.notes[i].tone = new int[n.tone.Length];
            chart.notes[i].pitches = new char[n.tone.Length];
            chart.notes[i].isTuplet = chart.notes[i].isTuplet;
            for (int j = 0; j < n.tone.Length; j++)
            {
                chart.notes[i].tone[j] = Int32.Parse(Regex.Match(n.tone[j], "[0-9]+").Value);
                Match note_pitch_match = Regex.Match(n.length, "[#b]");
                if(note_pitch_match.Index > 0)
                    chart.notes[i].pitches[j] = note_pitch_match.Value[0];
            }
            chart.notes[i].length = n.length;
            if(n.option != null)
                Array.Copy(n.option, chart.notes[i].options, n.option.Length);
            else
                chart.notes[i].options = new string[0];
            chart.notes[i].timing = timing;
            double length = Int32.Parse(Regex.Match(n.length, "[0-9]+").Value);
            Match length_mod_match = Regex.Match(n.length, "[.#]");
            if(length_mod_match.Index > 0)
            {
                char length_mod = length_mod_match.Value[0];
                if(length_mod == '.')
                    length /= 1.5;
                if(length_mod == '/')
                    length *= 2;
            }
            timing += 4 / length / chart.bpm * 60;
        }
        return chart;
    }
}
