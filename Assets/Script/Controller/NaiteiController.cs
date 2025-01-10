using DG.Tweening;
using System.Collections;
using UnityEngine;

public class NaiteiController : BasePrefab
{
    int point = 500;
    [SerializeField] GameObject effect;
    private bool SEPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(Vector3.zero, 5)
            .OnComplete(() =>
            {
                ApplyAndDestroyUtil.FadeDestroyWithEffect(gameObject, effect, 4.0f);
            });
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        transform.position = new Vector3(0, -10, 0);
    }

    private void Update()
    {
        if(!SEPlayed && transform.position.y > -8)
        {
            SEPlayed = true;
            SoundManagerController.soundManager.PlaySE((int)SEType.Naitei);
        }
    }

    private IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(4.0f);
        transform.GetComponent<SpriteRenderer>().DOFade(0, 1)
            .OnComplete(() => Destroy(gameObject));
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
