using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAssetShadow : MonoBehaviour
{
    public GameObject parent;
    public Vector3 offset = Vector3.zero;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parent == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.transform.position = parent.transform.position + offset;
            
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = parent.GetComponent<SpriteRenderer>().sprite;
            }
        }
    }
}
