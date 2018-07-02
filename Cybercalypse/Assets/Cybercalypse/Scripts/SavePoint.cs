using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 윤동준
    /// 스크립트 : 세이브 포인트 구현 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>


    //!< Player가 캐릭터에 닿으면 세이브 포인트가 사라지면서, 세이브 포인트가 저장되었다는 알림을 띄운다.
    // 캐릭터가 죽으면 가장 최근에 저장되었던 세이브 포인트에서 시작된다.


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
