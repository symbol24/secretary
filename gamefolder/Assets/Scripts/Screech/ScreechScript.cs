using UnityEngine;
using System.Collections;

public class ScreechScript : MonoBehaviour
{

    private IController controller;
    public GameObject sphereColliderToUse;
    private SphereCollider sphereCollider;
    public AudioSource screechSound;
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
        if (controller.Screech())
        {
            if (!_isScreechingAsync)
            {
                StartCoroutine(ScreechAsync());
            }
        }
    }

    private bool _isScreechingAsync = false;

    private IEnumerator ScreechAsync()
    {
        if (!_isScreechingAsync)
        {
            _isScreechingAsync = true;
            if(screechSound != null) screechSound.Play();
            else Debug.LogWarning("NoScreechSoundAttached");
            var currentTime = 0f;sphereCollider.enabled = true;
            var originalRadius = sphereCollider.radius;
            while (currentTime < timeTraveling)
            {
                sphereCollider.radius += soundSpeedtotravel*Time.deltaTime;
                currentTime += Time.deltaTime;
                yield return null;
            }
            sphereCollider.radius = originalRadius;
            sphereCollider.enabled = false;
            yield return null;
            _isScreechingAsync = false;
        }
    }

}
