using UnityEngine;
using System.Collections;

public class WingGustPlayerScript : MonoBehaviour
{
    private IController controller;
    public GameObject gustCollider;
    public float gustStrength;
    // Use this for initialization
    private void Start()
    {
        controller = GetComponent(typeof (IController)) as IController;
        if(controller == null) Debug.LogError("NoControllerFound");
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);
        if (controller.DoGust())
        {
            var gustcoll =
                (Instantiate(gustCollider, transform.position, Quaternion.LookRotation(transform.forward)) as GameObject)
                    .GetComponent<GustCollider>();
            gustcoll.SourceOfGust = gameObject;
            gustcoll.ForceToUse = gustStrength;
        }
    }
}
