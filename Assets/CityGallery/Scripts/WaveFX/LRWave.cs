﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class LRWave : MonoBehaviour
{
    [Serializable]
    public class Harmonic
    {
        public float Amp;
        public float Lambda;
        public float Speed;
        public float PhaseOffset;
        /// <summary>
        /// Radian increment
        /// </summary>
        public float RadianStep
        {
            get;set;
        }
        public float TimeShift
        {
            get;set;
        }
    }

    [Range(0f,4096f)]
    [SerializeField]
    private int samples = 1024;
    [SerializeField]
    private float length = 25;

    /// <summary>
    /// Domain dimension spacing
    /// </summary>
    private static float DomainIncrement;

    [SerializeField]
    public Harmonic[] Harmonics;
    private Vector3[] superpositionPoints;

    [SerializeField]
    private bool masterWindow;

    [SerializeField]
    private LineRenderer line;

    [SerializeField]
    private MouseSpring cursor;
    [SerializeField]
    private bool cursorInteractive;

    [SerializeField]
    [Tooltip("Blends Mouse Mask across multiple frames")]
    private bool windowBlending;
    [SerializeField]
    [Range(2f, 1024f)]
    private int effectSize;     
    [SerializeField]
    private float mouseLambda;




    [SerializeField]
    private float mouseFxSpeed;
    [SerializeField]
    private float mouseAmpMultiplier = 1;
    private float mouseRadianStep;
    private float mouseTimeShift = 0;

    private float[] mouseSuperpositionPts;
    private float[] mouseXMask;

    [SerializeField]
    [Tooltip("max deltaX for cursor")]
    private float maxMouseDX;
    private float dxMouse;
    [SerializeField]
    [Range(0f, 1f)]
    private float maskDamping = 0.9f;
    [SerializeField]
    [Range(0f, 1f)]
    private float dxMouseDamping = 0.9f;

#if UNITY_EDITOR
    private void OnValidate()
    {
        foreach (var harmonic in Harmonics)
        {
            harmonic.Lambda = Mathf.Max(harmonic.Lambda, 0);
        }

        mouseLambda = Mathf.Max(mouseLambda, 0);
    }

    private void OnDrawGizmos()
    {
        if (Application.isEditor)
        {
            RenderWave(); 
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isEditor)
        {
            CalcPoints();
        }
    }
#endif

    private void Awake()
    {
        if (line == null)
        {
            line = GetComponent<LineRenderer>();
        }

        mouseXMask = new float[samples];
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTimeShift();
        }
        if (cursorInteractive)
        {
            UpdateAndGetMouseXSpeed(); 
        }
        CalcPoints();
        if (cursorInteractive)
        {           
            CalculateMouseFXPoints();
            AddMouseFXToWave(); 
        }
        //RenderWave();
    }

    private void LateUpdate()
    {
        RenderWave();
    }

    private void CalcPoints()
    {
        superpositionPoints = new Vector3[samples];
        DomainIncrement = length / samples;

        foreach (var harmonic in Harmonics)
        {
            harmonic.RadianStep = harmonic.Lambda == 0 ? float.NaN : Mathf.PI * 2 * (DomainIncrement / harmonic.Lambda);
            if (harmonic.Lambda != 0)
            {
                harmonic.TimeShift += (Time.deltaTime ) * (harmonic.Speed / harmonic.Lambda);
            }
            harmonic.TimeShift %= (2 * Mathf.PI);
        }

        for (int i = 0; i < superpositionPoints.Length; i++)
        {
            float superposedValue = 0;
            foreach (var component in Harmonics)
            {
                if ( !float.IsNaN(component.RadianStep))
                {
                    superposedValue += Mathf.Sin(i * component.RadianStep + component.TimeShift + component.PhaseOffset * Mathf.PI / 180f) * component.Amp;
                }
            }
            superpositionPoints[i] = new Vector3(i * DomainIncrement, superposedValue);
        }

        if (masterWindow)
        {
            ApplyMasterHanningWindow(ref superpositionPoints);
        }
    }

    private void RenderWave()
    {

        line.positionCount = samples;
        if (superpositionPoints != null && superpositionPoints.Length == samples)
        {
            line.SetPositions(superpositionPoints);
        }      
    }

    public void ResetTimeShift()
    {
        foreach (var harmonic in Harmonics)
        {
            harmonic.TimeShift = 0;
        }
    }

    private float UpdateAndGetMouseXSpeed()
    {
        dxMouse = Mathf.Max(dxMouse * dxMouseDamping, Mathf.Abs(cursor.CursorDX));
        return dxMouse;
    }

    private void UpdateMouseXMask(int mouseXPosIdx)
    {
        if (mouseXMask == null || mouseXMask.Length != samples)
        {
            mouseXMask = new float[samples];
        }

        int j = 0;
        for (int i = 0; i < mouseXMask.Length; i++)
        {
            mouseXMask[i] *= maskDamping;

            int idxOffset = (int)(mouseXPosIdx - effectSize / 2);
            if (i > mouseXPosIdx - effectSize / 2f && i < mouseXPosIdx + effectSize / 2f)
            {
                if (IsMouseInsideScreenWidth())
                {
                    float maskCoefficient = Mathf.InverseLerp(0, maxMouseDX, dxMouse);
                    if (maskCoefficient > mouseXMask[i])
                    {
                        if (windowBlending)
                        {
                            mouseXMask[i] = Mathf.Max(mouseXMask[i], HanningWindow(ref maskCoefficient, effectSize, (i - idxOffset))); 
                        }
                        else
	                    {
                            mouseXMask[i] = HanningWindow(ref maskCoefficient, effectSize, (i - idxOffset)); 
                        }
                    }
                    j++;
                }               
            }
        }
    }

    private void CalculateMouseFXPoints()
    {
        mouseTimeShift = (mouseTimeShift + Time.deltaTime * mouseFxSpeed / mouseLambda) % (2 * Mathf.PI);
        
        ////Option A: short array following mouse X position
        //if (mouseSuperpositionPts == null || mouseSuperpositionPts.Length != effectSize)
        //{
        //    mouseSuperpositionPts = new float[effectSize];
        //}
        //mouseRadianStep = mouseLambda == 0 ? float.NaN : Mathf.PI * 2 * (DomainIncrement / mouseLambda);

        //for (int i = 0; i < mouseSuperpositionPts.Length; i++)
        //{
        //    mouseSuperpositionPts[i] = Mathf.Sin(i * mouseRadianStep + Time.fixedTime * mouseFxSpeed / mouseLambda) * mouseAmpMultiplier * (Mathf.InverseLerp(0, maxMouseX, Mathf.Abs(dxMouse)));
        //    HanningWindow(ref mouseSuperpositionPts[i], effectSize, i);
        //}

        //Option B: full length mouse FX array, MouseX as coefficient mask
        mouseSuperpositionPts = new float[samples];
        mouseRadianStep = mouseLambda == 0 ? float.NaN : Mathf.PI * 2 * (DomainIncrement / mouseLambda);

        for (int i = 0; i < mouseSuperpositionPts.Length; i++)
        {
            mouseSuperpositionPts[i] = Mathf.Sin(i * mouseRadianStep + mouseTimeShift) * mouseAmpMultiplier;
        }
    }

    private void AddMouseFXToWave()
    {
        if (Camera.main.orthographic)
        {
            float mouseWorldX = cursor.CursorPos.x;
            //Debug.Log( "Mouse X: "+ mouseWorldX);

            //Calculate the idx position of MouseX on the Wave Sample Points array
            int mouseXOnWaveIdx = (int)(Mathf.InverseLerp(transform.position.x, transform.position.x + length, mouseWorldX) * samples);
            //Debug.Log(mouseXOnWaveIdx);

            //Snapped to mouseFx wavelength
            //int mouseLambdaSampleSize = (int)(mouseLambda * samples / length);
            //mouseXOnWaveIdx = Mathf.RoundToInt(mouseXOnWaveIdx / mouseLambdaSampleSize) * mouseLambdaSampleSize;

            ////Option A: short array following mouse X position
            //if (mouseSuperpositionPts != null)
            //{

            //    for (int i = 0; i < mouseSuperpositionPts.Length; i++)
            //    {
            //        int j = i + mouseXOnWaveIdx - mouseSuperpositionPts.Length / 2;
            //        if (j >= 0 && j < superpositionPoints.Length)
            //        {
            //            superpositionPoints[j].y += mouseSuperpositionPts[i];
            //        }
            //    }
            //}

            //Option B: full length mouse FX array, MouseX as coefficient mask
            UpdateMouseXMask(mouseXOnWaveIdx);
            if ((samples == mouseXMask.Length) == (mouseSuperpositionPts.Length == superpositionPoints.Length))
            {
                for (int i = 0; i < samples; i++)
                {
                    superpositionPoints[i].y = superpositionPoints[i].y - mouseXMask[i] * mouseXMask[i] * (superpositionPoints[i].y - mouseSuperpositionPts[i]);
                }
            }

        }
    }

    private void ApplyMasterHanningWindow(ref Vector3[] input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            input[i].y *= 0.5f * (1f - Mathf.Cos(Mathf.PI * 2 * i / (input.Length)));
        }
    }

    private void HammingWindow(ref float[] input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            input[i] *= (0.54f - 0.46f * Mathf.Cos(Mathf.PI * 2 * i / (input.Length)));
        }
    }

    private void HanningWindow(ref float[] input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            input[i] *= 0.5f * (1f - Mathf.Cos(Mathf.PI * 2 * i/(input.Length)));
        }
    }

    private float HanningWindow(ref float value, in int N, int n)
    {
        return value *= 0.5f * (1f - Mathf.Cos(Mathf.PI * 2 * n / (N-1)));
    }

    private void SineWindow(ref float[] input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            input[i] *= Mathf.Sin(Mathf.PI * i / input.Length);
        }
    }

    private bool IsMouseInsideScreenWidth()
    {
        return (Input.mousePosition.x >= 0) && (Input.mousePosition.x <= Screen.width);
    }

    private bool IsMouseInsideScreenHeight()
    {
        return (Input.mousePosition.y >= 0) && (Input.mousePosition.y <= Screen.height);
    }

    private bool IsMouseInsideScreen()
    {
        return IsMouseInsideScreenWidth() && IsMouseInsideScreenHeight();
    }
}
