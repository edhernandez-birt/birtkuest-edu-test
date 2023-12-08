using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Bounty // que hereda de NetworkBehaviour
{
    //public event Action<Treasure> OnCollected;

    public override int Collect()
    {
        // si no es el server, ocultarlo y devolver 0
        if (!IsServer)
        {
            Show(false); return 0;
        }   
        // si ya se ha recogido devolver 0
        if (alreadyCollected) { return 0; }
        // sino indicar que se ha recogido
        alreadyCollected = true;

        //OnCollected?.Invoke(this);

        // devolver el valor
        return points;
    }

}
