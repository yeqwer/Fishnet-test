using FishNet.Example.Scened;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private int _sceneNumber;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            SceneManager.LoadScene(_sceneNumber);
        }
    }
}
