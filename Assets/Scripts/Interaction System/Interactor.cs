using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  Interactor : MonoBehaviour {

    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider2D[] _colliders = new Collider2D[3];
    [SerializeField] private int _numFound;

    private void Update() {
        _numFound = Physics2D.OverlapCircleNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
             _interactableMask);
    }

    private void OnDrawGizmos() {
         Gizmos.color = Color.red;
         Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
