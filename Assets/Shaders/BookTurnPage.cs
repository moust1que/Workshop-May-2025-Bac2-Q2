using Attribute.Runtime;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BookTurnPage : MonoBehaviour
{
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
    }


    public void TurnPageLeft()
    {
        if (isAnimating) return;
        if (currentPageIndex >= pages.Count - 1) return;

        currentRenderer = pages[currentPageIndex].GetComponent<Renderer>();
        if (currentRenderer == null) return;

        animationStart = 0f;
        animationTarget = 1f;
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

        animationStart = 1f;
        animationTarget = 0f;
        shaderValue = animationStart;
        isAnimating = true;

        AnimateShaderValue();
        currentPageIndex--;
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

        foreach (var page in pages)
        {
            var renderer = page.GetComponent<Renderer>();
            if (renderer == null) continue;

            var block = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(block);
            block.SetFloat("_Turn", 0f);
            renderer.SetPropertyBlock(block);
        }
    }
}
