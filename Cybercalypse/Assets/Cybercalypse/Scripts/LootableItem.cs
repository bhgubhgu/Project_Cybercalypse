using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableItem : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 윤동준
    /// 스크립트 : 아이템을 획득 하게 할 수 있는 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    private CExecutor executor;
    public float recoverAmount;
    private string triggerTag;
    private bool contactToPlayer;
    private bool looted;
    private bool isUp;
    //
    //!< 0.0003f 정도가 둥둥 떠다니는데에 적합함
    private float velocity;

    Coroutine coroutine;

	// Use this for initialization
	void Start () {
        executor = CGameManager.instance.playerObject.GetComponent<CExecutor>();
        triggerTag = CGameManager.instance.playerObject.tag;
        velocity = 5.0f;
        isUp = true;
        //coroutine = StartCoroutine(HoverItem(velocity));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(triggerTag))
        {
            contactToPlayer = true;
            //StopCoroutine(coroutine);
            ObtainedByPlayer();
        }
    }
    
    private void ObtainedByPlayer()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 둥둥 떠다니는듯한 느낌으로 애니메이션 해야 함
    /// </summary>
    /// <param name="velocity">떠다니는 힘(=반경)</param>
    /// <returns></returns>
    IEnumerator HoverItem(float velocity)
    {
        float total = 0.0f;
        float hovertime = 1.0f;
        while(true)
        {
            if (total >= hovertime)
            {
                isUp = !isUp;
                total = 0.0f;
            }

            if (isUp)
                transform.Translate(Vector3.up * 0.01f * velocity * Time.deltaTime);
            else
                transform.Translate(Vector3.down * 0.01f * velocity * Time.deltaTime);

            total += Time.deltaTime;
            yield return null;
        }
    }
}