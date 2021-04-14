using DG.Tweening;
using SOVariables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlipDotWaveView : MonoBehaviour
{
    [SerializeField]
    private FloatReferenceArray bodyDistances;

    [SerializeField]
    private LRWave wave;
    private LineRenderer lr;

    //input parameters
    [SerializeField]
    private FloatVariable inRangeDistance;
    [SerializeField]
    private FloatVariable enterGameDistance;
    [SerializeField]
    private FloatReference closestDist;
    private float mappedDist01;

    private bool isIdle = false;

    //view lambda parameters
    [SerializeField]
    [Tooltip("X: shortest wavelength; Y: longest wavelength")]
    private Vector2 lambdaRange;

    [SerializeField]
    private float lambdaModifier;
    private float lambdaDampVelocity;

    //view amp parameters
    [SerializeField]
    [Tooltip("")]
    private float ampMultiplier;

    [SerializeField]
    private float ampModifier;
    private float ampDampVelocity;

    private float[] harmonicRefAmps;

    //view speed parameters
    [SerializeField]
    [Tooltip("X: lowest speed; Y: highest speed")]
    private Vector2 speedRange;

    [SerializeField]
    private float speedModifier;
    private float speedDampVelocity;

    private float[] harmonicRefSpeeds;

    //view width parameters
    [SerializeField]
    private float lineWidthMin;
    [SerializeField]
    private float lineWidthMax;
    private float widthDampVelocity;

    private void Awake()
    {
        InitializeWaveParams();
    }

    private void OnEnable()
    {
        ResetView();
    }

    private void Update()
    {
        if (isIdle)
        {
            if (AnybodyWithinRange())
            { 
                //smooth transition to mapped value
                mappedDist01 = Mathf.InverseLerp(-inRangeDistance, -enterGameDistance, -closestDist);
                //lambda
                lambdaModifier = Mathf.SmoothDamp(lambdaModifier, mappedDist01, ref lambdaDampVelocity, 0.2f);
                //wave.Harmonics[0].Lambda = -Mathf.Lerp(-lambdaRange.y, -lambdaRange.x, lambdaModifier);
                //lambdaRange.y + (lambdaRange.x - lambdaRange.y) * lambdaModifier;

                //amp
                ampModifier = Mathf.SmoothDamp(ampModifier, mappedDist01 * ampMultiplier, ref ampDampVelocity, 0.2f);

                for (int i = 0; i < harmonicRefAmps.Length; i++)
                {
                    wave.Harmonics[i].Amp = ampModifier * harmonicRefAmps[i];
                }


                //speed
                speedModifier = Mathf.SmoothDamp(speedModifier, mappedDist01, ref speedDampVelocity, 0.2f);
                for (int i = 0; i < harmonicRefSpeeds.Length; i++)
                {
                    wave.Harmonics[i].Speed = (speedRange.x + (speedRange.y - speedRange.x) * speedModifier) * Mathf.Abs(harmonicRefSpeeds[i]) / (harmonicRefSpeeds[0] == 0 ? 1 : Mathf.Abs(harmonicRefSpeeds[0])); 
                }
                //Mathf.Lerp(speedRange.x, speedRange.y, speedModifier);

                //width
                lr.widthMultiplier = Mathf.SmoothDamp(lr.widthMultiplier, Mathf.Lerp(lineWidthMin, lineWidthMax, mappedDist01), ref widthDampVelocity, 1f);
            }
            else
            {
                //smooth return to ref value
                //lambda
                lambdaModifier = Mathf.SmoothDamp(lambdaModifier, 0, ref lambdaDampVelocity, 1f);
                wave.Harmonics[0].Lambda = -Mathf.Lerp(-lambdaRange.y, -lambdaRange.x, lambdaModifier);

                //amp
                ampModifier = Mathf.SmoothDamp(ampModifier, 0, ref ampDampVelocity, 0.2f);
                for (int i = 0; i < harmonicRefAmps.Length; i++)
                {
                    wave.Harmonics[i].Amp = ampModifier * harmonicRefAmps[i];
                }

                //speed 
                speedModifier = Mathf.SmoothDamp(speedModifier, 0, ref speedDampVelocity, 0.2f);
                for (int i = 0; i < harmonicRefSpeeds.Length; i++)
                {
                    wave.Harmonics[i].Speed = (speedRange.x + (speedRange.y - speedRange.x) * speedModifier) * Mathf.Abs(wave.Harmonics[i].Speed) / (wave.Harmonics[0].Speed == 0 ? 1 : Mathf.Abs(wave.Harmonics[0].Speed));
                }

                //width
                lr.widthMultiplier = Mathf.SmoothDamp(lr.widthMultiplier, Mathf.Lerp(lineWidthMin, lineWidthMax, mappedDist01), ref widthDampVelocity, 1f);
            }
        }
        else
        {
            //amp
            ampModifier = Mathf.SmoothDamp(ampModifier, 0, ref ampDampVelocity, 0.2f);
            for (int i = 0; i < harmonicRefAmps.Length; i++)
            {
                wave.Harmonics[i].Amp = ampModifier * harmonicRefAmps[i];
            }

            //speed 
            speedModifier = Mathf.SmoothDamp(speedModifier, 0, ref speedDampVelocity, 0.2f);
            for (int i = 0; i < harmonicRefSpeeds.Length; i++)
            {
                wave.Harmonics[i].Speed = (speedRange.x + (speedRange.y - speedRange.x) * speedModifier) * Mathf.Abs(wave.Harmonics[i].Speed) / (wave.Harmonics[0].Speed == 0 ? 1 : Mathf.Abs(wave.Harmonics[0].Speed));
            }

            //width
            //lr.widthMultiplier = Mathf.SmoothDamp(0, lineWidthMin, ref widthDampVelocity, 1f);
        }
    }

    private bool AnybodyWithinRange()
    {
        return closestDist < inRangeDistance;
        
        //var dists = bodyDistances.GetArray();

        //if (dists.Length > 0)
        //{
        //    minDistInRange = Mathf.Min(dists);

        //    if (minDistInRange <= inRangeDistance)
        //    {
        //        return true;
        //    }
        //}

        //return false;
    }

    private void InitializeWaveParams()
    {
        //refLambda = wave.Harmonics[0].Lambda;
        lr = wave.GetComponent<LineRenderer>();

        harmonicRefAmps = wave.Harmonics.Select(h => h.Amp).ToArray();
        harmonicRefSpeeds = wave.Harmonics.Select(h => h.Speed).ToArray();

        isIdle = true;
    }

    private void ResetView()
    {
        //lambda
        wave.Harmonics[0].Lambda = lambdaRange.y;
        lambdaModifier = 0;
        //width
        DOTween.To(() => lr.widthMultiplier, f => lr.widthMultiplier = f, lineWidthMin, 0.2f);
        //lr.widthMultiplier = lineWidthMin;
    }

    public void FlipDotWaveIdleToggle(bool on)
    {
        isIdle = on;

        ResetView();
    }

    public void HideWave()
    {
        DOTween.To(() => lr.widthMultiplier, f => lr.widthMultiplier = f, 0, 0.2f);
    }

}
