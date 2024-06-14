using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoAnomalias2 : MonoBehaviour
{

    public GameObject camara;
    public GameObject anomalia;

    private AudioSource audioSource;
    private bool flag;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        flag = true;
    }

    void Update()
    {
        if (camara.activeSelf && anomalia.activeSelf && flag)
        {
            audioSource.Play();
            flag = false;
        }
        if ((!camara.activeSelf || !anomalia.activeSelf)) 
        {
            audioSource.Stop();
            flag = true;
        }
    }
}