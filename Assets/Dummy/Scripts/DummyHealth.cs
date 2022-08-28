using UnityEngine;
using Unity.Netcode;
using kaputt.core;

namespace kaputt.Dummy
{
public class DummyHealth : NetworkBehaviour, IDamageable {

    public const int maxHealth = 100;
    public NetworkVariable<int> health = new NetworkVariable<int>();

    void Start(){
        if(!IsServer) return;
        health.Value = maxHealth;
    }

    public void registerHit(int amount, GameObject origin){
        takeDamage(amount);
    }

    void takeDamage(int damage){
        if(!IsServer) return;
        this.health.Value -= damage;
        if(health.Value <= 0)
            resetHealth();
    }

    void resetHealth(){
        this.health.Value = maxHealth;
    }

}
}