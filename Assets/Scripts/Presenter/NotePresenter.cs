using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class NotePresenter : MonoBehaviour
{
    [SerializeField]
    private MasterData.Settings settings;
    [SerializeField]
    private MasterData.JudgeTiming judgeTiming;
    public ScorePresenter scorePresenter;
    private float timing;

    public void Init(float timing)
    {
        this.timing = timing;
    }

    void Update()
    {
        transform.position = transform.position + Vector3.left * settings.notesSpeed * Time.deltaTime;
        float distance = timing - Time.time;
        if(Input.GetKeyDown(KeyCode.Space))
            Judge(distance);
        if(distance < -judgeTiming.missSecond)
        {
            scorePresenter.AddScore("miss");
            Destroy(this.gameObject);
        }
    }
    
    private void Judge(float distance)
    {
        float absDistance = Mathf.Abs(timing - Time.time);
        if(absDistance < judgeTiming.perfectSecond)
        {
            scorePresenter.AddScore("perfect");
            Destroy(this.gameObject);
        }
        else if(absDistance < judgeTiming.greatSecond)
        {
            scorePresenter.AddScore("great");
            Destroy(this.gameObject);
        }
        else if(absDistance < judgeTiming.goodSecond)
        {
            scorePresenter.AddScore("good");
            Destroy(this.gameObject);
        }
        else if(absDistance < judgeTiming.missSecond)
        {
            scorePresenter.AddScore("miss");
            Destroy(this.gameObject);
        }
    }
}
