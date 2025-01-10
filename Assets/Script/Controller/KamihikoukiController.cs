using DG.Tweening;
using UnityEngine;

public class KamihikoukiController : BasePrefab
{
    int point = 100;
    [SerializeField] GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        var targetPosition = new Vector3(transform.position.x < 0 ? 1 : -1 * Random.Range(4.0f, 7.0f), -7, 0);
        SoundManagerController.soundManager.PlaySE((int)SEType.Throw);
        transform.DOMove(targetPosition, Random.Range(5.0f, 10.0f))
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        int x = Random.value <= 0.5 ? -11 : 11;
        float y = Random.Range(2.0f, 6.0f);
        transform.position = new Vector3(x, y, 0);
        if (x < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 150);
            transform.localScale = new Vector3(0.5f, -0.5f, 1);
        }
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
