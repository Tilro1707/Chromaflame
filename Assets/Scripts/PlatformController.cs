using UnityEngine;
using static ColourWorldController;

public class PlatformController : MonoBehaviour
{

    [SerializeField] private ColourWorldController colourWorld;
    [SerializeField] private LightColour platformColour;
    [SerializeField] private SpriteRenderer platformRenderer;
    [SerializeField] private Collider2D physicalCollider;
    private bool hasCorrectColour;
    private bool isInsideRadius;

    private void OnEnable()
    {
        colourWorld.ColourChanged += HandleColourChanged;
        HandleColourChanged(colourWorld.CurrentColour);

    }

    private void OnDisable()
    {
        colourWorld.ColourChanged -= HandleColourChanged;
    }

    private void Awake()
    {
        if (colourWorld == null)
        {
            colourWorld = FindAnyObjectByType<ColourWorldController>();
        }

        if (platformRenderer == null)
        {
            platformRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        platformRenderer.color =
            colourWorld.GetDisplayColour(platformColour);
    }

    private void HandleColourChanged(
        ColourWorldController.LightColour newColour)
    {
        if (platformColour == newColour)
        {
            hasCorrectColour = true;
        }
        else
        {
            hasCorrectColour = false;
        }
        UpdateActiveState();
    }

    public void SetInsideRadius(bool inside)
    {
        isInsideRadius = inside;
        UpdateActiveState();
    }

    private void UpdateActiveState()
    {
        bool shouldBeActive = isInsideRadius && hasCorrectColour;

        platformRenderer.enabled = shouldBeActive;
        physicalCollider.enabled = shouldBeActive;
    }
}
