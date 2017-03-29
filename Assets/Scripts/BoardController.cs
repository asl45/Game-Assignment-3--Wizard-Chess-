using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BoardController : NetworkBehaviour {

	private float x, y, z;
	private const int OFFSET = 8;
	private List<GameObject> boardSquares;
	public GameObject boardSquareWhite;
	public GameObject boardSquareBlack;
	public GameObject pawn;
    public GameObject pawn2;
    public GameObject bKing; //mewtwo
	public GameObject wKing; //mew
	public GameObject queen;
    public GameObject queen2;
    public GameObject rook;
    public GameObject rook2;
    public GameObject bishop;
    public GameObject bishop2;
    public GameObject knight;
    public GameObject knight2;
    public NetworkManager network;
    public Material black;
    public Camera maincamera;
    public GameObject Player;
    private NetworkIdentity objNetId;
    // Use this for initialization
    void Start()
    {
		//NetworkServer.Reset();
		//Debug.Log("I'm the client");
        //network.StopHost();
        //network.StartHost();
        //network.StartClient();
		//network.StartServer();
        //Debug.Log(this);
       // Debug.Log(network);
       // Debug.Log(network.client.connection);
       // objNetId.AssignClientAuthority(connectionToClient);
		Time.timeScale = 1.0f;
		if (isServer) {    

			boardSquares = new List<GameObject> ();
			x = 0f;
			y = 1f;
			z = 0f;
			int row = 1;
			int col = 1;
			int swichStartingColor = 0;
			for (int i = 1; i <= 8; i++) {
				col = 1;
				for (int j = 1; j <= 8; j++) {
					x = (float)(row * 2 - OFFSET);
					z = (float)(col * 2);

					GameObject boardSquareObj;
					if (col % 2 == swichStartingColor) {
						boardSquareObj = (GameObject)Instantiate (boardSquareWhite, new Vector3 (x, y, z), transform.rotation);
						NetworkServer.Spawn (boardSquareObj);
					} else {
						boardSquareObj = (GameObject)Instantiate (boardSquareBlack, new Vector3 (x, y, z), transform.rotation);
						NetworkServer.Spawn (boardSquareObj);
					}

					//piece generation below:
                
					//Debug.Log("I'm the server");
					GameObject pieceObj;
					if (col == 1) {
						switch (row) {
						case 1:
							pieceObj = (GameObject)Instantiate (rook, new Vector3 (x, y, z), transform.rotation);
							NetworkServer.Spawn (pieceObj);

							pieceObj.GetComponent<PieceController> ().currNum = col;
							pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
							break;
						case 2:
							pieceObj = (GameObject)Instantiate (knight, new Vector3 (x, y, z), transform.rotation);
							NetworkServer.Spawn (pieceObj);
							pieceObj.GetComponent<PieceController> ().currNum = col;
							pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
							break;
						case 3:
							pieceObj = (GameObject)Instantiate (bishop, new Vector3 (x, y, z), transform.rotation);
							NetworkServer.Spawn (pieceObj);
							pieceObj.GetComponent<PieceController> ().currNum = col;
							pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
							break;
						case 4:
							pieceObj = (GameObject)Instantiate (wKing, new Vector3 (x, y, z), transform.rotation);
							NetworkServer.Spawn (pieceObj);
							pieceObj.GetComponent<PieceController> ().currNum = col;
							pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
							break;
						case 5:
							pieceObj = (GameObject)Instantiate (queen, new Vector3 (x, y, z), transform.rotation);
							NetworkServer.Spawn (pieceObj);
							pieceObj.GetComponent<PieceController> ().currNum = col;
							pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
							break;
						case 6:
							pieceObj = (GameObject)Instantiate (bishop, new Vector3 (x, y, z), transform.rotation);
							NetworkServer.Spawn (pieceObj);
							pieceObj.GetComponent<PieceController> ().currNum = col;
							pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
							break;
						case 7:
							pieceObj = (GameObject)Instantiate (knight, new Vector3 (x, y, z), transform.rotation);
							NetworkServer.Spawn (pieceObj);
							pieceObj.GetComponent<PieceController> ().currNum = col;
							pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
							break;
						case 8:
							pieceObj = (GameObject)Instantiate (rook, new Vector3 (x, y, z), transform.rotation);
							NetworkServer.Spawn (pieceObj);
							pieceObj.GetComponent<PieceController> ().currNum = col;
							pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
							break;
						}
					}
					if (col == 2) {
						pieceObj = (GameObject)Instantiate (pawn, new Vector3 (x, y, z), transform.rotation);
						NetworkServer.Spawn (pieceObj);
						pieceObj.GetComponent<PieceController> ().currNum = col;
						pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
					}
					if (col == 7) {
						pieceObj = (GameObject)Instantiate (pawn2, new Vector3 (x, y, z), transform.rotation);

						pieceObj.transform.Rotate (0, 180, 0);
						pieceObj.transform.GetChild (0).GetComponent<Renderer> ().material = black;
						NetworkServer.Spawn (pieceObj);
						pieceObj.GetComponent<PieceController> ().currNum = col;
						pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
					}
					if (col == 8) {
						switch (row) {
						case 1:
							pieceObj = (GameObject)Instantiate (rook2, new Vector3 (x, y, z), transform.rotation);

							pieceObj.transform.Rotate (0, 180, 0);
							pieceObj.transform.GetChild (0).GetComponent<Renderer> ().material = black;
							pieceObj.GetComponent<PieceController> ().currNum = col;
							pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
							NetworkServer.Spawn (pieceObj);
							break;
						case 2:
							pieceObj = (GameObject)Instantiate (knight2, new Vector3 (x, y, z), transform.rotation);

							pieceObj.transform.Rotate (0, 180, 0);
							pieceObj.transform.GetChild (0).GetComponent<Renderer> ().material = black;
							pieceObj.GetComponent<PieceController> ().currNum = col;
							pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
							NetworkServer.Spawn (pieceObj);
							break;
						case 3:
							pieceObj = (GameObject)Instantiate (bishop2, new Vector3 (x, y, z), transform.rotation);

							pieceObj.transform.Rotate (0, 180, 0);
							pieceObj.transform.GetChild (0).GetComponent<Renderer> ().material = black;
							pieceObj.GetComponent<PieceController> ().currNum = col;
							pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
							NetworkServer.Spawn (pieceObj);
							break;
						case 4:
							pieceObj = (GameObject)Instantiate (bKing, new Vector3 (x, y, z), transform.rotation);

							pieceObj.transform.Rotate (0, 180, 0);
							pieceObj.transform.GetChild (0).GetComponent<Renderer> ().material = black;
							pieceObj.GetComponent<PieceController> ().currNum = col;
							pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
							NetworkServer.Spawn (pieceObj);
							break;
						case 5:
							pieceObj = (GameObject)Instantiate (queen2, new Vector3 (x, y, z), transform.rotation);

							pieceObj.transform.Rotate (0, 180, 0);
							pieceObj.transform.GetChild (0).GetComponent<Renderer> ().material = black;
							pieceObj.GetComponent<PieceController> ().currNum = col;
							pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
							NetworkServer.Spawn (pieceObj);
							break;
						case 6:
							pieceObj = (GameObject)Instantiate (bishop2, new Vector3 (x, y, z), transform.rotation);

							pieceObj.transform.Rotate (0, 180, 0);
							pieceObj.transform.GetChild (0).GetComponent<Renderer> ().material = black;
							pieceObj.GetComponent<PieceController> ().currNum = col;
							pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
							NetworkServer.Spawn (pieceObj);
							break;
						case 7:
							pieceObj = (GameObject)Instantiate (knight2, new Vector3 (x, y, z), transform.rotation);

							pieceObj.transform.Rotate (0, 180, 0);
							pieceObj.transform.GetChild (0).GetComponent<Renderer> ().material = black;
							pieceObj.GetComponent<PieceController> ().currNum = col;
							pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
							NetworkServer.Spawn (pieceObj);
							break;
						case 8:
							pieceObj = (GameObject)Instantiate (rook2, new Vector3 (x, y, z), transform.rotation);

							pieceObj.transform.Rotate (0, 180, 0);
							pieceObj.transform.GetChild (0).GetComponent<Renderer> ().material = black;
							pieceObj.GetComponent<PieceController> ().currNum = col;
							pieceObj.GetComponent<PieceController> ().currLetter = (char)(row + 96);
							NetworkServer.Spawn (pieceObj);
                               
							break;
						}
					}
                
					//end piece generation section

					//boardSquares.Add(boardSquareObj);

					BoardSquare boardSquareScript = boardSquareObj.GetComponent<BoardSquare> ();
					//the following two lines were adjusted from what they were originally given in class
					boardSquareScript.setCellNumber (col);
					boardSquareScript.setCellLetter ((char)(row + 96));
					col = col + 1;
				}
				if (swichStartingColor == 0) {
					swichStartingColor = 1;
				} else {
					swichStartingColor = 0;
				}
				row = row + 1;
				// Debug.Log (row);
			}
		}
            if (!isServer)
            {
                Debug.Log("I'm the client");
            }
        
    

	}
	// Update is called once per frame
	void Update () {
        
    }
		
}
