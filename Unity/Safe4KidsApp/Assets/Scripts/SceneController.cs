using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public const int gridRows = 3;
    public const int gridCols = 4;
    public float offsetX = 4f;
    public float offsetY = 3f;

    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] images;

    private void Start()
    {
        Vector3 startPos = originalCard.transform.position;

        int[] numbers = {0,0,0,0,0,0,0,0,0,0,0,0,0,0};
        //Create random picked array
        numbers = PickImages(numbers);
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)

        {
            for (int j = 0; j < gridRows; j++)
            {

                MainCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] PickImages(int[] numbers)
    {
        //int ImageNumber = 12;
        int ImageNumber = (gridRows * gridCols);

        for (int i = 0; i < ImageNumber; i++)
        {
            int tmp;

            tmp = i;
            
            int rnd = 0;
            foreach (int y in numbers)
            {
                
                if (y.Equals(rnd))
                {
                    Debug.Log(rnd + "Already exists");
                    rnd = Random.Range(0, ImageNumber);
                    break;
                }
                else { Debug.Log(rnd + " doesn't exist"); }


                
            }
            numbers[tmp] = rnd;

            Debug.Log("     entry: " + tmp + "contains: " + rnd);

            
            int pairTemp = tmp++;
            numbers[tmp] = rnd;
            Debug.Log("pair entry: " + pairTemp + "contains: " + rnd);

        }
        return numbers;

    }

    private bool CheckExists(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        foreach (int x in numbers)
        {

            foreach (int y in numbers)
            {
                if (numbers[x] == numbers[y])
                {
                    return true;
                }
            }
        }
        return false;
    }
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)

        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);

            newArray[i] = newArray[r];
            newArray[r] = tmp;

        }
        return newArray;
    }


    private MainCard _firstRevealed;
    private MainCard _secondRevealed;


    private int _score = 0;
    [SerializeField] private TextMesh scoreLabel;


    public bool canReveal
    {

        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MainCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());

        }
    }

    private IEnumerator CheckMatch()

    {
        if (_firstRevealed.id == _secondRevealed.id)
        {

            _score++;
            scoreLabel.text = "Score: " + _score;

        }
        else
        {
            yield return new WaitForSeconds(0.5f);


            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;

    }

    public void Restart()
    {
        SceneManager.LoadScene("Scene_001");
    }

}