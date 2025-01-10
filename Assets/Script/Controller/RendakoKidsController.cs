using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendakoKidsController : BasePrefab
{
    int point = 300;
    [SerializeField] GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        var targetPosition = new Vector3(transform.position.x < 0 ? 1 : -1 * Random.Range(1.0f, 5.0f), -8, 0);
        transform.DOMove(targetPosition, Random.Range(10.0f, 15.0f))
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        int x = Random.value <= 0.5 ? -11 : 11;
        float y = Random.Range(2.0f, 7.0f);
        transform.position = new Vector3(x, y, 0);
        if (x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, 1);
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
