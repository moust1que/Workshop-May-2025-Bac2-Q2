using Attribute.Runtime;
using Events.Runtime;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BookTurnPage : MonoBehaviour
{

    //! les pages en partant de l'index 0 lorsqu'elles sont à droites doivent avoir la valeur la plus éloigné de 1 (donc page 0 est une float de 0.8 et toute les index qui suivent aurront une float égal 0.8 += 0.01)
    //! les pages en partant de l'index 0 lorsqu'elles sont à gauche doivent avoir la valeur la plus proche de 0 (donc page 0 est une float de 0.1 et toute les index qui suivent aurront une float égal 0.1 += 0.01)
    public List<GameObject> pages = new();
    private int currentPageIndex = 0;


    private bool isAnimating = false;
    private float shaderValue = 0f;
    private float animationStep = 0.1f;

    private MaterialPropertyBlock propertyBlock;
    private Renderer currentRenderer;
    private float animationTarget;
    private float animationStart;

    private void Start()
    {
        propertyBlock = new MaterialPropertyBlock();
        ResetPages();
        GameEvents.OnTurnPageLeft += TurnPageLeft;
        GameEvents.OnDisableFeature += TurnPageRight;
    }

    public void TurnPageLeft()
    {
        if (isAnimating) return;
        if (currentPageIndex >= pages.Count - 1) return;

        currentRenderer = pages[currentPageIndex].GetComponent<Renderer>();
        if (currentRenderer == null) return;

        animationStart = GetValue(currentPageIndex);
        animationTarget = animationStart + 0.993f;
        shaderValue = animationStart;
        isAnimating = true;

        AnimateShaderValue();
        currentPageIndex++;
    }


    public void TurnPageRight()
    {
        if (isAnimating) return;
        if (currentPageIndex <= 0) return;

        currentRenderer = pages[currentPageIndex - 1].GetComponent<Renderer>();
        if (currentRenderer == null) return;

        animationStart = GetValue(currentPageIndex - 1);
        animationTarget = animationStart - 0.993f;
        shaderValue = animationStart;
        isAnimating = true;

        AnimateShaderValue();
        currentPageIndex--;
    }

    private float GetValue(int index)
    {
        MaterialPropertyBlock block = new();
        pages[index].GetComponent<Renderer>().GetPropertyBlock(block);
        return block.GetFloat("_Turn");
    }

    public void AnimateShaderValue()
    {
        if (!isAnimating || currentRenderer == null)
            return;

        if (DelayManager.instance == null)
        {
            Debug.LogWarning("DelayManager.instance is null! Cannot animate.");
            isAnimating = false;
            return;
        }

        if ((animationStart < animationTarget && shaderValue >= animationTarget) ||
            (animationStart > animationTarget && shaderValue <= animationTarget))
        {
            shaderValue = animationTarget;
            ApplyShaderValue(shaderValue);
            isAnimating = false;
            return;
        }
        else
        {
            shaderValue += (animationTarget > animationStart ? animationStep : -animationStep);
            shaderValue = Mathf.Clamp01(shaderValue);
            ApplyShaderValue(shaderValue);

            DelayManager.instance.Delay(0.01f, AnimateShaderValue);
        }
    }

    private void ApplyShaderValue(float value)
    {
        currentRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat("_Turn", value);
        currentRenderer.SetPropertyBlock(propertyBlock);
    }


    public void ResetPages()
    {
        currentPageIndex = 0;
        int pageId = 7;

        foreach (var page in pages)
        {
            pageId--;
            var renderer = page.GetComponent<Renderer>();
            if (renderer == null) continue;

            var block = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(block);
            block.SetFloat("_Turn", 0.001f * pageId);
            renderer.SetPropertyBlock(block);
        }
    }
    private void OnDestroy()
    {
        GameEvents.OnTurnPageLeft -= TurnPageLeft;
        GameEvents.OnDisableFeature -= TurnPageRight;
    }
}
