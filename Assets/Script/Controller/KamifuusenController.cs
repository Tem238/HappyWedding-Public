using System.Collections;
using UnityEngine;

public class KamifuusenController : BasePrefab
{
    int point = 200;
    [SerializeField] GameObject effect;

    private void Start()
    {
        effect.transform.parent = null;
    }

    private void Update()
    {
        effect.transform.position = transform.position;
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        transform.position = new Vector3(Random.value <= 0.5 ? -12 : 12, -4, 0);
        StartCoroutine(impact());
    }

    private IEnumerator impact()
    {
        yield return new WaitForSeconds(1.0f);
        var rbody = gameObject.GetComponent<Rigidbody2D>();
        rbody.simulated = true;
        int rnd = (transform.position.x < 0 ? 1 : -1);
        rbody.AddForce(new Vector3(rnd * Random.Range(4, 9), 10, 0), ForceMode2D.Impulse);
        rbody.AddTorque(rnd * 20.0f, ForceMode2D.Impulse);
        SoundManagerController.soundManager.PlaySE((int)SEType.Throw);
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
