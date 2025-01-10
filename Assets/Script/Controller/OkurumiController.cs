using DG.Tweening;
using UnityEngine;

public class OkurumiController : BasePrefab
{
    int point = 100;
    [SerializeField] GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        var RotateTween = transform.DORotate(new Vector3(0, 0, 20.0f), 2.0f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuart);
        transform.DOMoveY(-10f, 20.0f)
            .OnComplete(() =>
            {
                RotateTween.Kill();
                Destroy(gameObject);
            });
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        transform.position = new Vector3(Random.Range(-4.5f, 4.5f), 10, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && point != 0)
        {
            GameManager.countUp(point, collision.transform.position);
            DisableEffect(effect, ref point);
        }
    }
}
