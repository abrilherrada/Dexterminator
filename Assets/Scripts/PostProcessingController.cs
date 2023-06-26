using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingController : MonoBehaviour
{
    [SerializeField] private Volume postProcessingVolume;

    private IEnumerator WaitForVignetteEffect()
    {
        yield return new WaitForSeconds(2f);
        DisableVignette();
    }

    private void DisableVignette()
    {
        if (postProcessingVolume.sharedProfile.TryGet(out Vignette vignette))
        {
            vignette.intensity.value = 0f;
        }
    }

    public void UseDamagedEffect()
    {
        if(postProcessingVolume.sharedProfile.TryGet(out Vignette vignette))
        {
            vignette.intensity.value = 0.5f;
            StartCoroutine(WaitForVignetteEffect());
        }
    }

    private void Awake()
    {
        if (postProcessingVolume.sharedProfile.TryGet(out Vignette vignette))
        {
            vignette.intensity.value = 0f;
        }
    }
}
