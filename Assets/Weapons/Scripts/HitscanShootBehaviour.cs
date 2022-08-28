using UnityEngine;
using Unity.Netcode;
using kaputt.Character.Input;
using kaputt.core;

namespace kaputt.Weapons {
public class HitscanShootBehaviour : NetworkBehaviour, IShootBehaviour {
	[SerializeField]int damage;
	[SerializeField]float hitForce;
	[SerializeField][Tooltip("Specifies the shots per second that can be fired")]int fireRate;
	[SerializeField]FireMode fireMode;

	[SerializeField]Camera mainCamera;
	public enum FireMode {semi,full,single};

	MainInput input;
	bool shotPressed = false;
	float nextShotTime;

	public bool canShoot{get;set;} = true;

	void Awake(){
		input = new MainInput();
		input.Foot.PrimaryMouse.performed += ctx => shootPerformedServerRpc();
		input.Foot.PrimaryMouse.canceled += ctx => shootCancelledServerRpc();

		if(!IsServer) return;
		nextShotTime = Time.time;
	}

	void Update(){
		if(shotPressed){
			handleShot();
		}
	}

	public override void OnNetworkSpawn(){
		enabled = true;
	}

	[ServerRpc]
	void shootPerformedServerRpc(){
		if(fireMode==FireMode.full){
			shotPressed = true;
			return;
		}
		handleShot();
	}

	[ServerRpc]
	void shootCancelledServerRpc(){
		shotPressed = false;
	}

	private void handleShot(){
		if(canShoot && nextShotTime <= Time.time){
			performRaycast();
			calculateNextShotTime();
		}
	}

	void performRaycast(){
		Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
		RaycastHit hit;
		if(Physics.Raycast(rayOrigin, mainCamera.transform.forward, out hit)){
			IDamageable health = hit.transform.GetComponent<IDamageable>();
			if (health != null){
				health.registerHit(damage, mainCamera.transform.parent.gameObject);
			}
			if (hit.rigidbody != null){
				hit.rigidbody.AddForce(-hit.normal * hitForce);
			}
		}
	}

	void calculateNextShotTime(){
		nextShotTime = Time.time + 1/fireRate;
	}

	void OnEnable(){
		if(IsOwner)
			input.Foot.Enable();
	}

	void OnDisable(){
		if(IsOwner)
			input.Foot.Disable();
	}


}
}