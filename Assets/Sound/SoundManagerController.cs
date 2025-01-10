using System.Collections.Generic;
using UnityEngine;

public enum BGMType
{
    /// <summary>BGN�Ȃ�</summary>
    None,
    /// <summary>����݂����ȉ��y</summary>
    Title,
    /// <summary>�I���S�[��</summary>
    InGame,
    /// <summary>����̏��̉�</summary>
    ChapelBell,
}

public enum SEType
{
    /// <summary>����������ы����čs����</summary>
    PegionFlying,
    /// <summary>�N���b�J�[</summary>
    Cracker,
    /// <summary>���g��</summary>
    Rattle,
    /// <summary>�|�C���g�n�[�g�o���Ƃ��̉��i�߯�j</summary>
    Heart1,
    /// <summary>�|�C���g�n�[�g�o���Ƃ��̉��i���߯�j</summary>
    Heart2,
    /// <summary>���w�Z�̃`���C��</summary>
    Chime1,
    /// <summary>���w�Z�̃`���C��</summary>
    Chime2,
    /// <summary>�X�^�[�Q�C�U�[</summary>
    StarGazer,
    /// <summary>���]�Ԃ̃x��</summary>
    BicycleBell,
    /// <summary>����a���[�v</summary>
    Warp,
    /// <summary>�������֍��L���L��</summary>
    Kirakira,
    /// <summary>���l���̊���</summary>
    BeAdult,
    /// <summary>����̉�</summary>
    Naitei,
    /// <summary>�r�b�O�n�[�g���킢�������̉�</summary>
    BigHeart,
    /// <summary>���U���g�̃n�[�g�ۂ��[��</summary>
    ResultHeart,
    /// <summary>�J�X�^�l�b�g</summary>
    Castanet,
    /// <summary>�����鉹</summary>
    Throw,
    /// <summary>�q���������J���鉹</summary>
    Nyuusi,
    /// <summary>���𓊂���Ԃ���̐�</summary>
    ThrowBaby,
    /// <summary>�������Ⴊ�Ȃ���</summary>
    Toy,
    /// <summary>�L�b�N</summary>
    Kick,
    /// <summary>�{�[�����Ȃ��鉹</summary>
    BallMove,
    /// <summary>�Q�[���J�`���J�`��</summary>
    GameKids,
    /// <summary>�X�m�{�W�����v�̉�</summary>
    SnowBoardJump,
    /// <summary>�X�m�{��]�̉�</summary>
    SnowBoardRotate,
    /// <summary>����������Ƃ��|�b�v�ȉ�</summary>
    ArrowHit,
    /// <summary>�ޯ�ݯ</summary>
    ArrowHit2,
    /// <summary>����˂�Ƃ��̉�</summary>
    ArrowShoot,
    /// <summary>YEAH</summary>
    Yeah,
    /// <summary>�V�[���ؑւ̉��i��ܰ݁j</summary>
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
