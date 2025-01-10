using UnityEngine;

public class ChuuniKidsController : BasePrefab
{
    int point = 200;
    int? initialPoint = null;
    bool canWape = false;
    int wapeCount = 0;
    float timer = 0;
    private CapsuleCollider2D trigger;
    private float initialEffectScale;
    [SerializeField] ParticleSystem wapeEffect;
    [SerializeField] GameObject effect;

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        initialPoint = point;
        initialEffectScale = effect.transform.localScale.x;
        trigger = gameObject.GetComponent<CapsuleCollider2D>();
        SoundManagerController.soundManager.PlaySE((int)SEType.Warp, 2.0f);
        Warp();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            timer = 0;
            canWape = true;
        }
        if (canWape)
        {
            canWape = false;
            Doron();
        }
    }

    private void Doron()
    {
        var effectObj = Instantiate(wapeEffect, transform.position, Quaternion.identity);
        effectObj.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        ParticleSystem.MainModule main = effectObj.GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Destroy;
        SoundManagerController.soundManager.PlaySE((int)SEType.Warp, 2.0f);
        if (++wapeCount > 5)
        {
            Destroy(gameObject);
        }
        else
        {
            Warp();
        }
    }

    private void Warp()
    {
        trigger.enabled = false;
        float x = Random.Range(-4.0f, 4.0f);
        float y;
        if (x >= -2 && x <= 2)
        {
            // シーソー中央寄りの場合
            y = Random.Range(-2.3f, -1.5f);
        }
        else
        {
            y = Random.Range(-2.3f, 0);
        }
        transform.position = new Vector3(x, y, 0);
        wapeEffect.Play();
        point = (int)initialPoint;
        EnableEffect(effect, initialEffectScale);
        trigger.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && point != 0 && initialPoint != null)
        {
            GameManager.countUp(point, collision.transform.position);
            DisableEffect(effect, ref point);
        }
    }
}
