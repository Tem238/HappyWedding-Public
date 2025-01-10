using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastImageParticleController : MonoBehaviour
{
    [SerializeField] SpriteRenderer message;
    [SerializeField] List<GameObject> crackers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().DOFade(1, 3);
        message.DOFade(1, 3);
        // ���̃N���b�J�[
        StartCoroutine(Cracker(crackers[0], 0));
        // �E�̃N���b�J�[
        StartCoroutine(Cracker(crackers[1], 0.2f));
    }

    private IEnumerator Cracker(GameObject cracker, float waitTime)
    {
        if(waitTime > 0)
        {
            yield return new WaitForSeconds(waitTime);
        }
        // �E�̃N���b�J�[
        SoundManagerController.soundManager.PlaySE((int)SEType.Cracker);
        cracker.GetComponent<SpriteRenderer>().DOFade(0, 1)
            .SetDelay(5 - waitTime)
            .OnComplete(() => Destroy(cracker));
    }

    public void FadeOut()
    {
        gameObject.GetComponent<SpriteRenderer>().DOFade(0, 2);
        message.DOFade(0, 2);
    }

    
}
