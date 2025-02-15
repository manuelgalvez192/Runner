using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "Runner/PlayerConfiguration")]
public class PlayerConfiguration : ScriptableObject {

    [Header("Movement")]
    [SerializeField]
    private float playerForwardSpeed = 3f;
    public float PlayerForwardSpeed {
        get {
            return playerForwardSpeed;
        }
    }
    [SerializeField]
    private float playerChangeLaneSpeed = 3f;
    public float PlayerChangeLaneSpeed {
        get {
            return playerChangeLaneSpeed;
        }
    }
    [Header("Jump")]
    [SerializeField]
    private float jumpForce = 3f;
    public float JumpForce {
        get {
            return jumpForce;
        }
    }
    [SerializeField]
    private float gravity = 10f;
    public float Gravity {
        get {
            return gravity;
        }
    }
    [SerializeField]
    private float jumpHover = 3f;
    public float JumpHover {
        get {
            return jumpHover;
        }
    }
    [SerializeField]
    private float jumpHoverPercent = 3f;
    public float JumpHoverPercent {
        get {
            return jumpHoverPercent;
        }
    }
}
