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
    [SerializeField]
    private ParticleSystem perfectParticle;
    [SerializeField]
    private ParticleSystem greatParticle;
    [SerializeField]
    private ParticleSystem goodParticle;

    private void Start()
    {
        SubAudioEvent();
        SubTextEvent();
        SubParticleEvent();
    }

    private void SubAudioEvent()
    {
        float defaultVolume = audioSource.volume;
        userScore.onSuccess.Subscribe(_ => audioSource.volume = defaultVolume );
        userScore.onMissCountChanged.Subscribe(_ => audioSource.volume = defaultVolume * 0.3f );
    }

    private void SubTextEvent()
    {
        userScore.onPerfectCountChanged.Subscribe(_ => Instantiate(perfectText));
        userScore.onGreatCountChanged.Subscribe(_ => Instantiate(greatText));
        userScore.onGoodCountChanged.Subscribe(_ => Instantiate(goodText));
        userScore.onMissCountChanged.Subscribe(_ => Instantiate(missText));
    }

    private void SubParticleEvent()
    {
        userScore.onPerfectCountChanged.Subscribe(_ => perfectParticle.Play());
        userScore.onGreatCountChanged.Subscribe(_ => greatParticle.Play());
        userScore.onGoodCountChanged.Subscribe(_ => goodParticle.Play());
    }
}
