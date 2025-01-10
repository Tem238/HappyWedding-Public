using DG.Tweening;
using System.Collections;
using System.Drawing;
using UnityEngine;

public class SoccerBallController : BasePrefab
{
    int point = 100;
    float rotateSpeed = 0;
    [SerializeField] Sprite handBall;
    [SerializeField] GameObject effect;
    [SerializeField] GameObject pinkEffect;
    [SerializeField] GameObject yellowEffect;

    public void StartAttack(int attackIndex)
    {
        if(attackIndex == (int)SoccerAttackEnum.handball)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = handBall;
            point = 500;
            attackIndex = Random.Range(0, 4);
            int realPoint = point * (attackIndex == 0 ? 1 : attackIndex);
            Destroy(effect);
            if (realPoint >= 1000)
            {
                effect = Instantiate(yellowEffect, transform.position, Quaternion.identity);
            }
            else if(realPoint >= 500)
            {
                effect = Instantiate(pinkEffect, transform.position, Quaternion.identity);
            }
        }
        ApplyAndDestroyUtil.FadeApplyWithEffect(gameObject, effect, 0.3f, () => StartCoroutine(Attack(attackIndex)), 0.5f);
    }

    private void Start()
    {
        if (effect)
        {
            effect.transform.parent = null;
        }
    }

    void Update()
    {
        if (rotateSpeed >= 1 && rotateSpeed <= 2)
        {
            rotateSpeed *= 1.01f;
        }
        if (rotateSpeed >= -2 && rotateSpeed <= -1)
        {
            rotateSpeed *= 1.01f;
        }
        if (effect)
        {
            effect.transform.position = transform.position;
        }
        transform.Rotate(0, 0, rotateSpeed);
    }

    private void OnDestroy()
    {
        Destroy(effect);
    }

    private IEnumerator Attack(int attackIndex)
    {
        Sequence a = null;
        point *= attackIndex == 0 ? 1 : attackIndex;

        switch (attackIndex)
        {
            // 回転カーブシュート
            case (int)SoccerAttackEnum.curve:
                yield return new WaitForSeconds(0.5f);
                rotateSpeed = Random.value < 0.5 ? -1 : 1;
                yield return new WaitForSeconds(0.5f);
                KickSE();
                a = DOTween.Sequence()
                    .Append(transform.DOMoveX((rotateSpeed < 0 ? -1 : 1) * 5, 0.8f)
                        .SetEase(Ease.OutQuart))
                    .Append(transform.DOMoveX((rotateSpeed < 0 ? -1 : 1) * -10, 1.5f)
                        .SetEase(Ease.InOutQuart)
                        .OnPlay(() => StartCoroutine(BallMoveSE(0.3f))));
                transform.DOMoveY(-10, 3)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                        {
                            a.Kill();
                            Destroy(gameObject);
                        });
                break;
            // 直球シュート
            case (int)SoccerAttackEnum.straight:
                yield return new WaitForSeconds(1.5f);
                KickSE();
                a = DOTween.Sequence()
                    .Append(transform.DOMove(new Vector3(Random.Range(-5f, 5f), -6, 0), 1.5f)
                        .SetEase(Ease.Linear))
                    .OnComplete(() =>
                    {
                        a.Kill();
                        Destroy(gameObject);
                    });
                break;
            // 魔球シュート
            case (int)SoccerAttackEnum.magic:
                yield return new WaitForSeconds(2.0f);
                KickSE();
                a = DOTween.Sequence()
                    .Append(transform.DOMoveX(-2, 0.25f)
                        .SetEase(Ease.OutQuart))
                    .Append(transform.DOMoveX(2, 0.5f)
                        .SetEase(Ease.InOutQuart)
                        .OnPlay(() => StartCoroutine(BallMoveSE())))
                    .Append(transform.DOMoveX(-2, 0.5f)
                        .SetEase(Ease.InOutQuart)
                        .OnPlay(() => StartCoroutine(BallMoveSE())))
                    .Append(transform.DOMoveX(2, 0.5f)
                        .SetEase(Ease.InOutQuart)
                        .OnPlay(() => StartCoroutine(BallMoveSE())))
                    .Append(transform.DOMoveX(-2, 0.5f)
                        .SetEase(Ease.InOutQuart))
                    .Append(transform.DOMoveX(2, 0.5f)
                        .SetEase(Ease.InOutQuart))
                    .Append(transform.DOMoveX(-2, 0.5f)
                        .SetEase(Ease.InOutQuart)); ;
                transform.DOMoveY(-10, 3)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        a.Kill();
                        Destroy(gameObject);
                    });
                break;
            // ハンドボール
            case (int)SoccerAttackEnum.handball:
                gameObject.GetComponent<SpriteRenderer>().sprite = handBall;
                StartCoroutine(Attack(Random.Range(0, 4)));
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && point != 0)
        {
            GameManager.countUp(point, collision.transform.position);
            DisableEffect(effect, ref point);
        }
    }

    private void KickSE()
    {
        SoundManagerController.soundManager.PlaySE((int)SEType.Kick);
    }

    private IEnumerator BallMoveSE(float time = 0.0f)
    {
        if(time > 0)
        {
            yield return new WaitForSeconds(time);
        }
        SoundManagerController.soundManager.PlaySE((int)SEType.BallMove);
    }
}
