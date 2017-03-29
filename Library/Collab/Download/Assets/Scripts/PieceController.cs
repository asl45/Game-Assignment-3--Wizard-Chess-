using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using UnityEditor;

public class PieceController : NetworkBehaviour
{

	public string name;
    public NetworkManager network;
    public Material selectionMat;
    public static int camera1;
	bool selectingSquare;
	GameObject square;
	public LayerMask mask;
	GameObject moveLog;
    //public NetworkManager network;
    //these variables deal with the square the piece is on
    public int currNum;
	public char currLetter;
	public int nextNum;
	public char nextLetter;
    private NetworkIdentity objNetId;
    private NetworkIdentity objNetId2;
    public GameObject player;
    //public Camera maincamera;
    // Use this for initialization
    void Start () {
		selectingSquare = false;
		moveLog = GameObject.Find ("MoveContainer");
        //maincamera = (Camera)Instantiate(maincamera, new Vector3(2.25f, 8.83f, 0f), Quaternion.Euler(51f, 0f, 0f));
        
    }
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown (0) && selectingSquare == true) {
            //network.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
            //Debug.Log(this);
            
            CmdmoveToSquare();
            //objNetId.RemoveClientAuthority(connectionToClient);
        }

        //var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        //var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        
        //transform.Rotate(0, x, 0);
        //transform.Translate(0, 0, z);
    }

	//A move is started when the user clicks on a piece. There are currently no other restrictions on this.
	void OnMouseDown(){
		if (selectingSquare == false) {
			//if (EditorUtility.DisplayDialog ("Move " + name + "?", "Would you like to move this piece?", "Yes", "No")) {
				//selectingSquare = true;
				StartCoroutine ("move");
			}
		}
	

	//move() had to be broken into a coroutine and a separate function because drawing raycasts can only be done during the Update function.
	//the delay can be changed to whatever, but there has to be some delay, otherwise unity will just grab the same click that was used to click on the piece.
	IEnumerator move(){
		//EditorUtility.DisplayDialog ("Instructions", "Click on the square you want to move this " + name + " to.", "Ok");
		yield return new WaitForSeconds (0.3f);
		selectingSquare = true;
    
	}

    [Command]
    //If you're going to add a trigger to declare 'move is over', then that goes at the end of moveToSquare().
    void CmdmoveToSquare(){
        
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100, mask)) {
            
            Debug.DrawLine(ray.origin, hit.point, Color.red, 30f);
			if (selectingSquare == true) {
                
                square = hit.transform.gameObject;
				//I wanted to add a confirmation here but actually finishing the assignment takes priority
				//square.GetComponent<Renderer> ().material = selectionMat;

				nextNum = square.GetComponent<BoardSquare>().cellNumber;
				nextLetter = square.GetComponent<BoardSquare> ().cellLetter;

				moveLog.GetComponent<MoveLog>().addMove (currNum + currLetter.ToString() + "->" + nextNum + nextLetter.ToString());

                //objNetId = square.GetComponent<NetworkIdentity>();        // get the object's network ID
                //objNetId2 = this.GetComponent<NetworkIdentity>();
                //Debug.Log(objNetId);
                //Debug.Log(connectionToClient);
                //Debug.Log(this.connectionToClient);
               // Debug.Log(objNetId2);
              
                //objNetId2.AssignClientAuthority(connectionToClient);

                this.transform.position = square.transform.position;
               // Debug.Log("this is: "+this);

				currNum = nextNum;
				currLetter = nextLetter;

				selectingSquare = false;
               

            }

		}
        
        if (camera1 == 0)
        {
            Camera.main.transform.rotation = Quaternion.Euler(51.1f, 180, 0);
            Camera.main.transform.position = new Vector3(-1.55f, 8.83f, 18.62f);
            camera1 = 1;
            
        }
        else
        {
           
            Camera.main.transform.rotation = Quaternion.Euler(51.1f, 0, 0);
            Camera.main.transform.position = new Vector3(2.25f, 8.83f, 0f);
            camera1 = 0;
        }
    }

}