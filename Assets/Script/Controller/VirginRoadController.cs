using DG.Tweening;
using System.Collections;
using UnityEngine;

public class NewBehaviourScript : BasePrefab
{
    [SerializeField] GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        SoundManagerController.soundManager.PlayBGM((int)BGMType.ChapelBell);
        DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0, 0, -10.0f), 1)
                .SetEase(Ease.InOutQuart))
            .Append(transform.DORotate(new Vector3(0, 0, 10.0f), 2)
                .SetLoops(9, LoopType.Yoyo)
                .SetEase(Ease.InOutQuart))
            .Append(transform.DORotate(new Vector3(0, 0, 0), 1)
                .SetEase(Ease.InOutQuart));
        transform.DOMoveY(-1.8f, 20.0f);
    }

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        transform.position = new Vector3(0, 10, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.BigHeartApply();
            GameManager.ChangePlayerSprite();
            ApplyAndDestroyUtil.FadeDestroyWithEffect(gameObject, effect, 0);
            GameManager.Ending();
        }
    }

    private IEnumerator SetEnding()
    {
        yield return new WaitForSeconds(2.0f);
        GameManager.Ending();
    }

    private void Ending()
    {
        GameManager.Ending();
    }
}
