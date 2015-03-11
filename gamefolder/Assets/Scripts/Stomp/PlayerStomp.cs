using UnityEngine;
using System.Collections;

public class PlayerStomp : MonoBehaviour {
	private Transform m_kickTransform;
	private Vector3 m_legResetPosition;
	private Quaternion m_legRestRotation;
	public GameObject[] m_legs;
	public GameObject[] m_LegReset;
	public GameObject m_kickTarget;
	private bool m_isKicking = false;
	private float m_endKickTime = 0.0f;
	public float m_kickSpeed = 30.0f;
	public float m_maxKickTime = 0.1f;
	public float m_retractKickTime = 0.1f;
	public Camera m_mainCamera;
	public string m_leftKickButton = "Fire1";
	public string m_rightKickButton = "Fire2";
	private float m_kickTimer = 0.0f;
	private float m_kickDelay = 0.3f;
	
	
	// Use this for initialization
	void Start () {
		m_kickTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis (m_leftKickButton) > 0.0f && !m_isKicking && m_kickTimer <= Time.time) {
			Kick (0);
			m_kickTimer = Time.time + m_kickDelay;
		}else if (Input.GetAxis (m_rightKickButton) > 0.0f && !m_isKicking && m_kickTimer <= Time.time) {
			Kick (1);
			m_kickTimer = Time.time + m_kickDelay;
		}
		
		CheckActiveLegs ();
	}
	
	private void Kick(int legID){
	if (!m_isKicking) {
			if(!m_legs[legID].activeSelf) m_legs[legID].SetActive(true);
			m_kickTransform = m_legs[legID].transform;
			m_legResetPosition = m_kickTransform.position;
			m_legRestRotation = m_kickTransform.localRotation;
			StartCoroutine (KickMotion (legID));
		}
	}
	
	private IEnumerator KickMotion(int legId){
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

		m_kickTransform.position = m_LegReset[legId].transform.position;
		m_kickTransform.localRotation = m_LegReset[legId].transform.localRotation;
		m_isKicking = false;
	}

	private void CheckActiveLegs(){
		foreach (GameObject leg in m_legs) {
			if (leg.activeSelf && !m_isKicking)	leg.SetActive (false);
		}
	}
}
