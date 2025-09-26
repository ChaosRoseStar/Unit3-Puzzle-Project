using UnityEngine;
using System.Collections;

public class ConveyorColorMixer : MonoBehaviour
{
    [Header("Input References")]
    public ConveyorColorInput input1;
    public ConveyorColorInput input2;

    [Header("Output Settings")]
    public Transform outputSpawnPoint;

    [Header("Output Light Visual Feedback")]
    public MeshRenderer outputLightRenderer; // The output light object's mesh renderer
    public Material orangeLightMaterial;
    public Material purpleLightMaterial;
    public Material greenLightMaterial;
    public Material defaultOutputMaterial; // White/off material for output

    [Header("Optional - Output Light Component")]
    public Light outputLightComponent; // Optional: for actual illumination

    [Header("Color Cube Prefabs")]
    public GameObject orangeCubePrefab;
    public GameObject purpleCubePrefab;
    public GameObject greenCubePrefab;

    [Header("Processing Settings")]
    public float mixingDelay = 2f;

    [Header("Debug")]
    public bool showDebugInfo = true;

    private bool isProcessing = false;

    private void Start()
    {
        if (input1 != null)
        {
            input1.OnColorDetected += OnInputColorChanged;
            input1.OnColorCleared += OnInputColorCleared;
        }

        if (input2 != null)
        {
            input2.OnColorDetected += OnInputColorChanged;
            input2.OnColorCleared += OnInputColorCleared;
        }

        // Set default output light appearance
        SetOutputLightAppearance(CubeColor.Red, false); // Use Red as "none", false = no output
    }

    public void OnInputColorChanged(ConveyorInputType inputType, CubeColor color)
    {
        if (showDebugInfo)
        {
            Debug.Log($"Input {inputType} color changed to {color}");
        }
        CheckForValidCombination();
    }

    public void OnInputColorCleared(ConveyorInputType inputType)
    {
        if (showDebugInfo)
        {
            Debug.Log($"Input {inputType} color cleared");
        }

        // Clear output light when any input is cleared
        SetOutputLightAppearance(CubeColor.Red, false); // false = no output
    }

    private void CheckForValidCombination()
    {
        if (isProcessing || input1 == null || input2 == null) return;

        if (!input1.hasColoredCube || !input2.hasColoredCube) return;

        CubeColor resultColor = GetMixedColor(input1.currentColor, input2.currentColor);

        if (IsValidResultColor(resultColor))
        {
            // Immediately show the result color on the output light
            SetOutputLightAppearance(resultColor, true);

            StartCoroutine(ProcessColorMixing(resultColor));
        }
        else
        {
            // No valid combination, turn off output light
            SetOutputLightAppearance(CubeColor.Red, false);
        }
    }

    private bool IsValidResultColor(CubeColor color)
    {
        return color == CubeColor.Orange || color == CubeColor.Purple || color == CubeColor.Green;
    }

    private CubeColor GetMixedColor(CubeColor color1, CubeColor color2)
    {
        // Check all valid combinations
        if ((color1 == CubeColor.Red && color2 == CubeColor.Yellow) ||
            (color1 == CubeColor.Yellow && color2 == CubeColor.Red))
        {
            return CubeColor.Orange;
        }

        if ((color1 == CubeColor.Red && color2 == CubeColor.Blue) ||
            (color1 == CubeColor.Blue && color2 == CubeColor.Red))
        {
            return CubeColor.Purple;
        }

        if ((color1 == CubeColor.Yellow && color2 == CubeColor.Blue) ||
            (color1 == CubeColor.Blue && color2 == CubeColor.Yellow))
        {
            return CubeColor.Green;
        }

        return CubeColor.Red; // Return Red as "invalid combination"
    }

    private IEnumerator ProcessColorMixing(CubeColor resultColor)
    {
        isProcessing = true;

        if (showDebugInfo)
        {
            Debug.Log($"Mixing {input1.currentColor} + {input2.currentColor} = {resultColor}");
        }

        // Wait for mixing delay (output light is already showing the result color)
        yield return new WaitForSeconds(mixingDelay);

        // Consume the input cubes
        input1.ConsumeCube();
        input2.ConsumeCube();

        // Create the result cube
        CreateResultCube(resultColor);

        // Keep the output light showing the created color for a bit longer
        yield return new WaitForSeconds(1f);

        // Turn off output light after cube is created
        SetOutputLightAppearance(CubeColor.Red, false);

        isProcessing = false;
    }

    private void CreateResultCube(CubeColor color)
    {
        GameObject prefabToSpawn = GetCubePrefab(color);

        if (prefabToSpawn != null && outputSpawnPoint != null)
        {
            GameObject newCube = Instantiate(prefabToSpawn, outputSpawnPoint.position, outputSpawnPoint.rotation);

            // Ensure the cube has the correct color component
            ColoredCube cubeComponent = newCube.GetComponent<ColoredCube>();
            if (cubeComponent != null)
            {
                cubeComponent.SetColor(color);
            }

            if (showDebugInfo)
            {
                Debug.Log($"Created {color} cube at output!");
            }
        }
        else
        {
            Debug.LogError($"No prefab assigned for {color} cube or no output spawn point!");
        }
    }

    private GameObject GetCubePrefab(CubeColor color)
    {
        switch (color)
        {
            case CubeColor.Orange:
                return orangeCubePrefab;
            case CubeColor.Purple:
                return purpleCubePrefab;
            case CubeColor.Green:
                return greenCubePrefab;
            default:
                return null;
        }
    }

    private void SetOutputLightAppearance(CubeColor color, bool isActive)
    {
        if (isActive && IsValidResultColor(color))
        {
            // Show the created color
            SetOutputLightMaterial(GetOutputMaterialForColor(color));

            // Also set the light component color if available
            if (outputLightComponent != null)
            {
                outputLightComponent.color = GetLightColorForCubeColor(color);
                outputLightComponent.enabled = true;
            }
        }
        else
        {
            // Show default/off state
            SetOutputLightMaterial(defaultOutputMaterial);

            if (outputLightComponent != null)
            {
                outputLightComponent.color = Color.white;
                outputLightComponent.enabled = false; // Turn off the light when no output
            }
        }
    }

    private void SetOutputLightMaterial(Material material)
    {
        if (outputLightRenderer != null && material != null)
        {
            outputLightRenderer.material = material;
        }
    }

    private Material GetOutputMaterialForColor(CubeColor color)
    {
        switch (color)
        {
            case CubeColor.Orange:
                return orangeLightMaterial;
            case CubeColor.Purple:
                return purpleLightMaterial;
            case CubeColor.Green:
                return greenLightMaterial;
            default:
                return defaultOutputMaterial;
        }
    }

    private Color GetLightColorForCubeColor(CubeColor color)
    {
        switch (color)
        {
            case CubeColor.Orange:
                return new Color(1f, 0.5f, 0f); // Orange
            case CubeColor.Purple:
                return new Color(0.5f, 0f, 1f); // Purple
            case CubeColor.Green:
                return Color.green;
            default:
                return Color.white;
        }
    }
}
