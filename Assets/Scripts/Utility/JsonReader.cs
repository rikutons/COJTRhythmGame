using System;
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
        string[] values = jsonChart.beat.Split('/');
        int upper = int.Parse(values[0]);
        int lower = int.Parse(values[1]);
        chart.notes = new Chart.Note[jsonChart.notes.Length];
        for (int i = 0; i < jsonChart.notes.Length; i++)
        {
            JsonChart.Note n = jsonChart.notes[i];
            chart.notes[i].tone = new int[n.tone.Length];
            Array.Copy(n.tone, chart.notes[i].tone, n.tone.Length);
            chart.notes[i].length = n.length;
            chart.notes[i].kind = n.kind;
            chart.notes[i].timing = (((double)n.timing[0] - 1) * upper / lower * 4 + (double)(n.timing[1] - 1)) / chart.bpm * 60;
        }
        return chart;
    }
}
