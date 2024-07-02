using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yarn : MonoBehaviour
{
    public int id = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "Player")
        {
            GameManager.instance.collectYarn(id);
            Destroy(this.gameObject);
        }
    }
}
