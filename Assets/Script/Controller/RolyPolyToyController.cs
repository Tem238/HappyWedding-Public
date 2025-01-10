using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolyPolyToyController : BasePrefab
{
    int point = 100;
    [SerializeField] GameObject body;
    [SerializeField] GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        effect.transform.parent = null;
        body.GetComponent<SpriteRenderer>().DOFade(1, 1)
            .OnComplete(() => ApplyAndDestroyUtil.FadeDestroyWithEffect(body, effect, 20.0f, () => Destroy(gameObject)));
    }

    private void Update()
    {
        effect.transform.position = body.transform.position;
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        transform.rotation = Quaternion.Euler(0, 0, 20.0f);
        transform.position = new Vector3(Random.Range(-3.0f, 3.0f), 0, 0);
    }

    public void CountUp(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && point != 0)
        {
            GameManager.countUp(point, collision.transform.position);
            DisableEffect(effect, ref point);
        }
    }
}
