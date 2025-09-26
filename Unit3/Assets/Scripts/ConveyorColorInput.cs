using UnityEngine;
using System.Collections.Generic;

public class ConveyorColorInput : MonoBehaviour
{
    [Header("Input Settings")]
    public ConveyorInputType inputType = ConveyorInputType.Input1;
    public float detectionRadius = 2f;
    public LayerMask cubeLayer = -1; // Set to Everything by default for testing

    [Header("Visual Feedback - Light Materials")]
    public MeshRenderer lightRenderer; // The light object's mesh renderer
    public Material redLightMaterial;
    public Material blueLightMaterial;
    public Material yellowLightMaterial;
    public Material defaultLightMaterial; // White/off material

    [Header("Optional - Actual Light Component")]
    public Light lightComponent; // Optional: for actual illumination

    [Header("Debug")]
    public bool showDebugInfo = true;

    public CubeColor currentColor { get; private set; } = CubeColor.Red; // Default to Red since there's no None
    public bool hasColoredCube { get; private set; } = false;

    private ConveyorColorMixer mixer;
    private ColoredCube currentCube;

    public System.Action<ConveyorInputType, CubeColor> OnColorDetected;
    public System.Action<ConveyorInputType> OnColorCleared;

    private void Start()
    {
        mixer = FindObjectOfType<ConveyorColorMixer>();
        if (mixer == null && showDebugInfo)
        {
            Debug.LogWarning("ConveyorColorMixer not found in scene!");
        }

        // Set default light appearance
        SetLightMaterial(null); // Use default material
    }

    private void Update()
    {
        DetectCubes();
    }

    private void DetectCubes()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, cubeLayer);
        ColoredCube detectedCube = null;

        if (showDebugInfo)
        {
            Debug.Log($"[{gameObject.name}] Found {colliders.Length} colliders in detection area");
        }

        foreach (Collider col in colliders)
        {
            ColoredCube cube = col.GetComponent<ColoredCube>();
            if (cube != null)
            {
                if (showDebugInfo)
                {
                    Debug.Log($"[{gameObject.name}] Found cube with color: {cube.cubeColor}");
                }

                if (IsValidInputColor(cube.cubeColor))
                {
                    detectedCube = cube;
                    break;
                }
            }
        }

        if (detectedCube != null && detectedCube != currentCube)
        {
            SetCurrentCube(detectedCube);
        }
        else if (detectedCube == null && currentCube != null)
        {
            ClearCurrentCube();
        }
    }

    private bool IsValidInputColor(CubeColor color)
    {
        return color == CubeColor.Red || color == CubeColor.Blue || color == CubeColor.Yellow;
    }

    private void SetCurrentCube(ColoredCube cube)
    {
        currentCube = cube;
        currentColor = cube.cubeColor;
        hasColoredCube = true;

        SetLightAppearance(currentColor, true); // true = cube detected
        OnColorDetected?.Invoke(inputType, currentColor);

        if (mixer != null)
        {
            mixer.OnInputColorChanged(inputType, currentColor);
        }

        if (showDebugInfo)
        {
            Debug.Log($"[{gameObject.name}] Detected {currentColor} cube!");
        }
    }

    private void ClearCurrentCube()
    {
        if (showDebugInfo)
        {
            Debug.Log($"[{gameObject.name}] Cube removed!");
        }

        currentCube = null;
        hasColoredCube = false;

        SetLightAppearance(CubeColor.Red, false); // false = no cube detected

        OnColorCleared?.Invoke(inputType);

        if (mixer != null)
        {
            mixer.OnInputColorCleared(inputType);
        }
    }

    private void SetLightAppearance(CubeColor color, bool isActive)
    {
        if (isActive)
        {
            // When a cube is detected, show the cube's color
            SetLightMaterial(GetMaterialForColor(color));

            // Also set the light component color if available
            if (lightComponent != null)
            {
                lightComponent.color = GetLightColorForCubeColor(color);
                lightComponent.enabled = true;
            }
        }
        else
        {
            // When no cube is detected, show default/off state
            SetLightMaterial(defaultLightMaterial);

            if (lightComponent != null)
            {
                lightComponent.color = Color.white;
                lightComponent.enabled = false; // Turn off the light when no cube
            }
        }
    }

    private void SetLightMaterial(Material material)
    {
        if (lightRenderer != null && material != null)
        {
            lightRenderer.material = material;
        }
    }

    private Material GetMaterialForColor(CubeColor color)
    {
        switch (color)
        {
            case CubeColor.Red:
                return redLightMaterial;
            case CubeColor.Blue:
                return blueLightMaterial;
            case CubeColor.Yellow:
                return yellowLightMaterial;
            default:
                return defaultLightMaterial;
        }
    }

    private Color GetLightColorForCubeColor(CubeColor color)
    {
        switch (color)
        {
            case CubeColor.Red:
                return Color.red;
            case CubeColor.Blue:
                return Color.blue;
            case CubeColor.Yellow:
                return Color.yellow;
            default:
                return Color.white;
        }
    }

    public void ConsumeCube()
    {
        if (currentCube != null)
        {
            Destroy(currentCube.gameObject);
            ClearCurrentCube();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Draw detection area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnDrawGizmos()
    {
        // Always show detection area when selected
        Gizmos.color = new Color(1, 1, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }
}

public enum ConveyorInputType
{
    Input1,
    Input2
}
