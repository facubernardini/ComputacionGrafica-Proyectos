using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.Timeline;

public class LucesEscenaB : MonoBehaviour
{
    public Material[] materiales;

    public Vector4 luzDireccional;
    public Color colorLuzDireccional;

    public Vector4 posicionLuzSpot;
    public Vector4 direccionLuzSpot;
    public float aperturaLuzSpot;
    public Color colorLuzSpot;

    public Vector4 posicionLuzPuntual;
    public Color colorLuzPuntual;

    private bool luzSpotActivada, luzDireccionalActivada, luzPuntualActivada;
    private Vector4 luzApagada;

    void Start()
    {
        luzSpotActivada = true;
        luzDireccionalActivada = true;
        luzPuntualActivada = true;

        luzApagada = new Vector4(0, 0, 0, 1);

        // DIRECCIONAL
        luzDireccional = new Vector4(10, 15, -8, 1);
        colorLuzDireccional = new Vector4(0.25f, 0.25f, 0.33f, 1);

        // SPOT
        posicionLuzSpot = new Vector4(-1.93f, 1.97f, 16.12f, 1);
        colorLuzSpot = new Vector4(1, 0.88f, 0.3f, 1);
        direccionLuzSpot = new Vector4(0, -20, 0, 1);
        aperturaLuzSpot = 30;

        // PUNTUAL
        posicionLuzPuntual = new Vector4(1.7f, 0.2f, 18, 0);
        colorLuzPuntual = new Vector4(1, 0.45f, 0, 1);

        foreach (Material mat in materiales)
        {

            //DIRECCIONAL
            mat.SetColor("_DirectionalLightDirection", -luzDireccional);
            mat.SetColor("_DirectionalLightColor", colorLuzDireccional);

            //SPOT
            mat.SetColor("_SpotLightPosition_w", posicionLuzSpot);
            mat.SetColor("_SpotLightColor", colorLuzSpot);
            mat.SetColor("_SpotLightDirection", direccionLuzSpot);
            mat.SetFloat("_Apertura", aperturaLuzSpot);

            //PUNTUAL
            mat.SetColor("_PointLightPosition_w", posicionLuzPuntual);
            mat.SetColor("_PointLightIntensity", colorLuzPuntual);
        }   
    }

    void Update()
    {
        foreach(Material mat in materiales)
        {

            if (luzPuntualActivada)
            {
                mat.SetColor("_PointLightPosition_w", posicionLuzPuntual);
                mat.SetColor("_PointLightIntensity", colorLuzPuntual);
            }

            if (luzSpotActivada)
            {
                mat.SetColor("_SpotLightPosition_w", posicionLuzSpot);
                mat.SetColor("_SpotLightColor", colorLuzSpot);
                mat.SetColor("_SpotLightDirection", direccionLuzSpot);
                mat.SetFloat("_Apertura", aperturaLuzSpot);
            }

            if (luzDireccionalActivada)
            {
                mat.SetColor("_DirectionalLightDirection", -luzDireccional);
                mat.SetColor("_DirectionalLightColor", colorLuzDireccional);
            }
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
                DesactivarLuzPuntual();
            else
                ActivarLuzPuntual();

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
        foreach (Material mat in materiales)
        {
            mat.SetColor("_SpotLightColor", luzApagada);
        }
    }

    private void DesactivarLuzPuntual()
    {
        foreach (Material mat in materiales)
        {
            mat.SetColor("_PointLightIntensity", luzApagada);
        }
    }

    private void DesactivarLuzDireccional()
    {
        foreach (Material mat in materiales)
        {
            mat.SetColor("_DirectionalLightColor", luzApagada);
        }
    }

    private void ActivarLuzSpot()
    {
        foreach (Material mat in materiales)
        {
            mat.SetColor("_SpotLightColor", colorLuzSpot);
        }
    }

    private void ActivarLuzPuntual()
    {
        foreach (Material mat in materiales)
        {
            mat.SetColor("_PointLightIntensity", colorLuzPuntual);
        }
    }

    private void ActivarLuzDireccional()
    {
        foreach (Material mat in materiales)
        {
            mat.SetColor("_DirectionalLightColor", colorLuzDireccional);
        }
    }
}
