using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToLevel3Trigger : MonoBehaviour
{
    public string nextSceneName = "Level3_UndergroundLab";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
