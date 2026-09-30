using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelResetInput : MonoBehaviour
{
    public void OnReset(InputValue value)
    {
        if (value.isPressed)
        {
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
