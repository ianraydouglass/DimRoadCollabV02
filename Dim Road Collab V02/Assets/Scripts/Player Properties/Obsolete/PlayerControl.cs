using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
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

    public float defaultMoveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    Vector3 velocity;
    Vector2 inputVector;
    public bool isGrounded;

    //public float dropDelaySeconds = 1;

    //added by Austin, 1/14/2022
    //public float maxRayDist = 3f;

    //added by Ian 1/14/2022
    //public bool withinRange;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isGrounded = true;
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

        //movement section
        float speed = defaultMoveSpeed;
        Vector3 finalVector = new Vector3();
        finalVector.x = inputVector.x;
        finalVector.z = inputVector.y;
        //controller.Move(finalVector * Time.deltaTime * speed);
        finalVector.y = velocity.y;
        controller.Move(finalVector * Time.deltaTime * speed);


        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
    }

    public void OnLook(InputValue input)
    {

        MouseX = input.Get<Vector2>().x;
        MouseY = input.Get<Vector2>().y;
    }

    public void OnMove(InputValue input)
    {
        inputVector = input.Get<Vector2>();
    }
}
