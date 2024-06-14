using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarAnomalias : MonoBehaviour
{
    // Referencia al objeto que contiene el script
    public GameObject objetoDeDestino; 

    // Referencia al script que quieres habilitar o desactivar
    private AnomaliaDesplazamiento anomaliaDesplazamiento;
    private AnomaliaDuplicado anomaliaDuplicado;
    private AnomaliaParanormal anomaliaParanormal;
    private AnomaliaImagen anomaliaImagen;
    private AnomaliaIntruso anomaliaIntruso;
    private AnomaliaElectrica anomaliaElectrica;

    private int i;

    private void Start()
    {
        // Obten la referencia al script
        anomaliaDesplazamiento = objetoDeDestino.GetComponent<AnomaliaDesplazamiento>();
        anomaliaDuplicado = objetoDeDestino.GetComponent<AnomaliaDuplicado>();
        anomaliaParanormal = objetoDeDestino.GetComponent<AnomaliaParanormal>();
        anomaliaImagen = objetoDeDestino.GetComponent<AnomaliaImagen>();
        anomaliaIntruso = objetoDeDestino.GetComponent<AnomaliaIntruso>();
        anomaliaElectrica = objetoDeDestino.GetComponent<AnomaliaElectrica>();

        i = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            i++;

            switch(i)
            {
                case 1:
                    anomaliaDesplazamiento.Activate();
                    break;
                case 2:
                    anomaliaDuplicado.Activate();
                    break;
                case 3:
                    anomaliaImagen.Activate();
                    break;
                case 4:
                    anomaliaIntruso.Activate();
                    break;
                case 5:
                    anomaliaElectrica.Activate();
                    break;
                case 6:
                    anomaliaParanormal.Activate();
                    break;
                case 7:
                {
                    i = 0;

                    anomaliaDesplazamiento.Deactivate();
                    anomaliaDuplicado.Deactivate();
                    anomaliaImagen.Deactivate();
                    anomaliaIntruso.Deactivate();
                    anomaliaElectrica.Deactivate();
                    anomaliaParanormal.Deactivate();

                    break; 
                }
            }
        }
    }
}
