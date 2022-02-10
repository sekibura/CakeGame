using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeGameObjects : MonoBehaviour
{
    public  void FadeIn(GameObject gameObject, float time)
    {
        SetMaterialTransparent(gameObject);
        iTween.FadeTo(gameObject, 0, time);
    }

    public void FadeOut(GameObject gameObject, float time)
    {
        iTween.FadeTo(gameObject, 1, time);
        Invoke("SetMaterialOpaque", time);
    }

    private void SetMaterialTransparent(GameObject gameObject)
    {
        var materials = gameObject.GetComponent<Renderer>();
        if (materials != null)
            foreach (var material in materials.materials)
            {
                try
                {
                    material.SetFloat("_Mode", 2);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 3000;
                }
                catch { }
                    
            }
      

        if (gameObject.transform.childCount > 0)
        {
            foreach (Transform child in gameObject.transform)
            {
                SetMaterialTransparent(child.gameObject);
            }
        }

        
    }

    private void SetMaterialOpaque(GameObject gameObject)
    {
        var materials = gameObject.GetComponent<Renderer>();
        if (materials != null)
            foreach (var material in materials.materials)
            {
                try
                {
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = -1;
                }
                catch { }
            }
        

        if (gameObject.transform.childCount > 0)
        {
            foreach (Transform child in gameObject.transform)
            {
                SetMaterialOpaque(child.gameObject);
            }
        }


      
    }
    #region with delay
    public void FadeIn(GameObject gameObject, float time, float delay)
    {
        StartCoroutine(FadeInWithDelay(gameObject, time, delay));
    }

    private IEnumerator FadeInWithDelay(GameObject gameObject,float time, float delay)
    {
        yield return new WaitForSeconds(delay);
        SetMaterialTransparent(gameObject);
        iTween.FadeTo(gameObject, 0, time);
    }

    public void FadeOut(GameObject gameObject, float time, float delay)
    {
        StartCoroutine(FadeOutWithDelay(gameObject, time, delay));
    }

    private IEnumerator FadeOutWithDelay(GameObject gameObject, float time, float delay)
    {
        yield return new WaitForSeconds(delay);
        iTween.FadeTo(gameObject, 1, time);
        Invoke("SetMaterialOpaque", time);
    }

    #endregion
}
