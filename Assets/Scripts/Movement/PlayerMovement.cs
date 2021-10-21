using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/PlayerMovement")]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    [SerializeField] float GravityModifer = 12f;
    [SerializeField] float GravityForce =0f;
    [SerializeField] float JumpForce =5f;
    private CharacterController _charController;
    public bool PlayerIsCrouch;
    private LayerMask WallLayer;
    private Camera Camera;

    [Header("CrouchStats")]
    [SerializeField] float CrouchControllerHeight;
    [SerializeField] float CrouchControllerCenter;
    [SerializeField] float CrouchCameraHeight;

    [Header("NormalStats")]
    [SerializeField] float ControllerHeight;
    [SerializeField] float ControllerCenter;
    [SerializeField] float CameraHeight;
    void Start()
    {
        Camera = Camera.main;
        _charController = GetComponent<CharacterController>();
        WallLayer = LayerMask.GetMask("TransparentFX");
        ControllerHeight = _charController.height;
        ControllerCenter = _charController.center.y;
        CameraHeight = Camera.transform.localPosition.y;
    }
    void Update()
    {
        GravityLogic();
        Run();
        Crouch();
        MovementLogic();
    }
    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement;
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = transform.TransformDirection(movement * speed);
        movement.y = GravityForce;
        _charController.Move(movement * Time.deltaTime);
    }
    private void GravityLogic()
    {
        if (!_charController.isGrounded)
        {
            GravityForce -= GravityModifer * Time.deltaTime;
        }
        else
        {
            GravityForce = -1f;
        }            
        if (Input.GetKeyDown(KeyCode.Space) && _charController.isGrounded) GravityForce = JumpForce;
    }
    public void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 8.9f;
        }
        else
        {
            speed = 6f;
        }
    }
    public void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            speed = 6f;
            PlayerIsCrouch = true;
            _charController.height = CrouchControllerHeight;
            _charController.center = new Vector3(_charController.center.x, CrouchControllerCenter, _charController.center.z);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            speed = 6f; 
            PlayerIsCrouch = false;
            _charController.height = ControllerHeight;
            _charController.center = new Vector3(_charController.center.x, ControllerCenter, _charController.center.z);
        }
        // плавное перемещение камеры ввехр и вних при присаживании и вставании
        if (PlayerIsCrouch == true && Camera.transform.localPosition.y > CrouchCameraHeight + 0.02f)
        {
            Camera.transform.localPosition = Vector3.Lerp(Camera.transform.localPosition, new Vector3(Camera.transform.localPosition.x, CrouchCameraHeight, Camera.transform.localPosition.z), 5f * Time.deltaTime);
        }
        if (PlayerIsCrouch == false && Camera.transform.localPosition.y < CameraHeight - 0.01f)
        {
            Camera.transform.localPosition = Vector3.Lerp(Camera.transform.localPosition, new Vector3(Camera.transform.localPosition.x, CameraHeight, Camera.transform.localPosition.z), 4.5f * Time.deltaTime);
        }
    }
}
