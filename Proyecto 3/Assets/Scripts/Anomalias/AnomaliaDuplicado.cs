using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomaliaDuplicado : Anomalia
{
    public override bool CheckAnomalyType(string type)
    {
        return type.Equals("Duplicado");
    }
}
