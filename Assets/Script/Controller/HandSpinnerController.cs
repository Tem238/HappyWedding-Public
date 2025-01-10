using DG.Tweening;
using UnityEngine;

public class HandSpinnerController : BasePrefab
{
    int point = 150;
    float rotateSpeed = 1;
    [SerializeField] GameObject effect;

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        rotateSpeed = Random.Range(1f, 3f);
        int x = Random.value <= 0.5 ? -11 : 11;
        float y = Random.Range(2.0f, 6.0f);
        transform.position = new Vector3(x, y, 0);
        var targetPosition = new Vector3(x < 0 ? 1 : -1 * Random.Range(4.0f, 7.0f), -7, 0);
        SoundManagerController.soundManager.PlaySE((int)SEType.Throw);
        transform.DOMove(targetPosition, Random.Range(5.0f, Random.Range(4.0f, 7.0f)))
            .OnComplete(() =>
            {
                Destroy(gameObject);
                Destroy(effect);
            });
    }

    private void Start()
    {
        effect.transform.parent = null;
    }

    void Update()
    {
        effect.transform.position = transform.position;
        transform.Rotate(0, 0, rotateSpeed);
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
