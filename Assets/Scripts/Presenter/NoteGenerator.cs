using UnityEngine;

public class NoteGenerator : MonoBehaviour {
    [SerializeField]
    GameObject notePrefab;
    [SerializeField]
    private MasterData.Settings settings;
    [SerializeField]
    private Transform judgeLineTransform;
    [SerializeField]
    private float C4Pos;
    [SerializeField]
    private float toneHeight;
    public void Generate(Chart.Note noteData, float noteSpawnX)
    {
        GameObject note = Instantiate(notePrefab);
        foreach (var tone in noteData.tone)
        {
            note.transform.position = judgeLineTransform.position + Vector3.right * noteSpawnX + Vector3.up * (C4Pos + tone * toneHeight);
            NotePresenter notePresenter = note.GetComponent<NotePresenter>();
            notePresenter.Init(noteData.length, noteSpawnX / settings.notesSpeed + Time.time);
        }
    }
}