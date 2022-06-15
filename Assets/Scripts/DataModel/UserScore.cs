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
    }
}