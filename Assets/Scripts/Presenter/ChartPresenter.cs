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
    private float noteSpawnX;
    private Chart chart;
    private int noteIndex = 0;
    private double time = 0;
    
    [SerializeField]
    private TextMeshProUGUI title;

    async void Start()
    {
        chart = JsonReader.Read(jsonFileName);
        noteIndex = 0;
        var clip = await Resources.LoadAsync<AudioClip>("Musics/" + chart.path);
        audioSource.clip = clip as AudioClip;
        time = Time.time;

        title.text = chart.title.ToString();

        audioSource.PlayDelayed(noteSpawnX / settings.notesSpeed + constants.timeOffset);
        this.UpdateAsObservable()
            .Where(_ => noteIndex < chart.notes.Length)
            .Where(_ => chart.notes[noteIndex].timing <= (Time.time - time))
            .Subscribe(_ =>
            {
                noteGenerator.Generate(chart.notes[noteIndex], noteSpawnX);
                noteIndex++;
            }
        );
    }
}
