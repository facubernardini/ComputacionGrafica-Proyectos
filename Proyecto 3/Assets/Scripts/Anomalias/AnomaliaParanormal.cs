using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomaliaParanormal : Anomalia
{
    public override bool CheckAnomalyType(string type)
    {
        return type.Equals("Paranormal");
    }
}
