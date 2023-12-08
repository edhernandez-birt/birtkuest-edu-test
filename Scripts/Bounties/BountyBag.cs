using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BountyBag : NetworkBehaviour
{
    public NetworkVariable<int> TotalPoints = new NetworkVariable<int>();

    public void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag != "Bounty") { return; }

        Bounty bounty = other.gameObject.GetComponent<Bounty>();

        int points = bounty.Collect();

        Destroy(other.gameObject);

        if (!IsServer) { return; }

        TotalPoints.Value += points;
    }

}
