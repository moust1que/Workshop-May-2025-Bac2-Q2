using Attribute.Runtime;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BookTurnPage : MonoBehaviour
{
    public static List<GameObject> pages = new();
    private static int currentPageIndex = 0;
    private static float ShaderValue = 0f;
    private static Renderer instRenderer;
    private static MaterialPropertyBlock propertyBlock;
    public static void TurnPageLeft()
    {

        if (currentPageIndex > 0)
        {
            Renderer renderer = pages[currentPageIndex].GetComponent<Renderer>();
            AnimateShader(renderer, 1f, 0f); // Lerp from 1 to 0

            //pages[currentPageIndex].SetActive(false);
            currentPageIndex++;
            //pages[currentPageIndex].SetActive(true);

        }
    }
    public static void TurnPageRight()
    {
        if (currentPageIndex < pages.Count - 1)
        {
            Renderer renderer = pages[currentPageIndex].GetComponent<Renderer>();
            AnimateShader(renderer, 0f, 1f); // Lerp from 1 to 0

            //pages[currentPageIndex].SetActive(false);
            currentPageIndex--;
            //pages[currentPageIndex].SetActive(true);

        }


    }


    private static void AnimateShader(Renderer renderer, float startValue, float endValue)
    {
        if (propertyBlock == null)
            propertyBlock = new MaterialPropertyBlock();

        float step = 0.1f;
        float currentValue = startValue;

        void StepAnimation()
        {
            
            if ((startValue > endValue && currentValue <= endValue) ||
                (startValue < endValue && currentValue >= endValue))
            {
                currentValue = endValue;
            }
            else
            {
                currentValue += (endValue > startValue ? step : -step);
                currentValue = Mathf.Clamp01(currentValue);
                DelayManager.instance.Delay(0.01f, StepAnimation);
            }

            renderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetFloat("_Fold", currentValue); 
            renderer.SetPropertyBlock(propertyBlock);
        }

        StepAnimation(); 
    }
}
