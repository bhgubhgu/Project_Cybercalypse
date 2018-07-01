using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHeadToMouse : MonoBehaviour {

    //!< 마우스 위치 저장
    public Vector3 mousePos;
    //public CPhysics cPhysics;

    private float seta;
    //private float scalForRevision;

    // Update is called once per frame
    void Update()
    {
        //scalForRevision = transform.parent.localScale.x;

        if (transform.parent.localScale.x <= -1)
            transform.localScale = new Vector3(-1, -1, 0);
        else
        {
            transform.localScale = new Vector3(1, 1, 0);
        }

        float dy = mousePos.y - transform.position.y;
        float dx = mousePos.x - transform.position.x;

        seta = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, seta);
    }
}
