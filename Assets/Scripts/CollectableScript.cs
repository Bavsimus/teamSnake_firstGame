using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    [SerializeField] private int GazozNum = 0;

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Gazoz"))
        {
            Destroy(collider.gameObject);
            GazozNum++;
        }
    }






}
