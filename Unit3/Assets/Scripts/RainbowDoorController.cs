using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RainbowDoorController : MonoBehaviour
{
    [Header("Required Colors")]
    [SerializeField]
    private CubeColor[] requiredColors = {
        CubeColor.Red,
        CubeColor.Blue,
        CubeColor.Green,
        CubeColor.Yellow,
        CubeColor.Purple,
        CubeColor.Orange
    };

    [Header("Pressure Light References")]
    [SerializeField] private ColorCodedPressurePad[] pressureLights;

    [Header("Door Animation")]
    [SerializeField] private Animator doorAnimator;

    [Header("Door Light (Single Light like other doors)")]
    [SerializeField] private MeshRenderer doorLight;
    [SerializeField] private Material doorOnMaterial;
    [SerializeField] private Material doorOffMaterial;

    

    private bool isDoorOpen = false;
    private Dictionary<CubeColor, bool> colorStates = new Dictionary<CubeColor, bool>();

    private void Start()
    {
        InitializeColorStates();
        UpdateDoorLight();

        if (pressureLights.Length == 0)
        {
            FindPressureLights();
        }
    }

    private void InitializeColorStates()
    {
        foreach (CubeColor color in requiredColors)
        {
            colorStates[color] = false;
        }
    }

    private void FindPressureLights()
    {
        pressureLights = FindObjectsOfType<ColorCodedPressurePad>();
        Debug.Log($"RainbowDoor found {pressureLights.Length} pressure lights in the scene");
    }

    private void Update()
    {
        CheckPressureLightStates();
        bool allColorsActive = AreAllColorsActive();

        if (allColorsActive && !isDoorOpen)
        {
            OpenDoor();
        }
        else if (!allColorsActive && isDoorOpen)
        {
            CloseDoor();
        }
    }

    private void CheckPressureLightStates()
    {
        foreach (var pressureLight in pressureLights)
        {
            if (pressureLight == null) continue;

            CubeColor lightColor = pressureLight.RequiredColor;
            bool isActive = pressureLight.IsCorrectCubeOnPad;

            if (colorStates.ContainsKey(lightColor))
            {
                colorStates[lightColor] = isActive;
            }
        }
    }

    private bool AreAllColorsActive()
    {
        return colorStates.Values.All(isActive => isActive);
    }

    private void OpenDoor()
    {
        isDoorOpen = true;

        if (doorAnimator != null)
        {
            doorAnimator.SetBool("DoorOpen", true);
        }

        UpdateDoorLight();       

        Debug.Log("Rainbow Door: All colors activated! Door opening...");
    }

    private void CloseDoor()
    {
        isDoorOpen = false;

        if (doorAnimator != null)
        {
            doorAnimator.SetBool("DoorOpen", false);
        }

        UpdateDoorLight();        

        Debug.Log("Rainbow Door: Not all colors active! Door closing...");
    }

    private void UpdateDoorLight()
    {
        if (doorLight != null)
        {
            if (isDoorOpen && doorOnMaterial != null)
            {
                doorLight.material = doorOnMaterial;
            }
            else if (!isDoorOpen && doorOffMaterial != null)
            {
                doorLight.material = doorOffMaterial;
            }
        }
    }

    

    private void OnValidate()
    {
        if (requiredColors.Length != 6)
        {
            Debug.LogWarning("RainbowDoor should require exactly 6 colors!");
        }
    }
}
