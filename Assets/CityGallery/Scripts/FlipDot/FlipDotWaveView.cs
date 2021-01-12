using SOVariables;
using System.Collections;
using System.Collections.Generic;
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
    private float inRangeDistance;
    [SerializeField]
    private float enterGameDistance;
    [SerializeField]
    private FloatReference closestDist;
    private float mappedDist01;

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

    //view speed parameters
    [SerializeField]
    [Tooltip("X: lowest speed; Y: highest speed")]
    private Vector2 speedRange;

    [SerializeField]
    private float speedModifier;
    private float speedDampVelocity;

    //view width parameters
    [SerializeField]
    private float lineWidthMin;
    [SerializeField]
    private float lineWidthMax;
    private float widthDampVelocity;

    //Debug
    [SerializeField]
    private bool manualDistance;

    private void Awake()
    {
        InitializeLambdaParams();
    }

    private void OnEnable()
    {
        ResetView();
    }

    private void Update()
    {
        float minDist = manualDistance ? closestDist.Value : inRangeDistance + 1;
        if (manualDistance || AnybodyWithinRange(out minDist))
        {
            if (!manualDistance)
            {
                closestDist.Variable.SetValue(minDist);
            }
            
            //smooth transition to mapped value
            mappedDist01 = Mathf.InverseLerp(-inRangeDistance, -enterGameDistance, -closestDist);
            //lambda
            lambdaModifier = Mathf.SmoothDamp(lambdaModifier, mappedDist01, ref lambdaDampVelocity, 0.2f);
            wave.Harmonics[0].Lambda =  -Mathf.Lerp(-lambdaRange.y, -lambdaRange.x, lambdaModifier);
            //lambdaRange.y + (lambdaRang.x - lambdaRange.y) * lambdaModifier;

            //amp
            ampModifier = Mathf.SmoothDamp(ampModifier, mappedDist01 * ampMultiplier, ref ampDampVelocity, 0.2f);
            wave.Harmonics[0].Amp = ampModifier;

            //speed
            speedModifier = Mathf.SmoothDamp(speedModifier, mappedDist01, ref speedDampVelocity, 0.2f);
            wave.Harmonics[0].Speed = speedRange.x + (speedRange.y - speedRange.x) * speedModifier;
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
            wave.Harmonics[0].Amp = ampModifier;

            //width
            lr.widthMultiplier = Mathf.SmoothDamp(lr.widthMultiplier, lineWidthMin, ref widthDampVelocity, 1f);
        }
    }

    private bool AnybodyWithinRange(out float minDistInRange)
    {
        var dists = bodyDistances.GetArray();

        minDistInRange = inRangeDistance;
        if (dists.Length > 0)
        {
            minDistInRange = Mathf.Min(dists);

            if (minDistInRange <= inRangeDistance)
            {
                return true;
            }
        }

        return false;
    }

    private void InitializeLambdaParams()
    {
        //refLambda = wave.Harmonics[0].Lambda;
        lr = wave.GetComponent<LineRenderer>();
    }

    private void ResetView()
    {
        //lambda
        wave.Harmonics[0].Lambda = lambdaRange.y;
        lambdaModifier = 0;
        //width
        lr.widthMultiplier = lineWidthMin;
    }

}
