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
            .Select(a => new string[]{a.name, a.text})
            .Select(data => {
                var info = JsonUtility.FromJson<SelectMenuData.ChartInfo>(data[1]);
                info.jsonFileName = data[0];
                return info;
            }).
            ToArray();
        listRenderer.OnLoad();
    }
}
