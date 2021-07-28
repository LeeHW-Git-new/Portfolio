using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] //¿Ã¡¯»≠
    private float MoveSpeed;


    [SerializeField]
    private float LookSensitivity;

    [SerializeField]
    private float CameraRotationLimit;
    private float CurrentCameraRotatonX = 0;

    [SerializeField]
    private Camera theCamera;
    
    private Rigidbody myRigid;

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Move();
        CameraRotation();
        CharacterRotation();
    }
    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * moveDirZ;

        Vector3 velcoity = (moveHorizontal + moveVertical).normalized * MoveSpeed;

        myRigid.MovePosition(transform.position + velcoity * Time.deltaTime);

    }

    private void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float CameraRotationX = xRotation * LookSensitivity;
        CurrentCameraRotatonX -= CameraRotationX;
        CurrentCameraRotatonX = Mathf.Clamp(CurrentCameraRotatonX, -CameraRotationLimit, CameraRotationLimit);
        theCamera.transform.localEulerAngles = new Vector3(CurrentCameraRotatonX, 0f, 0f);
    }

    private void CharacterRotation()
    {
        float yRotation = Input.GetAxis("Mouse X");
        Vector3 CharacterRotationY = new Vector3(0f, yRotation, 0f) * LookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(CharacterRotationY));
    }
}
