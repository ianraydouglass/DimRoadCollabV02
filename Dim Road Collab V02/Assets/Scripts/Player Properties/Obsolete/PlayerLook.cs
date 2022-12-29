using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public GameObject frontHold;
    //public Transform playerBody;
    public GameObject mainCamera;
    public GameObject player;
    public CharacterController controller;
    float MouseX;
    float MouseY;

    //public PlayerInfo playerInfo; //c
    //ontains all playerinfo code (like is he holding etc.)
    float xRotation = 0f;

    //public float dropDelaySeconds = 1;

    //added by Austin, 1/14/2022
    //public float maxRayDist = 3f;

    //added by Ian 1/14/2022
    //public bool withinRange;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = MouseX * mouseSensitivity * Time.deltaTime;
        float mouseY = MouseY * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        //this should prevent you from flipping and looking behind you upside down
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        Vector3 rotationvalue = new Vector3(0, mouseX, 0);

        this.transform.Rotate(rotationvalue);
    }

    public void OnLook(InputValue input)
    {
        
        MouseX = input.Get<Vector2>().x;
        MouseY = input.Get<Vector2>().y;
    }
}
