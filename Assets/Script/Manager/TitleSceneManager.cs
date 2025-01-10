using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] Image backGroundImage;
    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        LoadImageUtil.Initialize();
        SoundManagerController.soundManager.PlayBGM((int)BGMType.Title);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStart()
    {
        foreach (var item in this.startButton.GetComponentsInChildren<Graphic>())
        {
            item.DOFade(0, 1.0f);
        }
        foreach (var item in this.exitButton.GetComponentsInChildren<Graphic>())
        {
            item.DOFade(0, 1.0f);
        }
        backGroundImage.DOFade(0, 5.0f);
        backGroundImage.transform.DOScale(10, 3.0f)
            .OnComplete(() => SceneManager.LoadScene("MainScene"));
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
