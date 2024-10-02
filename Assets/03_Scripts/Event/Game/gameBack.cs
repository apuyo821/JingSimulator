using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameBack : MonoBehaviour
{
    [SerializeField] Image buttonImage;
    [SerializeField] Sprite[] sprites;

    public void Update()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Event")
        {
            buttonImage.sprite = sprites[0];
        }
        else if(scene.name == "EventForGallery")
        {
            buttonImage.sprite = sprites[1];
        }
    }

    public  void division()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Event")
        {
            SceneManager.LoadScene("Main");
        }
        else if (scene.name == "EventForGallery")
        {
            SceneManager.LoadScene("Title");
        }
    }
}
