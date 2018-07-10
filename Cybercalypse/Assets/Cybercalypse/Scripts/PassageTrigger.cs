using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageTrigger : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 윤동준
    /// 스크립트 : 문 트리거를 작동 하게 할 수 있는 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    //!< 작동 여부 판별에 필요함
    public string triggerTag;
    private float sinkVelocity;
    private float openMovement;
    private bool isOpend;
    public int myIndex;
    public Transform door;

    private List<SpriteRenderer> rendererList;

    public float Velocity
    {
        get;set;
    }
    public bool IsOpend
    {
        get
        {
            return isOpend;
        }

        set
        {
            isOpend = value;
        }
    }

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        sinkVelocity = 3.0f;
        openMovement = 1.5f;

        isOpend = false;

        rendererList = new List<SpriteRenderer>();

        for (int i = 0; i < transform.childCount; i++)
        {
            rendererList.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
        }
        
    }

    //!< 문이 열렸다는 사실을 저장해야함. 이 문이 열렸는가.
    /// <summary>
    /// 문을 List로 관리해서 넘겨주는 형태로 수정할 수도 있음을 감안할 것
    /// </summary>
    /// <param name="collision"></param>
    /// 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 && !isOpend)
        {
            StartCoroutine(OpenDoor(door));
            StartCoroutine(SinkLever());
        }
    }

    IEnumerator OpenDoor(Transform door)
    {
        isOpend = true;

        for (float time = 0.0f, runningTime = 3.0f; time <= runningTime; time += Time.deltaTime)
        {
            door.Translate(Vector3.up * 0.1f * openMovement * Time.deltaTime);

            yield return null;
            //!< 0.02가 되면 그만큼 더 적게 기다려야 함. (1.0f / 0.01 = 1.0 * 100);
            //!< 2초동안 동작하게 하려면, time이 있고 
        }
    }

    IEnumerator SinkLever()
    {
        for (int i = 0; i < rendererList.Count; i++)
            rendererList[i].sortingOrder = -1;

        for (float time = 0.0f, runningTime = 0.1f; time <= runningTime; time += Time.deltaTime)
        {
            transform.Translate(Vector3.down * 0.1f * sinkVelocity * Time.deltaTime);

            yield return null;
            //!< 0.02가 되면 그만큼 더 적게 기다려야 함. (1.0f / 0.01 = 1.0 * 100);
            //!< 2초동안 동작하게 하려면, time이 있고 
        }
    }
}