using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    #region 定数宣言



    #endregion

    #region 変数宣言

    /// <summary>
    /// 右側のプレイヤー
    /// </summary>
    [SerializeField] GameObject playerRight;
    /// <summary>
    /// 左側のプレイヤー
    /// </summary>
    [SerializeField] GameObject playerLeft;
    Tweener playerRightTweener;
    Tweener playerLeftTweener;
    /// <summary>
    /// 回転速度倍率
    /// </summary>
    float rotateSpeed = 15.0f;
    /// <summary>
    /// シーソーZ軸の最大ローテーション
    /// </summary>
    float maxZRotation = 7.0f;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerRightTweener = playerLeft.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 1.0f), 1.0f);
        playerLeftTweener = playerRight.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 1.0f), 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float input;
        if (GameManager.gameState == "playing")
        {
            input = Input.GetAxis("Ctrl") * Time.deltaTime * rotateSpeed;
        }
        else
        {
            input = 0;
        }
        if(input == 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime);
        }
        else if(Mathf.Abs(transform.rotation.z * Mathf.Rad2Deg + input) <= maxZRotation)
        {
            transform.Rotate(0, 0, input);
        }
        
        if(input > 0)
        {
            if (!playerRightTweener.active)
            {
                playerRightTweener = playerLeft.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 1.0f), 1.0f);
            }
        }
        else if(input < 0)
        {
            if (!playerLeftTweener.active)
            {
                playerLeftTweener = playerRight.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 1.0f), 1.0f);
            }
        }
    }
}
