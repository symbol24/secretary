using UnityEngine;
using System.Collections;

public class Leg : MonoBehaviour {
	private int m_kickDamage = 1;
	private float m_detecttionTimer = 0.0f;
	private float m_detectionDelay = 0.3f;
	
	public int GetDamage(){
		return m_kickDamage;
	}

	void OnTriggerEnter(Collider col){
		FillingCabinet theCabinet = col.GetComponent<FillingCabinet>() as FillingCabinet;
		if (theCabinet != null && Time.time > m_detecttionTimer) {
			theCabinet.DealDamage(m_kickDamage);
			m_detecttionTimer = Time.time + m_detectionDelay;
			print ("Leg Trigger! HP: " + theCabinet.GetCabinetHp());
		}
	}
}
