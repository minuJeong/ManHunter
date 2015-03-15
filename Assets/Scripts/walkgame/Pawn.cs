using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour
{
	public Vector3 PrevPos = Vector3.zero;
	public Vector3 ToForward = Vector3.forward;
	protected float speed = 0f;
	private bool _forwardUpdate = true;

	// Use this for initialization
	protected virtual void Start ()
	{
		PrevPos = transform.position;
	}
	
	// Update is called once per frame
	protected virtual void Update ()
	{
		AnimationControl ();

		// Adjust forward
		if (_forwardUpdate) {
			transform.forward = Vector3.Lerp (transform.forward, ToForward, 0.5F);
		}

		// Save previous position
		PrevPos = transform.position;
	}

	protected virtual void LateUpdate ()
	{
	}

	protected virtual void AnimationControl ()
	{
		speed = Vector3.Distance (transform.position, PrevPos);
		GetComponentInChildren<Animator> ().SetFloat ("Speed", speed);
	}

	public void EnableTransformForwardUpdate ()
	{
		_forwardUpdate = true;
	}

	public void DisableTransformForwardUpdate ()
	{
		_forwardUpdate = false;
	}
}
