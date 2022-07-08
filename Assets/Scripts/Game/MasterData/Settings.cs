using UnityEngine;

// 後々ここを動的に変更できるUIが欲しい
namespace MasterData
{
    [CreateAssetMenu]
    public class Settings : ScriptableObject
    {
        public float notesSpeed;
    }
}
