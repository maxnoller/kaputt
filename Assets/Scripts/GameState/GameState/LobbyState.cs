using UnityEngine;
using System;
using System.Collections;

namespace NOBRAIN.CrackStrike.GameState{

class LobbyState : GameState
{
	public LobbyState()
	{
		Debug.Log("LobbyState");
	}

	public override void Enter()
	{
		//Show Lobbdy UI
		Debug.Log("LobbyState.Enter");
	}

	public override void Exit()
	{
		//Hide Lobby UI
		Debug.Log("LobbyState.Exit");
	}
}

}
