using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("CreditTrigger"))
        {
           SceneManager.LoadScene("Main Menu");
        }
    }
}
