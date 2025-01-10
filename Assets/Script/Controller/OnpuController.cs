using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class OnpuController : BasePrefab
{
    int point = 50;
    Tweener RotateTween;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        SoundManagerController.soundManager.PlaySE((int)SEType.Castanet);
        RotateTween = transform.DORotate(new Vector3(0, 0, 10.0f), 0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuart);
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        int index = Random.Range(0, sprites.Count);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index];
        transform.rotation = Quaternion.Euler(0, 0, -10.0f);
        impact();
    }

    private void impact()
    {
        var rbody = gameObject.GetComponent<Rigidbody2D>();
        rbody.simulated = true;
        rbody.AddForce(new Vector3(Random.Range(-3.0f, 3.0f), 2.0f, 0), ForceMode2D.Impulse);
        Invoke(nameof(destroyThis), 5.0f);
    }

    void destroyThis()
    {
        RotateTween.Kill();
        Destroy(gameObject);
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
