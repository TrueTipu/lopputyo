using UnityEngine;
using System.Collections;

public class StreamCore : MonoBehaviour
{
    bool powered;

    bool onTrigger;

    UIManager uIManager; //safety reasons to avoid singleton problems

    private void Start()
    {
        uIManager = UIManager.Instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onTrigger = false;
        }
    }

    private void Update()
    {
        if(onTrigger && Keys.InteractKeysDown())
        {
            if (!powered)
            {
                uIManager.ActivateItemChoose(true);
            }
            else
            {

            }
        }
    }
}
