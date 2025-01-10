using DG.Tweening;
using System.Collections;
using UnityEngine;

public class SnowboardController : BasePrefab
{
    int point = 100;
    [SerializeField] Sprite girlsMode;
    [SerializeField] GameObject effect;
    bool canRotate = true;

    // Start is called before the first frame update
    void Start()
    {
        effect.transform.parent = null;
        var rbody = gameObject.GetComponent<Rigidbody2D>();
        rbody.simulated = true;
        rbody.AddForce(new Vector3(transform.position.x < 0 ? 5 : -5, 13, 0), ForceMode2D.Impulse);
        SoundManagerController.soundManager.PlaySE((int)SEType.SnowBoardJump);
    }

    private void Update()
    {
        effect.transform.position = transform.position;
        if (canRotate && transform.position.y > 0)
        {
            canRotate = false;
            if(Random.Range(0f, 1f) > 0.75f)
            {
                SoundManagerController.soundManager.PlaySE((int)SEType.SnowBoardRotate);
                transform.DOLocalRotate(new Vector3(0, 0, 180), 0.2f)
                    .SetEase(Ease.Linear)
                    .SetLoops(2, LoopType.Incremental);
            }
        }
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        transform.position = new Vector3(Random.value < 0.5 ? -7 : 7, -7, 0);
        if(transform.position.x < 0)
        {
            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = girlsMode;
            spriteRenderer.flipX = true;
        }
        StartCoroutine(DestroyThis());
    }

    private IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
        Destroy(effect);
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
