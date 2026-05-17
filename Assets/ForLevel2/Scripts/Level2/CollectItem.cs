using UnityEngine;

public class CollectItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Level2Manager.Instance != null)
            {
                Level2Manager.Instance.CollectPage();
            }

            Destroy(gameObject);
        }
    }
}
