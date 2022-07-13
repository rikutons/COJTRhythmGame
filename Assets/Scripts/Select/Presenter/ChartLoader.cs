using System.Linq;
using UnityEngine;

public class ChartLoader : MonoBehaviour
{
    [SerializeField]
    SelectMenuData selectMenuData;
    [SerializeField]
    ChartListRenderer listRenderer;
    void Start()
    {
        selectMenuData.InfoList = Resources.LoadAll<TextAsset>("Charts")
            .Select(a => a.text)
            .Select(data => JsonUtility.FromJson<SelectMenuData.ChartInfo>(data)).
            ToArray();
        selectMenuData.Paths = Resources.LoadAll("Charts")
            .Select(a => a.name).ToArray();
        listRenderer.Render();
    }
}
