using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] private Button addChickenButton;
    [SerializeField] private ARRaycastManager arRaycastManager;
    [SerializeField] private GameObject chickenPrefab;
    
    private bool _canAddChicken;
    private GameObject _chickenPreview;
    private Vector3 _detectedPosition = new Vector3();
    private Quaternion _detectedRotation = Quaternion.identity;
    private ARTrackable _currentTrackable = null;

    private void Start()
    {
        InputHandler.OnTap += SpawnChicken;
        _chickenPreview = Instantiate(chickenPrefab); // Chicken preview
        SetCanAddChicken(true);
    }

    private void SpawnChicken()
    {
        if (!_canAddChicken) return;

        var chicken = Instantiate(chickenPrefab); // Actual spawned chicken
        chicken.GetComponent<Chicken>().PlaceChicken(_currentTrackable);
        chicken.transform.position = _detectedPosition;
        chicken.transform.rotation = _detectedRotation;
        
        SetCanAddChicken(false); // User has to tap on the button to spawn new chicken
    }

    private void Update()
    {
        // Detect a position and rotation over the detected surface
        GetRaycastHitTransform();
    }

    private void GetRaycastHitTransform()
    {
        var hits = new List<ARRaycastHit>();
        var middleScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        if (arRaycastManager.Raycast(middleScreen, hits, TrackableType.PlaneWithinPolygon))
        {
            _detectedPosition = hits[0].pose.position;
            _detectedRotation = hits[0].pose.rotation;
            _chickenPreview.transform.position = _detectedPosition;
            _chickenPreview.transform.rotation = _detectedRotation;
            _currentTrackable = hits[0].trackable;
        }
        
        
    }

    private void OnDestroy()
    {
        InputHandler.OnTap -= SpawnChicken;
    }

    public void SetCanAddChicken(bool canAddChicken)
    {
        _canAddChicken = canAddChicken;
        addChickenButton.gameObject.SetActive(!_canAddChicken); // show after spawn
        _chickenPreview.gameObject.SetActive(canAddChicken); // show when waiting for tap
    }
}
