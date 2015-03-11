using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreechRunAwayScript : MonoBehaviour
{
    public float speedToRunAway = 20f;
    public float factorOfCloseness = 0.5f;
    public float randomnessFactor = 4f;

    private Rigidbody rigidbody;

    // Use this for initialization
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {

    }


    void OnTriggerEnter(Collider coll)
    {
        var test = coll.GetComponent<ScreechingCollider>();
        if (test != null && !_isRunning)
        {
            //RunAway
            //Debug.Log("RunAway");
            StartCoroutine(RunAwayCoroutine(coll.gameObject.transform));
        }
    }

    private bool _isRunning = false;

    public IEnumerator RunAwayCoroutine(Transform sourceOfSound)
    {
        if (!_isRunning)
        {
            _isRunning = true;
            var directionAway = new Vector3(transform.position.x - sourceOfSound.position.x, 0,
                                    transform.position.z - sourceOfSound.position.z);
            
            var timeToRunAway = factorOfCloseness/(directionAway.magnitude);
            var timeElapsed = 0f;
            yield return null;
            while (timeToRunAway > timeElapsed)
            {
                timeElapsed += Time.deltaTime;
                directionAway = new Vector3(directionAway.x + Random.Range(-randomnessFactor,randomnessFactor), directionAway.y,
                    directionAway.z + Random.Range(-randomnessFactor, randomnessFactor));
                transform.LookAt(directionAway);
                rigidbody.MovePosition(transform.position - transform.forward.normalized*speedToRunAway * Time.deltaTime);
                yield return null;
            }
            _isRunning = false;
        }
    }
}
