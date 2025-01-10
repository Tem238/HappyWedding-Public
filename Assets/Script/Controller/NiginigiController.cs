using System.Collections;
using UnityEngine;

public class NiginigiController : BasePrefab
{
    int point = 200;
    [SerializeField] GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        effect.transform.parent = null;
        ApplyAndDestroyUtil.FadeApplyWithEffect(gameObject, effect, effect.transform.localScale.x, () => StartCoroutine(impact()));
    }

    void Update()
    {
        effect.transform.position = transform.position;
    }

    private IEnumerator impact()
    {
        var rbody = gameObject.GetComponent<Rigidbody2D>();
        yield return new WaitForSeconds(1.0f);
        rbody.simulated = true;
        int sign = transform.position.x < 0 ? 1 : -1;
        rbody.AddForce(new Vector3(sign * Random.Range(2.0f, 5.0f), Random.Range(5.0f, 7.0f), 0), ForceMode2D.Impulse);
        rbody.AddTorque(1.0f, ForceMode2D.Impulse);
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
