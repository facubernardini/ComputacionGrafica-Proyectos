using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomaliaDesplazamiento : Anomalia
{
    public GameObject objectToRemove;

    public override void Activate()
    {
        objectToRemove.SetActive(false);
        objectToActivate.SetActive(true);
        activated = true;
    }

    public override bool CheckAnomalyType(string type)
    {
        return type.Equals("Desplazamiento");
    }

    public override void Deactivate()
    {
        objectToRemove.SetActive(true);
        objectToActivate.SetActive(false);
        activated = false;
    }
}
