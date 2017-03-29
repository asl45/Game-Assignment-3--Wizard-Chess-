using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    //public NetworkManager network;
    void Start() {
        
    }
    void Update()
    {
        if (!isLocalPlayer)
        {

           // Debug.Log("i am not a local player :(");
            return;
        }
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        
        CmdServerAssignClient(x,z);

       // Debug.Log("i am a local player ");
       //CmdServerAssignClient();
       //Debug.Log(network);

    }

    [Command]
    void CmdServerAssignClient(float x, float z)
    {
        GameObject CarServer = GameObject.Find("pawn 1(Clone)");
        // CarServer.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
        CarServer.transform.Rotate(0, x, 0);
        //Debug.Log("i am a local");
        transform.Translate(0, 0, z);
        CarServer.transform.Translate(0, 0, z);
        // GameObject CarServer = GameObject.Find("pawn 1(Clone)");
        // Debug.Log("CarServer = " + CarServer);
        // CarServer.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);

    }

    public override void OnStartLocalPlayer()
    {
        // GetComponent<MeshRenderer>().material.color = Color.blue;
        gameObject.name = "LOCAL player";

       // Debug.Log("IDs: " + this.GetComponent<NetworkIdentity>().connectionToServer.connectionId.ToString());
       // GameObject CarServer = GameObject.Find("pawn 1(Clone)");
       // CarServer.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
    }

	[Command]
	public void CmdSetAuth(NetworkInstanceId instanceId, NetworkIdentity piece){
		Debug.Log ("Wrapper called.");
		var iObject = NetworkServer.FindLocalObject(instanceId);
		var networkIdentity = iObject.GetComponent<NetworkIdentity> ();
		networkIdentity.AssignClientAuthority (piece.connectionToClient);
	}

	[Command]
	public void CmdRemAuth(NetworkInstanceId instanceId, NetworkIdentity piece)
	{
		var iObject = NetworkServer.FindLocalObject(instanceId);
		var networkIdentity = iObject.GetComponent<NetworkIdentity>();
		networkIdentity.RemoveClientAuthority(piece.connectionToClient);        
	}
	[Command]
	public void CmdWrapper(GameObject piece, GameObject square){
		piece.GetComponent<PieceController> ().servermoveToSquare (square);

	}
}