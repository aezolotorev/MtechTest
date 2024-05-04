using UnityEngine;


public class SimpleMovement: IMovable
{     
    public  void  Move(Transform player, Vector2 input, float speed, float delta){
        Vector3 moveVector = new Vector3(input.x,  input.y, 0);
        Vector3 newPosition = player.position + moveVector * delta*speed;     
        player.position = newPosition;         
    }

   
}
