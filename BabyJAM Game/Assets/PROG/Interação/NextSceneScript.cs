using UnityEngine;
using UnityEngine.SceneManagement;
public class NextSceneScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D cool){
        if (cool.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
