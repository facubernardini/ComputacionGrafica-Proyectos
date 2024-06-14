using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public string roomName;
    public GameObject camara;
    private Anomalia[] anomalies;

    private void Start()
    {
        anomalies = transform.GetComponents<Anomalia>();
        foreach(Anomalia a in anomalies)
        {
            a.Deactivate();
        }
    }

    public GameObject GetCamera()
    {
        return camara;
    }

    public bool ActivateAnomaly()
    {
        if (GetActiveAnomaliesNumber() == anomalies.Length)
        {   //ya est�n todas activas
            return false;
        }
        else
        {
            FindNonActiveAnomaly().Activate();
        }
        //Debug.Log("Anomaly Activated in Room " + gameObject.transform.name);
        return true;
    }

    private Anomalia FindNonActiveAnomaly()
    {
        Anomalia nueva;
        int r = Random.Range(0, anomalies.Length);
        if (anomalies[r].IsActivated())
        {
            nueva = FindNonActiveAnomaly();
        }
        else
        {
            nueva = anomalies[r];
        }
        return nueva;
    }

    public int GetActiveAnomaliesNumber()
    {
        int n = 0;
        foreach(Anomalia a in anomalies)
        {
            if (a.IsActivated()) n++;
        }
        return n;
    }

    public bool CheckForAnomaly(string anomalyType)
    {
        bool anomalyPresent = false;
        for(int i =0; i<anomalies.Length && anomalyPresent==false; i++)
        {
            if (anomalies[i].IsActivated() && anomalies[i].CheckAnomalyType(anomalyType))
            {
                anomalyPresent = true;
                anomalies[i].Deactivate();
            }
            
        }
        return anomalyPresent;
    }

}