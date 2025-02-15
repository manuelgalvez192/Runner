using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner
{
    public class PlayerAnimatorEvents : MonoBehaviour
    {
        public void ResetPlayer()
        {
            if(PlayerController.Instance.playerHealth.runtimeValue > 0)
            {
                //Reset player movement
                PlayerController.Instance.PlayerMovement.ResetPlayerMovement();
            }
            
        }
    }
}
