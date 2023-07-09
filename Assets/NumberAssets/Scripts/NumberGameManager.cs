using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NumberGameManager : MonoBehaviour

{
    public GameObject[] numberPrefabs;
    public Transform spawnPosition;
    public Button[] numberButtons;
    public Text[] feedbackTexts = new Text[2];
    public Text resultText;
    public Animator animator;
    public AudioSource positiveSound;
    public AudioSource negativeSound;
    public UnityEngine.UI.Button restartButton;

    private List<int> availableNumbers = new List<int>();
    private int totalNumbers = 10;
    private int currentNumber;
    private int selectedNumber;
    private GameObject currentNumberObject;
    
 

    private void Start()
    {
        // Initialize the list of available numbers
        for (int i = 1; i <= totalNumbers; i++)
        {
            availableNumbers.Add(i);
        }

        //Hide the result text congratulations
        resultText.gameObject.SetActive(false);

        //Reset the feedback text
        feedbackTexts[0].text = "";
        feedbackTexts[1].text = "";

        restartButton.gameObject.SetActive(false);

        // Start the game by spawning the first number
        SpawnNumber();

       
    }





    private void SpawnNumber()
    {
        // Destroy the previous number if it exists
        if (currentNumberObject != null)
        {
            Destroy(currentNumberObject);
        }

   
            
        // Get a random number from the available numbers
        int randomIndex = Random.Range(0, availableNumbers.Count);
        currentNumber = availableNumbers[randomIndex];

        //Remove the selected number from the available numbers list
        availableNumbers.RemoveAt(randomIndex);

        // Spawn the 3D number object
        currentNumberObject = Instantiate(numberPrefabs[currentNumber - 1], spawnPosition.position, Quaternion.identity);


        Debug.Log("Number is spawned");

       
        

    }


    private void ShowNumberButtons()
    {
        foreach (Button button in numberButtons)
        {
            button.gameObject.SetActive(true);
        }
    }


    private void UpdateGame()
    {
        

        // Check if all numbers have been spawned
        if (availableNumbers.Count == 0)
        {

            Debug.Log("All numbers have been spawned.");
            // Display the congratulations message and end the game
            resultText.gameObject.SetActive(true);
            resultText.text = "Congratulations! You've counted all the numbers!";

            feedbackTexts[0].text = " ";
            feedbackTexts[1].text = " ";


            restartButton.gameObject.SetActive (true);

            availableNumbers.Clear();
           
            return;
        }

        else
        {
            //Spawn the next number
            SpawnNumber();
        }

        if (availableNumbers.Count > 0)
        {
            SpawnNumber();
        }
        else
        {
            resultText.text = "Congratulations! You've counted all the numbers!";

        }
 

       
       
    }

    public void NumberButtonOnClick(int selectedNumber)
    {

        this.selectedNumber = selectedNumber;

        // Check if the selected number matches the spawned number
        if (selectedNumber == currentNumber)
        {


            feedbackTexts[1].text = "Bingo, you're doing a great job!";
            feedbackTexts[0].text = " ";

            animator.SetBool("Walk", false);
            animator.SetTrigger("Jump");

            positiveSound.Play();
            
        }
        else
        {

            feedbackTexts[0].text = "Oops, not a match.";
            feedbackTexts[1].text = " ";

            animator.SetBool("Walk", false);
           
            animator.SetBool("Slide",true);

            negativeSound.Play();

            

        }

        StartCoroutine(ResetWalkParameter());
        
        Debug.Log("NumberButtonOnClick called");
        Debug.Log("Selected Number: " + selectedNumber);
        Debug.Log("Current Number: " + currentNumber);

       
        UpdateGame();
    }


    private IEnumerator ResetWalkParameter()
    {
        yield return new WaitForSeconds(1f);

        animator.SetBool("Walk", true);
        animator.SetBool("Slide", false);

    }



}

