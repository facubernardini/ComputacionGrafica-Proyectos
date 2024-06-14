using UnityEngine;
using System.Collections.Generic;

public class RespiracionCaballo : MonoBehaviour {

    public ParticleSystem particulas1;
    public ParticleSystem particulas2;

    private float espera = 0;

    void Update() 
    {
        espera += 1 * Time.deltaTime;

        if (espera > 5)
        {
            ActivoRespiracion();
        }
        if (espera > 7)
        {
            espera = 0;
        }
    }

    private void ActivoRespiracion()
    {
        particulas1.Emit(1);
        particulas2.Emit(1);
    }

}