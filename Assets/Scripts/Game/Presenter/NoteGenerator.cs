using UnityEngine;
using System;

public class NoteGenerator : MonoBehaviour {
    [SerializeField]
    GameObject notePrefab;
    [SerializeField]
    GameObject noteSpritePrefab;
    [SerializeField]
    private MasterData.Settings settings;
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
        judgePresenter.setDistanceOffset(noteSpawnX / settings.notesSpeed);
    }

    public void Generate(Chart.Note noteData)
    {
        GameObject note = Instantiate(notePrefab);
        bool isRest = Array.Exists(noteData.pitches, x => x == '-');
        bool isTie = Array.Exists(noteData.options, x => x == "tie");
        bool isAccent = Array.Exists(noteData.options, x => x == "accent");
        
        foreach (var tone in noteData.tone)
        {
            GameObject noteSprite = Instantiate(noteSpritePrefab);
            noteSprite.transform.position = judgeLineTransform.position + Vector3.right * noteSpawnX + Vector3.up * (C4Pos + tone * toneHeight);
            noteSprite.transform.parent = note.transform;
            if(!isRest && tone >= 6)
            {
                noteSprite.transform.Rotate(Vector3.back * 180);
                noteSprite.transform.position -= Vector3.up * (rotateOffset);
            }
            NoteSpritePresenter noteSpritePresenter = noteSprite.GetComponent<NoteSpritePresenter>();

            if(isRest){
                noteSpritePresenter.Init(noteData.length + "rest");
            }else {
                noteSpritePresenter.Init(noteData.length);
            }

            if(isAccent){
                GameObject accentSprite = Instantiate(noteSpritePrefab);
                accentSprite.transform.position = noteSprite.transform.position - Vector3.up * 1.2f;
                accentSprite.transform.localScale *= 0.8f; 
                accentSprite.transform.parent = note.transform;
                NoteSpritePresenter accentSpritePresenter = accentSprite.GetComponent<NoteSpritePresenter>();
                accentSpritePresenter.Init("accent");
            }

            if(beforeIsTie){
                GameObject tieSprite = Instantiate(noteSpritePrefab);
                tieSprite.transform.position = noteSprite.transform.position - Vector3.right * (float)(noteData.timing - beforeTiming) / 2 * settings.notesSpeed - Vector3.up;
                tieSprite.transform.localScale = new Vector3(tieSprite.transform.localScale.x * (float)(noteData.timing - beforeTiming) * settings.notesSpeed * 0.35f,tieSprite.transform.localScale.y,tieSprite.transform.localScale.z);
                tieSprite.transform.parent = note.transform;
                NoteSpritePresenter tieSpritePresenter = tieSprite.GetComponent<NoteSpritePresenter>();
                tieSpritePresenter.Init("tie");
            }
        }

        beforeIsTie = isTie;
        beforeTiming = noteData.timing;
        judgePresenter.AddNote(note, isRest ? new string[]{"rest"} : noteData.options);
    }
}