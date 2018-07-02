using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShop : MonoBehaviour
{
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = this.transform.localPosition;
    }

    private void OnMouseEnter()
    {
        Debug.Log("enter");
    }

    private void OnMouseDrag()
    {
        this.transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0);
    }

    private void OnMouseUpAsButton()
    {
        this.transform.localPosition = startPosition;
    }
}
