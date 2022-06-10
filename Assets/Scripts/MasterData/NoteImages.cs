using UnityEngine;
using UnityEngine.UI;

// 後々ここを動的に変更できるUIが欲しい
namespace MasterData
{
    [System.Serializable]
    public class NoteImage
    {
        public string code;
        public Sprite sprite;
    }

    [CreateAssetMenu]
    public class NoteImages : ScriptableObject
    {
        public NoteImage[] noteImages;
    }
}
