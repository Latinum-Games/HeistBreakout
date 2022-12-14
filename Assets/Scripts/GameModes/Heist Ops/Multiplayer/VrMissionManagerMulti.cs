using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class VrMissionManagerMulti : MonoBehaviourPunCallbacks {
    
    // TODO: IMPROVE MAP OBJECTIVE ITEMS COUNT, MOVE COUNT FROM PLAYER TO MANAGER
    // TODO: UPDATE COUNTDOWN TIMER MODAL INFO
    // TODO: UPDATE TO A GENERAL WIN/LOSE CONDITION MODAL FOR END MISSION DATA
    // TODO: DISPLAY SCORE AT THE END OF THE LEVEL IN MODAL
    
    // Foreign Game Systems
    [Header("Foreign Managers")] 
    [SerializeField] private InputControllerMulti inputController;

    // Brief Mission Variables
    [Header("Mission Brief Data")] 
    [SerializeField] private GameObject missionBriefModal;
    [SerializeField] private GameObject startMissionButton;
    
    // Finish Mission Data
    [SerializeField] private GameObject missionFailedModal; 

    // Stopwatch Variables
    [Header("Stopwatch")]
    [SerializeField] private int startMinutes;
    [SerializeField] private int startSeconds;
    [SerializeField] private GameObject timerLabel;
    [SerializeField] private GameObject timerLabelPosition;
    private TextMeshProUGUI timerLabelText;
    public float currentTime;
    private bool isTimerActive;

    // Score and Objectives Variables
    [Header("Score and Objectives")] 
    [SerializeField] private int score;
    [SerializeField] private int scoreMultiplier;

    // Countdown To Start Variables
    [Header("Countdown to Start")] 
    [SerializeField] private int countdownTimer;
    [SerializeField] private GameObject countdownLabel;
    
    // Map inventory
    [Header("Map Inventory")] 
    [SerializeField] private InventoryMulti mapInventory;

    //All enemies in scene
    [Header("Enemies in map")]
    [SerializeField] private GameObject enemies;
    
    //Player reference in scene
    [Header("Player in map")]
    [SerializeField] private GameObject player;
    
    // TEMPORARY WIN CONDITION
    [Header("Player Inventory REMOVE AFTER SHOWCASE")] 
    //[SerializeField] private Inventory playerInventory;
    [SerializeField] private GameObject extractionPoint;
    
    //MULTIPLAYER VARIABLES
    [Header("Player variables")]
    public GameObject playerCatPrefab;
    public GameObject playerElfPrefab;
    public GameObject playerFOV;
    public InteractionPromptUI interactionUI;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private GameObject fovObj;
    public InGamePauseMenuManager pauseManager;

    public CinemachineVirtualCamera playerCamera;

    public Transform[] spawnPlayerPositions;

    private void Awake() {
        SpawnPlayers();
        photonView.RPC("AssignPlayerProps", RpcTarget.All);
        // Subscribe partial controller to just enable UI controls and not pause menu controls
        inputController.PartialSubscribeMenuMap();
        currentTime = (startMinutes * 60) + startSeconds;

    }

    private void Start() {

        // Set Timer Text Mesh Pro Component
        timerLabelText = timerLabel.GetComponentInChildren<TextMeshProUGUI>();
        
        // Add listener to button
        startMissionButton.GetComponent<Button>().onClick.AddListener(StartMultiMission);
        
        // Add listener to enemies
        foreach (var child in enemies.GetComponentsInChildren<EnemyFieldOfView>()) {
            // Add Lose condition to the player 
            child.onDetection.AddListener(LoseMultiMission);
        }
        
        //Assignment of player
        foreach (AIChasing child in enemies.GetComponentsInChildren<AIChasing>()) {
            // Add Lose condition to the player 
            child.player = playerObj.transform;
        }
        
        // Add listener to extraction point
        extractionPoint.GetComponent<ExtractionPoint>().onExtraction.AddListener(WinMultiMission);
        
        // Set modal to activate
        StartCoroutine(InitialCountdown());
    }

    private void Update() {
        UpdateTimer();
        
        // TODO: REMOVE AFTER SHOWCASE
        if (mapInventory.GetInventoryItemCount() >= 4) {
            // ACTIVATE EXTRACTION POINT
            extractionPoint.SetActive(true);
        }
    }
    
    [PunRPC]
    // Mission Brief Modal Functions
    private void StartVRMission() {
        // Close Modal Animation
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
        LeanTween.move(timerLabel, timerLabelPosition.transform.position, 0.3f).setEaseInOutExpo();
        isTimerActive = true;
    }

    private void StopTimer() {
        // Deactivates Timer
        isTimerActive = false;
    }
    
    // GAME CONDITIONS
    
    [PunRPC]
    // Win condition
    private void WinGame() {
        // Switch Controller Map
        inputController.DisablePlayerOverworldMap();
        inputController.PartialSubscribeMenuMap();
        
        // Disable Enemies and Player
        photonView.RPC("CharactersDeactivation", RpcTarget.All);
        //CharactersDeactivation();

        // Stop Timer
        StopTimer();
        
        // Open Mission Failed Modal TODO: UPDATE THE MODAL FOR THE WIN CONDITION
        LeanTween.cancel(missionFailedModal);
        LeanTween.scale(missionFailedModal, new Vector3(1f, 1f, 1f), 0.5f).setEaseOutElastic();
    }
    
    [PunRPC]
    // Lose Condition
    private void LoseGame() {
        // Switch Controller Map
        inputController.DisablePlayerOverworldMap();
        inputController.PartialSubscribeMenuMap();

        // Disable Enemies and Player
        photonView.RPC("CharactersDeactivation", RpcTarget.All);
        //CharactersDeactivation();

        // Stop Timer
        StopTimer();

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
            enemies.SetActive(true); //AQUI DESCOMENTAR
            countdownLabel.SetActive(false); 
        });
    }

    [PunRPC]
    //Deactivation of Enemies and Player
    private void CharactersDeactivation() {
        
        foreach (WaypointMover child in enemies.GetComponentsInChildren<WaypointMover>()) {
            // Add Lose condition to the player 
            Destroy(child.GetComponent<NavMeshAgent>());
            Destroy(child.GetComponent<AIChasing>());
            Destroy(child.GetComponent<EnemyFieldOfView>());
            Destroy(child);
            
        }

        Destroy(playerObj.GetComponent<MovementV2Multi>());

        //enemies.SetActive(false);
        //player.SetActive(false);
    }
    
    private void SpawnPlayers() {
        int randomPosition = Random.Range(0, spawnPlayerPositions.Length);//Obtener posicion random de lista de posiciones
        Vector3 playerPosition = spawnPlayerPositions[randomPosition].position;
        playerPosition.z = 0;

        string selectedCharacter = CharacterSelection.instance.characterController;
        // instanciar el player en una posiciocion aleatoria dependiendo de su skin
        if (selectedCharacter == "CatController") {
            playerObj = PhotonNetwork.Instantiate(playerCatPrefab.name, playerPosition, Quaternion.identity);
        } else if (selectedCharacter == "DuendeController") {
            playerObj = PhotonNetwork.Instantiate(playerElfPrefab.name, playerPosition, Quaternion.identity);
        }
        
        fovObj = Instantiate(playerFOV, Vector3.zero, Quaternion.identity);
    }
    
    [PunRPC]
    private void AssignPlayerProps() {
        inputController = playerObj.GetComponent<InputControllerMulti>();
        player = playerObj;
        //playerInventory = playerObj.GetComponent<Inventory>();
        playerObj.GetComponent<MovementV2Multi>().fieldOfView = fovObj.GetComponent<FieldOfView>();

        playerCamera.Follow = playerObj.transform;
        playerObj.GetComponent<Interactor>().interactionPromptUI = interactionUI;
        inputController.inGameMenu = pauseManager;

    }
    
    //MULTIPLAYER FUNCTION IMPLEMENTATIONS
    private void StartMultiMission() {
        photonView.RPC("StartVRMission", RpcTarget.All);
    }

    private void LoseMultiMission() {
        photonView.RPC("LoseGame", RpcTarget.All);
    }

    private void WinMultiMission() {
        photonView.RPC("WinGame", RpcTarget.All);
    }
}
