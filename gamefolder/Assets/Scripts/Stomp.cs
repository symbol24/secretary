using UnityEngine;
using System.Collections;

public class Stomp : MonoBehaviour {
	public Transform m_kickTransform;
	public Vector3 m_kickTarget;
	private bool isKicking = false;

	// Use this for initialization
	void Start () {
		m_kickTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Fire1") && !isKicking) {
			Kick ();
		}
	}

	private void Kick(){

	}
}
