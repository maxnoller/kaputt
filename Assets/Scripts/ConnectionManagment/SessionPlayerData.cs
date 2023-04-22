
using NOBRAIN.KAPUTT.Infrastructure;
using NOBRAIN.KAPUTT.Utils;
using UnityEngine;
using Steamworks;

namespace NOBRAIN.KAPUTT.ConnectionManagement
{
    public struct SessionPlayerData : ISessionPlayerData
    {
        public SteamId SteamID;
        public string DisplayName;
        public int PlayerNumber;
        public Vector3 PlayerPosition;
        public Quaternion PlayerRotation;
        /// Instead of using a NetworkGuid (two ulongs) we could just use an int or even a byte-sized index into an array of possible avatars defined in our game data source
        public int CurrentHitPoints;
        public bool HasCharacterSpawned;

        public SessionPlayerData(ulong clientID, SteamId steamId, string displayName, int currentHitPoints = 0, bool isConnected = false, bool hasCharacterSpawned = false)
        {
            ClientID = clientID;
            SteamID = steamId;
            DisplayName = displayName;
            PlayerNumber = -1;
            PlayerPosition = Vector3.zero;
            PlayerRotation = Quaternion.identity;
            CurrentHitPoints = currentHitPoints;
            IsConnected = isConnected;
            HasCharacterSpawned = hasCharacterSpawned;
        }

        public bool IsConnected { get; set; }
        public ulong ClientID { get; set; }

        public void Reinitialize()
        {
            HasCharacterSpawned = false;
        }
    }
}
