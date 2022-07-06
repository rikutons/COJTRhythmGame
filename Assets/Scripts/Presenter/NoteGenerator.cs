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

    public void setNoteSpawnX(float noteSpawnX)
    {
        this.noteSpawnX = noteSpawnX;
        judgePresenter.setDistanceOffset(noteSpawnX / settings.notesSpeed);
    }

    public void Generate(Chart.Note noteData)
    {
        GameObject note = Instantiate(notePrefab);
        foreach (var tone in noteData.tone)
        {
            GameObject noteSprite = Instantiate(noteSpritePrefab);
            noteSprite.transform.position = judgeLineTransform.position + Vector3.right * noteSpawnX + Vector3.up * (C4Pos + tone * toneHeight);
            noteSprite.transform.parent = note.transform;
            bool isRest = Array.Exists(noteData.pitches, x => x == '-');
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
        }
        judgePresenter.AddNote(note);
    }
}