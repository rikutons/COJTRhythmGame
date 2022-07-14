using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;

public class ChartPresenter : MonoBehaviour
{
    [SerializeField]
    private string jsonFileName;
    [SerializeField]
    private NoteGenerator noteGenerator;
    [SerializeField]
    private Settings settings;
    [SerializeField]
    private MasterData.Constants constants;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private ResultPresenter resultPresenter;
    [SerializeField]
    private float noteSpawnX;
    [SerializeField]
    private int waitSecond;
    private Chart chart;
    private int noteIndex = 0;
    private double startTime = 0;
    private float barTime = 0;
    bool start = false;
    
    [SerializeField]
    private TextMeshProUGUI title;

    public void SetJson(string json)
    {
        jsonFileName = json;
    }
    async void Start()
    {
        chart = JsonReader.Read(jsonFileName);
        var clip = await Resources.LoadAsync<AudioClip>("Musics/" + chart.path);
        noteIndex = 0;
        audioSource.clip = clip as AudioClip;
        noteGenerator.setNoteSpawnX(noteSpawnX);
        title.text = chart.title.ToString();
        StartPlaying();
    }

    private async void StartPlaying()
    {
        await UniTask.Delay(waitSecond);
        audioSource.PlayDelayed(noteSpawnX / settings.NoteSpeed + constants.timeOffset);
        start = true;
        barTime = -4f / 8 / chart.bpm * 60 + chart.barInterval;
        this.UpdateAsObservable()
            .Where(_ => !audioSource.isPlaying)
            .Take(1)
            .Subscribe(_ =>
            {
                Debug.Log("show");
                resultPresenter.Show();
            }
        );
        startTime = Time.realtimeSinceStartup;
    }

    void FixedUpdate()
    {
        if(!start) return;
        double time = Time.realtimeSinceStartup - startTime;
        if(barTime <= time) 
        {
            noteGenerator.GenerateBar();
            barTime += chart.barInterval;
        }
        if(noteIndex >= chart.notes.Length) return;
        if(chart.notes[noteIndex].timing > time) return;
        noteGenerator.Generate(chart.notes[noteIndex]);
        noteIndex++;
    }
}
