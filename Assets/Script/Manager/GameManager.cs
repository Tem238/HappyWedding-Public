using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    // 背景類
    [SerializeField] Image backgroundImageBack;
    [SerializeField] Image backgroundImageFront;
    [SerializeField] List<Sprite> backgroundImages;
    private int imageIndex;

    // イベント類
    [SerializeField] List<GameObject> eventPrefabs;
    private int eventCount;

    // ポイント管理・表示UI
    [SerializeField] Text totalPointText;
    [SerializeField] GameObject pointHeart;
    private int totalPoint;

    // UI全面表示用Canvas、写真表示用Image
    [SerializeField] Canvas frontCanvas;
    [SerializeField] Image localFilePreview;
    [SerializeField] Image lastImage;
    [SerializeField] Image lastImageMask;
    [SerializeField] Text exitAnnounce;
    [SerializeField] GameObject lastImageParticle;
    Tweener exitAnim;

    // パーティクル等の演出を管理する変数ども
    [SerializeField] GameObject CherryBlossom;
    [SerializeField] Image whitePigeon;
    [SerializeField] GameObject Balloon1;
    [SerializeField] GameObject Balloon2;
    [SerializeField] GameObject heart;

    // BigHeart演出Sprite
    [SerializeField] GameObject bigHeart;

    // 最後に消すオブジェクト
    [SerializeField] List<GameObject> destroyObjectsInEnding;
    // 転がしている花冠オブジェクト
    [SerializeField] GameObject playerObj;
    [SerializeField] Sprite ring;
    [SerializeField] GameObject ringEffect;

    // デバッグ用
    [SerializeField] int eventDebugIndex;
    [SerializeField] GameObject soundManagerPrefab;

    // Sceneの経過時間
    private float gameTime;
    public static string gameState;

    private void Awake()
    {
#if UNITY_EDITOR
        if (SoundManagerController.soundManager == null)
        {
            Instantiate(soundManagerPrefab);
        }
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        localFilePreview.color = new Color(255, 255, 255, 0);
        imageIndex = 0;
        eventCount = 0;
        gameTime = 0;
        totalPoint = 0;
        gameState = "playing";
        SoundManagerController.soundManager.PlayBGM((int)BGMType.InGame);
#if UNITY_EDITOR
        LoadImageUtil.Initialize();
        playEvent(eventDebugIndex);
#else
        playEvent();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        if ((int)gameTime % 5 == 0 && (int)(gameTime / 5) - eventCount == 1 && gameState == "playing")
        {
            if (++eventCount < eventPrefabs.Count)
            {
                playEvent();
                switch (eventCount)
                {
                    // 小学生
                    case 10:
                        ChangeBackgroundImage();
                        SoundManagerController.soundManager.PlaySE((int)SEType.Chime1);
                        break;
                    // 中学生
                    case 15:
                        ChangeBackgroundImage();
                        SoundManagerController.soundManager.PlaySE((int)SEType.Chime2);
                        break;
                    // 高校生
                    case 20:
                        ChangeBackgroundImage();
                        break;
                    // 大学生
                    case 28:
                        ChangeBackgroundImage();
                        break;
                    // 成人
                    case 31:
                        SoundManagerController.soundManager.PlaySE((int)SEType.BeAdult);
                        break;
                    // 社会人
                    case 35:
                        ChangeBackgroundImage();
                        SoundManagerController.soundManager.PlaySE((int)SEType.Yeah);
                        break;
                }
            }
        }
        if (gameState == "ended")
        {
            if (Input.GetKey(KeyCode.Return))
            {
                lastImageParticle.GetComponent<LastImageParticleController>().FadeOut();
                lastImageMask.DOFade(1, 2)
                    .OnComplete(() =>
                    {
                        StartCoroutine(goTitle());
                    });
            }
        }
    }

    void playEvent(int? eventPrefabIndex = null)
    {
        GameObject prefab = Instantiate(eventPrefabs[eventPrefabIndex ?? eventCount], new Vector3(0, 10, 0), Quaternion.identity);
        prefab.GetComponent<BasePrefab>().Init(this);
    }

    public void countUp(int point, Vector3 target)
    {
#if UNITY_EDITOR
        if (eventDebugIndex != 0)
        {
            point *= eventDebugIndex;
        }
#endif
        GameObject heart = Instantiate(pointHeart, frontCanvas.transform);
        heart.transform.position = target;
        heart.GetComponent<PointHeartController>().Init(point);
        totalPoint += point;
        totalPointText.text = totalPoint.ToString();
        localFilePreview.GetComponent<LoadImageUtil>().LoadImageFromFileName();
        SoundManagerController.soundManager.PlaySE(Random.Range((int)SEType.Heart1, (int)SEType.Heart2));

        // パーティクル演出強化
        if (totalPoint > 5000)
        {
            if (whitePigeon.color.a == 0)
            {
                whitePigeon.DOFade(1, 2);
                SoundManagerController.soundManager.PlaySE((int)SEType.PegionFlying);
            }
        }
        if (totalPoint > 500)
        {
            CherryBlossom.GetComponent<CherryBlossonController>().updateParticle(totalPoint);
        }
        Balloon1.GetComponent<BalloonController>().updateParticle(totalPoint);
        Balloon2.GetComponent<BalloonController>().updateParticle(totalPoint);
    }

    private void ChangeBackgroundImage()
    {
        if (++imageIndex < backgroundImages.Count)
        {
            backgroundImageBack.sprite = backgroundImages[imageIndex];
            backgroundImageFront.DOFade(0, 2)
                .OnComplete(() =>
                {
                    backgroundImageFront.sprite = backgroundImages[imageIndex];
                    backgroundImageFront.color = Color.white;
                });
        }
        else
        {
            Debug.LogError("背景のリストが足りないよ！" + nameof(backgroundImages));
        }
    }

    public void StartHeartEffect()
    {
        ParticleSystem.EmissionModule emission = heart.GetComponent<ParticleSystem>().emission;
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(3.0f);
    }

    public void BigHeartApply()
    {
        SoundManagerController.soundManager.PlaySE((int)SEType.BigHeart);
        bigHeart.transform.DOScale(new Vector3(3000, 3000, 1), 1.0f)
            .OnComplete(() => bigHeart.transform.localScale = new Vector3(0, 0, 1));
        bigHeart.GetComponent<SpriteRenderer>().DOFade(0, 1.0f)
            .OnComplete(() => bigHeart.GetComponent<SpriteRenderer>().color = Color.white);
    }

    public void ChangePlayerSprite()
    {
        playerObj.GetComponent<SpriteRenderer>().sprite = ring;
        Instantiate(ringEffect, playerObj.transform);
    }

    public void Ending()
    {
        gameState = "ending";
        playerObj.GetComponent<Rigidbody2D>().simulated = false;
        DOTween.Sequence()
                .SetDelay(2.0f)
            .Append(playerObj.transform.DOMove(Vector3.zero, 1))
            .Join(playerObj.transform.DORotateQuaternion(Quaternion.identity, 1))
            .Join(totalPointText.transform.DOScale(0, 1)
                .OnComplete(() => ResultDisplay()))
            .Append(lastImageMask.DOFade(1, 2)
                .SetDelay(5.0f)
                .OnComplete(() =>
                {
                    // エンディング画面の飾りや演出を加える

                    Destroy(totalPointText);
                    Destroy(playerObj);
                    foreach (GameObject obj in destroyObjectsInEnding)
                    {
                        Destroy(obj);
                    }
                    lastImage.DOFade(1, 1)
                        .OnComplete(() =>
                        {
                            lastImageMask.DOFade(0, 2)
                                .OnComplete(() =>
                                {
                                    Instantiate(lastImageParticle);
                                    Invoke(nameof(changeEnded), 5.0f);
                                });
                        });
                }));
    }

    public void ResultDisplay()
    {
        GameObject heart = Instantiate(pointHeart, frontCanvas.transform);
        heart.transform.position = playerObj.transform.position;
        heart.GetComponent<PointHeartController>().ResultDisplay(totalPoint);
    }

    private void changeEnded()
    {
        exitAnim = exitAnnounce.DOFade(1, 2)
            .SetLoops(-1, LoopType.Yoyo);
        gameState = "ended";
    }

    private IEnumerator goTitle()
    {
        //1秒待つ
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("TitleScene");
        exitAnim.Kill();
        DOTween.KillAll();
    }
}
