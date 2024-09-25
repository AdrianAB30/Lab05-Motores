using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox_Rotate : MonoBehaviour
{
    [SerializeField] public float rotationSpeed;
    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
