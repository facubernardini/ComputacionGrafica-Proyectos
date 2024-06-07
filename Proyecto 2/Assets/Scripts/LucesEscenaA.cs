using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucesEscenaA : MonoBehaviour
{
    public GameObject autos;
    public GameObject foco;
    public Material piso;

    public Vector4 pointLightPosition;
    public Color colorLuzPuntual;

    public Vector4 directionalLightDirection;
    public Color colorLuzDireccional;

    public Vector4 spotLightPosition;
    public Vector4 spotLightDirection;
    public float spotLightAperture;
    public Color colorLuzSpot;

    private Material mat0, mat1, mat2, mat3, mat4, mat5, mat6, mat7, mat8, mat9, mat10, mat11, mat12, mat13, mat14;

    private Vector4 luzApagada;

    private bool luzSpotActivada, luzDireccionalActivada, luzPuntualActivada;

    void Start()
    {
        pointLightPosition = new Vector4(-12, 8, 26, 1);
        
        directionalLightDirection = new Vector4(5, -10, -8, 1);

        spotLightPosition = new Vector4(-12, 20, 26, 1);
        spotLightDirection = new Vector4(0, -20, 0, 1);
        spotLightAperture = 30.0f;

        foco.transform.position = new Vector3(pointLightPosition.x, pointLightPosition.y, pointLightPosition.z);

        luzApagada = new Vector4(0, 0, 0, 1);

        colorLuzDireccional = new Vector4(1, 1, 1, 1);
        colorLuzPuntual = new Vector4(1, 1, 1, 1);
        colorLuzSpot = new Vector4(1, 1, 1, 1);

        luzSpotActivada = true;
        luzDireccionalActivada = true;
        luzPuntualActivada = true;

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
        // Posicion luz puntual
        mat0.SetColor("_PointLightPosition_w", pointLightPosition);
        mat1.SetColor("_PointLightPosition_w", pointLightPosition);
        mat2.SetColor("_PointLightPosition_w", pointLightPosition);
        mat3.SetColor("_PointLightPosition_w", pointLightPosition);
        mat4.SetColor("_PointLightPosition_w", pointLightPosition);
        mat5.SetColor("_PointLightPosition_w", pointLightPosition);
        mat6.SetColor("_PointLightPosition_w", pointLightPosition);
        mat7.SetColor("_PointLightPosition_w", pointLightPosition);
        mat8.SetColor("_PointLightPosition_w", pointLightPosition);
        mat9.SetColor("_PointLightPosition_w", pointLightPosition);
        mat10.SetColor("_PointLightPosition_w", pointLightPosition);
        mat11.SetColor("_PointLightPosition_w", pointLightPosition);
        mat12.SetColor("_PointLightPosition_w", pointLightPosition);
        mat13.SetColor("_PointLightPosition_w", pointLightPosition);
        mat14.SetColor("_PointLightPosition_w", pointLightPosition);

        piso.SetColor("_PointLightPosition_w", pointLightPosition);


        // Direccion luz direccional
        mat0.SetColor("_DirectionalLightDirection", directionalLightDirection);
        mat1.SetColor("_DirectionalLightDirection", directionalLightDirection);
        mat2.SetColor("_DirectionalLightDirection", directionalLightDirection);
        mat3.SetColor("_DirectionalLightDirection", directionalLightDirection);
        mat4.SetColor("_DirectionalLightDirection", directionalLightDirection);
        mat5.SetColor("_DirectionalLightDirection", directionalLightDirection);
        mat6.SetColor("_DirectionalLightDirection", directionalLightDirection);
        mat7.SetColor("_DirectionalLightDirection", directionalLightDirection);
        mat8.SetColor("_DirectionalLightDirection", directionalLightDirection);
        mat9.SetColor("_DirectionalLightDirection", directionalLightDirection);
        mat10.SetColor("_DirectionalLightDirection", directionalLightDirection);
        mat11.SetColor("_DirectionalLightDirection", directionalLightDirection);
        mat12.SetColor("_DirectionalLightDirection", directionalLightDirection);
        mat13.SetColor("_DirectionalLightDirection", directionalLightDirection);
        mat14.SetColor("_DirectionalLightDirection", directionalLightDirection);

        piso.SetColor("_DirectionalLightDirection", directionalLightDirection);


        // Posicion luz spot
        mat1.SetColor("_SpotLightPosition_w", spotLightPosition);
        mat0.SetColor("_SpotLightPosition_w", spotLightPosition);
        mat2.SetColor("_SpotLightPosition_w", spotLightPosition);
        mat3.SetColor("_SpotLightPosition_w", spotLightPosition);
        mat4.SetColor("_SpotLightPosition_w", spotLightPosition);
        mat5.SetColor("_SpotLightPosition_w", spotLightPosition);
        mat6.SetColor("_SpotLightPosition_w", spotLightPosition);
        mat7.SetColor("_SpotLightPosition_w", spotLightPosition);
        mat8.SetColor("_SpotLightPosition_w", spotLightPosition);
        mat9.SetColor("_SpotLightPosition_w", spotLightPosition);
        mat10.SetColor("_SpotLightPosition_w", spotLightPosition);
        mat11.SetColor("_SpotLightPosition_w", spotLightPosition);
        mat12.SetColor("_SpotLightPosition_w", spotLightPosition);
        mat13.SetColor("_SpotLightPosition_w", spotLightPosition);
        mat14.SetColor("_SpotLightPosition_w", spotLightPosition);

        piso.SetColor("_SpotLightPosition_w", spotLightPosition);

        // Direccion luz spot
        mat1.SetColor("_SpotLightDirection", spotLightDirection);
        mat0.SetColor("_SpotLightDirection", spotLightDirection);
        mat2.SetColor("_SpotLightDirection", spotLightDirection);
        mat3.SetColor("_SpotLightDirection", spotLightDirection);
        mat4.SetColor("_SpotLightDirection", spotLightDirection);
        mat5.SetColor("_SpotLightDirection", spotLightDirection);
        mat6.SetColor("_SpotLightDirection", spotLightDirection);
        mat7.SetColor("_SpotLightDirection", spotLightDirection);
        mat8.SetColor("_SpotLightDirection", spotLightDirection);
        mat9.SetColor("_SpotLightDirection", spotLightDirection);
        mat10.SetColor("_SpotLightDirection", spotLightDirection);
        mat11.SetColor("_SpotLightDirection", spotLightDirection);
        mat12.SetColor("_SpotLightDirection", spotLightDirection);
        mat13.SetColor("_SpotLightDirection", spotLightDirection);
        mat14.SetColor("_SpotLightDirection", spotLightDirection);

        piso.SetColor("_SpotLightDirection", spotLightDirection);


        // Apertura luz spot
        mat0.SetFloat("_Apertura", spotLightAperture);
        mat1.SetFloat("_Apertura", spotLightAperture);
        mat2.SetFloat("_Apertura", spotLightAperture);
        mat3.SetFloat("_Apertura", spotLightAperture);
        mat4.SetFloat("_Apertura", spotLightAperture);
        mat5.SetFloat("_Apertura", spotLightAperture);
        mat6.SetFloat("_Apertura", spotLightAperture);
        mat7.SetFloat("_Apertura", spotLightAperture);
        mat8.SetFloat("_Apertura", spotLightAperture);
        mat9.SetFloat("_Apertura", spotLightAperture);
        mat10.SetFloat("_Apertura", spotLightAperture);
        mat11.SetFloat("_Apertura", spotLightAperture);
        mat12.SetFloat("_Apertura", spotLightAperture);
        mat13.SetFloat("_Apertura", spotLightAperture);
        mat14.SetFloat("_Apertura", spotLightAperture);

        piso.SetFloat("_Apertura", spotLightAperture);

        foco.transform.position = new Vector3(pointLightPosition.x, pointLightPosition.y, pointLightPosition.z);

        if (luzPuntualActivada)
        {
            mat0.SetColor("_PointLightIntensity", colorLuzPuntual);
            mat1.SetColor("_PointLightIntensity", colorLuzPuntual);
            mat2.SetColor("_PointLightIntensity", colorLuzPuntual);
            mat4.SetColor("_PointLightIntensity", colorLuzPuntual);
            mat3.SetColor("_PointLightIntensity", colorLuzPuntual);
            mat5.SetColor("_PointLightIntensity", colorLuzPuntual);
            mat6.SetColor("_PointLightIntensity", colorLuzPuntual);
            mat7.SetColor("_PointLightIntensity", colorLuzPuntual);
            mat8.SetColor("_PointLightIntensity", colorLuzPuntual);
            mat9.SetColor("_PointLightIntensity", colorLuzPuntual);
            mat10.SetColor("_PointLightIntensity", colorLuzPuntual);
            mat11.SetColor("_PointLightIntensity", colorLuzPuntual);
            mat12.SetColor("_PointLightIntensity", colorLuzPuntual);
            mat13.SetColor("_PointLightIntensity", colorLuzPuntual);
            mat14.SetColor("_PointLightIntensity", colorLuzPuntual);

            piso.SetColor("_PointLightIntensity", colorLuzPuntual);
        }

        if (luzSpotActivada)
        {
            mat0.SetColor("_SpotLightColor", colorLuzSpot);
            mat1.SetColor("_SpotLightColor", colorLuzSpot);
            mat2.SetColor("_SpotLightColor", colorLuzSpot);
            mat4.SetColor("_SpotLightColor", colorLuzSpot);
            mat3.SetColor("_SpotLightColor", colorLuzSpot);
            mat5.SetColor("_SpotLightColor", colorLuzSpot);
            mat6.SetColor("_SpotLightColor", colorLuzSpot);
            mat7.SetColor("_SpotLightColor", colorLuzSpot);
            mat8.SetColor("_SpotLightColor", colorLuzSpot);
            mat9.SetColor("_SpotLightColor", colorLuzSpot);
            mat10.SetColor("_SpotLightColor", colorLuzSpot);
            mat11.SetColor("_SpotLightColor", colorLuzSpot);
            mat12.SetColor("_SpotLightColor", colorLuzSpot);
            mat13.SetColor("_SpotLightColor", colorLuzSpot);
            mat14.SetColor("_SpotLightColor", colorLuzSpot);

            piso.SetColor("_SpotLightColor", colorLuzSpot);
        }

        if (luzDireccionalActivada)
        {
            mat0.SetColor("_DirectionalLightColor", colorLuzDireccional);
            mat1.SetColor("_DirectionalLightColor", colorLuzDireccional);
            mat2.SetColor("_DirectionalLightColor", colorLuzDireccional);
            mat4.SetColor("_DirectionalLightColor", colorLuzDireccional);
            mat3.SetColor("_DirectionalLightColor", colorLuzDireccional);
            mat5.SetColor("_DirectionalLightColor", colorLuzDireccional);
            mat6.SetColor("_DirectionalLightColor", colorLuzDireccional);
            mat7.SetColor("_DirectionalLightColor", colorLuzDireccional);
            mat8.SetColor("_DirectionalLightColor", colorLuzDireccional);
            mat9.SetColor("_DirectionalLightColor", colorLuzDireccional);
            mat10.SetColor("_DirectionalLightColor", colorLuzDireccional);
            mat11.SetColor("_DirectionalLightColor", colorLuzDireccional);
            mat12.SetColor("_DirectionalLightColor", colorLuzDireccional);
            mat13.SetColor("_DirectionalLightColor", colorLuzDireccional);
            mat14.SetColor("_DirectionalLightColor", colorLuzDireccional);

            piso.SetColor("_DirectionalLightColor", colorLuzDireccional);
        }

        // Activar y desactivar luz spot
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (luzSpotActivada)
                DesactivarLuzSpot();
            else
                ActivarLuzSpot();

            luzSpotActivada = !luzSpotActivada;
        }

        // Activar y desactivar luz puntual
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (luzPuntualActivada)
            {
                DesactivarLuzPuntual();
                foco.SetActive(false);
            }
            else
            {
                ActivarLuzPuntual();
                foco.SetActive(true);
            }
                
            luzPuntualActivada = !luzPuntualActivada;
        }

        // Activar y desactivar luz direccional
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (luzDireccionalActivada)
                DesactivarLuzDireccional();
            else
                ActivarLuzDireccional();

            luzDireccionalActivada = !luzDireccionalActivada;
        }
    }

    private void DesactivarLuzSpot()
    {
        mat0.SetColor("_SpotLightColor", luzApagada);
        mat1.SetColor("_SpotLightColor", luzApagada);
        mat2.SetColor("_SpotLightColor", luzApagada);
        mat4.SetColor("_SpotLightColor", luzApagada);
        mat3.SetColor("_SpotLightColor", luzApagada);
        mat5.SetColor("_SpotLightColor", luzApagada);
        mat6.SetColor("_SpotLightColor", luzApagada);
        mat7.SetColor("_SpotLightColor", luzApagada);
        mat8.SetColor("_SpotLightColor", luzApagada);
        mat9.SetColor("_SpotLightColor", luzApagada);
        mat10.SetColor("_SpotLightColor", luzApagada);
        mat11.SetColor("_SpotLightColor", luzApagada);
        mat12.SetColor("_SpotLightColor", luzApagada);
        mat13.SetColor("_SpotLightColor", luzApagada);
        mat14.SetColor("_SpotLightColor", luzApagada);

        piso.SetColor("_SpotLightColor", luzApagada);
    }

    private void DesactivarLuzPuntual()
    {
        mat0.SetColor("_PointLightIntensity", luzApagada);
        mat1.SetColor("_PointLightIntensity", luzApagada);
        mat2.SetColor("_PointLightIntensity", luzApagada);
        mat4.SetColor("_PointLightIntensity", luzApagada);
        mat3.SetColor("_PointLightIntensity", luzApagada);
        mat5.SetColor("_PointLightIntensity", luzApagada);
        mat6.SetColor("_PointLightIntensity", luzApagada);
        mat7.SetColor("_PointLightIntensity", luzApagada);
        mat8.SetColor("_PointLightIntensity", luzApagada);
        mat9.SetColor("_PointLightIntensity", luzApagada);
        mat10.SetColor("_PointLightIntensity", luzApagada);
        mat11.SetColor("_PointLightIntensity", luzApagada);
        mat12.SetColor("_PointLightIntensity", luzApagada);
        mat13.SetColor("_PointLightIntensity", luzApagada);
        mat14.SetColor("_PointLightIntensity", luzApagada);

        piso.SetColor("_PointLightIntensity", luzApagada);

        mat0.SetColor("_PointAmbientLight", luzApagada);
        mat1.SetColor("_PointAmbientLight", luzApagada);
        mat2.SetColor("_PointAmbientLight", luzApagada);
        mat4.SetColor("_PointAmbientLight", luzApagada);
        mat3.SetColor("_PointAmbientLight", luzApagada);
        mat5.SetColor("_PointAmbientLight", luzApagada);
        mat6.SetColor("_PointAmbientLight", luzApagada);
        mat7.SetColor("_PointAmbientLight", luzApagada);
        mat8.SetColor("_PointAmbientLight", luzApagada);
        mat9.SetColor("_PointAmbientLight", luzApagada);
        mat10.SetColor("_PointAmbientLight", luzApagada);
        mat11.SetColor("_PointAmbientLight", luzApagada);
        mat12.SetColor("_PointAmbientLight", luzApagada);
        mat13.SetColor("_PointAmbientLight", luzApagada);
        mat14.SetColor("_PointAmbientLight", luzApagada);

        piso.SetColor("_PointAmbientLight", luzApagada);
    }

    private void DesactivarLuzDireccional()
    {
        mat0.SetColor("_DirectionalLightColor", luzApagada);
        mat1.SetColor("_DirectionalLightColor", luzApagada);
        mat2.SetColor("_DirectionalLightColor", luzApagada);
        mat4.SetColor("_DirectionalLightColor", luzApagada);
        mat3.SetColor("_DirectionalLightColor", luzApagada);
        mat5.SetColor("_DirectionalLightColor", luzApagada);
        mat6.SetColor("_DirectionalLightColor", luzApagada);
        mat7.SetColor("_DirectionalLightColor", luzApagada);
        mat8.SetColor("_DirectionalLightColor", luzApagada);
        mat9.SetColor("_DirectionalLightColor", luzApagada);
        mat10.SetColor("_DirectionalLightColor", luzApagada);
        mat11.SetColor("_DirectionalLightColor", luzApagada);
        mat12.SetColor("_DirectionalLightColor", luzApagada);
        mat13.SetColor("_DirectionalLightColor", luzApagada);
        mat14.SetColor("_DirectionalLightColor", luzApagada);

        piso.SetColor("_DirectionalLightColor", luzApagada);
    }

    private void ActivarLuzSpot()
    {
        mat0.SetColor("_SpotLightColor", colorLuzSpot);
        mat1.SetColor("_SpotLightColor", colorLuzSpot);
        mat2.SetColor("_SpotLightColor", colorLuzSpot);
        mat4.SetColor("_SpotLightColor", colorLuzSpot);
        mat3.SetColor("_SpotLightColor", colorLuzSpot);
        mat5.SetColor("_SpotLightColor", colorLuzSpot);
        mat6.SetColor("_SpotLightColor", colorLuzSpot);
        mat7.SetColor("_SpotLightColor", colorLuzSpot);
        mat8.SetColor("_SpotLightColor", colorLuzSpot);
        mat9.SetColor("_SpotLightColor", colorLuzSpot);
        mat10.SetColor("_SpotLightColor", colorLuzSpot);
        mat11.SetColor("_SpotLightColor", colorLuzSpot);
        mat12.SetColor("_SpotLightColor", colorLuzSpot);
        mat13.SetColor("_SpotLightColor", colorLuzSpot);
        mat14.SetColor("_SpotLightColor", colorLuzSpot);

        piso.SetColor("_SpotLightColor", colorLuzSpot);
    }

    private void ActivarLuzPuntual()
    {
        mat0.SetColor("_PointLightIntensity", colorLuzPuntual);
        mat1.SetColor("_PointLightIntensity", colorLuzPuntual);
        mat2.SetColor("_PointLightIntensity", colorLuzPuntual);
        mat4.SetColor("_PointLightIntensity", colorLuzPuntual);
        mat3.SetColor("_PointLightIntensity", colorLuzPuntual);
        mat5.SetColor("_PointLightIntensity", colorLuzPuntual);
        mat6.SetColor("_PointLightIntensity", colorLuzPuntual);
        mat7.SetColor("_PointLightIntensity", colorLuzPuntual);
        mat8.SetColor("_PointLightIntensity", colorLuzPuntual);
        mat9.SetColor("_PointLightIntensity", colorLuzPuntual);
        mat10.SetColor("_PointLightIntensity", colorLuzPuntual);
        mat11.SetColor("_PointLightIntensity", colorLuzPuntual);
        mat12.SetColor("_PointLightIntensity", colorLuzPuntual);
        mat13.SetColor("_PointLightIntensity", colorLuzPuntual);
        mat14.SetColor("_PointLightIntensity", colorLuzPuntual);

        piso.SetColor("_PointLightIntensity", colorLuzPuntual);
    }

    private void ActivarLuzDireccional()
    {
        mat0.SetColor("_DirectionalLightColor", colorLuzDireccional);
        mat1.SetColor("_DirectionalLightColor", colorLuzDireccional);
        mat2.SetColor("_DirectionalLightColor", colorLuzDireccional);
        mat4.SetColor("_DirectionalLightColor", colorLuzDireccional);
        mat3.SetColor("_DirectionalLightColor", colorLuzDireccional);
        mat5.SetColor("_DirectionalLightColor", colorLuzDireccional);
        mat6.SetColor("_DirectionalLightColor", colorLuzDireccional);
        mat7.SetColor("_DirectionalLightColor", colorLuzDireccional);
        mat8.SetColor("_DirectionalLightColor", colorLuzDireccional);
        mat9.SetColor("_DirectionalLightColor", colorLuzDireccional);
        mat10.SetColor("_DirectionalLightColor", colorLuzDireccional);
        mat11.SetColor("_DirectionalLightColor", colorLuzDireccional);
        mat12.SetColor("_DirectionalLightColor", colorLuzDireccional);
        mat13.SetColor("_DirectionalLightColor", colorLuzDireccional);
        mat14.SetColor("_DirectionalLightColor", colorLuzDireccional);

        piso.SetColor("_DirectionalLightColor", colorLuzDireccional);
    }
}