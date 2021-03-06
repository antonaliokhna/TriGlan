using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GlowMeneger : MonoBehaviour
{
    public Bloom bloom;
    public ChromaticAberration chromaticAberration;
    public AutoExposure autoExposure;
    public LensDistortion lensDistortion;

    public bool isAnimation = false;
    void Start()
    {
        this.gameObject.GetComponent<PostProcessVolume>().profile.TryGetSettings(out bloom);
        this.gameObject.GetComponent<PostProcessVolume>().profile.TryGetSettings(out autoExposure);
        this.gameObject.GetComponent<PostProcessVolume>().profile.TryGetSettings(out chromaticAberration);
        this.gameObject.GetComponent<PostProcessVolume>().profile.TryGetSettings(out lensDistortion);
    }

    void FixedUpdate()
    {
        if (isAnimation)
        {
            if (autoExposure.maxLuminance.value < 0f) 
                autoExposure.maxLuminance.value += 0.05f;

            if (lensDistortion.intensity.value < -25f)
                lensDistortion.intensity.value += 1f;

            if (bloom.threshold.value < 1f)
                bloom.threshold.value += 0.01f;

            if (chromaticAberration.intensity.value > 0.4f)
                chromaticAberration.intensity.value -= 0.01f;

            if (autoExposure.maxLuminance.value > 0f && lensDistortion.intensity.value > -25f && bloom.threshold.value > 1f && chromaticAberration.intensity.value < 0.4f)
                isAnimation = false;
        }
    }

    public void TakeNextLvl()
    {
        chromaticAberration.intensity.value = 2.0f;
        autoExposure.maxLuminance.value = -0.5f;
        lensDistortion.intensity.value = -60f;
        isAnimation = true;
    }

    public void TakeCoin()
    {
        chromaticAberration.intensity.value = 0.7f;
        autoExposure.maxLuminance.value = -0.3f;
        isAnimation = true;
    }

    public void TakeGun()
    {
        chromaticAberration.intensity.value = 1.8f;
        bloom.threshold.value -= 0.30f;
        autoExposure.maxLuminance.value = -0.45f;
        lensDistortion.intensity.value = -35f;
        isAnimation = true;
    }

    public void TakeBoost()
    {
        chromaticAberration.intensity.value = 2.5f;
        lensDistortion.intensity.value = -60f;
        autoExposure.maxLuminance.value = -0.6f;
        isAnimation = true;
    }

    public void OpenBox()
    {
        chromaticAberration.intensity.value = 1f;
        autoExposure.maxLuminance.value = -0.3f;
        isAnimation = true;
    }

    public void OpenChest()
    {
        chromaticAberration.intensity.value = 1f;
        autoExposure.maxLuminance.value = -0.35f;
        bloom.threshold.value -= 0.2f;
        isAnimation = true;
    }

    public void HitPlayer()
    {
        bloom.threshold.value -= 0.25f;
        autoExposure.maxLuminance.value = -0.65f;
        chromaticAberration.intensity.value = 2f;
        isAnimation = true;
    }

    public void JumpEffect()
    {
        lensDistortion.intensity.value = -45f;
        bloom.threshold.value -= 0.25f;
        autoExposure.maxLuminance.value = -0.6f;
        chromaticAberration.intensity.value = 1.3f;
        isAnimation = true;
    }

    public void DoorClose()
    {
        lensDistortion.intensity.value = -42.5f;
        bloom.threshold.value -= 0.30f;
        autoExposure.maxLuminance.value = -0.5f;
        chromaticAberration.intensity.value = 1.5f;
        isAnimation = true;
    }

    public void BotDead()
    {
        autoExposure.maxLuminance.value = -0.6f;
        bloom.threshold.value -= 0.15f;
        isAnimation = true;
    }

    public void BosDead()
    {
        chromaticAberration.intensity.value = 3.5f;
        lensDistortion.intensity.value = -70f;
        autoExposure.maxLuminance.value = -1f;
        bloom.threshold.value -= 0.5f;
        isAnimation = true;
    }
}
