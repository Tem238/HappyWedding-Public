using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// RawImage�̑傫����ς���g�����\�b�h
/// </summary>
public static class FixAspectExtensions
{
    /// <summary>
    /// �A�X�y�N�g��ɍ��킹��RawImage�̃T�C�Y���C������
    /// ���݂�UI�T�C�Y����ƂȂ�
    /// </summary>
    public static void FixAspect(this RawImage image)
    {
        image.FixAspect(image.rectTransform.rect.size);
    }
    /// <summary>
    /// �A�X�y�N�g��ɍ��킹��RawImage�̃T�C�Y���C������
    /// </summary>
    /// <param name="originalSize">��ƂȂ�UI�T�C�Y</param>
    public static void FixAspect(this RawImage image, Vector3 originalSize)
    {
        var textureSize = new Vector2(image.texture.width, image.texture.height);

        var heightScale = originalSize.y / textureSize.y;
        var widthScale = originalSize.x / textureSize.x;
        var rectSize = textureSize * Mathf.Min(heightScale, widthScale);

        var anchorDiff = image.rectTransform.anchorMax - image.rectTransform.anchorMin;
        var parentSize = (image.transform.parent as RectTransform).rect.size;
        var anchorSize = parentSize * anchorDiff;

        image.rectTransform.sizeDelta = rectSize - anchorSize;
    }
}

