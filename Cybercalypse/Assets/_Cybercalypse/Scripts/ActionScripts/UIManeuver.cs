using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 해상도 변경에 따른 UI 요소 재배치 관련 스크립트
/// </summary>
public class UIManeuver : MonoBehaviour {

    public Transform trLeftTop;
    public Transform trLeftBottom;
    public Transform trMiddleBottom;
    public Transform trRightTop;
    public Transform trRightBottom;

    public Camera _camera;

	// Use this for initialization
	void Start ()
    {
        trLeftBottom = transform.Find("1_LeftBottom");
        trMiddleBottom = transform.Find("2_MiddleBottom");
        trRightBottom = transform.Find("3_RightBottom");
        trLeftTop = transform.Find("7_LeftTop");
        trRightTop = transform.Find("9_RightTop");
    }

    // Update is called once per frame
    void Update () {
		
	}

    void ModifyPivot()
    {
        /*
        trLeftTop.position = _camera.ViewportToWorldPoint(Vector2.up);
        trLeftBottom.position = _camera.ViewportToWorldPoint(Vector2.zero);
        trRightTop.position = _camera.ViewportToWorldPoint(Vector2.one);
        trRightBottom.position = _camera.ViewportToWorldPoint(Vector2.right);
        */
    }
}
