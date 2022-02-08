using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeGameObjects : MonoBehaviour
{
    public void FadeIn(GameObject gameObject)
    {
        SetMaterialTransparent(gameObject);
        iTween.FadeTo(gameObject, 0, 1);
    }

    public void FadeOut(GameObject gameObject)
    {
        iTween.FadeTo(gameObject, 1, 1);
        Invoke("SetMaterialOpaque", 1.0f);
    }

    private void SetMaterialTransparent(GameObject gameObject)
    {
        try
        {
            var materials = gameObject.GetComponent<Renderer>();
            if (materials != null)
                foreach (var material in materials.materials)
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
        }
        catch{}

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
        try
        {
            var materials = gameObject.GetComponent<Renderer>();
            if (materials != null)
                foreach (var material in materials.materials)
                {
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = -1;
                }
        }catch{}

        if (gameObject.transform.childCount > 0)
        {
            foreach (Transform child in gameObject.transform)
            {
                SetMaterialOpaque(child.gameObject);
            }
        }


      
    }

 
}
