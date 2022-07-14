using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonPresenter : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Select Scene");
    }
}
