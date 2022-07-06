using System;
using UnityEngine;
using UniRx;

public class EffectPresenter : MonoBehaviour
{
    [SerializeField]
    private DataModel.UserScore userScore;
    [SerializeField]
    private GameObject missText;
    [SerializeField]
    private GameObject goodText;
    [SerializeField]
    private GameObject greatText;
    [SerializeField]
    private GameObject perfectText;
    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        SubAudioEvent();
        SubTextEvent();
    }

    private void SubAudioEvent()
    {
        float defaultVolume = audioSource.volume;
        userScore.onSuccess.Subscribe(_ => audioSource.volume = defaultVolume );
        userScore.onMissCountChanged.Subscribe(_ => audioSource.volume = defaultVolume * 0.3f );
    }

    private void SubTextEvent()
    {
        userScore.onPerfectCountChanged.Subscribe(_ =>
        {
            perfectText.SetActive(true);

            Observable.Timer(TimeSpan.FromMilliseconds(200))
                      .Subscribe(_ => perfectText.SetActive(false));
        });
        userScore.onGreatCountChanged.Subscribe(_ =>
        {
            greatText.SetActive(true);

            Observable.Timer(TimeSpan.FromMilliseconds(200))
                      .Subscribe(_ => greatText.SetActive(false));
        });
        userScore.onGoodCountChanged.Subscribe(_ =>
        {
            goodText.SetActive(true);

            Observable.Timer(TimeSpan.FromMilliseconds(200))
                      .Subscribe(_ => goodText.SetActive(false));
        });
        userScore.onMissCountChanged.Subscribe(_ =>
        {
            missText.SetActive(true);

            Observable.Timer(TimeSpan.FromMilliseconds(200))
                      .Subscribe(_ => missText.SetActive(false));
        });
    }
}
