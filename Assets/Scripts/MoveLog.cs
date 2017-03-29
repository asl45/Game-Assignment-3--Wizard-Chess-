using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//some of this (and most of the scripts relating to move log) were sourced from: www.folio3.com/blog/creating-dynamic-scrollable-lists-with-new-unity-canvas-ui/

public class MoveLog : MonoBehaviour {

	public GameObject ContentPanel;
	public GameObject moveTemplate;
	int moveCount;
	ArrayList moves;

	// Use this for initialization
	void Start () {
		moves = new ArrayList () {
			new Move("No.", "Black", "White")

		};
		moveCount = 1;
		generateLog ();

	}


	void generateLog(){
		foreach (Move move in moves) {
			GameObject newMove = Instantiate (moveTemplate) as GameObject;
			MoveLogItemController controller = newMove.GetComponent <MoveLogItemController>();
			controller.moveNumber.text = move.moveNumber;
			controller.whiteMove.text = move.whiteMove;
			controller.blackMove.text = move.blackMove;
			newMove.transform.SetParent( ContentPanel.transform, false);

		}

		moves.Clear ();
	}

	public void addMove(string move){
		if (moveCount % 2 == 1) {
			moves.Add(new Move (moveCount.ToString(), move, ""));
			moveCount++;
			generateLog ();
		}
		else if (moveCount % 2 == 0) {
			moves.Add (new Move (moveCount.ToString(), "", move));
			moveCount++;
			generateLog ();
		}
	}

}
