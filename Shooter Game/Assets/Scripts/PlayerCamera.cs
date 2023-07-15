using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float sensX;

    [SerializeField]
    private float sensY;

    private float xRotation;

    private float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!PauseMenu.paused)
        {
            float num = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float num2 = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
            yRotation += num;
            xRotation -= num2;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            base.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }
}
