using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalDoor : MonoBehaviour
{

    private bool hasFinished = false;
    [SerializeField] private GameObject levelCompletePanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !hasFinished)
        {
            hasFinished = true;
            levelCompletePanel.SetActive(true);
            other.attachedRigidbody.simulated = false;

            PlayerAudio playerAudio =
                other.GetComponentInParent<PlayerAudio>();
            playerAudio?.PlayVictory();

            Invoke(nameof(LoadNextScene), 3f);
        }
    }

    private void LoadNextScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
}
