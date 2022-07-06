using UnityEngine;
using UniRx;

namespace DataModel
{
    public class UserScore : MonoBehaviour
    {
        public int Score { get => _score.Value; set => _score.Value = value; }
        public System.IObservable<int> onScoreChanged => _score;
        private readonly UniRx.ReactiveProperty<int> _score = new();
        public int Combo { get => _combo.Value; set => _combo.Value = value; }
        public System.IObservable<int> onComboChanged => _combo;
        private readonly UniRx.ReactiveProperty<int> _combo = new();
        public int PerfectCount { get => _perfectCount.Value; set => _perfectCount.Value = value; }
        public System.IObservable<int> onPerfectCountChanged => _perfectCount;
        private readonly UniRx.ReactiveProperty<int> _perfectCount = new();
        public int GreatCount { get => _greatCount.Value; set => _greatCount.Value = value; }
        public System.IObservable<int> onGreatCountChanged => _greatCount;
        private readonly UniRx.ReactiveProperty<int> _greatCount = new();
        public int GoodCount { get => _goodCount.Value; set => _goodCount.Value = value; }
        public System.IObservable<int> onGoodCountChanged => _goodCount;
        private readonly UniRx.ReactiveProperty<int> _goodCount = new();
        public int MissCount { get => _missCount.Value; set => _missCount.Value = value; }
        public System.IObservable<int> onMissCountChanged => _missCount;
        private readonly UniRx.ReactiveProperty<int> _missCount = new();
        public UniRx.Subject<Unit> onSuccess = new();
        private void Start()
        {
            onPerfectCountChanged.Subscribe(_ => onSuccess.OnNext(Unit.Default));
            onGreatCountChanged.Subscribe(_ => onSuccess.OnNext(Unit.Default));
            onGoodCountChanged.Subscribe(_ => onSuccess.OnNext(Unit.Default));
        }
    }
}