using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class ApplyAndDestroyUtil : MonoBehaviour
{
    /// <summary>
    /// <paramref name="obj"/>��SpriteRenderer��1�b�����ĕs�����ɂ��A������<paramref name="effect"/>��Scale��<paramref name="effectScale"/>�ɂ�����A<paramref name="action"/>�����s����
    /// </summary>
    /// <param name="obj">�{�̃I�u�W�F�N�g</param>
    /// <param name="effect">�G�t�F�N�g�̃I�u�W�F�N�g</param>
    /// <param name="effectScale">�G�t�F�N�g�̃X�P�[��</param>
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
    /// <paramref name="delay"/>�b���<paramref name="obj"/>��SpriteRenderer��1�b�����ē����ɂ��A������<paramref name="effect"/>��Scale��0�ɂ�����A����Destroy����B���̌�A<paramref name="action"/>�����s�B
    /// </summary>
    /// <param name="obj">�{�̃I�u�W�F�N�g</param>
    /// <param name="effect">�G�t�F�N�g�̃I�u�W�F�N�g</param>
    /// <param name="delay">������܂ł̎���</param>
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
