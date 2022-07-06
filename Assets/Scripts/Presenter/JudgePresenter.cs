using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgePresenter : MonoBehaviour
{
    struct NoteObject
    {
        public float startTime;
        public GameObject note;
    }
    [SerializeField]
    private MasterData.Settings settings;
    [SerializeField]
    private MasterData.JudgeTiming judgeTiming;
    [SerializeField]
    private ScorePresenter scorePresenter;
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

    public void AddNote(GameObject note, string[] type)
    {
        if(Array.Exists(type, x => x == "rest"))
        {
            Destroy(note, distanceOffset + 3f);
            return;
        }
        NoteObject noteObject;
        noteObject.note = note;
        noteObject.startTime = Time.time;
        notes.Enqueue(noteObject);
    }

    private void Update() 
    {
        if(notes.Count == 0)
            return;
        float deltaTime = notes.Peek().startTime + distanceOffset - Time.time;
        if(Input.GetKeyDown(KeyCode.J)||Input.GetKeyDown(KeyCode.F)||Input.GetKeyDown(KeyCode.Space))
            Judge(deltaTime);
        if(isDebugMode && deltaTime < 0)
        {
            scorePresenter.AddScore("perfect");
            debugSE.Play();
            Destroy(notes.Dequeue().note);
        }
        if(deltaTime < -judgeTiming.missSecond)
        {
            scorePresenter.AddScore("miss");
            Destroy(notes.Dequeue().note, 3f);
        }
    }

    private void Judge(float deltaTime)
    {
        float absDeltaTime = Mathf.Abs(deltaTime);
        if(absDeltaTime >= judgeTiming.missSecond)
            return;

        if(absDeltaTime < judgeTiming.perfectSecond)
            scorePresenter.AddScore("perfect");
        else if(absDeltaTime < judgeTiming.greatSecond)
            scorePresenter.AddScore("great");
        else if(absDeltaTime < judgeTiming.goodSecond)
            scorePresenter.AddScore("good");
        else if(absDeltaTime < judgeTiming.missSecond)
            scorePresenter.AddScore("miss");
        Destroy(notes.Dequeue().note);
    }
}
