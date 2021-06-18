using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar(hook = nameof(HandleDisplayNameText))]
    [SerializeField]
    private string displayName = "Missing Name";
    [SyncVar(hook = nameof(HandleDisplayColorUpdated))]
    [SerializeField]
    private Color playerColor = Color.white;

    [SerializeField]
    private TMP_Text displayNameText = null;
    [SerializeField]
    private Renderer displayColorRenderer = null;

    #region Server

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }

    [Server]
    public void SetColor(Color color)
    {
        playerColor = color;
    }

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        if (newDisplayName.Length < 2 || newDisplayName.Length > 20)
        {
            return;
        }

        RpcLogNewName(newDisplayName);
        SetDisplayName(newDisplayName);
    }

    #endregion

    #region Client

    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        if (displayColorRenderer)
        {
            displayColorRenderer.material.SetColor("_BaseColor", newColor);
        }
    }

    private void HandleDisplayNameText(string oldName, string newName)
    {
        if (displayNameText)
        {
            displayNameText.text = newName;
        }
    }

    [ContextMenu("Set My Name")]
    private void SetMyName()
    {
        CmdSetDisplayName("My New Name");
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }

    #endregion
}
