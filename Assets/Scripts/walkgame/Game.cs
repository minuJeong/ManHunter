using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{

	private Camera _inputCamera;
	private bool _inputIsKeyboard = false;
	private PlayerPawn _playerPawn;
	private const float _keyboardInputMoveSpeed = 0.06F;
	private float _mouseInputMoveSpeed = 0;

	public void ToggleInputMode ()
	{
		_inputIsKeyboard = !_inputIsKeyboard;

		if (_inputIsKeyboard) {
			_playerPawn.GetComponent<NavMeshAgent> ().acceleration = 0;
		} else {
			_playerPawn.GetComponent<NavMeshAgent> ().acceleration = _mouseInputMoveSpeed;
		}

		_playerPawn.GetComponent<NavMeshAgent> ().SetDestination (_playerPawn.transform.position);
	}

	// Use this for initialization
	private void Start ()
	{
		_inputCamera = GameObject.Find ("InputCamera").GetComponent<Camera> ();
		_playerPawn = GameObject.FindObjectOfType<PlayerPawn> ();

		_mouseInputMoveSpeed = _playerPawn.GetComponent<NavMeshAgent> ().acceleration;
	}
	
	// Update is called once per frame
	private void Update ()
	{
		// Input process
		if (_inputIsKeyboard) {
			if (Input.GetKey (KeyCode.LeftArrow)) {
				_playerPawn.transform.position += Vector3.left * _keyboardInputMoveSpeed;
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
				_playerPawn.transform.position += Vector3.forward * _keyboardInputMoveSpeed;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				_playerPawn.transform.position += Vector3.right * _keyboardInputMoveSpeed;
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				_playerPawn.transform.position += Vector3.back * _keyboardInputMoveSpeed;
			}

			if ((_playerPawn.transform.position - _playerPawn.PrevPos).sqrMagnitude > 0) {
				_playerPawn.ToForward = _playerPawn.transform.position - _playerPawn.PrevPos;
				_playerPawn.EnableTransformForwardUpdate ();
			}

		} else {
			if (Input.GetMouseButtonDown (1)) {
				Ray ray = _inputCamera.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.CompareTag ("Floor")) {
						PutMoveOrder (hit.point);
						_playerPawn.DisableTransformForwardUpdate ();
					}
				}
			}
		}
	}

	private void LateUpdate ()
	{
		RelaxeWave ();
	}

	private void RelaxeWave ()
	{
		// Relax floor waving effect
		GameObject[] _floors = GameObject.FindGameObjectsWithTag ("Floor");
		foreach (var _floor in _floors) {
			Mesh mesh = _floor.GetComponent<MeshFilter> ().mesh;
			Vector3[] vertices = mesh.vertices;
			
			for (int i = 0; i < vertices.Length; i++) {
				vertices [i].y = vertices [i].y + (- vertices [i].y) * 0.024F;
			}

			_floor.GetComponent<MeshCollider> ().sharedMesh = null;
			_floor.GetComponent<MeshCollider> ().sharedMesh = mesh;
			mesh.vertices = vertices;
			mesh.RecalculateNormals ();
			mesh.RecalculateBounds ();
		}
	}

	private void PutMoveOrder (Vector3 point)
	{
		_playerPawn.GetComponent<NavMeshAgent> ().SetDestination (point);
	}
}
