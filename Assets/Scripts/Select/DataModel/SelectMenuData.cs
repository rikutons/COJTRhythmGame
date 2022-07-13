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
    }
    public ChartInfo[] InfoList { get => _infoList.Value; set => _infoList.Value = value; }
    public System.IObservable<ChartInfo[]> onInfoListChanged => _infoList;
    private readonly UniRx.ReactiveProperty<ChartInfo[]> _infoList = new();
    public string[] Paths { get => _paths.Value; set => _paths.Value = value; }
    public System.IObservable<string[]> onPathsChanged => _paths;
    private readonly UniRx.ReactiveProperty<string[]> _paths = new();
}
