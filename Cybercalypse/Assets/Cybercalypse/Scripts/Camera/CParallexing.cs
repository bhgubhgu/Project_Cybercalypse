using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CParallexing : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 :배경 패럴렉싱을 구현한 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    //복합식(구조체 등)의 배열 또는 동적으로 인덱스를 추가 할당이 아닌 이상은 List 보다 배열이 빠르다.
    public Transform[] backgrounds;
    public float smoother; //Lerp smoother

    private float[] parallexingZScale;//각 parallex 오브젝트의 z 스케일에 -1을 곱할 것임 

    private Transform mainCamera; //메인 카메라 포지션
    private Vector3 previousCamera; //메인 카메라 이전 프레임의 포지션

    private Vector3 backgroundTargetPosition; // 각 타겟의 포지션 Vector

    private float parallexScale; //메인 카메라와 움직임이 반대를 위함
    private float backgroundTargetXPosition;
    private int backgroundIndex;

    private void Awake()
    {
        mainCamera = Camera.main.transform;
    }

    private void Start()
    {
        backgroundIndex = 0;
        previousCamera = mainCamera.position;

        parallexingZScale = new float[backgrounds.Length];

        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallexingZScale[i] = backgrounds[i].position.z * -1f * ((i * 2.3f)+ 11.2f * (i + 1)); //Z축 반전
        }
    }

    private void LateUpdate()
    {
        for (backgroundIndex = 0; backgroundIndex < backgrounds.Length; backgroundIndex++)
        {
            //이전 프레임의 스케일을 곱하기 떄문에 parallax는 카메라의 움직임과 반대이다.
            parallexScale = (previousCamera.x - mainCamera.position.x) * parallexingZScale[backgroundIndex];

            //타겟(B/Fground) 의 포지션은 현재 포지션 + parallex 포지션
            backgroundTargetXPosition = backgrounds[backgroundIndex].position.x + parallexScale;
            backgroundTargetPosition = new Vector3(backgroundTargetXPosition, backgrounds[backgroundIndex].position.y, backgrounds[backgroundIndex].position.z);
            backgrounds[backgroundIndex].position = Vector3.Lerp(backgrounds[backgroundIndex].position, backgroundTargetPosition, smoother * Time.deltaTime);
        }

        previousCamera = mainCamera.position;
    }
}
