using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoTelevision : MonoBehaviour
{

    public Material material;

    private float valor;

    void Update()
    {
        valor = Random.Range(50f, 150f);
        material.SetFloat("_Density", valor);
    }
}
