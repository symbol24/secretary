using UnityEngine;
using System.Collections;

public class WingGustPlayerScript : MonoBehaviour
{
    private IController controller;
    public SphereCollider sphereColliderToUse;
    public float wingMovementTime = 3f;
    public float radiousSpeed = 10f;
    // Use this for initialization
    private void Start()
    {
        controller = GetComponent(typeof (IController)) as IController;
        if(controller == null) Debug.LogError("NoControllerFound");
    }

    // Update is called once per frame
    private void Update()
    {
        if (controller.DoGust())
        {
            StartCoroutine(StartGust());
        }
    }

    private IEnumerator StartGust()
    {
        float currentTime = 0f;
        sphereColliderToUse.enabled = true;
        float startRadious = sphereColliderToUse.radius;
        while (currentTime < wingMovementTime)
        {
            currentTime += Time.deltaTime;
            sphereColliderToUse.radius += radiousSpeed*Time.deltaTime;
            yield return null;
        }
        sphereColliderToUse.enabled = false;
        sphereColliderToUse.radius = startRadious;


    }
}
