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
    private MasterData.Settings settings;
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
    
    [SerializeField]
    private TextMeshProUGUI title;

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
        startTime = Time.time;
        audioSource.PlayDelayed(noteSpawnX / settings.notesSpeed + constants.timeOffset);
        this.UpdateAsObservable()
            .Where(_ => noteIndex < chart.notes.Length)
            .Where(_ => chart.notes[noteIndex].timing <= (Time.time - startTime))
            .Subscribe(_ =>
            {
                noteGenerator.Generate(chart.notes[noteIndex]);
                noteIndex++;
            }
        );
        this.UpdateAsObservable()
            .Where(_ => !audioSource.isPlaying)
            .Take(1)
            .Subscribe(_ =>
            {
                Debug.Log("show");
                resultPresenter.Show();
            }
        );
    }
}
