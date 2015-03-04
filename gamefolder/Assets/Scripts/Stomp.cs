using UnityEngine;
using System.Collections;

<<<<<<< HEAD
public class Stomp : MonoBehaviour
{
    public Transform m_kickTransform;
    public Vector3 m_kickTarget;
    private bool isKicking = false;

    // Use this for initialization
    private void Start()
    {
        m_kickTransform = gameObject.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isKicking)
        {
            Kick();
        }
    }

    private void Kick()
    {

    }
=======
public class Stomp : MonoBehaviour {
	private Transform m_kickTransform;
	public Transform m_legResetPosition;
	public GameObject m_kickTarget;
	private bool m_isKicking = false;
	private float m_fireButton = 0.0f;
	private float m_endKickTime = 0.0f;
	public float m_kickSpeed = 1.0f;
	public float m_maxKickTime = 0.3f;
	public float m_retractKickTime = 0.3f;
	public Camera m_mainCamera;


	// Use this for initialization
	void Start () {
		m_kickTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		m_fireButton = Input.GetAxis ("Fire1");
		if (m_fireButton > 0.0f) {
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
		m_kickTransform.position = m_legResetPosition.position;
		m_isKicking = false;
	}
>>>>>>> e33fcc2745d7a5e130bbccf804abfb68eb0be705
}
