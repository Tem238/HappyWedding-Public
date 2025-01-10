using DG.Tweening;
using UnityEngine;

public class StarController : BasePrefab
{
    int point = 1000;
    [SerializeField] GameObject particleStar;
    private GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        effect = Instantiate(particleStar, transform.position, Quaternion.identity);
        effect.transform.localScale = Vector3.zero;
        SoundManagerController.soundManager.PlaySE((int)SEType.StarGazer);
        DOTween.Sequence()
            .Append(effect.transform.DOScale(new Vector3(0.5f, 0.5f, 1), 1.2f))
            .Join(transform.DOScale(new Vector3(0.5f, 0.5f, 1), 1.2f))
            .Append(transform.DOMove(new Vector3(transform.position.x < 0 ? 5.0f : -5.0f, -10, 0), 3f)
            .OnComplete(() =>
            {
                Destroy(gameObject);
                Destroy(effect);
            }));
    }

    void Update()
    {
        effect.transform.position = transform.position;
        transform.Rotate(0, 0, 500 * Time.deltaTime);
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
