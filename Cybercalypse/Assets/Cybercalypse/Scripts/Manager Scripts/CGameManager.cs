using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CGameManager : SingleTonManager<CGameManager>
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : CyberCalypse의 전체적인 게임 흐름을 관리하는 매니저 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.07.08
    /// </summary>

    public bool isPlayerInvincible;
    public GameObject playerObject;
    public GameObject skillLibrary;
    public GameObject abilityLibrary;
    public GameObject equipmentLibrary;

    public bool isDead;
    public bool isGameOver;
    public bool isMenuClose;

    public List<Sprite> testSkillList;
    public List<Sprite> testAbilityList;
    public List<Sprite> testWeaponList;
    public List<Sprite> testMaskList;
    public List<Sprite> testSuitList;

    private SpriteRenderer sprite;
    private Vector2 cursorTexturePosition;
    
    //!< GameOver
    private GameObject fadeObject;
    private GameObject retryText;
    private GameObject retryButton;

    private Text content;

    private new void Awake()
    {
        base.Awake();

        testSkillList = new List<Sprite>();
        testAbilityList = new List<Sprite>();
        testWeaponList = new List<Sprite>();
        testMaskList = new List<Sprite>();
        testSuitList = new List<Sprite>();
        sprite = playerObject.GetComponent<SpriteRenderer>();

        fadeObject = transform.Find("Fade").gameObject;
        fadeObject.GetComponent<SpriteRenderer>().color = Color.clear;

        skillLibrary = GameObject.Find("Skill Library").gameObject;
        abilityLibrary = GameObject.Find("Ability Library").gameObject;
        equipmentLibrary = GameObject.Find("Equipment Library").gameObject;

        testSkillList.Add(GameObject.Find("NullSkill").gameObject.GetComponent<SpriteRenderer>().sprite);
        testSkillList.Add(GameObject.Find("Skill1").gameObject.GetComponent<SpriteRenderer>().sprite);
        testSkillList.Add(GameObject.Find("Skill2").gameObject.GetComponent<SpriteRenderer>().sprite);
        testSkillList.Add(GameObject.Find("Skill3").gameObject.GetComponent<SpriteRenderer>().sprite);
        testSkillList.Add(GameObject.Find("Skill4").gameObject.GetComponent<SpriteRenderer>().sprite);
        testSkillList.Add(GameObject.Find("Skill5").gameObject.GetComponent<SpriteRenderer>().sprite);
        testSkillList.Add(GameObject.Find("Skill6").gameObject.GetComponent<SpriteRenderer>().sprite);

        testWeaponList.Add(GameObject.Find("NullWeapon").gameObject.GetComponent<SpriteRenderer>().sprite);
        testWeaponList.Add(GameObject.Find("Weapon1").gameObject.GetComponent<SpriteRenderer>().sprite);

        testMaskList.Add(GameObject.Find("NullMask").gameObject.GetComponent<SpriteRenderer>().sprite);
        testMaskList.Add(GameObject.Find("Mask1").gameObject.GetComponent<SpriteRenderer>().sprite);

        testSuitList.Add(GameObject.Find("NullSuit").gameObject.GetComponent<SpriteRenderer>().sprite);
        testSuitList.Add(GameObject.Find("Suit1").gameObject.GetComponent<SpriteRenderer>().sprite);

        testAbilityList.Add(GameObject.Find("NullAbility").gameObject.GetComponent<SpriteRenderer>().sprite);
        testAbilityList.Add(GameObject.Find("Ability1").gameObject.GetComponent<SpriteRenderer>().sprite);
        testAbilityList.Add(GameObject.Find("Ability2").gameObject.GetComponent<SpriteRenderer>().sprite);
        testAbilityList.Add(GameObject.Find("Ability3").gameObject.GetComponent<SpriteRenderer>().sprite);
    }

    private void Start()
    {
        CInputManager.instance.GamePause += GamePause;
        CInputManager.instance.GameRetry += isRetry;
        CInputManager.instance.MenuClose += TurnOffWindow;

        Time.timeScale = 1.0f;
    }

    public void GamePause(bool isGamePauseNow)
    {
        //crossHair 중심을 마우스 좌표로 사용하기 위해 crossHair 텍스쳐의 정 중앙의 포지션으로 놓는다.

        if(isGamePauseNow)
        {
            Cursor.visible = true;
            Cursor.SetCursor(null, cursorTexturePosition, CursorMode.Auto);
        }
        else
        {
            Cursor.visible = false;
        }
    }

    /// <summary>
    /// 플레이어가 세이브 지점에 도달하거나 던전이 끝날때마다 이곳으로 세이브 이벤트를 던져준다.
    /// 세이브 이벤트가 실행되면 CDataManager의 GameSavaJson.json으로 저장이 된다.(일단은 플레이어의 최근 위치(Position))
    /// </summary>
    public void GameSave()
    {
        //save event 실행
    }


    public void GameOver()
    {
        Time.timeScale = 0.7f;

        isDead = true;
        StartCoroutine(FadeOut(5.0f));
    }

    public void PlayerHasInvincible()
    {
        StartCoroutine(HasInvincibleTime());
        StartCoroutine(HasBlink());
    }

    private void isRetry(bool isRetryInput)
    {
        if (!isGameOver)
        {
            return;
        }
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SceneReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NotifyMessage(string message)
    {
        TurnOnWindow();
        content.text = message;
    }

    public void TurnOnWindow()
    {
    }

    public void TurnOffWindow(bool isMenuClose)
    {

    }

    IEnumerator HasBlink()
    {
        sprite.color = new Vector4(1,0,0,0.75f);
        yield return new WaitForSeconds(0.05f);
        sprite.color = new Vector4(1, 1, 1, 1);

        for (float i = 0; i < 1.5f; i += Time.deltaTime)
        {
            if(!isPlayerInvincible)
            {
                sprite.enabled = true;
                yield break;
            }

            sprite.enabled = false;

            yield return new WaitForSeconds(0.01f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator HasInvincibleTime()
    {
        isPlayerInvincible = true;
        Physics2D.IgnoreLayerCollision(9, 25, true);
        yield return new WaitForSeconds(1.5f);
        isPlayerInvincible = false;
        Physics2D.IgnoreLayerCollision(9, 25, false);
    }

    IEnumerator FadeOut(float fadeTime)
    {
        SpriteRenderer renderer = fadeObject.GetComponent<SpriteRenderer>();

        for (float f = 0.0f, interval = 0.1f; f <= 1.0f; f += interval)
        {
            fadeObject.transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
            renderer.color = new Color(0, 0, 0, f); 
            yield return new WaitForSeconds(interval);
        }

        NotifyMessage("죽었습니다\n아무 버튼이나 눌러 재시작 합니다");

        renderer.color = Color.black;
        //!< fadeTime 동안 1.0f 증가

        //!< 2개는 주어져야 계산이 가능함. 주어진것이 fadeTime 하고 interval.
        //!< fadeTime = interval * for문 회전횟수
        //!< interval = fadeTime / for문 회전횟수
        //!< for문 회전횟수 = fadeTime / interval
        //!< 1.0f = interval * for문 회전횟수

        //retryText.GetComponent<TextMesh>().color = Color.yellow;

        Physics2D.IgnoreLayerCollision(9, 25,false);
        isGameOver = true;
    }

    IEnumerator slowlyTime()
    {
        yield return null;
    }

    public enum StatusType
    {
        health,
        shield,
        energy
    }
}