using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPresenter : MonoBehaviour
{
    private string path;
    public string Path{ set => this.path = value; }

    public void OnClick()
    {
        SceneManager.sceneLoaded += OnLoaded;
        SceneManager.LoadScene("Game Scene");
    }

    private void OnLoaded(Scene next, LoadSceneMode mode)
    {
        GameObject.Find("Chart Presenter").GetComponent<ChartPresenter>().SetJson(path);
        SceneManager.sceneLoaded -= OnLoaded;
    }
}
