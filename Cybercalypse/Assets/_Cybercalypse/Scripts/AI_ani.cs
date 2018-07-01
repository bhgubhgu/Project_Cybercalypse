using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ani : MonoBehaviour
{
    private Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
}
