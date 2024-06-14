using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamaraFPVNuevo : MonoBehaviour
{

    /*
    EXTENDED FLYCAM
    Desi Quintans (CowfaceGames.com), 17 August 2012.
    Based on FlyThrough.js by Slin (http://wiki.unity3d.com/index.php/FlyThrough), 17 May 2011.

    LICENSE
    Free as in speech, and free as in beer.

    FEATURES
    WASD/Arrows: Movement
    Q: Climb
    E: Drop
    Shift: Move faster
    Control: Move slower
    End: Toggle cursor locking to screen (you can also press Ctrl+P to toggle play mode on and off).
    */

    public float cameraSensitivity = 50;
    public float normalMoveSpeed = 0.8f;
    public float slowMoveFactor = 0.5f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private GameObject camara;

    void Start()
    {
        CreateCamera();
    }

    void Update()
    {
        /*rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90, 90);
        */
        camara.transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        camara.transform.localRotation *= Quaternion.AngleAxis(-rotationY, Vector3.left);

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            camara.transform.position += camara.transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
            camara.transform.position += camara.transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
        else
        {
            camara.transform.position += camara.transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            camara.transform.position += camara.transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        }

        if (camara.activeSelf && Input.GetKey(KeyCode.Mouse0))
        {
            rotationX += normalMoveSpeed/2 * Input.GetAxis("Mouse X");
            rotationY -= normalMoveSpeed/2 * Input.GetAxis("Mouse Y");

            // Limito el rango de la rotacion vertical
            rotationY = Mathf.Clamp(rotationY, -90f, 90f);

            camara.transform.eulerAngles = new Vector3(rotationY, rotationX, 0.0f);
        }

        //if (Input.GetKey(KeyCode.Q)) { camara.transform.position += camara.transform.up * climbSpeed * Time.deltaTime; }
        //if (Input.GetKey(KeyCode.E)) { camara.transform.position -= camara.transform.up * climbSpeed * Time.deltaTime; }


    }

    private void CreateCamera()
    {
        camara = new GameObject("Camara FPV");
        camara.AddComponent<Camera>();
        camara.transform.position = new Vector3(0, 1.7f, -2.0f);

        camara.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        camara.GetComponent<Camera>().backgroundColor = Color.black;
    }
}