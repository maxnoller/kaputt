using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using kaputt.core;

namespace kaputt.Character{
public class PlayerHealth : NetworkBehaviour, IDamageable
{
    private const int maxHealth = 100;
    public NetworkVariable<int> health = new NetworkVariable<int>();

    public delegate void OnPlayerDiedEventHandler();
    public event OnPlayerDiedEventHandler OnPlayerDied = delegate {};

	public override void OnNetworkSpawn()
	{
		health.Value = maxHealth;
	}
	
    public void registerHit(int amount, GameObject origin){
        takeDamage(amount);
    }

    public void heal(int amount){
        if(!IsServer) return;
        this.health.Value += amount;
    }

    public void resetHealth(){
        if(!IsServer) return;
        this.health.Value = maxHealth;
    }

    void takeDamage(int damage){
        if(!IsServer) return;
        this.health.Value -= damage;
        if(health.Value <= 0)
            OnPlayerDied();
    }

}
}
