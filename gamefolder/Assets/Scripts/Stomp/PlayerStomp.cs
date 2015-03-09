using UnityEngine;
using System.Collections;

public class PlayerStomp : MonoBehaviour {
	private Transform m_kickTransform;
	private Vector3 m_legResetPosition;
	public GameObject m_leftLeg;
	public GameObject m_rightLeg;
	public GameObject m_kickTarget;
	private bool m_isKicking = false;
	private float m_fireButton = 0.0f;
	private float m_endKickTime = 0.0f;
	public float m_kickSpeed = 30.0f;
	public float m_maxKickTime = 0.1f;
	public float m_retractKickTime = 0.1f;
	public Camera m_mainCamera;
	public string m_leftKickButton = "Fire1";
	public string m_rightKickButton = "Fire2";
	
	
	// Use this for initialization
	void Start () {
		m_kickTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		m_fireButton = Input.GetAxis ("Fire1");
		if (Input.GetAxis (m_leftKickButton) > 0.0f) {
			m_kickTransform = m_leftLeg.transform;
			m_legResetPosition = m_kickTransform.position;
			Kick ();
		}else if (Input.GetAxis (m_rightKickButton) > 0.0f) {
			m_kickTransform = m_rightLeg.transform;
			m_legResetPosition = m_kickTransform.position;
			Kick ();
		}
	}
	
	private void Kick(){
		if(!m_isKicking) StartCoroutine (KickMotion ());
	}
	
	private IEnumerator KickMotion(){
		m_isKicking = true;
		m_endKickTime = Time.time + m_maxKickTime;
		Vector3 motion = Vector3.forward * m_kickSpeed * Time.deltaTime;
		m_kickTransform.LookAt (m_kickTarget.transform.position);
		while (Time.time < m_endKickTime) {
			m_kickTransform.Translate(motion, Space.Self);
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForEndOfFrame();
		m_endKickTime = Time.time + m_retractKickTime;
		while (Time.time < m_endKickTime) {
			m_kickTransform.Translate(-motion, Space.Self);
			yield return new WaitForEndOfFrame();
		}
		m_kickTransform.position = m_legResetPosition;
		m_isKicking = false;
	}
}
