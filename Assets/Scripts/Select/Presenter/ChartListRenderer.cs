using System.Linq;
using UnityEngine;
using UniRx;
using TMPro;

public class ChartListRenderer : MonoBehaviour
{
    [SerializeField]
    SelectMenuData selectMenuData;
    [SerializeField]
    GameObject buttonPrefab;
    [SerializeField]
    Transform buttonParent;

    public void Render()
    {
        int i = 0;
        foreach (var info in selectMenuData.InfoList)
        {
            var button = Instantiate(buttonPrefab);
            button.transform.SetParent(buttonParent);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = info.title;
            button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = info.rank;
            button.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = info.difficulty.ToString();
            button.GetComponent<ButtonPresenter>().Path = selectMenuData.Paths[i];
            i++;
        }
    }
}
