using System.Collections;
using UnityEngine;

public class CardCapture : MonoBehaviour
{
    public Camera captureCam;
    LevelInitiator initiateNextLevel;

    void Start()
    {
        initiateNextLevel = gameObject.GetComponent<LevelInitiator>();
    }

    public void CaptureCard()
    {
        StartCoroutine(CaptureThenProceed());
    }

    private IEnumerator CaptureThenProceed()
    {
        yield return StartCoroutine(CaptureThis());
        yield return StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
        initiateNextLevel.NextChapter();
        yield return null;
    }

    private IEnumerator CaptureThis()
    {
        yield return new WaitForSeconds(0.5f);
        CaptureTransparentScreenshot(captureCam, 1080, 1920);
    }

    public static void CaptureTransparentScreenshot(Camera cam, int width, int height)
    {
        // This is slower, but seems more reliable.
        var bak_cam_targetTexture = cam.targetTexture;
        var bak_cam_clearFlags = cam.clearFlags;
        var bak_RenderTexture_active = RenderTexture.active;

        var tex_white = new Texture2D(width, height, TextureFormat.ARGB32, false);
        var tex_black = new Texture2D(width, height, TextureFormat.ARGB32, false);
        var tex_transparent = new Texture2D(width, height, TextureFormat.ARGB32, false);
        // Must use 24-bit depth buffer to be able to fill background.
        var render_texture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
        var grab_area = new Rect(0, 0, width, height);

        RenderTexture.active = render_texture;
        cam.targetTexture = render_texture;
        cam.clearFlags = CameraClearFlags.SolidColor;

        cam.backgroundColor = Color.black;
        cam.Render();
        tex_black.ReadPixels(grab_area, 0, 0);
        tex_black.Apply();

        cam.backgroundColor = Color.white;
        cam.Render();
        tex_white.ReadPixels(grab_area, 0, 0);
        tex_white.Apply();

        // Create Alpha from the difference between black and white camera renders
        for (int y = 0; y < tex_transparent.height; ++y)
        {
            for (int x = 0; x < tex_transparent.width; ++x)
            {
                float alpha = tex_white.GetPixel(x, y).r - tex_black.GetPixel(x, y).r;
                alpha = 1.0f - alpha;
                Color color;
                if (alpha == 0)
                {
                    color = Color.clear;
                }
                else
                {
                    color = tex_black.GetPixel(x, y) / alpha;
                }
                color.a = alpha;
                tex_transparent.SetPixel(x, y, color);
            }
        }
        ES3.SaveImage(tex_transparent, "StatCard.png");
        NativeGallery.SaveImageToGallery(tex_transparent, "PaleBlue", "StatCard", null);

        cam.clearFlags = bak_cam_clearFlags;
        cam.targetTexture = bak_cam_targetTexture;
        cam.backgroundColor = new Color32(215, 215, 215, 255);
        RenderTexture.active = bak_RenderTexture_active;
        RenderTexture.ReleaseTemporary(render_texture);

        Destroy(tex_black);
        Destroy(tex_white);
        Destroy(tex_transparent);
    }
}
