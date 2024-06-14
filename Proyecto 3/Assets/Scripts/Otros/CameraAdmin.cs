using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdmin : MonoBehaviour
{
    public GameObject[] cameras;
    public GameObject[] espejos;
    public GameObject[] pantallasAnomalia;
    public GameObject maincamCamioneta, maincamHabitacion, anomaliaElectrica;

    void Update()
    {
        if (!maincamCamioneta.activeSelf)
        {
            foreach (var cam in cameras)
            {
                cam.SetActive(false);
            }
        }

        if (maincamHabitacion.activeSelf)
        {
            foreach (var cam in espejos)
            {
                cam.SetActive(true);
            }
        } else
        {
            foreach (var cam in espejos)
            {
                cam.SetActive(false);
            }
        }

        if (anomaliaElectrica.activeSelf && maincamCamioneta.activeSelf)
        {
            foreach (var pant in pantallasAnomalia)
            {
                pant.SetActive(true);
            }
            foreach (var cam in cameras)
            {
                cam.SetActive(false);
            }
        }

        if (!anomaliaElectrica.activeSelf && maincamCamioneta.activeSelf)
        {
            foreach (var pant in pantallasAnomalia)
            {
                pant.SetActive(false);
            }
            foreach (var cam in cameras)
            {
                cam.SetActive(true);
            }
        }
    }
}