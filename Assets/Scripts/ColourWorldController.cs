using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColourWorldController : MonoBehaviour
{
    public enum LightColour
    {
        Red,
        Cyan
    }

    [SerializeField] private LightColour currentColour = LightColour.Red;
    [SerializeField] private SpriteRenderer radiusRenderer;
    [SerializeField] private Color redColour;
    [SerializeField] private Color cyanColour;
    public event Action<LightColour> ColourChanged;
    public LightColour CurrentColour => currentColour;
    private PlayerAudio playerAudio;

    private void Start()
    {
        UpdateRadiusColour();
        playerAudio = GetComponent<PlayerAudio>();
    }

    void OnColourSwitch(InputValue value)
    {
        if (value.isPressed)
        {
            if (currentColour == LightColour.Red)
            {
                currentColour = LightColour.Cyan;
            }
            else
            {
                currentColour = LightColour.Red;
            }
            playerAudio.PlayColourSwitch();
            UpdateRadiusColour();
            ColourChanged?.Invoke(currentColour);
        }
    }

    private void UpdateRadiusColour()
    {
        radiusRenderer.color =
            currentColour == LightColour.Red
                ? redColour
                : cyanColour;
    }

    public Color GetDisplayColour(LightColour colour)
    {
        return colour == LightColour.Red
            ? redColour
            : cyanColour;
    }
}
