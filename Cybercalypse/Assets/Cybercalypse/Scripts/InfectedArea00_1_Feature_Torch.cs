using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedArea00_1_Feature_Torch : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 김현우
    /// 스크립트 : 배경 오브젝트의 애니메이션 구현 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>
    /// 
    private Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
}
