using UnityEngine;
using System.Collections;

public class PlayerStomp : MonoBehaviour {
	private Transform m_kickTransform;
	private Vector3 m_legResetPosition;
	private Quaternion m_legRestRotation;
	public GameObject m_leftLeg;
	public GameObject m_rightLeg;
	public GameObject m_kickTarget;
	private bool m_isKicking = false;
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
		if (Input.GetAxis (m_leftKickButton) > 0.0f && !m_isKicking) {
			if(!m_leftLeg.activeSelf) m_leftLeg.SetActive(true);
			m_kickTransform = m_leftLeg.transform;
			Kick ();
		}else if (Input.GetAxis (m_rightKickButton) > 0.0f && !m_isKicking) {
			if(!m_rightLeg.activeSelf) m_rightLeg.SetActive(true);
			m_kickTransform = m_rightLeg.transform;
			Kick ();
		}
	}
	
	private void Kick(){
		if (!m_isKicking) {
			m_legResetPosition = m_kickTransform.position;
			m_legRestRotation = m_kickTransform.localRotation;
			StartCoroutine (KickMotion ());
		}
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
		m_kickTransform.localRotation = m_legRestRotation;
		m_isKicking = false;
		CheckActiveLegs ();
	}

	private void CheckActiveLegs(){
		if(m_rightLeg.activeSelf && !m_isKicking) m_rightLeg.SetActive (false);
		if(m_leftLeg.activeSelf && !m_isKicking) m_leftLeg.SetActive(true);
	}
}
