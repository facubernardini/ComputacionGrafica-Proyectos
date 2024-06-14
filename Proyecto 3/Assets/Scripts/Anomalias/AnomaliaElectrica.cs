using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomaliaElectrica : Anomalia
{
    public override bool CheckAnomalyType(string type)
    {
        return type.Equals("Electrica");
    }
}
