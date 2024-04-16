using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DirectionalMomentumAddition : MonoBehaviour
{
    public VisualEffect VFX;
    [SerializeField] Vector2 direction;
    [SerializeField] float strength;
    [SerializeField] bool active;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetBool("active", active);
        }
        if (VFX != null)
        {
            VFX.enabled = active;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void toggleActive()
    {
        active = !active;
        if (animator != null)
        {
            animator.SetBool("active", active);
        }
        if (VFX != null)
        {
            VFX.enabled = active;
        }
    }

    public void Off()
    {
        active = false;
        if (animator != null)
        {
            animator.SetBool("active", active);
        }
        if (VFX != null)
        {
            VFX.enabled = active;
        }
    }

    public void On()
    {
        active = true;
        if (animator != null)
        {
            animator.SetBool("active", active);
        }
        if (VFX != null)
        {
            VFX.enabled = active;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (active && collision.gameObject.tag == "Player")
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            player.WorldAddMomentum(direction * strength);
        }
    }
}
