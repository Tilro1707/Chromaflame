using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            return;
        }
        PlayerAudio playerAudio =
            other.GetComponentInParent<PlayerAudio>();
        playerAudio?.PlayDeath();

        LevelResetInput resetInput =
            other.GetComponentInParent<LevelResetInput>();

        if (resetInput == null)
        {
            return;
        }
        
        resetInput.RestartLevel();
    }
}