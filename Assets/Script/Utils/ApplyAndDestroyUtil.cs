using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class ApplyAndDestroyUtil : MonoBehaviour
{
    /// <summary>
    /// <paramref name="obj"/>のSpriteRendererを1秒かけて不透明にし、同時に<paramref name="effect"/>のScaleを<paramref name="effectScale"/>にした後、<paramref name="action"/>を実行する
    /// </summary>
    /// <param name="obj">本体オブジェクト</param>
    /// <param name="effect">エフェクトのオブジェクト</param>
    /// <param name="effectScale">エフェクトのスケール</param>
    public static void FadeApplyWithEffect(GameObject obj, GameObject effect, float effectScale = 1.0f, Action action = null, float fadeTime = 1.0f)
    {
        effect.transform.localScale = Vector3.zero;
        DOTween.Sequence()
            .Append(obj.GetComponent<SpriteRenderer>().DOFade(1, fadeTime))
            .Join(effect.transform.DOScale(new Vector3(effectScale, effectScale, effectScale), 1.0f))
            .OnComplete(() =>
            {
                if (action != null) action();
            });
    }

    /// <summary>
    /// <paramref name="delay"/>秒後に<paramref name="obj"/>のSpriteRendererを1秒かけて透明にし、同時に<paramref name="effect"/>のScaleを0にした後、両方Destroyする。その後、<paramref name="action"/>を実行。
    /// </summary>
    /// <param name="obj">本体オブジェクト</param>
    /// <param name="effect">エフェクトのオブジェクト</param>
    /// <param name="delay">消えるまでの時間</param>
    public static void FadeDestroyWithEffect(GameObject obj, GameObject effect, float delay, Action action = null)
    {
        DOTween.Sequence()
            .Append(obj.GetComponent<SpriteRenderer>().DOFade(0, 1))
            .Join(effect.transform.DOScale(Vector3.zero, 1.0f))
            .SetDelay(delay)
            .OnComplete(() =>
            {
                Destroy(effect);
                Destroy(obj);
                if(action != null) action();
            });
    }

    public static IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
