using DG.Tweening;
using UnityEngine;

public class BasePrefab: MonoBehaviour
{
    protected GameManager GameManager;
    protected Tweener effectAnim;

    public virtual void Init(GameManager gameManager) 
    {
        GameManager = gameManager;
    }

    protected void DisableEffect(GameObject effect, ref int point)
    {
        point = 0;
        effectAnim = effect.transform.DOScale(Vector3.zero, 1.0f);
    }

    protected void EnableEffect(GameObject effect, float effectScale = 1.0f)
    {
        if (effectAnim != null)
        {
            effectAnim.Kill();
        }
        effectAnim = effect.transform.DOScale(new Vector3(effectScale, effectScale, effectScale), 1.0f);
    }
}
