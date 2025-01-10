using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurieGirlController : BasePrefab
{
    [SerializeField] GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        DOTween.Sequence()
            .Append(spriteRenderer.DOFade(1, 1))
            .Append(transform.DOPunchRotation(new Vector3(0, 0, 5), 2)
                .OnComplete(() => DrawBear()))
            .Append(spriteRenderer.DOFade(0, 1)
                .SetDelay(7)
                .OnComplete(() => Destroy(gameObject)));
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        transform.position = new Vector3(-7, 3, 0);
    }

    private void DrawBear()
    {
        var obj = Instantiate(prefab, new Vector3(10, 10, 0), Quaternion.identity);
        obj.GetComponent<BasePrefab>().Init(GameManager);
    }
}
