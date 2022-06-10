using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

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
    private float noteSpawnX;
    private Chart chart;
    private int noteIndex = 0;
    private double time = 0;
    async void Start()
    {
        chart = JsonReader.Read(jsonFileName);
        noteIndex = 0;
        Debug.Log(chart.path);
        var clip = await Resources.LoadAsync<AudioClip>("Musics/" + chart.path);
        audioSource.clip = clip as AudioClip;
        Debug.Log(noteSpawnX / settings.notesSpeed + constants.timeOffset);
        time = Time.time;
        audioSource.PlayDelayed(noteSpawnX / settings.notesSpeed + constants.timeOffset);
        this.UpdateAsObservable()
            .Where(_ => noteIndex < chart.notes.Length)
            .Where(_ => chart.notes[noteIndex].timing <= (Time.time - time))
            .Subscribe(_ =>
            {
                Debug.Log(Time.time - time);
                noteGenerator.Generate(chart.notes[noteIndex], noteSpawnX);
                noteIndex++;
            }
        );
    }
}
