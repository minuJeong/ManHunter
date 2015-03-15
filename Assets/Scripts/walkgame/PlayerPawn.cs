using UnityEngine;
using System.Collections;

public class PlayerPawn : Pawn
{

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();

		PushWave ();
	}

	private void PushWave ()
	{
		GameObject[] _floors = GameObject.FindGameObjectsWithTag ("Floor");
		foreach (var _floor in _floors) {
			Mesh mesh = _floor.GetComponent<MeshFilter> ().mesh;
			Vector3[] vertices = mesh.vertices;
			
			for (int i = 0; i < vertices.Length; i++) {
				
				Vector3 diff = _floor.transform.position + vertices [i] - transform.position;
				
				if (diff.sqrMagnitude < 4.0F) {
					vertices [i].y = vertices [i].y + (-10.0F/diff.sqrMagnitude - vertices [i].y) * 0.5F;
				}
			}

			_floor.GetComponent<MeshCollider> ().sharedMesh = null;
			_floor.GetComponent<MeshCollider> ().sharedMesh = mesh;
			mesh.vertices = vertices;
			mesh.RecalculateNormals ();
			mesh.RecalculateBounds ();
		}
	}
}
