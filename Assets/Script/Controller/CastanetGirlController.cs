using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastanetGirlController : BasePrefab
{
    [SerializeField] GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        DOTween.Sequence()
            .Append(spriteRenderer.DOFade(1, 1))
            .Append(transform.DORotate(new Vector3(0, 0, -20.0f), 0.5f)
                .SetEase(Ease.InOutQuart)
                .OnStart(() => attack())
                .OnStepComplete(() => attack()))
            .Append(transform.DORotate(new Vector3(0, 0, 20.0f), 1.0f)
                .OnStepComplete(() => attack())
                .SetLoops(4, LoopType.Yoyo)
                .SetEase(Ease.InOutQuart))
            .Join(transform.DOMoveX(2.5f, 2.0f)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.InOutQuart))
            .Append(spriteRenderer.DOFade(0, 1)
                .SetDelay(1)
                .OnComplete(() => Destroy(gameObject)));
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        transform.position = new Vector3(-2.5f, 3.0f, 0);
    }

    private void attack()
    {
        var obj = Instantiate(prefab, transform.position, Quaternion.identity);
        obj.GetComponent<BasePrefab>().Init(GameManager);
    }
}
