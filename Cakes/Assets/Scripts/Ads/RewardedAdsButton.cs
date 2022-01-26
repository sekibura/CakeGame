using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System;
using UnityEngine.Events;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Button _showAdButton;

    [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] private string _iOSAdUnitId = "Rewarded_iOS";
    private bool _isAdFinished = false;
    
    public UnityEvent[] _rewardActions;

    private string _adUnitId;

    private void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSAdUnitId : _androidAdUnitId;

        //Disable button until ad is ready to show
        if (_showAdButton != null)
            MainThreadExecuter.Instance.DoInMainThread(()=> {
                _showAdButton.interactable = false;
            });
            
    }

    private void Start()
    {
        //LoadAd();
        StartCoroutine("LoadAdRewarded");
    }

    //private void Update()
    //{
    //    if (_isAdFinished)
    //    {
    //        foreach (var action in _rewardActions)
    //        {
    //            if (action != null)
    //                action.Invoke();
    //        }

    //        _isAdFinished = false;
    //    }
    //}

    //private void Start()
    //{
    //    StartCoroutine(LoadAdRewarded());
    //}

    private IEnumerator LoadAdRewarded()
    {
        yield return new WaitForSeconds(1f);
        LoadAd();
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad button: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded button: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            if (_showAdButton != null)
            {
                _showAdButton.onClick.AddListener(ShowAd);
                // Enable the button for users to click:

                MainThreadExecuter.Instance.DoInMainThread(() => {
                    _showAdButton.interactable = true;
                });
            }
        
        }
    }

    // Implement a method to execute when the user clicks the button.
    public void ShowAd()
    {
        // Disable the button: 
        _showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad button Completed");
            // Grant a reward.

            //_isAdFinished = true;
            MainThreadExecuter.Instance.DoInMainThread(() =>
            {
                foreach (var action in _rewardActions)
                {
                    if (action != null)
                        action.Invoke();
                }

            });

            // Load another ad:
            Advertisement.Load(_adUnitId, this);
        }
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    private void OnDestroy()
    {

        // Clean up the button listeners:
        if (_showAdButton != null)
            _showAdButton.onClick.RemoveAllListeners();
    }
}