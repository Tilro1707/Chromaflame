using UnityEngine;

public class RadiusSensor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (!other.isTrigger)
        {
            return;
        }
        PlatformController platform =
            other.GetComponentInParent<PlatformController>();

        if (platform != null)
        {
            platform.SetInsideRadius(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.isTrigger)
        {
            return;
        }

        PlatformController platform =
            other.GetComponentInParent<PlatformController>();

        if (platform != null)
        {
            platform.SetInsideRadius(false);
        }
    }
}