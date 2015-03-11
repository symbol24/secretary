using UnityEngine;
using System.Collections;

public class GustCollider : MonoBehaviour
{
    public GameObject SourceOfGust { get; set; }
    public float horizontalSpreadSpeed = 5;
    public float gustSpeed = 5;

    private BoxCollider boxCollider;
    public float ForceToUse { get; set; }

    // Use this for initialization
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        StartCoroutine(GustColliderCoroutine());
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private bool _isRunning = false;
    private IEnumerator GustColliderCoroutine()
    {
        if (!_isRunning)
        {
            _isRunning = true;
            while (true)
            {
                Debug.DrawRay(transform.position, transform.forward*4);
                transform.Translate(transform.forward*gustSpeed*Time.deltaTime);
                boxCollider.size = new Vector3(boxCollider.size.x + gustSpeed*Time.deltaTime, boxCollider.size.y,
                    boxCollider.size.z);
                yield return null;
            }
            _isRunning = false;
        }
    }
}
