using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraEscenaA : MonoBehaviour
{
    private GameObject camaraOrbital;
    private Vector3 centro = new Vector3(-12.0f, 1f, 26.0f);

    private Vector3 centroA1 = new Vector3(0f, 2f, 0f);
    private Vector3 centroA2 = new Vector3(-12f, 2f, 0f);
    private Vector3 centroA3 = new Vector3(-24.0f, 2f, 0f);
    private Vector3 centroA4 = new Vector3(0f, 2f, 13f);
    private Vector3 centroA5 = new Vector3(-12f, 2f, 13f);
    private Vector3 centroA6 = new Vector3(-24f, 2f, 13f);
    private Vector3 centroA7 = new Vector3(0f, 2f, 26f);
    private Vector3 centroA8 = new Vector3(-12f, 2f, 26f);
    private Vector3 centroA9 = new Vector3(-24f, 2f, 26f);
    private Vector3 centroA10 = new Vector3(0f, 2f, 39f);
    private Vector3 centroA11 = new Vector3(-12f, 2f, 39f);
    private Vector3 centroA12 = new Vector3(-24f, 2f, 39f);
    private Vector3 centroA13 = new Vector3(0f, 2f, 52f);
    private Vector3 centroA14 = new Vector3(-12f, 2f, 52f);
    private Vector3 centroA15 = new Vector3(-24f, 2f, 52f);

    private float zoomSpeed = 15f;
    private float minDistance = 10;
    private float maxDistance = 80;

    private float speedV = 50f;
    private float pitch;

    private float distance = 50;
    private int cont = 1;
    private bool camaraAuto = false;

    void Start()
    {
        CreateOrbitalCamera();

        pitch = camaraOrbital.transform.eulerAngles.x;
    }

    void Update()
    {
        // Rotacion de la camara orbital con teclas A y D
        if (Input.GetKey(KeyCode.D))
        {
            camaraOrbital.transform.RotateAround(centro, -Vector3.up, 80.0f * Time.deltaTime);
            camaraOrbital.transform.rotation = Quaternion.LookRotation(centro - camaraOrbital.transform.position);
        }

        if (Input.GetKey(KeyCode.A))
        {
            camaraOrbital.transform.RotateAround(centro, Vector3.up, 80.0f * Time.deltaTime);
            camaraOrbital.transform.rotation = Quaternion.LookRotation(centro - camaraOrbital.transform.position); 
        }

        // Subo la camara con W
        if (Input.GetKey(KeyCode.W))
        {  
            pitch += speedV * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, 10f, 80f);
            
            camaraOrbital.transform.eulerAngles = new Vector3(pitch, camaraOrbital.transform.eulerAngles.y, camaraOrbital.transform.eulerAngles.z);
        }

        // Bajo la camara con S
        if (Input.GetKey(KeyCode.S))
        {
            pitch -= speedV * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, 10, 80f);

            camaraOrbital.transform.eulerAngles = new Vector3(pitch, camaraOrbital.transform.eulerAngles.y, camaraOrbital.transform.eulerAngles.z);
        }

        // Cambio de target
        if (Input.GetKeyDown(KeyCode.E) && camaraAuto)
        {
            switch(cont)
            {
                case 1:
                    centro = centroA2;
                    cont++;
                    break;
                case 2:
                    centro = centroA3;
                    cont++;
                    break;
                case 3:
                    centro = centroA4;
                    cont++;
                    break;
                case 4:
                    centro = centroA5;
                    cont++;
                    break;
                case 5:
                    centro = centroA6;
                    cont++;
                    break;
                case 6:
                    centro = centroA7;
                    cont++;
                    break;
                case 7:
                    centro = centroA8;
                    cont++;
                    break;
                case 8:
                    centro = centroA9;
                    cont++;
                    break;
                case 9:
                    centro = centroA10;
                    cont++;
                    break;
                case 10:
                    centro = centroA11;
                    cont++;
                    break;
                case 11:
                    centro = centroA12;
                    cont++;
                    break;
                case 12:
                    centro = centroA13;
                    cont++;
                    break;
                case 13:
                    centro = centroA14;
                    cont++;
                    break;
                case 14:
                    centro = centroA15;
                    cont++;
                    break;
                case 15:
                    centro = centroA1;
                    cont = 1;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && camaraAuto)
        {
            switch (cont)
            {
                case 1:
                    centro = centroA15;
                    cont = 15;
                    break;
                case 2:
                    centro = centroA1;
                    cont--;
                    break;
                case 3:
                    centro = centroA2;
                    cont--;
                    break;
                case 4:
                    centro = centroA3;
                    cont--;
                    break;
                case 5:
                    centro = centroA4;
                    cont--;
                    break;
                case 6:
                    centro = centroA5;
                    cont--;
                    break;
                case 7:
                    centro = centroA6;
                    cont--;
                    break;
                case 8:
                    centro = centroA7;
                    cont--;
                    break;
                case 9:
                    centro = centroA8;
                    cont--;
                    break;
                case 10:
                    centro = centroA9;
                    cont--;
                    break;
                case 11:
                    centro = centroA10;
                    cont--;
                    break;
                case 12:
                    centro = centroA11;
                    cont--;
                    break;
                case 13:
                    centro = centroA12;
                    cont--;
                    break;
                case 14:
                    centro = centroA13;
                    cont--;
                    break;
                case 15:
                    centro = centroA14;
                    cont--;
                    break;
            }
        }
        
        // Centrar escena
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!camaraAuto)
            {
                centro = centroA1;
                minDistance = 5.0f;
                maxDistance = 20.0f;
                distance = 20.0f;

                camaraAuto = true;
            }
            else
            {
                centro = new Vector3(-12.0f, 1f, 26.0f);
                minDistance = 10.0f;
                maxDistance = 50.0f;
                distance = 50.0f;

                camaraAuto = false;
            }
        }

        // Zoom
        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        camaraOrbital.transform.position = centro - camaraOrbital.transform.forward * distance;
    }

    private void CreateOrbitalCamera()
    {
        camaraOrbital = new GameObject("Camara Orbital");
        camaraOrbital.AddComponent<Camera>();
        camaraOrbital.transform.position = new Vector3(-12f, 18f, 80f);
        camaraOrbital.transform.rotation = Quaternion.Euler(60, 0, 0);
        camaraOrbital.transform.rotation = Quaternion.LookRotation(centro - camaraOrbital.transform.position);

        camaraOrbital.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        camaraOrbital.GetComponent<Camera>().backgroundColor = Color.black;
    }

}