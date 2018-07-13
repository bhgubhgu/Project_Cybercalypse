using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCameraController : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : Player 객체를 비추는 MainCamera 구현 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    private GameObject target;
    /* CamerShake */
    private float shakeCircleX;
    private float shakeCircleY;
    private float shakeRadius;

    private float insideX;

    private float shakeDistance;
    
    /* Camera Field */

    public Transform leftWallPosition;
    public Transform rightWallPosition;
    public Transform bottomWallPosition;
    public Transform roofWallPosition;

    /* private */
    private float horizontalAddition;
    private float vertexAddition;
    private float smoother;

    private Vector3 targetPosition;
    private Vector3 cameraPosition;

    private int resolutionWidth;
    private float pixelUnit;
    private int height;
    private float resolutionSize;
    private float anothorResolutionPositionRoofChecker;
    private float resolutionPositionRoofChecker;

    // 5:4 = -3 더 뺀거
    // 4 : 3
    // 3 : 2 -2
    // 16 : 10 -1
    private void Awake()
    {
        target = CGameManager.instance.playerObject;

        horizontalAddition = -0.36f;
        shakeCircleX = 0.06f;
        shakeCircleY = 0.06f;
        insideX = 0.015f;
        shakeDistance = 0.0f;
        vertexAddition = 0.15f;

        shakeRadius = Mathf.Sqrt(Mathf.Pow(shakeCircleX, 2) + Mathf.Pow(shakeCircleY, 2)); //Shake Distance Circle

        smoother = 4.1f;
    }

    private void Start()
    {
        resolutionWidth = 1920;
        pixelUnit = 550;
        height = Mathf.RoundToInt(resolutionWidth / (float)Screen.width * Screen.height);
        resolutionSize = height / pixelUnit * 0.5f;
        Camera.main.orthographicSize = resolutionSize;
        Screen.SetResolution(1920, 1080, true);

        if (insideX > shakeRadius)
        {
            throw new System.ArgumentException("Get off shakeRadius");
        }
    }
   
    private void Update()
    {
        /*Camera Resolution Size */ //이벤트로 처리할 것
        height = Mathf.RoundToInt(resolutionWidth / (float)Screen.width * Screen.height);
        resolutionSize = height / pixelUnit * 0.5f;
        Camera.main.orthographicSize = resolutionSize;

        //해상도 표준
        // 4 : 3  = 640 * 480 //창모드
        //        = 1024 * 768 //창모드
        // HD : 1280 * 720
        // FHD : 1920 * 1080
        // UHD : 3840 * 2160
        // WFHD : 2560 * 1080

        //이벤트 처리 -- > 마우스로 클릭해서 처리한다.

        /*if (Camera.main.orthographicSize < 1.2f)
        {
            roofChecker.localPosition = new Vector3(0, resolutionPositionRoofChecker, 0);
            vertexAddition = firstVertex;
        }
        else
        {
            roofChecker.localPosition = new Vector3(0, anothorResolutionPositionRoofChecker, 0);
            vertexAddition = resolutionVertex;
        }*/
    }

    private void LateUpdate()
    {
        //만약 발이 빠졌을때는 카메라가 움직이지 못하게 한다. 발이 나올때까지(y축)
        targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        cameraPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //오른쪽,왼쪽으로 잠깐 움직이는 동안은 카메라의 x축이 고정되어야한다.
        //한 3,4걸음 걸을동안은 카메라의 x축은 움직이면 안된다.
        //y축은 움직이어야한다.


        if(target.transform.localScale.x > 0.0f)
        {
            targetPosition = new Vector3(targetPosition.x - horizontalAddition, targetPosition.y + vertexAddition, this.transform.position.z);
        }
        else
        {
            targetPosition = new Vector3(targetPosition.x + horizontalAddition, targetPosition.y + vertexAddition, this.transform.position.z);
        }

        //targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, leftWallPosition.position.x, rightWallPosition.position.x), Mathf.Clamp(targetPosition.y, bottomWallPosition.position.y, roofWallPosition.position.y), this.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoother * Time.deltaTime);     
    }

    public void CameraShake() //Test용, ArcanePhsics에서 Collide 될때마다 호출
    {
        StartCoroutine(CameraInsideXShake());
    }

    public IEnumerator CameraInsideXShake()
    {
        //Camera Shake Loop
        for (float i = 0; i < 0.1f; i += Time.deltaTime) // 임시 루프
        {
            shakeDistance = Random.Range(-insideX, +insideX); //프레임마다 랜덤 처리, 루프 안에서 처리
            transform.position = new Vector3(this.transform.position.x + shakeDistance, this.transform.position.y, this.transform.position.z);
            yield return null;
        }
    }
}
