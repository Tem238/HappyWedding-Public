using DG.Tweening;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadImageUtil : MonoBehaviour
{
    [SerializeField] GameObject mask;
    //private Vector2 initialSize;
    //private static string localImagePath = Path.Combine(Application.dataPath, "LocalImage");
    private static List<Sprite> commonImages = new List<Sprite>();
    //private static int fileIndex = 0;
    public static bool isPreview = false;

    public static void Initialize()
    {
        if(commonImages.Count == 0)
        {
            commonImages.AddRange(Resources.LoadAll<Sprite>("Images/Common/"));
            foreach (Sprite file in commonImages)
            {
                Debug.Log(file.name);
            }
        }
    }

    public void LoadImageFromFileName()
    {
        if (isPreview)
        {
            return;
        }
        Initialize();
        isPreview = true;
        this.mask.transform.localPosition = new Vector3(Random.Range(-500.0f, 500.0f), Random.Range(-100.0f, 100.0f), 0);
        int index = Random.Range(0, commonImages.Count);
        var image = gameObject.GetComponent<Image>();
        image.sprite = commonImages[index];
        commonImages.RemoveAt(index);
        Vector2 size = image.sprite.rect.size;
        if(size.x > size.y)
        {
            size = new Vector2(800.0f, 800.0f * size.y / size.x);
        }
        else
        {
            size = new Vector2(800.0f * size.x / size.y, 800.0f);
        }
        mask.GetComponent<RectTransform>().sizeDelta = size;
        DOTween.Sequence()
            .Append(image.DOFade(1, 1))
            .Append(image.DOFade(0, 1).SetDelay(3))
            .OnComplete(() => isPreview = false);
    }

    /*public void LoadImageFromFileName()
    {
        if (isPreview || files.Count == 0)
        {
            return;
        }
        isPreview = true;
        this.mask.transform.localPosition = new Vector3(Random.Range(-500.0f, 500.0f), Random.Range(-100.0f, 100.0f), 0);
        if (fileIndex >= files.Count)
        {
            fileIndex = 0;
        }
        var texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
        var bytes = File.ReadAllBytes(files[fileIndex++]);
        texture.LoadImage(bytes);
        var rawImage = gameObject.GetComponent<RawImage>();
        rawImage.texture = texture;
        rawImage.FixAspect(initialSize);
        this.mask.GetComponent<RectTransform>().sizeDelta = rawImage.rectTransform.sizeDelta;
        DOTween.Sequence()
            .Append(rawImage.DOFade(1, 1))
            .Append(rawImage.DOFade(0, 1).SetDelay(3))
            .OnComplete(() => isPreview = false);
    }*/
}