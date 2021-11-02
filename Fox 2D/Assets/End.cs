using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class End : MonoBehaviour
{
    [SerializeField] private string endScene;
    [SerializeField] private AudioSource success;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(endScene); 
        }
        
    }
}

