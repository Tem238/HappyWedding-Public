using DG.Tweening;
using UnityEngine;

public class CrayonBearController : BasePrefab
{
    [SerializeField] GameObject Crayon;
    [SerializeField] GameObject effect;

    int point = 200;
    private CircleCollider2D trigger;
    private Tweener effectTweener;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Sequence()
            .Append(Crayon.GetComponent<SpriteRenderer>().DOFade(1, 1)
                .OnComplete(() =>
                {
                    effectTweener = effect.transform.DOScale(1.0f, 4.0f);
                    trigger.enabled = true;
                }))
            .Append(Crayon.transform.DOLocalMoveY(1.0f, 1.0f)
                .SetLoops(4, LoopType.Yoyo)
                .SetEase(Ease.InOutQuart))
            .Join(Crayon.transform.DOLocalMoveX(1.0f, 4.0f))
            .OnComplete(() =>
            {
                Crayon.GetComponent<SpriteRenderer>().DOFade(0, 1);
                ApplyAndDestroyUtil.FadeDestroyWithEffect(gameObject, effect, 1.0f);
            });
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        trigger = gameObject.GetComponent<CircleCollider2D>();
        trigger.enabled = false;
        transform.position = new Vector3(Random.value <= 0.5 ? -3.5f : 3.5f, -0.5f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && point != 0 && effectTweener != null)
        {
            GameManager.countUp(point, collision.transform.position);
            effectTweener.Kill();
            DisableEffect(effect, ref point);
        }
    }
}
