using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerInputManager : MonoBehaviour {
    public static PlayerInputManager Instance;

    //Input actions
    private const string VERTICAL_MOVEMENT = "VerticalMovement";
    private const string HORIZONTAL_MOVEMENT = "HorizontalMovement";
    private const string JUMP = "Jump";
    private const string RIGHT_MOVEMENT = "RightMovement";
    private const string LEFT_MOVEMENT = "LeftMovement";
    private const string AGACHARSE = "Agacharse";

    private Player playerInput;

    private bool isInputAllowed = true;
    public bool IsInputAllowed {
        set {
            isInputAllowed = value;
        }
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(this);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        playerInput = ReInput.players.GetPlayer(0);
    }

    public float GetVerticalMovement() {
        if (!isInputAllowed) {
            return 0f;
        }
        return playerInput.GetAxis(VERTICAL_MOVEMENT);
    }
    public float GetHorizontalMovement() {
        if (!isInputAllowed) {
            return 0f;
        }
        return playerInput.GetAxis(HORIZONTAL_MOVEMENT);
    }

    public bool IsJumpPressed() {
        if (!isInputAllowed) {
            return false;
        }
        return playerInput.GetButtonDown(JUMP);
    }

    public bool IsRightPressed() {
        if (!isInputAllowed) {
            return false;
        }
        return playerInput.GetButtonDown(RIGHT_MOVEMENT);
    }
    public bool IsLeftPressed() {
        if (!isInputAllowed) {
            return false;
        }
        return playerInput.GetButtonDown(LEFT_MOVEMENT);
    }

    public bool IsAgacharsePressed()//comprueba en el rewired si se esta pulsando el boton correspondiente
    {
        if(!isInputAllowed)
        {
            return false;
        }

        return playerInput.GetButton(AGACHARSE);
    }
}
