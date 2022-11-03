using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VrMissionManager : MonoBehaviour {
    
    // Foreign Game Systems
    [Header("Foreign Managers")] 
    [SerializeField] private InputController inputController;

    // Brief Mission Variables
    [Header("Mission Brief Data")] 
    [SerializeField] private GameObject missionBriefModal;
    [SerializeField] private GameObject startMissionButton;
    
    // Finish MIssion Data
    [SerializeField] private GameObject missionFailedModal;

    // Stopwatch Variables
    [Header("Stopwatch")] 
    public float currentTime;
    [SerializeField] private int startMinutes;
    [SerializeField] private int startSeconds;
    [SerializeField] private GameObject timerLabel;
    private TextMeshProUGUI timerLabelText;
    [SerializeField] private bool isTimerActive;

    // Score and Objectives Variables
    [Header("Score and Objectives")] 
    [SerializeField] private int score;
    [SerializeField] private int scoreMultiplier;

    // Countdown To Start Variables
    [Header("Countdown to Start")] 
    [SerializeField] private int countdownTimer;
    [SerializeField] private GameObject countdownLabel;

    //All enemies in scene
    [Header("Enemies in map")]
    [SerializeField] private GameObject enemies;
    
    [Header("Player in map")]
    [SerializeField] private GameObject player;
    
    // TEMPORARY WIN CONDITION
    [Header("Player Inventory REMOVE AFTER SHOWCASE")] 
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private GameObject extractionPoint;
    [SerializeField] private GameObject controlsLabel;

    private void Awake() {
        // Subscribe partial controller to just enable UI controls and not pause menu controls
        inputController.PartialSubscribeMenuMap();
        currentTime = (startMinutes * 60) + startSeconds;

    }

    private void Start() {

        // Set Timer Text Mesh Pro Component
        timerLabelText = timerLabel.GetComponentInChildren<TextMeshProUGUI>();
        
        // Add listener to button
        startMissionButton.GetComponent<Button>().onClick.AddListener(StartVRMission);
        
        // Add listener to enemies
        foreach (var child in enemies.GetComponentsInChildren<EnemyFieldOfView>()) {
            // Add Lose condition to the player 
            child.onDetection.AddListener(LoseGame);
        }
        
        // Add listener to extraction point
        extractionPoint.GetComponent<ExtractionPoint>().onExtraction.AddListener(WinGame);
        
        // Set modal to activate
        StartCoroutine(InitialCountdown());
    }

    private void Update() {
        UpdateTimer();
        
        // TODO: REMOVE AFTER SHOWCASE
        if (playerInventory.GetInventoryItemCount() >= 4) {
            // ACTIVATE EXTRACTION POINT
            extractionPoint.SetActive(true);
        }
    }
    
    // Mission Brief Modal Functions
    private void StartVRMission() {
        // Close Modal Animaation
        LeanTween.cancel(missionBriefModal);
        LeanTween.scale(missionBriefModal, new Vector3(0f, 0f, 0f), 0.6f).setEaseOutBounce().setOnComplete(() => {
            
            // Start game loop
            StartCoroutine(CountdownToStart());
        });
    }

    // Timer Functions
    private void UpdateTimer() {
        if (isTimerActive) {
            score = Mathf.RoundToInt((currentTime * 100) * scoreMultiplier);
            currentTime -= Time.deltaTime;
            if (currentTime <= 0) {
                StopTimer();
                
                // If time runs out, lose condition
                LoseGame();
            }
        }

        var time = TimeSpan.FromSeconds(currentTime);
        timerLabelText.text = time.ToString(@"mm\:ss\:ff");
    }
    
    private void StartTimer() {
        // Start Timer Animation
        LeanTween.cancel(timerLabel);
        var timerLabelRt = timerLabel.GetComponent <RectTransform>();
        LeanTween.move(timerLabel, timerLabelRt.position + new Vector3(0, -Screen.height, 0), 0.3f).setEaseInOutExpo();
        
        // TODO: REMOVE AFTER DEMO
        LeanTween.cancel(controlsLabel);
        var controlsLabelRt = controlsLabel.GetComponent<RectTransform>();
        LeanTween.move(controlsLabel, controlsLabelRt.position + new Vector3(-Screen.width, 0, 0), 0.3f).setEaseInOutExpo();
        
        isTimerActive = true;


    }

    private void StopTimer() {
        isTimerActive = false;
    }
    
    // GAME CONDITIONS
    
    // Win condition
    private void WinGame() {
        // Switch Controller Map
        inputController.DisablePlayerOverworldMap();
        inputController.PartialSubscribeMenuMap();
        
        // Disable Enemies
        enemies.SetActive(false);
        player.SetActive(false);

        // Stop Timer
        StopTimer();
        
        // Open Mission Failed Modal TODO: UPDATE THE MODAL FOR THE WIN CONDITION
        LeanTween.cancel(missionFailedModal);
        LeanTween.scale(missionFailedModal, new Vector3(1f, 1f, 1f), 0.5f).setEaseOutElastic();
    }
    
    // Lose Condition
    private void LoseGame() {
        // Switch Controller Map
        inputController.DisablePlayerOverworldMap();
        inputController.PartialSubscribeMenuMap();

        // Disable Enemies
        enemies.SetActive(false);
        player.SetActive(false);

        // Stop Timer
        StopTimer();
        
        // TODO: REMOVE AFTER DEMO
        LeanTween.cancel(controlsLabel);
        var controlsLabelRt = controlsLabel.GetComponent<RectTransform>();
        LeanTween.move(controlsLabel, controlsLabelRt.position + new Vector3(Screen.width, 0, 0), 0.3f).setEaseInOutExpo();

        // Open Mission Failed Modal
        LeanTween.cancel(missionFailedModal);
        LeanTween.scale(missionFailedModal, new Vector3(1f, 1f, 1f), 0.5f).setEaseOutElastic();
    }

    // Coroutines
    IEnumerator InitialCountdown() {
        yield return new WaitForSeconds(0.7f);
        
        // Open Mission Brief Modal
        LeanTween.cancel(missionBriefModal);
        LeanTween.scale(missionBriefModal, new Vector3(1f, 1f, 1f), 0.5f).setEaseOutElastic();
    }
    
    IEnumerator CountdownToStart() {
        // Open Countdown Timer Animation
        LeanTween.cancel(countdownLabel);
        LeanTween.scale(countdownLabel, new Vector3(1f, 1f, 1f), 0.5f).setEaseOutElastic();
        
        var label = countdownLabel.GetComponentInChildren<TextMeshProUGUI>();
        
        while (countdownTimer > 0) {
            label.text = countdownTimer.ToString();
            yield return new WaitForSeconds(0.5f);
            countdownTimer--;
        }

        label.text = "GO!";
        
        // TODO: START GAME LOOP
        
        // Update Controller Maps
        inputController.DisableMenuMap();
        inputController.SubscribePlayerOverworldMap();

        // Close Countdown Timer Animation
        LeanTween.cancel(countdownLabel);
        LeanTween.scale(countdownLabel, new Vector3(0f, 0f, 0f), 0.6f).setEaseOutBounce().setOnComplete(() => {
            StartTimer();
            enemies.SetActive(true);
            countdownLabel.SetActive(false); 
        });
    }
}
