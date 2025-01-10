using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BabyCarController : BasePrefab
{
    [SerializeField] int point = 200;
    [SerializeField] float minSpeed = 10.0f;
    [SerializeField] float maxSpeed = 15.0f;
    [SerializeField] GameObject effect;
    [SerializeField] bool isBicycle = false;

    // Start is called before the first frame update
    void Start()
    {
        var targetPosition = new Vector3(transform.position.x < 0 ? 1 : -1 * Random.Range(1.0f, 5.0f), -7, 0);
        transform.DOMove(targetPosition, Random.Range(minSpeed, maxSpeed))
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
        if(x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, 1);
        }
    }

    private void Update()
    {
        if(isBicycle && transform.position.y < 0)
        {
            isBicycle = false;
            SoundManagerController.soundManager.PlaySE((int)SEType.BicycleBell);
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
