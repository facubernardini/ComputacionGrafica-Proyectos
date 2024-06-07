using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamarasEscenaB : MonoBehaviour
{
    public GameObject PisoPasto;
    
    private GameObject camara;
    private GameObject camaraOrbital; 

    private float speedH = 1.5f;
    private float speedV = 1.5f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float movimientoHorizontal, movimientoVertical;
    private float velocidad = 6.0f;

    private float zoomSpeed = 15f;
    private float minDistance = 8;
    private float maxDistance = 40;
    private float distance = 35;

    private Vector3 centro;

    void Start()
    {

        centro = new Vector3(PisoPasto.transform.position.x , PisoPasto.transform.position.y , PisoPasto.transform.position.z);

        CreateCamera();
        CreateOrbitalCamera();
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

        // Rotacion de la camara orbital con teclas Q y E
        if (camaraOrbital.activeSelf && Input.GetKey(KeyCode.E))
        {
            camaraOrbital.transform.RotateAround(centro, -Vector3.up, 80.0f * Time.deltaTime);
            camaraOrbital.transform.rotation = Quaternion.LookRotation(centro - camaraOrbital.transform.position);
        }

        if (camaraOrbital.activeSelf && Input.GetKey(KeyCode.Q))
        {
            camaraOrbital.transform.RotateAround(centro, Vector3.up, 80.0f * Time.deltaTime);
            camaraOrbital.transform.rotation = Quaternion.LookRotation(centro - camaraOrbital.transform.position);
        }

        // Cambiar de camara principal a orbital y viceversa
        if (Input.GetKeyDown(KeyCode.C))
        {
            cambiarCamara(camara.activeSelf);
        }

        // Zoom
        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        camaraOrbital.transform.position = centro - camaraOrbital.transform.forward * distance;
    }

    private void CreateCamera()
    {
        camara = new GameObject("Camara Principal");
        camara.AddComponent<Camera>();
        camara.transform.position = new Vector3(0, 1.7f, -2.0f);

        camara.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        camara.GetComponent<Camera>().backgroundColor = Color.black;
    }

    private void CreateOrbitalCamera()
    {
        camaraOrbital = new GameObject("Camara Orbital");
        camaraOrbital.AddComponent<Camera>();
        camaraOrbital.transform.position = new Vector3(8, 10f, -8);
        camaraOrbital.transform.rotation = Quaternion.Euler(40, 0, 0);
        camaraOrbital.transform.rotation = Quaternion.LookRotation(centro - camaraOrbital.transform.position);

        camaraOrbital.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        camaraOrbital.GetComponent<Camera>().backgroundColor = Color.black;

        camaraOrbital.SetActive(false);
    }

    private void cambiarCamara(bool camaraPrincipal)
    {
        if (camaraPrincipal)
        {
            camara.SetActive(false);
            camaraOrbital.SetActive(true);
        }
        else
        {
            camara.SetActive(true);
            camaraOrbital.SetActive(false);
        }

    }
}