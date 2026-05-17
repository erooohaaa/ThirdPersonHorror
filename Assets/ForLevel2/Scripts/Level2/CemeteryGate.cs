using UnityEngine;

public class CemeteryGate : MonoBehaviour
{
    [Header("Gate Movement")]
    public Transform gateObject;
    public Vector3 openOffset = new Vector3(0f, 4f, 0f);
    public float openSpeed = 2f;

    [Header("Exit Trigger")]
    public Collider exitTrigger;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpening = false;
    private bool isOpen = false;

    private void Start()
    {
        if (gateObject == null)
        {
            gateObject = transform;
        }

        closedPosition = gateObject.position;
        openPosition = closedPosition + openOffset;

        LockGate();
    }

    private void Update()
    {
        if (!isOpening)
        {
            return;
        }

        gateObject.position = Vector3.MoveTowards(
            gateObject.position,
            openPosition,
            openSpeed * Time.deltaTime
        );

        if (Vector3.Distance(gateObject.position, openPosition) < 0.05f)
        {
            isOpening = false;
            isOpen = true;
        }
    }

    public void LockGate()
    {
        isOpen = false;
        isOpening = false;

        if (exitTrigger != null)
        {
            exitTrigger.enabled = false;
        }
    }

    public void OpenGate()
    {
        isOpening = true;

        if (exitTrigger != null)
        {
            exitTrigger.enabled = true;
        }
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}
