using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [field: SerializeField] public int MaxHealth { get; private set; } = 100; // editable en el editor

    public NetworkVariable<int> CurrentHealth = new NetworkVariable<int>();

    public Action<Health> OnDie;

    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            return;
        }
        CurrentHealth.Value = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        ModifyHealth(-damage);
    }

    public void RestoreHealth(int health)
    {
        ModifyHealth(health);
    }

    private void ModifyHealth(int value)    
    {
        //Debug.Log("ModifyHealth " + value + " CurrentHealth.Value " + CurrentHealth.Value + " CurrentHealth.Value " + CurrentHealth.Value);
        if (CurrentHealth.Value == 0)
        {
            return;
        }

        int newHealth = CurrentHealth.Value + value;
        CurrentHealth.Value = Mathf.Clamp(newHealth, 0, MaxHealth);

        if (CurrentHealth.Value == 0)
        {
            OnDie?.Invoke(this);
        }

    }
}
