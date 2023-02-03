using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOBRAIN.CrackStrike.GameState{
public enum GameStatus{
    WaitingForPlayers,
    Playing,
    GameOver
}

public class NewBehaviourScript : MonoBehaviour
{
    internal readonly LobbyState _LobbyState = new LobbyState();
    internal readonly PlayingState _PlayingState = new PlayingState();
}
}