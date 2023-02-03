using System.Collections;
using UnityEngine;
using Unity.Netcode;

namespace kaputt.core.objectPooling{
public class NetworkPoolController : NetworkBehaviour {
	ObjectPool localObjectPool;
	
	public static NetworkPoolController Instance;

	void Awake(){
		Instance = this;
	}
	void Start(){
		localObjectPool = gameObject.GetComponent<ObjectPool>();
	}

	[ClientRpc]
	public void OrderObjectClientRpc(string tag, Vector3 position, Quaternion rotation){
		GameObject bullethole = localObjectPool.SpawnFromPool(tag, position, rotation);
	}
}
}