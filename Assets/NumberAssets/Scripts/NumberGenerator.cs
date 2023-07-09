
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;


public class NumberGenerator : MonoBehaviour
{
    public GameObject[] numberPrefabs;
    public Transform spawnPosition;
    public UnityEngine.UI.Button nextButton;
    public UnityEngine.UI.Button challengeButton;
    public AudioClip[] numberAudioClips;//Serialized array for number audio clips
    public AudioClip readAloudAudioClip;
    public AudioSource audioSource;
    public UnityEngine.UI.Image voiceIcon;
    public UnityEngine.UI.Image penguineWalk;
    public UnityEngine.UI.Text readLoudText;
    public UnityEngine.UI.Text readyForChallenge;
    
     


    private List<int> availableNumbers = new List<int>();
    private int totalNumbers = 10;
    private int currentNumber;
    private GameObject currentNumberObject;
   

    private void Start()
    {
        //Hide the nextButton initially
        nextButton.gameObject.SetActive(false);
        challengeButton.gameObject.SetActive(false);
        readyForChallenge.gameObject.SetActive(false);


        //Initialize the list of available numbers
        for (int i = 1; i <= totalNumbers; i++)
        {
            availableNumbers.Add(i);
        }

        //Delay the initial read by 2 seconds
        Invoke("PlayReadAloud", 2f);


        //Spawn the first numnber after a short delay
        Invoke("SpawnNumber", 2f);

        //Activate the next button after a delay of 7 seconds
        Invoke("ShowNextButton", 7f);

    }

    private void PlayReadAloud()
    {
        //Play the "Read Aloud" audio clip 
        audioSource.PlayOneShot(readAloudAudioClip);
    }



    public void VoiceIconOnClick()
    {
        //Play the audio clip for the current number
        audioSource.PlayOneShot(numberAudioClips[currentNumber - 1]);
    }


    public void NextButtonOnClick()
    {
        //Hide the next button
        nextButton.gameObject.SetActive(false);

        //Spawn the next number
        SpawnNumber();

    }

    private void SpawnNumber()
    {
        //Destrou the previous number if it exists
        if(currentNumberObject != null)
        {
            Destroy(currentNumberObject);
        }

        //Check if all numbers have been spawned
        if (availableNumbers.Count == 0)
        {
            voiceIcon.gameObject.SetActive(false);
            readLoudText.gameObject.SetActive(false);
            penguineWalk.gameObject.SetActive(false);
            readyForChallenge.gameObject.SetActive(true);
            Invoke("ShowChallengeButton", 2f);
            //Show the next button if all numbers have been processed
            challengeButton.gameObject.SetActive(true);
            return;
        }

        //Get a random number from the available numbers
        int randomIndex = Random.Range(0, availableNumbers.Count);
        currentNumber = availableNumbers[randomIndex];

        //spawn the 3D number object
        currentNumberObject = Instantiate(numberPrefabs[currentNumber - 1], spawnPosition.position, Quaternion.identity);

        //Set the spawned number's text or 3D model representation

        //Remove the spawned number from the available numbers
        availableNumbers.RemoveAt(randomIndex);

        //Show the next button after a delay
        Invoke("ShowNextButton", 1.5f);

    }

    private void ShowChallengeButton()
    {
        
        challengeButton.gameObject.SetActive(true);
    }


    private void ShowNextButton()
    {
        //Show the next button
        nextButton.gameObject.SetActive(true);
    }

   



}
    