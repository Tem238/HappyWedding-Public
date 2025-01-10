using UnityEngine;

public class ArrowController : BasePrefab
{
    int point = 1000;
    float speed = 2.0f;
    private GameObject player;
    [SerializeField] GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        effect.transform.parent = null;
        ApplyAndDestroyUtil.FadeApplyWithEffect(gameObject, effect, effect.transform.localScale.x);
        SoundManagerController.soundManager.PlaySE((int)SEType.ArrowShoot);
    }

    void Update()
    {
        Vector3 diff = (player.transform.position - gameObject.transform.position);
        this.transform.rotation = Quaternion.FromToRotation(Vector3.left, diff);
        diff.Normalize();
        transform.position = transform.position + diff * Time.deltaTime * speed;
        effect.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && point != 0)
        {
            SoundManagerController.soundManager.PlaySE((int)SEType.ArrowHit, 2.0f);
            GameManager.countUp(point, collision.transform.position);
            GameManager.StartHeartEffect();
            point = 0;
            ApplyAndDestroyUtil.FadeDestroyWithEffect(gameObject, effect, 0, SoundEffectPlay);
        }
    }

    private void SoundEffectPlay()
    {
        SoundManagerController.soundManager.PlaySE((int)SEType.ArrowHit2, 2.0f);
    }
}
