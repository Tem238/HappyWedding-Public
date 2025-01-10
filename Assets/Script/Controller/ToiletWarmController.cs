using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ToiletWarmController : BasePrefab
{
    int point = 777;
    [SerializeField] ParticleSystem warmEffect;
    [SerializeField] GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        SoundManagerController.soundManager.PlaySE((int)SEType.Kirakira);
        DOTween.Sequence()
            .Append(gameObject.transform.DOScale(new Vector3(0.7f, 0.7f, 1), 1))
            .Append(gameObject.transform.DOScale(new Vector3(0, 0, 0), 1).SetDelay(5))
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        transform.position = new Vector3(4, -2.3f, 0);
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
