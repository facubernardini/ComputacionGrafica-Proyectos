using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public FileReader filereader;

    private GameObject habitacion1;
    private GameObject habitacion1a;
    private GameObject habitacion2;
    private GameObject habitacion2a;

    private GameObject marcoPuerta1;
    private GameObject marcoPuerta2;

    private GameObject puerta1;
    private GameObject puerta2;
    private GameObject puerta3;
    private GameObject puerta4;

    // Pared frente
    private GameObject paredFrenteA;
    private GameObject paredFrenteB;
    private Vector3[] verticesParedFrente;
    private int[] triangulosParedFrente;

    // Pared fondo
    private GameObject paredFondoA;
    private GameObject paredFondoB;
    private Vector3[] verticesParedFondo;
    private int[] triangulosParedFondo;

    // Paredes laterales
    private GameObject paredLateralIzqA;
    private GameObject paredLateralIzqB;
    private GameObject paredLateralDerA;
    private GameObject paredLateralDerB;
    private Vector3[] verticesParedLateral;
    private int[] triangulosParedLateral;

    // Piso
    private GameObject piso;
    private Vector3[] verticesPiso;
    private int[] triangulosPiso;

    // Piso exterior
    private GameObject pisoExterior;
    private Vector3[] verticesPisoExterior;
    private int[] triangulosPisoExterior;

    // Techo
    private GameObject techoA;
    private GameObject techoB;

    // Camaras
    private GameObject camara;
    private GameObject camaraOrbital;

    // Colores
    private Color[] coloresParedFrente;
    private Color[] coloresParedFondo;
    private Color[] coloresParedLateral;
    private Color[] coloresPiso;
    private Color[] coloresPisoExterior;

    // Rotacion y movimiento de las camaras
    private float speedH = 1.0f;
    private float speedV = 1.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float movimientoHorizontal, movimientoVertical;
    private float velocidad = 6.0f;
    private Vector3 centro = new Vector3(8.0f, 0.0f, 6.0f); // Centro de la escena a la cual la camara orbitara

    private bool flagPuertas = true;

    void Start()
    {
        // Creo la estructura de la habitacion
        CrearHabitacion();

        // Junto las partes de la primera habitacion como hijos de un mismo GameObject (para posicionarla mas facilmente)
        AgrupoHabitacion1();

        // Duplico la habitacion1 en habitacion2
        habitacion2 = Instantiate(habitacion1, habitacion1.transform.position, habitacion1.transform.rotation);
        habitacion2.name = "Habitacion2";

        habitacion2a = Instantiate(habitacion2);
        habitacion2a.transform.localScale = new Vector3(1f, 1f, 1.05f);
        habitacion2a.transform.position = new Vector3(0f, 0, -0.2f);
        habitacion2a.name = "Habitacion2a";

        habitacion1a = Instantiate(habitacion1);
        habitacion1a.transform.localScale = new Vector3(1f, 1f, 1.05f);
        habitacion1a.transform.position = new Vector3(9f, 0, -0.2f);
        habitacion1a.name = "Habitacion1a";

        // Traslado la habitacion1
        habitacion1.transform.position = new Vector3(9,0,0);

        // Creacion, centrado y posicionamiento de los objetos en el interior de las aulas
        ObjetosAula1(); 
        ObjetosAula2();

        ColocarPuertas();

        // Creacion de camaras
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
            camaraOrbital.transform.RotateAround(centro, -Vector3.up, 1.0f);
            camaraOrbital.transform.rotation = Quaternion.LookRotation(centro - camaraOrbital.transform.position);
        }

        if (camaraOrbital.activeSelf && Input.GetKey(KeyCode.Q))
        {
            camaraOrbital.transform.RotateAround(centro, Vector3.up, 1.0f);
            camaraOrbital.transform.rotation = Quaternion.LookRotation(centro - camaraOrbital.transform.position);
        }

        // Activar y desactivar techo
        if (Input.GetKeyDown(KeyCode.T))
        {
            ocultarTecho(techoA.activeSelf);
        }

        // Activar y desactivar paredes
        if (Input.GetKeyDown(KeyCode.P))
        {
            ocultarParedes(paredFrenteA.activeSelf);
        }

        // Cambiar de camara principal a orbital y viceversa
        if (Input.GetKeyDown(KeyCode.C))
        {
            cambiarCamara(camara.activeSelf);
        }

        // Abrir y cerrar puertas
        if (Input.GetKeyDown(KeyCode.F))
        {
            AbrirPuertas();
        }
    }

    private void ocultarTecho(bool visible)
    {
        if (visible)
        {
            techoA.SetActive(false);
            techoB.SetActive(false);

            habitacion2.transform.GetChild(9).gameObject.SetActive(false);
            habitacion2.transform.GetChild(10).gameObject.SetActive(false);

            habitacion2a.transform.GetChild(9).gameObject.SetActive(false);
            habitacion2a.transform.GetChild(10).gameObject.SetActive(false);

            habitacion1a.transform.GetChild(9).gameObject.SetActive(false);
            habitacion1a.transform.GetChild(10).gameObject.SetActive(false);

        }
        else
        {
            techoA.SetActive(true);
            techoB.SetActive(true);

            habitacion2.transform.GetChild(9).gameObject.SetActive(true);
            habitacion2.transform.GetChild(10).gameObject.SetActive(true);

            habitacion2a.transform.GetChild(9).gameObject.SetActive(true);
            habitacion2a.transform.GetChild(10).gameObject.SetActive(true);

            habitacion1a.transform.GetChild(9).gameObject.SetActive(true);
            habitacion1a.transform.GetChild(10).gameObject.SetActive(true);

        }
    }

    private void ocultarParedes(bool visibles)
    {
        if (visibles)
        {
            paredFrenteA.SetActive(false);
            paredFrenteB.SetActive(false);
            paredFondoA.SetActive(false);
            paredFondoB.SetActive(false);
            paredLateralIzqA.SetActive(false);
            paredLateralIzqB.SetActive(false);
            paredLateralDerA.SetActive(false);
            paredLateralDerB.SetActive(false);
            marcoPuerta1.SetActive(false);
            marcoPuerta2.SetActive(false);

            habitacion2.transform.GetChild(0).gameObject.SetActive(false);
            habitacion2.transform.GetChild(1).gameObject.SetActive(false);
            habitacion2.transform.GetChild(2).gameObject.SetActive(false);
            habitacion2.transform.GetChild(3).gameObject.SetActive(false);
            habitacion2.transform.GetChild(4).gameObject.SetActive(false);
            habitacion2.transform.GetChild(5).gameObject.SetActive(false);
            habitacion2.transform.GetChild(6).gameObject.SetActive(false);
            habitacion2.transform.GetChild(7).gameObject.SetActive(false);

            habitacion2a.transform.GetChild(0).gameObject.SetActive(false);
            habitacion2a.transform.GetChild(1).gameObject.SetActive(false);
            habitacion2a.transform.GetChild(2).gameObject.SetActive(false);
            habitacion2a.transform.GetChild(3).gameObject.SetActive(false);
            habitacion2a.transform.GetChild(4).gameObject.SetActive(false);
            habitacion2a.transform.GetChild(5).gameObject.SetActive(false);
            habitacion2a.transform.GetChild(6).gameObject.SetActive(false);
            habitacion2a.transform.GetChild(7).gameObject.SetActive(false);

            habitacion1a.transform.GetChild(0).gameObject.SetActive(false);
            habitacion1a.transform.GetChild(1).gameObject.SetActive(false);
            habitacion1a.transform.GetChild(2).gameObject.SetActive(false);
            habitacion1a.transform.GetChild(3).gameObject.SetActive(false);
            habitacion1a.transform.GetChild(4).gameObject.SetActive(false);
            habitacion1a.transform.GetChild(5).gameObject.SetActive(false);
            habitacion1a.transform.GetChild(6).gameObject.SetActive(false);
            habitacion1a.transform.GetChild(7).gameObject.SetActive(false);
        }
        else
        {
            paredFrenteA.SetActive(true);
            paredFrenteB.SetActive(true);
            paredFondoA.SetActive(true);
            paredFondoB.SetActive(true);
            paredLateralIzqA.SetActive(true);
            paredLateralIzqB.SetActive(true);
            paredLateralDerA.SetActive(true);
            paredLateralDerB.SetActive(true);
            marcoPuerta1.SetActive(true);
            marcoPuerta2.SetActive(true);

            habitacion2.transform.GetChild(0).gameObject.SetActive(true);
            habitacion2.transform.GetChild(1).gameObject.SetActive(true);
            habitacion2.transform.GetChild(2).gameObject.SetActive(true);
            habitacion2.transform.GetChild(3).gameObject.SetActive(true);
            habitacion2.transform.GetChild(4).gameObject.SetActive(true);
            habitacion2.transform.GetChild(5).gameObject.SetActive(true);
            habitacion2.transform.GetChild(6).gameObject.SetActive(true);
            habitacion2.transform.GetChild(7).gameObject.SetActive(true);

            habitacion2a.transform.GetChild(0).gameObject.SetActive(true);
            habitacion2a.transform.GetChild(1).gameObject.SetActive(true);
            habitacion2a.transform.GetChild(2).gameObject.SetActive(true);
            habitacion2a.transform.GetChild(3).gameObject.SetActive(true);
            habitacion2a.transform.GetChild(4).gameObject.SetActive(true);
            habitacion2a.transform.GetChild(5).gameObject.SetActive(true);
            habitacion2a.transform.GetChild(6).gameObject.SetActive(true);
            habitacion2a.transform.GetChild(7).gameObject.SetActive(true);

            habitacion1a.transform.GetChild(0).gameObject.SetActive(true);
            habitacion1a.transform.GetChild(1).gameObject.SetActive(true);
            habitacion1a.transform.GetChild(2).gameObject.SetActive(true);
            habitacion1a.transform.GetChild(3).gameObject.SetActive(true);
            habitacion1a.transform.GetChild(4).gameObject.SetActive(true);
            habitacion1a.transform.GetChild(5).gameObject.SetActive(true);
            habitacion1a.transform.GetChild(6).gameObject.SetActive(true);
            habitacion1a.transform.GetChild(7).gameObject.SetActive(true);
        }
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

    private void ParedFrente()
    {
        verticesParedFrente = new Vector3[] 
        {
            new Vector3(0,0,0), //v0
            new Vector3(3,0,0), //v1
            new Vector3(0,3.5f,0), //v2
            new Vector3(3,3.5f,0), //v3
            new Vector3(3,2,0), //v4
            new Vector3(5,2,0), //v5
            new Vector3(5,3.5f,0), //v6
            new Vector3(5,0,0), //v7
            new Vector3(8,0,0), //v8
            new Vector3(8,3.5f,0), //v9
        };

        triangulosParedFrente = new int[] 
        { 
            0,2,1,
            1,2,3,
            4,3,5,
            5,3,6,
            7,6,8,
            6,9,8 
        };

        coloresParedFrente = new Color[]
        {
            new Color(1, 0, 0, 1),
            new Color(0.5f, 0.5f, 0.5f, 1),
            new Color(0.5f, 0.5f, 0.5f, 1),
            new Color(0.5f, 0.5f, 0.5f, 1),
            new Color(0.5f, 0.5f, 0.5f, 1),
            new Color(0.5f, 0.5f, 0.5f, 1),
            new Color(0.5f, 0.5f, 0.5f, 1),
            new Color(0.5f, 0.5f, 0.5f, 1),
            new Color(1, 0, 0, 1),
            new Color(0.5f, 0.5f, 0.5f, 1)
        };
    }

    private void ParedFondo()
    {
        verticesParedFondo = new Vector3[] 
        {
            new Vector3(0,0,0), //v0
            new Vector3(0,3.5f,0), //v1
            new Vector3(8,0,0), //v2
            new Vector3(8,3.5f,0), //v3
        };

        triangulosParedFondo = new int[] 
        {   
            0,2,1,
            1,2,3,
        };

        coloresParedFondo = new Color[]
        {
            new Color(1, 0.8627f, 0.3922f, 1),
            new Color(0.2353f, 0.9412f, 0.8902f, 1),
            new Color(1, 0.8627f, 0.3922f, 1),
            new Color(0.2353f, 0.9412f, 0.8902f, 1)
        };
    }

    private void ParedLateral()
    {
        verticesParedLateral = new Vector3[] 
        {
            new Vector3(0,0,0), //v0
            new Vector3(0,3.5f,0), //v1
            new Vector3(0,0,12), //v2
            new Vector3(0,3.5f,12), //v3
        };

        triangulosParedLateral = new int[] 
        {   
            0,1,2,
            1,3,2,
        };

        coloresParedLateral = new Color[]
        {
            new Color(1, 0.8627f, 0.3922f, 1),
            new Color(0.2353f, 0.9412f, 0.8902f, 1),
            new Color(1, 0.8627f, 0.3922f, 1),
            new Color(0.2353f, 0.9412f, 0.8902f, 1)
        };
    }

    private void Piso()
    {
        verticesPiso = new Vector3[] 
        {
            new Vector3(0,0,0), //v0
            new Vector3(0,0,12), //v1
            new Vector3(8,0,0), //v2
            new Vector3(8,0,12) //v3
        };

        triangulosPiso = new int[] 
        {
            0,1,2,
            2,1,3
        };

        coloresPiso = new Color[]
        {
            new Color(0.3f, 0.3f, 0.3f, 1),
            new Color(0.3f, 0.3f, 0.3f, 1),
            new Color(0.3f, 0.3f, 0.3f, 1),
            new Color(0.3f, 0.3f, 0.3f, 1)
        };
    }

    private void PisoExterior()
    {
        verticesPisoExterior = new Vector3[] 
        {
            new Vector3(-1000,-0.001f,-1000), //v0
            new Vector3(-1000,-0.001f,1000), //v1
            new Vector3(1000,-0.001f,-1000), //v2
            new Vector3(1000,-0.001f,1000) //v3
        };

        triangulosPisoExterior = new int[] 
        {
            0,1,2,
            2,1,3
        };

        coloresPisoExterior = new Color[]
        {
            new Color(0.0784f, 0.451f, 0, 1),
            new Color(0.0784f, 0.451f, 0, 1),
            new Color(0.0784f, 0.451f, 0, 1),
            new Color(0.0784f, 0.451f, 0, 1)
        };
    }

    private void CreateCamera()
    {
        camara = new GameObject("Camara");
        camara.AddComponent<Camera>();
        camara.transform.position = new Vector3(4, 1.4f, -4);

        camara.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        camara.GetComponent<Camera>().backgroundColor = Color.black; 
    }

    private void CreateOrbitalCamera()
    {
        camaraOrbital = new GameObject("Camara Orbital");
        camaraOrbital.AddComponent<Camera>();
        camaraOrbital.transform.position = new Vector3(8, 10f, -8);
        camaraOrbital.transform.rotation = Quaternion.Euler(40,0,0);
        camaraOrbital.transform.rotation = Quaternion.LookRotation(centro - camaraOrbital.transform.position);

        camaraOrbital.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        camaraOrbital.GetComponent<Camera>().backgroundColor = Color.black;

        camaraOrbital.SetActive(false);
    }

    private void UpdateMeshParedFrente(GameObject obj)
    {
        obj.GetComponent<MeshFilter>().mesh.vertices = verticesParedFrente;
        obj.GetComponent<MeshFilter>().mesh.triangles = triangulosParedFrente;
        obj.GetComponent<MeshFilter>().mesh.colors = coloresParedFrente;
    }

    private void UpdateMeshParedFondo(GameObject obj)
    {
        obj.GetComponent<MeshFilter>().mesh.vertices = verticesParedFondo;
        obj.GetComponent<MeshFilter>().mesh.triangles = triangulosParedFondo;
        obj.GetComponent<MeshFilter>().mesh.colors = coloresParedFondo;
    }

    private void UpdateMeshParedLateral(GameObject obj)
    {
        obj.GetComponent<MeshFilter>().mesh.vertices = verticesParedLateral;
        obj.GetComponent<MeshFilter>().mesh.triangles = triangulosParedLateral;
        obj.GetComponent<MeshFilter>().mesh.colors = coloresParedLateral;
    }

    private void UpdateMeshPiso(GameObject obj)
    {
        obj.GetComponent<MeshFilter>().mesh.vertices = verticesPiso;
        obj.GetComponent<MeshFilter>().mesh.triangles = triangulosPiso;
        obj.GetComponent<MeshFilter>().mesh.colors = coloresPiso;
    }

    private void UpdateMeshPisoExterior(GameObject obj)
    {
        obj.GetComponent<MeshFilter>().mesh.vertices = verticesPisoExterior;
        obj.GetComponent<MeshFilter>().mesh.triangles = triangulosPisoExterior;
        obj.GetComponent<MeshFilter>().mesh.colors = coloresPisoExterior;
    }

    private void CreateMaterial(GameObject obj)
    {
        Material miMaterial = new Material(Shader.Find("Shader"));
        obj.GetComponent<MeshRenderer>().material = miMaterial;
    }

    private void AgrupoHabitacion1()
    {
        habitacion1 = new GameObject("Habitacion1");

        paredFrenteA.transform.parent = habitacion1.transform;
        paredFrenteB.transform.parent = habitacion1.transform;
        paredFondoA.transform.parent = habitacion1.transform;
        paredFondoB.transform.parent = habitacion1.transform;
        paredLateralIzqA.transform.parent = habitacion1.transform;
        paredLateralIzqB.transform.parent = habitacion1.transform;
        paredLateralDerA.transform.parent = habitacion1.transform;
        paredLateralDerB.transform.parent = habitacion1.transform;
        piso.transform.parent = habitacion1.transform;
        techoA.transform.parent = habitacion1.transform;
        techoB.transform.parent = habitacion1.transform;
    }

    private void CrearHabitacion()
    {
        ParedFrente();
        paredFrenteA = new GameObject("ParedFrenteA");
        paredFrenteA.AddComponent<MeshFilter>();
        paredFrenteA.GetComponent<MeshFilter>().mesh = new Mesh();
        paredFrenteA.AddComponent<MeshRenderer>();
        UpdateMeshParedFrente(paredFrenteA);
        CreateMaterial(paredFrenteA);

        paredFrenteB = new GameObject("ParedFrenteB");
        paredFrenteB.AddComponent<MeshFilter>();
        paredFrenteB.GetComponent<MeshFilter>().mesh = new Mesh();
        paredFrenteB.AddComponent<MeshRenderer>();
        UpdateMeshParedFrente(paredFrenteB);
        CreateMaterial(paredFrenteB);
        paredFrenteB.transform.rotation = Quaternion.Euler(0,180,0);
        paredFrenteB.transform.position = new Vector3(8,0,0);

        ParedFondo();
        paredFondoA = new GameObject("ParedFondoA");
        paredFondoA.AddComponent<MeshFilter>();
        paredFondoA.GetComponent<MeshFilter>().mesh = new Mesh();
        paredFondoA.AddComponent<MeshRenderer>();
        UpdateMeshParedFondo(paredFondoA);
        CreateMaterial(paredFondoA);
        paredFondoA.transform.rotation = Quaternion.Euler(0,180,0);
        paredFondoA.transform.position = new Vector3(8,0,12);

        paredFondoB = new GameObject("ParedFondoA");
        paredFondoB.AddComponent<MeshFilter>();
        paredFondoB.GetComponent<MeshFilter>().mesh = new Mesh();
        paredFondoB.AddComponent<MeshRenderer>();
        UpdateMeshParedFondo(paredFondoB);
        CreateMaterial(paredFondoB);
        paredFondoB.transform.position = new Vector3(0,0,12);

        ParedLateral();
        paredLateralIzqA = new GameObject("ParedLateralIzqA");
        paredLateralIzqA.AddComponent<MeshFilter>();
        paredLateralIzqA.GetComponent<MeshFilter>().mesh = new Mesh();
        paredLateralIzqA.AddComponent<MeshRenderer>();
        UpdateMeshParedLateral(paredLateralIzqA);
        CreateMaterial(paredLateralIzqA);

        paredLateralIzqB = new GameObject("ParedLateralIzqB");
        paredLateralIzqB.AddComponent<MeshFilter>();
        paredLateralIzqB.GetComponent<MeshFilter>().mesh = new Mesh();
        paredLateralIzqB.AddComponent<MeshRenderer>();
        UpdateMeshParedLateral(paredLateralIzqB);
        CreateMaterial(paredLateralIzqB);
        paredLateralIzqB.transform.rotation = Quaternion.Euler(0,180,0);
        paredLateralIzqB.transform.position = new Vector3(0,0,12);

        paredLateralDerA = new GameObject("ParedLateralDerA");
        paredLateralDerA.AddComponent<MeshFilter>();
        paredLateralDerA.GetComponent<MeshFilter>().mesh = new Mesh();
        paredLateralDerA.AddComponent<MeshRenderer>();
        UpdateMeshParedLateral(paredLateralDerA);
        CreateMaterial(paredLateralDerA);
        paredLateralDerA.transform.rotation = Quaternion.Euler(0,180,0);
        paredLateralDerA.transform.position = new Vector3(8,0,12);

        paredLateralDerB = new GameObject("ParedLateralDerB");
        paredLateralDerB.AddComponent<MeshFilter>();
        paredLateralDerB.GetComponent<MeshFilter>().mesh = new Mesh();
        paredLateralDerB.AddComponent<MeshRenderer>();
        UpdateMeshParedLateral(paredLateralDerB);
        CreateMaterial(paredLateralDerB);
        paredLateralDerB.transform.position = new Vector3(8,0,0);

        Piso();
        piso = new GameObject("Piso");
        piso.AddComponent<MeshFilter>();
        piso.GetComponent<MeshFilter>().mesh = new Mesh();
        piso.AddComponent<MeshRenderer>();
        UpdateMeshPiso(piso);
        CreateMaterial(piso);

        PisoExterior();
        pisoExterior = new GameObject("PisoExterior");
        pisoExterior.AddComponent<MeshFilter>();
        pisoExterior.GetComponent<MeshFilter>().mesh = new Mesh();
        pisoExterior.AddComponent<MeshRenderer>();
        UpdateMeshPisoExterior(pisoExterior);
        CreateMaterial(pisoExterior);

        techoA = new GameObject("TechoA");
        techoA.AddComponent<MeshFilter>();
        techoA.GetComponent<MeshFilter>().mesh = new Mesh();
        techoA.AddComponent<MeshRenderer>();
        UpdateMeshPiso(techoA);
        CreateMaterial(techoA);
        techoA.transform.rotation = Quaternion.Euler(0,0,180);
        techoA.transform.position = new Vector3(8,3.5f,0);

        techoB = new GameObject("TechoB");
        techoB.AddComponent<MeshFilter>();
        techoB.GetComponent<MeshFilter>().mesh = new Mesh();
        techoB.AddComponent<MeshRenderer>();
        UpdateMeshPiso(techoB);
        CreateMaterial(techoB);
        techoB.transform.position = new Vector3(0,3.5f,0);

        filereader.leer("marcoPuerta");
        marcoPuerta1 = filereader.getObjeto();
        marcoPuerta1.name = "Marco Puerta 1";
        marcoPuerta1.transform.position = new Vector3(4f, 1f, -0.1f);
        marcoPuerta1.transform.rotation = Quaternion.Euler(0,-90,0);

        marcoPuerta2 = Instantiate(marcoPuerta1, marcoPuerta1.transform.position, marcoPuerta1.transform.rotation);
        marcoPuerta2.name = "Marco Puerta 2";
        marcoPuerta2.transform.position = new Vector3(13f, 1f, -0.1f);
    }

    private void ObjetosAula1() // Aula normal
    {
        GameObject banco1 = new GameObject("Banco 1");
        GameObject banco2, banco3, banco4, banco5, banco6;

        filereader.leer("escritorio");
        GameObject escritorio = filereader.getObjeto();
        escritorio.transform.localScale = new Vector3(0.18f, 0.18f, 0.18f);
        escritorio.transform.position = new Vector3(1.6f, 0.365f, 8.2f);
        escritorio.transform.rotation = Quaternion.Euler(0,-180,0);
        filereader.SetColor(0.55f, 0.35f, 0f); // El color se aplica al ultimo objeto creado por el FileReader, si no se especifica se establece un gris por defecto

        filereader.leer("notebook");
        GameObject notebook = filereader.getObjeto();
        notebook.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
        notebook.transform.position = new Vector3(1.6f, 0.828f, 8f);
        filereader.SetColor(0.278f, 0.513f, 0.603f);

        filereader.leer("sillaPlastica");
        GameObject sillaPlastica = filereader.getObjeto();
        sillaPlastica.transform.localScale = new Vector3(0.858f, 0.858f, 0.858f);
        sillaPlastica.transform.position = new Vector3(1.6f, 0.53f, 7.1f);
        filereader.SetColor(0.7f, 0.7f, 0.7f);
        
        filereader.leer("pizarron");
        GameObject pizarron1 = filereader.getObjeto();
        pizarron1.transform.position = new Vector3(4.2f, 1.5f, 11.923f);
        pizarron1.transform.rotation = Quaternion.Euler(0,90,0);
        filereader.SetColor(0.9f, 0.9f, 0.9f);

        escritorio.transform.parent = banco1.transform;
        notebook.transform.parent = banco1.transform;
        sillaPlastica.transform.parent = banco1.transform;

        banco2 = Instantiate(banco1, banco1.transform.position, banco1.transform.rotation);
        banco2.transform.position = new Vector3(0, 0, -2.9f);
        banco2.name = "Banco 2";

        banco3 = Instantiate(banco1, banco1.transform.position, banco1.transform.rotation);
        banco3.transform.position = new Vector3(0, 0, -5.75f);
        banco3.name = "Banco 3";

        banco4 = Instantiate(banco1, banco1.transform.position, banco1.transform.rotation);
        banco4.transform.position = new Vector3(4.8f, 0, 0f);
        banco4.name = "Banco 4";

        banco5 = Instantiate(banco1, banco1.transform.position, banco1.transform.rotation);
        banco5.transform.position = new Vector3(4.8f, 0, -2.9f);
        banco5.name = "Banco 5";

        banco6 = Instantiate(banco1, banco1.transform.position, banco1.transform.rotation);
        banco6.transform.position = new Vector3(4.8f, 0, -5.75f);
        banco6.name = "Banco 6";

        filereader.leer("profesor");
        GameObject profesor = filereader.getObjeto();
        profesor.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        profesor.transform.position = new Vector3(4.3f, 0.825f, 11.2f);
        filereader.SetColor(0.5f, 0.45f, 0.3f);
    }

    private void ObjetosAula2() // Aula pokemones
    {
        filereader.leer("gohan");
        GameObject profegohan = filereader.getObjeto();
        profegohan.transform.position = new Vector3(13, 1.26f, 10);
        profegohan.transform.rotation = Quaternion.Euler(0, 180, 0);
        profegohan.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        filereader.SetColor(0.5f, 0.45f, 0.3f);

        filereader.leer("mesa2");
        GameObject mesa1 = filereader.getObjeto();
        GameObject mesa2, mesa3;

        mesa1.transform.position = new Vector3(13, 0.5f, 7.8f);
        mesa1.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        mesa1.transform.rotation = Quaternion.Euler(0, -90, 0);
        filereader.SetColor(0.2f, 0.1f, 0);
        mesa1.name = "mesa1";

        mesa2 = Instantiate(mesa1, mesa1.transform.position, mesa1.transform.rotation);
        mesa3 = Instantiate(mesa1, mesa1.transform.position, mesa1.transform.rotation);

        mesa2.transform.position = new Vector3(13, 0.5f, 5.3f);
        mesa2.name = "mesa2";

        mesa3.transform.position = new Vector3(13, 0.5f, 2.8f);
        mesa3.name = "mesa3";

        filereader.leer("bulbasaur");
        GameObject bulbasaurmascota = filereader.getObjeto();
        bulbasaurmascota.transform.position = new Vector3(11.3f, 0.95f, 2.8f);
        bulbasaurmascota.transform.localScale = new Vector3(30, 30, 30);
        filereader.SetColor(0.47f, 0.72f, 0.68f);

        filereader.leer("caterpie");
        GameObject caterpiemascota = filereader.getObjeto();
        caterpiemascota.transform.position = new Vector3(14.7f, 0.91f, 2.8f);
        caterpiemascota.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        filereader.SetColor(0.49f, 0.8f, 0.42f);

        filereader.leer("charmander");
        GameObject charmandermascota = filereader.getObjeto();
        charmandermascota.transform.position = new Vector3(11.3f, 1.045f, 5.3f);
        charmandermascota.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        filereader.SetColor(0.91f, 0.46f, 0.11f);

        filereader.leer("metapod");
        GameObject metapodmascota = filereader.getObjeto();
        metapodmascota.transform.position = new Vector3(14.7f, 0.982f, 5.3f);
        metapodmascota.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        filereader.SetColor(0.64f, 0.82f, 0.37f);

        filereader.leer("pikachu");
        GameObject pikachumascota = filereader.getObjeto();
        pikachumascota.transform.position = new Vector3(14.7f, 1.06f, 7.8f);
        pikachumascota.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        filereader.SetColor(0.92f, 0.77f, 0.06f);

        filereader.leer("squirtle");
        GameObject squirtlemascota = filereader.getObjeto();
        squirtlemascota.transform.position = new Vector3(11.3f, 1.025f, 7.8f);
        squirtlemascota.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        filereader.SetColor(0.42f, 0.84f, 0.98f);

        filereader.leer("pokedex");
        GameObject pokedex1 = filereader.getObjeto();
        pokedex1.transform.position = new Vector3(12.5f, 0.95f, 7.8f);
        pokedex1.transform.rotation = Quaternion.Euler(0, 180, 0);
        pokedex1.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        filereader.SetColor(0.75f, 0.44f, 0.44f);
        pokedex1.name = "pokedex1";

        GameObject pokedex2 = Instantiate(pokedex1, pokedex1.transform.position, pokedex1.transform.rotation);
        pokedex2.transform.position = new Vector3(13.5f, 0.95f, 7.8f);
        pokedex2.name = "pokedex2";

        GameObject pokedex3 = Instantiate(pokedex1, pokedex1.transform.position, pokedex1.transform.rotation);
        pokedex3.transform.position = new Vector3(13.5f, 0.95f, 3);
        pokedex3.name = "pokedex3";
        
        GameObject pokedex4 = Instantiate(pokedex1, pokedex1.transform.position, pokedex1.transform.rotation);
        pokedex4.transform.position = new Vector3(12.5f, 0.95f, 3);
        pokedex4.name = "pokedex4";
        
        GameObject pokedex5 = Instantiate(pokedex1, pokedex1.transform.position, pokedex1.transform.rotation);
        pokedex5.transform.position = new Vector3(13.5f, 0.95f, 5.35f);
        pokedex5.name = "pokedex5";

        GameObject pokedex6 = Instantiate(pokedex1, pokedex1.transform.position, pokedex1.transform.rotation);
        pokedex6.transform.position = new Vector3(12.5f, 0.95f, 5.35f);
        pokedex6.name = "pokedex6";

        filereader.leer("silla2");
        GameObject silla1 = filereader.getObjeto();
        silla1.transform.position = new Vector3(12.2f, 0.65f, 6.8f);
        silla1.transform.localScale = new Vector3(0.016f, 0.016f, 0.016f);
        silla1.name = "silla1";

        GameObject silla2 = Instantiate(silla1, silla1.transform.position, silla1.transform.rotation);
        silla2.transform.position = new Vector3(13.8f, 0.65f, 6.8f);
        silla2.name = "silla2";

        GameObject silla3 = Instantiate(silla1, silla1.transform.position, silla1.transform.rotation);
        silla3.transform.position = new Vector3(13.8f, 0.65f, 4.3f);
        silla3.name = "silla3";

        GameObject silla4 = Instantiate(silla1, silla1.transform.position, silla1.transform.rotation);
        silla4.transform.position = new Vector3(12.2f, 0.65f, 4.3f);
        silla4.name = "silla4";

        GameObject silla5 = Instantiate(silla1, silla1.transform.position, silla1.transform.rotation);
        silla5.transform.position = new Vector3(12.2f, 0.65f, 1.8f);
        silla5.name = "silla5";

        GameObject silla6 = Instantiate(silla1, silla1.transform.position, silla1.transform.rotation);
        silla6.transform.position = new Vector3(13.8f, 0.65f, 1.8f);
        silla6.name = "silla6";

        filereader.leer("pizarron");
        GameObject pizarron2 = filereader.getObjeto();
        pizarron2.transform.position = new Vector3(13f, 1.5f, 11.923f);
        pizarron2.transform.rotation = Quaternion.Euler(0,90,0);
        filereader.SetColor(0.9f, 0.9f, 0.9f);
        
        GameObject candelabro1 = CrearCandelabro();
        candelabro1.name = "candelabro1";
        candelabro1.transform.position = new Vector3(13, 2.65f, 6);

        GameObject candelabro2 = CrearCandelabro();
        candelabro2.name = "candelabro2";
        candelabro2.transform.position = new Vector3(4, 2.65f, 6);
    }

    private void ColocarPuertas()
    {
        filereader.leer("puerta");
        puerta1 = filereader.getObjeto();
        puerta1.name = "Puerta1";
        puerta1.transform.position = new Vector3(3.51f, 1f, -0.17f);
        puerta1.transform.rotation = Quaternion.Euler(0,0,0);
        filereader.SetColor(0.392f, 0.274f, 0f);

        filereader.leer("puerta");
        puerta2 = filereader.getObjeto();
        puerta2.name = "Puerta2";
        puerta2.transform.position = new Vector3(4.49f, 1f, -0.17f);
        puerta2.transform.rotation = Quaternion.Euler(0,-180,0);
        filereader.SetColor(0.392f, 0.274f, 0f);

        filereader.leer("puerta");
        puerta3 = filereader.getObjeto();
        puerta3.name = "Puerta3";
        puerta3.transform.position = new Vector3(12.51f, 1f, -0.17f);
        puerta3.transform.rotation = Quaternion.Euler(0,0,0);
        filereader.SetColor(0.392f, 0.274f, 0f);

        filereader.leer("puerta");
        puerta4 = filereader.getObjeto();
        puerta4.name = "Puerta4";
        puerta4.transform.position = new Vector3(13.49f, 1f, -0.17f);
        puerta4.transform.rotation = Quaternion.Euler(0,-180,0);
        filereader.SetColor(0.392f, 0.274f, 0f);
    }

    private GameObject CrearCandelabro()
    {
        GameObject padre = new GameObject();

        filereader.leer("candelabro");
        GameObject candelabro = filereader.getObjeto();
        candelabro.transform.SetParent(padre.transform, true);
        filereader.SetColor(0.1f, 0.1f, 0.1f);

        filereader.leer("vela");
        GameObject vela1 = filereader.getObjeto();
        vela1.name = "vela1";
        vela1.transform.SetParent(candelabro.transform, true);
        filereader.SetColor(0.89f, 0.77f, 0.51f);

        GameObject vela2 = Instantiate(vela1);
        vela2.name = "vela2";
        vela2.transform.SetParent(candelabro.transform, true);

        GameObject vela3 = Instantiate(vela1);
        vela3.name = "vela3";
        vela3.transform.SetParent(candelabro.transform, true);

        GameObject vela4 = Instantiate(vela1);
        vela4.name = "vela4";
        vela4.transform.SetParent(candelabro.transform, true);
        
        filereader.leer("llama");
        GameObject llama1 = filereader.getObjeto();
        llama1.name = "llama1";
        llama1.transform.SetParent(vela1.transform, true);
        filereader.SetColor(0.9f, 0.1f, 0);
        
        GameObject llama2 = Instantiate(llama1);
        llama2.name = "llama2";
        llama2.transform.SetParent(vela2.transform, true);
        
        GameObject llama3 = Instantiate(llama1);
        llama3.name = "llama3";
        llama3.transform.SetParent(vela3.transform, true);

        GameObject llama4 = Instantiate(llama1);
        llama4.name = "llama4";
        llama4.transform.SetParent(vela4.transform, true);

        candelabro.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);

        vela1.transform.localScale = new Vector3(4, 4, 4);
        vela1.transform.rotation = Quaternion.Euler(-90, 0, 0);
        vela1.transform.position = new Vector3(0, 0, 0.96f);

        vela2.transform.localScale = new Vector3(4, 4, 4);
        vela2.transform.rotation = Quaternion.Euler(-90, 0, 0);
        vela2.transform.position = new Vector3(0, 0, -0.96f);

        vela3.transform.localScale = new Vector3(4, 4, 4);
        vela3.transform.rotation = Quaternion.Euler(-90, 0, 0);
        vela3.transform.position = new Vector3(0.96f, 0, 0);

        vela4.transform.localScale = new Vector3(4, 4, 4);
        vela4.transform.rotation = Quaternion.Euler(-90, 0, 0);
        vela4.transform.position = new Vector3(-0.96f, 0, 0);   

        llama1.transform.position = new Vector3(-0.01f, 0.2f, 0.95f);
        llama2.transform.position = new Vector3(-0.01f, 0.2f, -0.97f);
        llama3.transform.position = new Vector3(0.95f, 0.2f, -0.01f);
        llama4.transform.position = new Vector3(-0.97f, 0.2f, -0.01f);

        return padre;
    }

    private void AbrirPuertas()
    {
        if (flagPuertas) //Abrir
        {
            puerta1.transform.rotation = Quaternion.Euler(0,-90,0);
            puerta2.transform.rotation = Quaternion.Euler(0,270,0);
            puerta3.transform.rotation = Quaternion.Euler(0,-90,0);
            puerta4.transform.rotation = Quaternion.Euler(0,270,0);

            puerta1.transform.position = new Vector3(3.02f, 1f, 0.375f);
            puerta2.transform.position = new Vector3(4.975f, 1f, 0.375f);
            puerta3.transform.position = new Vector3(12.025f, 1f, 0.375f);
            puerta4.transform.position = new Vector3(13.975f, 1f, 0.375f); 
        }
        else //Cerrar
        {
            puerta1.transform.rotation = Quaternion.Euler(0,0,0);
            puerta2.transform.rotation = Quaternion.Euler(0,-180,0);
            puerta3.transform.rotation = Quaternion.Euler(0,0,0);
            puerta4.transform.rotation = Quaternion.Euler(0,-180,0);

            puerta1.transform.position = new Vector3(3.51f, 1f, -0.17f);
            puerta2.transform.position = new Vector3(4.49f, 1f, -0.17f);
            puerta3.transform.position = new Vector3(12.51f, 1f, -0.17f);
            puerta4.transform.position = new Vector3(13.49f, 1f, -0.17f); 
        }

        flagPuertas = !flagPuertas;
    }
}