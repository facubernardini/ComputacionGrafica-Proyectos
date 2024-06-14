using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamaraFPV : MonoBehaviour
{
    private GameObject camara;

    private float speedH = 1.5f;
    private float speedV = 1.5f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float movimientoHorizontal, movimientoVertical;
    private float velocidad = 6.0f;

    void Start()
    {
        CreateCamera();
    }

    void Update()
    {
        // Movimiento de la camara principal con teclado
        if (camara.activeSelf)
        {
            movimientoHorizontal = Input.GetAxis("Horizontal");
            movimientoVertical = Input.GetAxis("Vertical");

            Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);
            movimiento = camara.transform.TransformDirection(movimiento);
            movimiento[1] = 0.0f; //Luego de la transformacion con respecto al mundo, vuelvo a setear el eje Y en 0 para que la camara este siempre al mismo nivel
            camara.transform.position += movimiento * velocidad * Time.deltaTime;
        }

        // Rotacion de la camara principal manteniendo el click izquierdo apretado
        if (camara.activeSelf && Input.GetKey(KeyCode.Mouse0))
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            // Limito el rango de la rotacion vertical
            pitch = Mathf.Clamp(pitch, -90f, 90f);

            camara.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }

    }

    private void CreateCamera()
    {
        camara = new GameObject("Camara Principal");
        camara.AddComponent<Camera>();
        camara.transform.position = new Vector3(0, 1.7f, -2.0f);

        camara.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        camara.GetComponent<Camera>().backgroundColor = Color.black;
    }

}