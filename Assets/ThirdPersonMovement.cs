using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private Transform cameraMain;





    public CharacterController controller;

    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;



    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }


    private void Start()
    {
        cameraMain = Camera.main.transform;
    }




    // Update is called once per frame
    void Update()
    {

        Vector2 movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        //Vector3 direction = (cameraMain.forward * movementInput.y + cameraMain.right * movementInput.x);

        float horizontal = movementInput.x;
        float vertical = movementInput.y;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        /* float horizontal = Input.GetAxisRaw("Horizontal");
         float vertical = Input.GetAxisRaw("Vertical");
         Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; */

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
