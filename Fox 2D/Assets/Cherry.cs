using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Collects()
    {
        animator.SetTrigger("collect");
    }

    private void Disapears()
    {
        Destroy(this.gameObject);
    }
}
