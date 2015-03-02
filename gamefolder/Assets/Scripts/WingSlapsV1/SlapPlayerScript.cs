using UnityEditor;
using UnityEngine;
using System.Collections;

public class SlapPlayerScript : MonoBehaviour
{
    private IController controller;
    public SlapWingScript leftFootScript;
    public SlapWingScript rightFootScript;

    // Use this for initialization
    private void Start()
    {
        #region GetController
        controller = GetComponent(typeof (IController)) as IController;
        if (controller == null)
        {
            Debug.LogError("No IController attached to GameObjet: {0}");
            if (EditorApplication.isPlaying) EditorApplication.isPlaying = false;
        }
        #endregion GetController
    }

    // Update is called once per frame
    private void Update()
    {
        if (controller.GetLeftWingDown())
        {
            leftFootScript.Slap();
        }
        if (controller.GetRightWingDown())
        {
            rightFootScript.Slap();
        }
    }
}
