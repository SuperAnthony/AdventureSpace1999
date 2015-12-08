using UnityEngine;
using System.Collections;

/// <summary>
/// Interface that implements fading.
/// </summary>
public interface IFadeable
{
    void BeginFade(AnimationCurve fadeCurve, bool fadeCurveType);
    void FadeOut(AnimationCurve curve);
    void StopFade();
}
