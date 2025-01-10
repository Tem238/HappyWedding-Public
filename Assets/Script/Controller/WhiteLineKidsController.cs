using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteLineKidsController : BasePrefab
{
    int point = 100;
    Tweener tweener;
    [SerializeField] GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        var targetPosition = new Vector3(transform.position.x + 6, -7, 0);
        tweener = transform.DOShakeRotation(1, new Vector3(0, 0, 3)).SetLoops(-1, LoopType.Restart);
        transform.DOMove(targetPosition, Random.Range(10.0f, 15.0f))
            .OnComplete(() =>
            {
                tweener.Kill();
                Destroy(gameObject);
            });
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        transform.position = new Vector3(Random.Range(-8.0f, 1.0f), 7, 0);
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
