using UnityEngine;
using System.Collections;

public class PowerUpsSpawnScript : MonoBehaviour {

    public GameObject rice;
    public GameObject soy;
	public GameObject wasabi;
	GameObject prev;
	int temp = 4;

	// Use this for initialization
	void Start () {
        //StartCoroutine(SpawnPowerUps());
        InvokeRepeating("SpawnPowerUps", 10f, 10f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnPowerUps()
    {
        GameObject pos1 = GameObject.Find("PowerUp Location 1");
        GameObject pos2 = GameObject.Find("PowerUp Location 2");
        GameObject pos3 = GameObject.Find("PowerUp Location 3");

		print (prev);
		if(prev != null)
		{
			Destroy (prev);
		}
		int powerupChoice = Random.Range(0, 3);
		while (powerupChoice == temp)
		{
			powerupChoice = Random.Range(0, 3);
		}
		temp = powerupChoice;
        int spawnLocation = Random.Range(0, 3);

        if (powerupChoice == 0)
        {
			
            switch(spawnLocation)
            {
                case 0:
                    Instantiate(rice, pos1.transform.position, transform.rotation);
                    break;
                case 1:
                    Instantiate(rice, pos2.transform.position, transform.rotation);
                    break;
                case 2:
                    Instantiate(rice, pos3.transform.position, transform.rotation);
                    break;
                default:
                    break;
            }
			prev = GameObject.Find ("Rice(Clone)");
        }

        if (powerupChoice == 1)
        {
			
            switch (spawnLocation)
            {
                case 0:
				Instantiate(soy, pos1.transform.position, transform.rotation);
                    break;
                case 1:
				Instantiate(soy, pos2.transform.position, transform.rotation);
                    break;
                case 2:
				Instantiate(soy, pos3.transform.position, transform.rotation);
                    break;
                default:
                    break;
            }
			prev = GameObject.Find ("SoySauce(Clone)");
        }

		if (powerupChoice == 2)
		{
			
			switch (spawnLocation)
			{
			case 0:
				Instantiate(wasabi, pos1.transform.position, transform.rotation);
				break;
			case 1:
				Instantiate(wasabi, pos2.transform.position, transform.rotation);
				break;
			case 2:
				Instantiate(wasabi, pos3.transform.position, transform.rotation);
				break;
			default:
				break;
			}
			prev = GameObject.Find ("Wasabi(Clone)");
		}
    }
}
