using System.Collections.Generic;
using UnityEngine;

public class SelectMenuData : MonoBehaviour
{
    [System.Serializable]
    public class ChartInfo
    {
        public string title;
        public string rank;
        public float difficulty;
        public string jsonFileName;
    }
    public ChartInfo[] InfoList { get => _infoList.Value; set => _infoList.Value = value; }
    public System.IObservable<ChartInfo[]> onInfoListChanged => _infoList;
    private readonly UniRx.ReactiveProperty<ChartInfo[]> _infoList = new();
    public string SelectedRank { get => _selectedRank.Value; set => _selectedRank.Value = value; }
    public System.IObservable<string> onSelectedRankChanged => _selectedRank;
    private readonly UniRx.ReactiveProperty<string> _selectedRank = new();
    public float NoteSpeed { get => _noteSpeed.Value; set => _noteSpeed.Value = value; }
    public System.IObservable<float> onNoteSpeedChanged => _noteSpeed;
    private readonly UniRx.ReactiveProperty<float> _noteSpeed = new();
}
