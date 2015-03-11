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
	private int m_folderSpawnAmount = 4;
	private int m_chanceToSpawnGoodFolder = 2;
	private float fileThrust = 250.0f;
	private int folderID = 0;
	private bool hasGoodSpawned = false;

	private void Start(){
		m_myState = cabinteState.Healthy;
		m_myRender = GetComponent<Renderer> () as Renderer;
	}
	
	public int GetCabinetHp(){
		return m_cabinetHp;
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
		for (int i = 0; i < m_folderSpawnAmount; i++) {
			if(!hasGoodSpawned && m_cabinetHp > 1){
				int percent =Random.Range(0,10);
				if(percent >= 10-m_chanceToSpawnGoodFolder){
					folderID = 1;
					hasGoodSpawned = true;
				}
			}else if(!hasGoodSpawned && m_cabinetHp == 1){
				folderID = 1;
				hasGoodSpawned = true;
			}else folderID = 0;

			GameObject tempFile = Instantiate (m_theFolders [folderID], m_folderSpawnPoint.transform.position, m_folderSpawnPoint.transform.localRotation) as GameObject;
			Vector3 euler = tempFile.gameObject.transform.eulerAngles;
			euler.z = Random.Range(-30.0f, 30.0f);
			tempFile.transform.eulerAngles = euler;
			Rigidbody fileBody = tempFile.GetComponent<Rigidbody>() as Rigidbody;
			if(fileBody != null){
				fileBody.AddForce(Vector3.up * fileThrust);
			}
		}
	}
}
