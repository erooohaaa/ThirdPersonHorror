using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToLevel3Trigger : MonoBehaviour
{
    public string nextSceneName = " ";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}