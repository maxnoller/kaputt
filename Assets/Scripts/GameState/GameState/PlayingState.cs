using System;
using System.Collections;
using UnityEngine;

namespace NOBRAIN.CrackStrike.GameState
{
	class PlayingState : GameState
	{
		public PlayingState()
		{
			Debug.Log("PlayingState");
		}

		public override void Enter()
		{
			//Spawn Player Prefabs
			Debug.Log("PlayingState.Enter");
		}

		public override void Exit()
		{
			//Switch Scene, Destroy Player Prefabs
			Debug.Log("PlayingState.Exit");
		}
	}
}