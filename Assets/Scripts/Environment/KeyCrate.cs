using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCrate : MonoBehaviour
{

    [SerializeField] private GameObject hitVFX = null;
    [SerializeField] private GameObject key = null;
    [SerializeField] private Transform origin = null;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeathSequence()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.layer = gameObject.layer;
        vfx.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;

        GameObject keyInstance = Instantiate(key, origin.position, origin.rotation);
        keyInstance.layer = gameObject.layer;
        keyInstance.GetComponent<SpriteRenderer>().sortingLayerName = LayerMask.LayerToName(keyInstance.layer);
        
        SpriteRenderer[] srs = keyInstance.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in srs)
        {
            sr.sortingLayerName = LayerMask.LayerToName(keyInstance.layer);
        }
    }
}
