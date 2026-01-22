using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Chicken : MonoBehaviour
{
    private static readonly int IsAwake = Animator.StringToHash("IsAwake");
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    public void PlaceChicken([CanBeNull]ARTrackable trackableParent)
    {
        transform.SetParent(trackableParent?.transform);
        animator.SetBool(IsAwake, true);
    }
}
