using UnityEngine;
using System.Collections;

public class LookAtPawn : MonoBehaviour
{

	private GameObject _target;
	private Vector3 _viewOffset;
	private Vector3 _lerpTo;

	// Use this for initialization
	void Start ()
	{
		_target = GameObject.FindObjectOfType<PlayerPawn> ().gameObject;
		_viewOffset = transform.position - _target.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		_lerpTo = _target.transform.position + _viewOffset;
		transform.position = Vector3.Lerp (transform.position, _lerpTo, 0.1F);
	}
}
