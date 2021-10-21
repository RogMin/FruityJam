using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("Control Script/MouseLook")]
public class MouseLook : MonoBehaviour
{
    [SerializeField] float minX = -360; 
    [SerializeField] float maxX = 360; 
    [SerializeField] float minY = -60;
    [SerializeField] float maxY = 60;   
	[SerializeField] float sensitiveX = 3f;
    [SerializeField] float sensitiveY = 3f;
    [SerializeField] float mouseX;
	[SerializeField] float mouseY;
    [SerializeField] GameObject CameraFPS;
    private Quaternion originalRot;
	
    private void Start()
    {
        originalRot = CameraFPS.transform.localRotation;
    }
    void Update()
    {
        RotationLogic();
    }
    private void RotationLogic()
	{
        mouseX += Input.GetAxis("Mouse X") * sensitiveX;
        mouseY += Input.GetAxis("Mouse Y") * sensitiveY;         
        mouseX = mouseX % 360;
        mouseY = mouseY % 360; 
        mouseX = Mathf.Clamp(mouseX, minX, maxX);
        mouseY = Mathf.Clamp(mouseY, minY, maxY); 
        Quaternion xQuaternion = Quaternion.AngleAxis(mouseX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(mouseY, Vector3.left);
        transform.localRotation = originalRot * xQuaternion;
        CameraFPS.transform.localRotation = originalRot * yQuaternion;
	}
}
