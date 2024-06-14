using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Anomalia : MonoBehaviour
{
    //[StringInList("Imagen", "Desplazamiento")]
   // public string anomayType;

    public GameObject objectToActivate;
    protected bool activated = false;


    public virtual void Activate()
    {
        objectToActivate.SetActive(true);
        activated = true;
    }

    public virtual void Deactivate()
    {
        objectToActivate.SetActive(false);
        activated = false;
    }

    public bool IsActivated()
    {
        return activated;
    }

    public abstract bool CheckAnomalyType(string type);
}
