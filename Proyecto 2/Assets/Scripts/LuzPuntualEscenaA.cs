using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzPuntualEscenaA : MonoBehaviour
{
    public GameObject autos;
    public GameObject foco;

    public Vector4 luzPuntual;

    private Material mat0, mat1, mat2, mat3, mat4, mat5, mat6, mat7, mat8, mat9, mat10, mat11, mat12, mat13, mat14;

    void Start()
    {
        luzPuntual = new Vector4(-12, 8, 26, 1);
        foco.transform.position = new Vector3(luzPuntual.x, luzPuntual.y, luzPuntual.z);

        // Blinn-Phong
        mat0 = autos.transform.GetChild(0).GetComponent<Renderer>().material;
        mat1 = autos.transform.GetChild(1).GetComponent<Renderer>().material;
        mat2 = autos.transform.GetChild(2).GetComponent<Renderer>().material;

        // Cook-Torrance
        mat3 = autos.transform.GetChild(3).GetComponent<Renderer>().material;
        mat4 = autos.transform.GetChild(4).GetComponent<Renderer>().material;
        mat5 = autos.transform.GetChild(5).GetComponent<Renderer>().material;

        // Toon
        mat6 = autos.transform.GetChild(6).GetComponent<Renderer>().material;
        mat7 = autos.transform.GetChild(7).GetComponent<Renderer>().material;
        mat8 = autos.transform.GetChild(8).GetComponent<Renderer>().material;

        // Texturas 1
        mat9 = autos.transform.GetChild(9).GetComponent<Renderer>().material;
        mat10 = autos.transform.GetChild(10).GetComponent<Renderer>().material;
        mat11 = autos.transform.GetChild(11).GetComponent<Renderer>().material;

        // Texturas 2
        mat12 = autos.transform.GetChild(12).GetComponent<Renderer>().material;
        mat13 = autos.transform.GetChild(13).GetComponent<Renderer>().material;
        mat14 = autos.transform.GetChild(14).GetComponent<Renderer>().material;
    }

    void Update()
    {
        mat0.SetColor("_LightPosition_w", luzPuntual);
        mat1.SetColor("_LightPosition_w", luzPuntual);
        mat2.SetColor("_LightPosition_w", luzPuntual);
        mat3.SetColor("_LightPosition_w", luzPuntual);
        mat4.SetColor("_LightPosition_w", luzPuntual);
        mat5.SetColor("_LightPosition_w", luzPuntual);
        mat6.SetColor("_LightPosition_w", luzPuntual);
        mat7.SetColor("_LightPosition_w", luzPuntual);
        mat8.SetColor("_LightPosition_w", luzPuntual);
        mat9.SetColor("_LightPosition_w", luzPuntual);
        mat10.SetColor("_LightPosition_w", luzPuntual);
        mat11.SetColor("_LightPosition_w", luzPuntual);
        mat12.SetColor("_LightPosition_w", luzPuntual);
        mat13.SetColor("_LightPosition_w", luzPuntual);
        mat14.SetColor("_LightPosition_w", luzPuntual);

        foco.transform.position = new Vector3(luzPuntual.x, luzPuntual.y, luzPuntual.z);
    }
}