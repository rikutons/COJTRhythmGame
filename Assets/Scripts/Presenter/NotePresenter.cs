using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class NotePresenter : MonoBehaviour
{
    [SerializeField]
    private MasterData.NoteImages noteImages;
    [SerializeField]
    private MasterData.Settings settings;
    [SerializeField]
    private MasterData.JudgeTiming judgeTiming;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    public ScorePresenter scorePresenter;

    public void Init(string code, float timing)
    {
        foreach(MasterData.NoteImage noteImage in noteImages.noteImages)
        {
            if(code == noteImage.code)
            {
                spriteRenderer.sprite = noteImage.sprite;
                break;
            }
        }

        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.Space))
            .Subscribe(_ =>
            {
                float distance = Mathf.Abs(timing - Time.time);
                if(distance < judgeTiming.perfectSecond)
                {
                    scorePresenter.AddScore("perfect");
                    Destroy(this.gameObject);
                }
                else if(distance < judgeTiming.greatSecond)
                {
                    scorePresenter.AddScore("great");
                    Destroy(this.gameObject);
                }
                else if(distance < judgeTiming.goodSecond)
                {
                    scorePresenter.AddScore("good");
                    Destroy(this.gameObject);
                }
                else if(distance < judgeTiming.missSecond)
                {
                    scorePresenter.AddScore("miss");
                    Destroy(this.gameObject);
                }
            }
        );
    }

    void Update()
    {
        transform.position = transform.position + Vector3.left * settings.notesSpeed * Time.deltaTime;
    }
}
