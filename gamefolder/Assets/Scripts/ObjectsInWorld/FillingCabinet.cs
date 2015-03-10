using UnityEngine;
using System.Collections;

public class FillingCabinet : MonoBehaviour {
	private int m_cabinetHp = 10;
	private enum cabinteState{Healthy, Damaged, SeverlyDamaged, Dead};
	private cabinteState m_myState;
	public Material[] m_stateMaterials;
	public int[] m_stateDamageValues;
	private Renderer m_myRender;
	public GameObject[] m_theFolders;
	public GameObject m_folderSpawnPoint;
	private int m_folderSpawnAmount = 3;
	private int m_chanceToSpawnGoodFolder = 10;

	private void Start(){
		m_myState = cabinteState.Healthy;
		m_myRender = GetComponent<Renderer> () as Renderer;
	}
	
	public int GetCabinetHp(){
		return m_cabinetHp;
	}

	void OnCollisionEnter(Collision col){
		print ("Collision! ");
		Leg colLeg = col.gameObject.GetComponent<Leg> () as Leg;
		if (colLeg != null) {
			DealDamage(colLeg.GetDamage());
		}
	}

	public void DealDamage(int dmg){
		if (m_myState != cabinteState.Dead) {
			m_cabinetHp -= dmg;
			UpdateState();
			UpdateMaterial();
			DropFiles();
		}
	}

	private void UpdateState(){
		if (m_cabinetHp > m_stateDamageValues[0]) {
			m_myState = cabinteState.Healthy;
		}else if(m_cabinetHp > m_stateDamageValues[1]){
			m_myState = cabinteState.Damaged;
		}else if(m_cabinetHp > m_stateDamageValues[2]){
			m_myState = cabinteState.SeverlyDamaged;
		}else if(m_cabinetHp == m_stateDamageValues[3]){
			m_myState = cabinteState.Dead;
		}
	}

	private void UpdateMaterial(){
		switch (m_myState) {
		case cabinteState.Healthy:
			m_myRender.material = m_stateMaterials[0];
			break;
		case cabinteState.Damaged:
			m_myRender.material = m_stateMaterials[1];
			break;
		case cabinteState.SeverlyDamaged:
			m_myRender.material = m_stateMaterials[2];
			break;
		case cabinteState.Dead:
			m_myRender.material = m_stateMaterials[3];
			break;
		}
	}

	private void DropFiles(){
		print ("Drop a file!");

		int folderID = 0;
		bool hasGoodSpawned = false;
		for (int i = 0; i < m_folderSpawnAmount; i++) {
			if(!hasGoodSpawned){


			}else folderID = 0;

			Instantiate (m_theFolders [folderID], m_folderSpawnPoint.transform.position, m_folderSpawnPoint.transform.localRotation);
		}
	}
}
