using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class NotePresenter : MonoBehaviour
{
    public float NoteSpeed { set => this.noteSpeed = value; }
    private float noteSpeed;
    private new Renderer renderer = null;
    private bool appeared = false;
    private float beforeTime;

    void Start()
    {
        if(transform.childCount > 0)
            renderer = transform.GetChild(0).GetComponent<Renderer>();
        else
            renderer = GetComponent<Renderer>();
        beforeTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        transform.position = transform.position + Vector3.left * noteSpeed * (Time.realtimeSinceStartup - beforeTime);
        if(appeared && !renderer.isVisible)
            Destroy(gameObject);
        appeared = renderer.isVisible;
        beforeTime = Time.realtimeSinceStartup;
    }
}
