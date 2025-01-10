using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NyuusiController : BasePrefab
{
    int point = 200;
    [SerializeField] GameObject effect;
    [SerializeField] bool isNyuusi = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isNyuusi)
        {
            SoundManagerController.soundManager.PlaySE((int)SEType.Nyuusi);
        }
        else
        {
            SoundManagerController.soundManager.PlaySE((int)SEType.GameKids, 5.0f);
        }
        DOTween.Sequence()
            .Append(gameObject.GetComponent<SpriteRenderer>().DOFade(1, 1))
            .OnComplete(() =>
            {
                ApplyAndDestroyUtil.FadeDestroyWithEffect(gameObject, effect, 5.0f);
            });
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        transform.position = new Vector3(Random.value <= 0.5 ? -4 : 4, -0.5f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && point != 0)
        {
            GameManager.countUp(point, collision.transform.position);
            DisableEffect(effect, ref point);
        }
    }
}
