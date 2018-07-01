using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorControl : MonoBehaviour {

    private Vector3 prevMousePos;
    private Vector3 mousePosition;
    private RawImage component;
    private RectTransform rectTransform;

    public GameObject player;
    private Vector3 normalVector;
    private float radius;

    private Vector3 joystickVector;
    private Vector3 lastJoyStickVector;

    private bool isMovedByMouse;

    // Use this for initialization
    void Start () {
        player = CGameManager.instance.playerObject;

        /* Cursor Control */
        CInputManager.instance.CursorHMove += CursorHMove;
        CInputManager.instance.CursorVMove += CursorVMove;
        prevMousePos = Vector3.zero;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Cursor.visible = false;
        component = transform.GetComponent<RawImage>();
        rectTransform = transform.GetComponent<RectTransform>();

        radius = 0.5f;

        joystickVector = Vector3.zero;
    }
    
    /// <summary>
    /// Input.MousePosition 가져와서 WorldPoint로 변환한다.
    /// 변환된 MousePosition과 Player의 Position의 Vector차이를 Normalize해서, 일정거리만큼 떨어진 위치에 표시한다.
    /// </summary>
    private void Update()
    {
        //!< 마우스 가만히 있으면
        if (prevMousePos.Equals(Input.mousePosition))
        {
            //!< R스틱을 확실하게 움직이면
            if (joystickVector.magnitude >= 0.9f)
            {
                lastJoyStickVector = joystickVector;
                transform.position = player.transform.position + lastJoyStickVector * radius;
                isMovedByMouse = false;
            }

            //!< R스틱을 움직이지 않으면
            else
            {
                if (isMovedByMouse)
                    transform.position = player.transform.position + normalVector * radius;

                else
                    transform.position = player.transform.position + lastJoyStickVector * radius;
            }
        }

        //!< 마우스가 움직이면
        else
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);  //!< z를 0으로 설정
            normalVector = (mousePosition - player.transform.position).normalized;
            transform.position = player.transform.position + normalVector * radius;

            isMovedByMouse = true;
        }
        prevMousePos = Input.mousePosition;
    }

    //!< 모조리 주석임
    private void FixedUpdate()
    {
        ////!< 마우스 가만히 있으면
        //if (prevMousePos.Equals(Input.mousePosition))
        //{
        //    totalTime += Time.fixedDeltaTime;

        //    if (totalTime >= 1.0f)
        //    {
        //        component.color = Color.white;
        //        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        //        Cursor.visible = false;
        //    }
        //}

        ////!< 마우스가 움직이면
        //else
        //{
        //    totalTime = 0.0f;
        //    component.color = Color.clear;
        //    Cursor.visible = true;
        //    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        //    //mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        //    //transform.position = Camera.main.ScreenToViewportPoint(mousePosition);
        //    //transform.position = new Vector3(transform.position.x * Screen.width, transform.position.y * Screen.height);
        //}

        //prevMousePos = Input.mousePosition;
    }
    
    //!< R스틱 입력값을 joystickVector에 저장한다.
    public void CursorHMove(float hInputValue)
    {
        joystickVector = new Vector3(hInputValue, joystickVector.y, 0);
    }
    public void CursorVMove(float vInputValue)
    {
        joystickVector = new Vector3(joystickVector.x, vInputValue, 0);
    }
}