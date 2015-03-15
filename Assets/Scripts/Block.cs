using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
	public int x;
	public int y;
	public bool xLock = false;
	public const int WIDTH_MOD = 9;
	public const int HEIGHT_MOD = 9;

	// Use this for initialization
	void Start ()
	{
		transform.position = new Vector3 (-4.5F + x,
		                                  0.5F,
		                                  4.5f - y);

		transform.localScale = new Vector3 (1, Random.Range (0.75F, 1.25F), 1);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = Vector3.Lerp (transform.position, new Vector3 (-4.5F + x, 0.5F, 4.5f - y), 0.1F);
	}

	public bool Shift (int xShift, int yShift)
	{
		if (xLock) {
			return true;
		}

		var nextX = x + xShift;
		var nextY = y + yShift;

		if (nextX > WIDTH_MOD) {
			nextX = WIDTH_MOD;
		}

		if (nextX < 0) {
			nextX = 0;
		}

		if (nextY > HEIGHT_MOD) {
			return true;
		}

		if (HitTestBlock (nextX, nextY)) {
			return true;
		}


		x = nextX;
		y = nextY;

		return false;
	}

	public void ForceShift (int xShift, int yShift)
	{
		x += xShift;
		y += yShift;

		x = Mathf.Clamp (x, 0, WIDTH_MOD);
		y = Mathf.Clamp (y, 0, HEIGHT_MOD);
	}

	private bool HitTestBlock (int _x, int _y)
	{
		foreach (var block in GameObject.FindObjectsOfType<Block>()) {
			if (block == this) {
				continue;
			}

			if (block.x == _x && (block.y == _y && block.xLock)) {
				return true;
			}
		}

		return false;
	}
}
