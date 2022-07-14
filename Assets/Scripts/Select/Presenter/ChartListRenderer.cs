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
    Transform buttonParentTransform;
    
    public void OnLoad()
    {
        selectMenuData.onSelectedRankChanged.Subscribe(_ => Render());
        Render();
    }

    private void Render()
    {
        while (buttonParentTransform.childCount > 0) {
            DestroyImmediate(buttonParentTransform.GetChild(0).gameObject);
        }
        var selected = selectMenuData.InfoList.Where(info => info.rank == selectMenuData.SelectedRank);
        foreach (var info in selected)
        {
            var button = Instantiate(buttonPrefab, buttonParentTransform, false);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = info.title;
            button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = info.difficulty.ToString();

            var chartButtonPresenter = button.GetComponent<ChartButtonPresenter>();
            chartButtonPresenter.Path = info.jsonFileName;
            chartButtonPresenter.selectMenuData = selectMenuData;
        }
    }
}
