using UnityEngine;

namespace kaputt.core{
public interface IDamageable {
    void registerHit(int amount, GameObject origin);
}
}