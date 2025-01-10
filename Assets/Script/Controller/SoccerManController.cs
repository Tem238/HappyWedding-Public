using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class SoccerManController : BasePrefab
{
    [SerializeField] GameObject throwPosition;
    [SerializeField] GameObject prefab;
    private float timer = 0f;
    private bool isPlay = true;
    private List<int> attacks = new List<int>
    { 
        (int)SoccerAttackEnum.curve, 
        (int)SoccerAttackEnum.curve,
        (int)SoccerAttackEnum.straight,
        (int)SoccerAttackEnum.magic,
        (int)SoccerAttackEnum.handball
    };
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().DOFade(1, 1)
            .OnComplete(() => StartAttack());
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        transform.position = new Vector3(0.6f, 3.7f, 0);
    }

    void Update()
    {
        if(attacks.Count == 0 && isPlay)
        {
            isPlay = false;
            gameObject.GetComponent<SpriteRenderer>().DOFade(0, 1)
                .SetDelay(5.0f)
                .OnComplete(() => Destroy(gameObject));
            return;
        }
        else if (isPlay)
        {
            timer += Time.deltaTime;
            if (timer > 5.0f)
            {
                StartAttack();
            }
        }
    }

    private void StartAttack()
    {
        int index = Random.Range(0, attacks.Count);
        attack(attacks[index]);
        attacks.RemoveAt(index);
    }

    private void attack(int attackIndex)
    {
        timer = 0;
        var obj = Instantiate(prefab, throwPosition.transform.position, Quaternion.identity);
        obj.GetComponent<BasePrefab>().Init(GameManager);
        obj.GetComponent<SoccerBallController>().StartAttack(attackIndex);
    }
}
