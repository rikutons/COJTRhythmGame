using System;
using System.Collections.Generic;
using UnityEngine;

public class JudgePresenter : MonoBehaviour
{
    enum NoteType
    {
        normal,
        accent,
        tieStart,
        tieMid,
        tieEnd,
    }
    struct NoteObject
    {
        public float startTime;
        public NoteType type;
        public GameObject note;
    }
    [SerializeField]
    private MasterData.Settings settings;
    [SerializeField]
    private MasterData.JudgeTiming judgeTiming;
    [SerializeField]
    private DataModel.UserScore userScore;
    [SerializeField]
    private AudioSource debugSE;
    [SerializeField]
    private bool isDebugMode;
    private float distanceOffset;
    Queue<NoteObject> notes = new Queue<NoteObject>();

    public void setDistanceOffset(float distanceOffset)
    {
        this.distanceOffset = distanceOffset;
    }

    private bool beforeIsTie = false;
    public void AddNote(GameObject note, string[] type)
    {
        if(Array.Exists(type, x => x == "rest"))
        {
            Destroy(note, distanceOffset + 3f);
            return;
        }
        NoteObject noteObject;
        noteObject.note = note;

        bool isTie = Array.Exists(type, x => x == "tie");
        if(Array.Exists(type, x => x == "accent"))
            noteObject.type = NoteType.accent;
        else if(!beforeIsTie && isTie)
            noteObject.type = NoteType.tieStart;
        else if(beforeIsTie && isTie)
            noteObject.type = NoteType.tieMid;
        else if(beforeIsTie && !isTie)
            noteObject.type = NoteType.tieEnd;
        else
            noteObject.type = NoteType.normal;
        beforeIsTie = isTie;

        noteObject.startTime = Time.time;
        notes.Enqueue(noteObject);
    }

    private void Update() 
    {
        if(notes.Count == 0)
            return;
        NoteObject nowNote = notes.Peek();
        float deltaTime = nowNote.startTime + distanceOffset - Time.time;
        switch (nowNote.type)
        {
        case NoteType.normal:
        case NoteType.tieStart:
            if(Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space))
                Judge(deltaTime);
            break;
        case NoteType.tieMid:
        case NoteType.tieEnd:
            if(Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.Space))
                if(deltaTime <= 0)
                    Judge(deltaTime);
            break;
        case NoteType.accent:
            if(Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.F))
                Judge(deltaTime);
            break;
        }
        if(isDebugMode && deltaTime < 0)
        {
            userScore.PerfectCount++;
            debugSE.Play();
            Destroy(notes.Dequeue().note);
        }
        if(deltaTime < -judgeTiming.missSecond)
        {
            userScore.MissCount++;
            Destroy(notes.Dequeue().note, 3f);
        }
    }

    private void Judge(float deltaTime, bool fastVanish = false)
    {
        float absDeltaTime = Mathf.Abs(deltaTime);
        if(!fastVanish && absDeltaTime >= judgeTiming.missSecond)
            return;

        if(absDeltaTime < judgeTiming.perfectSecond)
            userScore.PerfectCount++;
        else if(absDeltaTime < judgeTiming.greatSecond)
            userScore.GreatCount++;
        else if(absDeltaTime < judgeTiming.goodSecond)
            userScore.GoodCount++;
        else if(absDeltaTime < judgeTiming.missSecond)
            userScore.MissCount++;
        Destroy(notes.Dequeue().note);
    }
}
