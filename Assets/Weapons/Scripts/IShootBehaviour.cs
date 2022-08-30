using System;
using Unity.Netcode;

namespace kaputt.Weapons{
public interface IShootBehaviour {
    NetworkVariable<bool> canShoot{get;set;}
    public event Action OnShotFired;
}
}