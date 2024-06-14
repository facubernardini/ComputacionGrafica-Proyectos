using UnityEngine;
using System.Collections.Generic;

public class Relampago : MonoBehaviour {

    public new Light light;

    public GameObject relampago1;
    public GameObject relampago2;

    private float espera = 0;
    private float random = 0;
    private int smoothing = 3;
    private bool flag;
    private bool flagSonido;

    private AudioSource audioSource;

    Queue<float> smoothQueue;
    float lastSum = 0;

    void Start() {

        smoothQueue = new Queue<float>(smoothing);
        // External or internal light?
        if (light == null) {
            light = GetComponent<Light>();
        }

        flag = true;
        flagSonido = true;

        relampago1.SetActive(false);
        relampago2.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    void Update() {

        if (light == null)
        {
            return;
        }

        if (flag)
        {
            random = Random.Range(20f, 40f);
            flag = false;
        }

        espera += 1 * Time.deltaTime;
        
        // Si espera llega a los 8 segundos se producen relampagos
        if ((espera > random) && (espera <= (random + 0.7f)))
        {
            if (flagSonido)
            {
                audioSource.Play();
                flagSonido = false;
            }

            Parpadeo();
        }
        else if (espera > random)
        {
            espera = 0;
            flag = true;
            flagSonido = true;
            ApagarLuz();
        }
    }

    private void Parpadeo()
    {

        while (smoothQueue.Count >= smoothing) {
            lastSum -= smoothQueue.Dequeue();
        }

        float newVal = Random.Range(0f, 2.8f);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        light.intensity = lastSum / (float)smoothQueue.Count;

        relampago1.SetActive(true);
        relampago2.SetActive(true);
    }

    private void ApagarLuz()
    {
        light.intensity = 0;
        
        relampago1.SetActive(false);
        relampago2.SetActive(false);
    }

}