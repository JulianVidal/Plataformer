using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
   //Holds the position of the player
   public Transform player;

   private void FixedUpdate()
   {
      
      // Moves the position of the camera to the one of the player
      // only on the x-axis
      if (!(player.position.x < 0)) {
         gameObject.transform.position = new Vector3(player.position.x, gameObject.transform.position.y, -10);
      }
      
   }
}
