using UnityEngine;
using System;

public class NoteGenerator : MonoBehaviour {
    [SerializeField]
    GameObject notePrefab;
    [SerializeField]
    GameObject barPrefab;
    [SerializeField]
    GameObject noteSpritePrefab;
    [SerializeField]
    private Settings settings;
    [SerializeField]
    private Transform judgeLineTransform;
    [SerializeField]
    private float C4Pos;
    [SerializeField]
    private float toneHeight;
    [SerializeField]
    private float rotateOffset;
    [SerializeField]
    private JudgePresenter judgePresenter;
    private float noteSpawnX;

    private bool beforeIsTie = false;
    private double beforeTiming = 0d;

    public void setNoteSpawnX(float noteSpawnX)
    {
        this.noteSpawnX = noteSpawnX;
        judgePresenter.setDistanceOffset(noteSpawnX / settings.NoteSpeed);
    }
    
    public void GenerateBar()
    {
        GameObject bar = Instantiate(barPrefab);
        var pos = judgeLineTransform.position;
        pos.x += noteSpawnX;
        pos.y = 0f;
        bar.transform.position = pos;
        bar.GetComponent<NotePresenter>().NoteSpeed = settings.NoteSpeed;
    }

    public void Generate(Chart.Note noteData)
    {
        GameObject note = Instantiate(notePrefab);
        bool isRest = Array.Exists(noteData.pitches, x => x == '-');
        bool isTie = Array.Exists(noteData.options, x => x == "tie");
        bool isAccent = Array.Exists(noteData.options, x => x == "accent");
        bool isStaccato = Array.Exists(noteData.options, x => x == "staccato");

        Vector3 noteTop = new Vector3(0,-100,0);
        Vector3 noteBottom = new Vector3(0,100,0);

        foreach (var tone in noteData.tone)
        {
            GameObject noteSprite = Instantiate(noteSpritePrefab);
            noteSprite.transform.position = judgeLineTransform.position + Vector3.right * noteSpawnX + Vector3.up * (C4Pos + tone * toneHeight);

            if(noteSprite.transform.position.y > noteTop.y){
                noteTop = noteSprite.transform.position;
            }
            if(noteSprite.transform.position.y < noteBottom.y){
                noteBottom = noteSprite.transform.position;
            }

            noteSprite.transform.parent = note.transform;
            if(!isRest && tone >= 6)
            {
                noteSprite.transform.Rotate(Vector3.back * 180);
                noteSprite.transform.position -= Vector3.up * (rotateOffset);
                if(noteData.length.EndsWith('.')){
                    noteData.length = "gyaku-" + noteData.length;
                }
            }
            NoteSpritePresenter noteSpritePresenter = noteSprite.GetComponent<NoteSpritePresenter>();

            if(isRest){
                noteSpritePresenter.Init(noteData.length + "rest");
            }else {
                noteSpritePresenter.Init(noteData.length);
            }

            if(beforeIsTie){
                GameObject tieSprite = Instantiate(noteSpritePrefab);
                tieSprite.transform.position = noteSprite.transform.position - Vector3.right * (float)(noteData.timing - beforeTiming) / 2 * settings.NoteSpeed - Vector3.up;
                tieSprite.transform.localScale = new Vector3(tieSprite.transform.localScale.x * (float)(noteData.timing - beforeTiming) * settings.NoteSpeed * 0.35f,tieSprite.transform.localScale.y,tieSprite.transform.localScale.z);
                tieSprite.transform.parent = note.transform;
                NoteSpritePresenter tieSpritePresenter = tieSprite.GetComponent<NoteSpritePresenter>();
                tieSpritePresenter.Init("tie");
            }
        }

        if(isStaccato){
            GameObject staccatoSprite = Instantiate(noteSpritePrefab);
            staccatoSprite.transform.position = noteBottom - Vector3.up * 1.1f;
            staccatoSprite.transform.parent = note.transform;
            NoteSpritePresenter staccatoSpritePresenter = staccatoSprite.GetComponent<NoteSpritePresenter>();
            staccatoSpritePresenter.Init("staccato");
        }
        if(isAccent){
            GameObject accentSprite = Instantiate(noteSpritePrefab);
            if(isStaccato) {
                accentSprite.transform.position = noteBottom - Vector3.up * 1.3f;
            } else {
                accentSprite.transform.position = noteBottom - Vector3.up * 1.2f;
            }
            accentSprite.transform.localScale *= 0.8f; 
            accentSprite.transform.parent = note.transform;
            NoteSpritePresenter accentSpritePresenter = accentSprite.GetComponent<NoteSpritePresenter>();
            accentSpritePresenter.Init("accent");
        }

        note.GetComponent<NotePresenter>().NoteSpeed = settings.NoteSpeed;
        beforeIsTie = isTie;
        beforeTiming = noteData.timing;
        judgePresenter.AddNote(note, isRest ? new string[]{"rest"} : noteData.options);
    }
}