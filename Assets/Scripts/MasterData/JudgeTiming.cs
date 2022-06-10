using UnityEngine;

namespace MasterData
{
    [CreateAssetMenu]
    public class JudgeTiming : ScriptableObject
    {
        #region public property
        public float perfectSecond;
        public float greatSecond;
        public float goodSecond;
        public float missSecond;
        #endregion
    }
}
