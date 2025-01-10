using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBabyController : BasePrefab
{
    [SerializeField] GameObject throwPoint;
    [SerializeField] GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if(transform.position.x < 0)
        {
            spriteRenderer.transform.localScale = new Vector3(-1, 1, 1);
        }
        SoundManagerController.soundManager.PlaySE((int)SEType.ThrowBaby, 2.0f);
        DOTween.Sequence()
            .Append(spriteRenderer.DOFade(1, 1))
            .Append(transform.DOPunchRotation(new Vector3(0, 0, 5), 2)
                .OnComplete(() => attack()))
            .Append(spriteRenderer.DOFade(0, 1)
                .SetDelay(3)
                .OnComplete(() => Destroy(gameObject)));
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
    }
}
