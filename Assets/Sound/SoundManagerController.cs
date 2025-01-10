using System.Collections.Generic;
using UnityEngine;

public enum BGMType
{
    /// <summary>BGNなし</summary>
    None,
    /// <summary>教会みたいな音楽</summary>
    Title,
    /// <summary>オルゴール</summary>
    InGame,
    /// <summary>教会の鐘の音</summary>
    ChapelBell,
}

public enum SEType
{
    /// <summary>白い鳩が飛び去って行く音</summary>
    PegionFlying,
    /// <summary>クラッカー</summary>
    Cracker,
    /// <summary>ラトル</summary>
    Rattle,
    /// <summary>ポイントハート出すときの音（ﾊﾟｯ）</summary>
    Heart1,
    /// <summary>ポイントハート出すときの音（ﾊﾟﾊﾟｯ）</summary>
    Heart2,
    /// <summary>小学校のチャイム</summary>
    Chime1,
    /// <summary>中学校のチャイム</summary>
    Chime2,
    /// <summary>スターゲイザー</summary>
    StarGazer,
    /// <summary>自転車のベル</summary>
    BicycleBell,
    /// <summary>中二病ワープ</summary>
    Warp,
    /// <summary>温かい便座キラキラ</summary>
    Kirakira,
    /// <summary>成人式の歓声</summary>
    BeAdult,
    /// <summary>内定の音</summary>
    Naitei,
    /// <summary>ビッグハートかわいい感じの音</summary>
    BigHeart,
    /// <summary>リザルトのハートぽわわーん</summary>
    ResultHeart,
    /// <summary>カスタネット</summary>
    Castanet,
    /// <summary>投げる音</summary>
    Throw,
    /// <summary>子供が口を開ける音</summary>
    Nyuusi,
    /// <summary>物を投げる赤さんの声</summary>
    ThrowBaby,
    /// <summary>おもちゃがなく音</summary>
    Toy,
    /// <summary>キック</summary>
    Kick,
    /// <summary>ボールが曲がる音</summary>
    BallMove,
    /// <summary>ゲームカチャカチャ</summary>
    GameKids,
    /// <summary>スノボジャンプの音</summary>
    SnowBoardJump,
    /// <summary>スノボ回転の音</summary>
    SnowBoardRotate,
    /// <summary>矢があたったときポップな音</summary>
    ArrowHit,
    /// <summary>ﾄﾞｯｸﾝｯ</summary>
    ArrowHit2,
    /// <summary>矢を射るときの音</summary>
    ArrowShoot,
    /// <summary>YEAH</summary>
    Yeah,
    /// <summary>シーン切替の音（ｼｭﾜｰﾝ）</summary>
    Work,
}

public class SoundManagerController : MonoBehaviour
{
    [SerializeField] List<AudioClip> BGMList;
    [SerializeField] List<AudioClip> SEList;

    public static SoundManagerController soundManager;
    public static int playingBGM = (int)BGMType.Title;
    // Start is called before the first frame update
    void Awake()
    {
        if(soundManager == null)
        {
            soundManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(int type) 
    {
        if(BGMList.Count > type && playingBGM != type)
        {
            var audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = BGMList[type];
            audioSource.Play();
            playingBGM = type;
        }
    }

    public void PlaySE(int type, float volume = 1.0f)
    {
        if (SEList.Count > type)
        {
            var audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(SEList[type], volume);
        }
    }
}
