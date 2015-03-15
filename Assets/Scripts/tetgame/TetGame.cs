using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TetGame : MonoBehaviour
{

	private GameObject block;
	public const int TIMER = 40;
	public static int timer = TIMER;

	// Use this for initialization
	void Start ()
	{
		block = Resources.Load<GameObject> ("Prefabs/Block");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (timer > 0) {
			timer--;
		} else {
			timer = TIMER;

			Block[] blocks = GameObject.FindObjectsOfType<Block> ();

			foreach (var _block in blocks) {
				if (_block.Shift (0, 1)) {
					_block.xLock = true;
				}
			}

			Block newBlock = ((GameObject)Instantiate (block)).GetComponent<Block> ();
			newBlock.x = Random.Range (0, 10);
			newBlock.y = 0;

//			LineClearCheck ();
		}


		if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)) {
			ShiftAll (-1, 0);
		}
		if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) {
			ShiftAll (1, 0);
		}
	}

	private void ShiftAll (int xShift, int yShift)
	{
		Block[] blocks = GameObject.FindObjectsOfType<Block> ();
		foreach (var block in blocks) {
			block.Shift (xShift, yShift);
		}
	}

	private void LineClearCheck ()
	{
		for (int yIndex = 0; yIndex < Block.HEIGHT_MOD; yIndex++) {
			bool isMatch = true;
			List<Block> blocksToDel = new List<Block> ();

			for (int xIndex = 0; xIndex < Block.WIDTH_MOD; xIndex++) {
				bool found = false;
				foreach (var block in GameObject.FindObjectsOfType<Block> ()) {
					if (block.x == xIndex &&
						block.y == yIndex) {
						found = true;
						blocksToDel.Add (block);
						break;
					}
				}

				if (! found) {
					isMatch = false;
					break;
				}
			}

			if (isMatch) {
				Debug.Log("SHIFTALL");
				foreach (var block in GameObject.FindObjectsOfType<Block> ()) {
					block.ForceShift (0, 1);
				}

				foreach (var blockToDel in blocksToDel) {
					DestroyImmediate (blockToDel.gameObject);
				}
			}

		}
	}
}
