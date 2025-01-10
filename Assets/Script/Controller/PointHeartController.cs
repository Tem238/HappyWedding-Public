using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PointHeartController : MonoBehaviour
{
    [SerializeField] Text pointText;
    // Start is called before the first frame update
    void Start()
    {
        print("ポイント表記でた");
    }

    public void Init(int point)
    {
        pointText.text = point.ToString();
        DOTween.Sequence()
            .Append(transform.DOScale(2.0f, 2.0f))
            .Join(transform.DOMoveY(1.5f, 2.0f))
            .OnComplete(() => Destroy(gameObject));
    }

    public void ResultDisplay(int point)
    {
        transform.localScale = Vector3.zero;
        pointText.text = point.ToString();
        Invoke(nameof(SoundEffect), 1.0f);
        DOTween.Sequence()
            .Append(transform.DOScale(2.0f, 2.0f))
            .Join(transform.DOMoveY(1.5f, 2.0f))
            .Append(transform.DOScale(3, 0.5f)
                .SetLoops(4, LoopType.Yoyo))
            .OnComplete(() => transform.DOScale(0, 1.0f).OnComplete(() => Destroy(gameObject)))
                .SetDelay(1.0f);
    }

    private void SoundEffect()
    {
        SoundManagerController.soundManager.PlaySE((int)SEType.ResultHeart);
    }
}
