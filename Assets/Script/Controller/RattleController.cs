using DG.Tweening;
using UnityEngine;

public class RattleController : BasePrefab
{
    int point = 200;
    [SerializeField] GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        effect.transform.parent = null;
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        transform.position = new Vector3(Random.value <= 0.5f ? -10 : 10, -4, 0);
        Invoke(nameof(impact), 1);
    }

    private void Update()
    {
        effect.transform.position = transform.position;
    }

    private void impact()
    {
        SoundManagerController.soundManager.PlaySE((int)SEType.Rattle);
        var rbody = gameObject.GetComponent<Rigidbody2D>();
        rbody.simulated = true;
        rbody.AddForce(new Vector3((transform.position.x < 0 ? 1 : -1) * Random.Range(3.0f, 8.0f), 10, 0), ForceMode2D.Impulse);
        rbody.AddTorque(1.0f, ForceMode2D.Impulse);
        ApplyAndDestroyUtil.FadeDestroyWithEffect(gameObject, effect, 6.0f);
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
