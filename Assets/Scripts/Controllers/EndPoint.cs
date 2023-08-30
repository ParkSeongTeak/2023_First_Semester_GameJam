using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    Coroutine HelloWorld; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Monster_Controller endOBJ = collision.GetComponent<Monster_Controller>();
            if (endOBJ.Live)
            {
                GameManager.Sound.Play("Effect/life1");

                GameManager.Data.Life -= 1;
                
                GameManager.UI.ShowSceneUI<GameUI>().MinusLife();

            }
            

            collision.gameObject.GetComponent<Monster_Controller>().Dead();
            
            GameManager.UI.ShowSceneUI<GameUI>().UpdateData();

        }
    }
    
}
