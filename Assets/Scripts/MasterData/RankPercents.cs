using UnityEngine;

namespace MasterData
{
    [System.Serializable]
    public class Rank
    {
        public string rankName;
        public float percent;
        public Color color;
    }
    [CreateAssetMenu]
    public class RankPercents : ScriptableObject
    {
        public Rank[] ranks;
    }
}
