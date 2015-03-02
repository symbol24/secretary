using UnityEngine;
using System.Collections;

public class SqueakScript : MonoBehaviour
{

    private IController controller;
    public GameObject sphereColliderToUse;
    private SphereCollider sphereCollider;
    public AudioSource squeakSound;
    public float soundSpeedtotravel = 100f;
    public float timeTraveling = 0.5f;


    // Use this for initialization
    private void Start()
    {
        controller = GetComponent(typeof (IController)) as IController;
        sphereCollider = sphereColliderToUse.GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (controller.Squeak())
        {
            if (!_isSqueakingAsync)
            {
                StartCoroutine(SqueakAsync());
            }
        }
    }

    private bool _isSqueakingAsync = false;

    private IEnumerator SqueakAsync()
    {
        if (!_isSqueakingAsync)
        {
            _isSqueakingAsync = true;
            squeakSound.Play();
            var currentTime = 0f;
            var originalRadius = sphereCollider.radius;
            while (currentTime < timeTraveling)
            {
                sphereCollider.radius += soundSpeedtotravel*Time.deltaTime;
                currentTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            sphereCollider.radius = originalRadius;

            _isSqueakingAsync = false;
        }
    }

}
