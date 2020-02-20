using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashFade : MonoBehaviour {

    public Image splashImage;

    IEnumerator Start()
    {
        splashImage.canvasRenderer.SetAlpha(0.0F);

        FadeIn();
        yield return new WaitForSeconds(2.5f);
        FadeOut();
        yield return new WaitForSeconds(2.5f);
        GameDataScript gameDataScript = GameObject.Find("GameData").GetComponent<GameDataScript>();
        gameDataScript.inLevel = true;
		if (gameDataScript.roundNum == 2)
		{
			Application.LoadLevel("Level2");
		}
		if (gameDataScript.roundNum == 3)
		{
			Application.LoadLevel ("Level3");
		}
		if (gameDataScript.roundNum == 4)
		{
			Application.LoadLevel ("Level4");
		}
    }
	
    void FadeIn()
    {
        splashImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut()
    {
        splashImage.CrossFadeAlpha(0.0f, 2.5f, false);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
