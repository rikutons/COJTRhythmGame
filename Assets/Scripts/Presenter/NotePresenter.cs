using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class NotePresenter : MonoBehaviour
{
    [SerializeField]
    private MasterData.Settings settings;

    void Update()
    {
        transform.position = transform.position + Vector3.left * settings.notesSpeed * Time.deltaTime;
    }
}
