using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOpt_Script : MonoBehaviour
{
    bool OnConfig;
    [SerializeField] GameObject Options;

    public void OnPlay()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OnOptions()
    {
        Options.SetActive(true);
        OnConfig = true;
    }

    public void OnExit()
    {
        if (OnConfig)
        {
            Options.SetActive(false);
            OnConfig = false;
        }
        else
        {
            Application.Quit();
            Debug.Log("Saindo...");
        }

    }
}
