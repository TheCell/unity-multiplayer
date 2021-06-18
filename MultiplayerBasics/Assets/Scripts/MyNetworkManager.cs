using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        var player = conn.identity.GetComponent<MyNetworkPlayer>();
        player.SetDisplayName($"Palyer {numPlayers}");
        player.SetColor(new Color(Random.value, Random.value, Random.value));
    }
}
