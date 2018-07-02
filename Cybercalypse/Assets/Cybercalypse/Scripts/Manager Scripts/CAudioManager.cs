using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAudioManager : SingleTonManager<CAudioManager>
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : CyberCalypse의 모든 Audio 들을 관리하는 매니저 스크립트
    /// 최초 작성일 : 2018.06.11
    /// 최종 수정일 : 2018.06.26
    /// </summary>

    //BackGround
    public AudioClip infectedArea02; //임시로 하나만 만들어 뒀다.

    //Sound Effect
    public List<AudioClip> playerAudioList; //플레이어 전용 sound list
    public List<AudioClip> monsterAudioList; //몬스터 전용 sound list
    public List<AudioClip> gameAudioList; //게임에 사용되는 sound list (정적 객체만 해당)
    public List<AudioClip> skillSoundList; //스킬 전용 sound list
    
    //AudioSourceComponent
    public AudioSource backgroundMusic; //sound play component
    public AudioSource effectMusic; //sound play component

    private Dictionary<string, AudioClip> backgroundDictionary;
    private Dictionary<string, AudioClip> skillSoundDictionary;

    private new void Awake()
    {
        base.Awake();
        backgroundDictionary = new Dictionary<string, AudioClip>();
        skillSoundDictionary = new Dictionary<string, AudioClip>();

        backgroundDictionary.Add("infectedArea00_2", infectedArea02); //예전에 쓴 백그라운드 음악(infectedArea02)

        /*skillSoundDictionary.Add("LightningSphere", skillSoundList[0]);
        skillSoundDictionary.Add("CrimsonStrike", skillSoundList[1]);
        skillSoundDictionary.Add("FireBall", skillSoundList[2]);
        skillSoundDictionary.Add("MoonLightSlash", skillSoundList[3]);
        skillSoundDictionary.Add("BlackOut", skillSoundList[4]);
        skillSoundDictionary.Add("FrozenContinuam", skillSoundList[5]); 스킬 사운드를 구하면 주석을 푼다.*/
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        backgroundMusic.clip = infectedArea02; // 현재 씬에 맞는 BackgroundMusic을 찾아서 가져온다.(아마 씬의 이름이 될 것 같다. 딕셔너리로)
        backgroundMusic.Play();
    }

    public void PlayEffectSoundMoveEvent(CInputManager.MoveInput moveEvent) //플레이어의 움직임에 대한 이벤트
    {
        if (moveEvent.Method.Name == "HMoveControl" && !effectMusic.isPlaying) //발소리가 재생 안될때만 재생 되게 함
        {
            effectMusic.clip = playerAudioList[0];
            effectMusic.Play();
        }

        //verticalMove 이벤트 추가 되면 조건 추가 할것
    }

    public void PlayEffectSoundUniqueEvent(CInputManager.UniqueInput uniqueEvent) //특정 행동을 하는 이벤트(점프, 대쉬), 공격 이벤트는 제외
    {
        if(uniqueEvent.Method.Name == "JumpControl")
        {
            effectMusic.clip = playerAudioList[1];
            effectMusic.Play();
        }
        else if(uniqueEvent.Method.Name == "HorizontalAccelControl")
        {
            effectMusic.clip = playerAudioList[2];
            effectMusic.Play();
        }
        //추가 이벤트 있으면 조건 추가 할 것
    }

    /// <summary>
    /// 스킬에 따른 사운드들을 골라서 리턴해준다.
    /// 스킬 이름들은 나중에 수정된 기획서에 따라서 달라진다.
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    public void SelectSkillSound(CSkillLibrary.Skill skill)
    {
        if(skill.Method.Name == "LightningSphere")
        {
            effectMusic.clip = skillSoundDictionary["LightningSphere"];
            effectMusic.Play();
        }
        else if (skill.Method.Name == "CrimsonStrike")
        {
            effectMusic.clip = skillSoundDictionary["CrimsonStrike"];
            effectMusic.Play();
        }
        else if (skill.Method.Name == "FireBall")
        {
            Debug.Log("FireBall!!");
            /*effectMusic.clip = skillSoundDictionary["FireBall"]; 
            effectMusic.Play(); 사운드를 구하면 주석 지우고 디버그로그 지움*/
        }
        else if (skill.Method.Name == "MoonLightSlash")
        {
            Debug.Log("MoonLightSlash!!");
            /* effectMusic.clip = skillSoundDictionary["MoonLightSlash"];
             effectMusic.Play(); 사운드를 구하면 주석 지우고 디버그로그 지움*/
        }
        else if (skill.Method.Name == "BlackOut")
        {
            effectMusic.clip = skillSoundDictionary["BlackOut"];
            effectMusic.Play();
        }
        else if (skill.Method.Name == "FrozenContinuam")
        {
            effectMusic.clip = skillSoundDictionary["FrozenContinuam"];
            effectMusic.Play();
        }
        else
        {
            return;
        }
    }
}
