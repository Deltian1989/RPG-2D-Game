using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string areaToLoad;

    public string areaTransitionName;

    public float waitToLoad = 1f;

    private bool shouldLoadAfterFade;

    public AreaEntrance entrance;

    // Start is called before the first frame update
    void Start()
    {
        entrance.transitionName = areaTransitionName;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLoadAfterFade)
        {
            waitToLoad -= Time.deltaTime;

            if (waitToLoad <= 0)
            {
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            shouldLoadAfterFade = true;

            GameManager.instance.fadingBetweenAreas = true;
            UIFade.instance.FadeToBlack();

            PlayerController.instance.areaTransitionName = areaTransitionName;
        }
    }
}
