using UnityEngine;
using System.Collections;

public class DestroyOnTimer : MonoBehaviour
{
    public float timer = 5f;
    private float currentTimer = 0f;
    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (currentTimer >= timer)
        {
            Destroy(gameObject);
        }
        currentTimer += Time.deltaTime;
    }
}
