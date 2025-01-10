using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGazerController : BasePrefab
{
    [SerializeField] GameObject throwPoint;
    [SerializeField] GameObject prefab;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (transform.position.x < 0)
        {
            spriteRenderer.transform.localScale = new Vector3(-1, 1, 1);
        }
        spriteRenderer.DOFade(1, 1)
            .OnComplete(() => Invoke(nameof(attack),1.0f));
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        transform.position = new Vector3(Random.value <= 0.5 ? -7 : 7, 2, 0);
    }

    private void attack()
    {
        var obj = Instantiate(prefab, throwPoint.transform.position, Quaternion.identity);
        obj.GetComponent<BasePrefab>().Init(GameManager);
        Invoke(nameof(DestroyThis), 4.0f);
    }

    private void DestroyThis()
    {
        spriteRenderer.DOFade(0, 1)
            .OnComplete(() => Destroy(gameObject));
    }
}
