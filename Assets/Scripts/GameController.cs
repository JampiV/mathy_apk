using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Linq;
public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;
    public Sprite[] puzzles;
    private Sprite[] copiaArray; //Array de puzzles aleatorio
    private List<int> superPirateList; //Lista utilizada para validar
    private List<int> idDePares = new List<int>();
    public List<Button> btns = new List<Button>();
    public List<Puzzle> gamePuzzles = new List<Puzzle>();

    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Sprites/lista");
        copiaArray = puzzles;
        superPirateList = new List<int>{};
    }

    void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = 5;
        //gameGuesses = gamePuzzles.Count / 2;
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }

    void AddGamePuzzles()
    {
        int contador = 0;
        randomizarArrayPirata(copiaArray);
        foreach (var puzzle in copiaArray)
        {
            int pairId = ExtractNumberFromName(puzzle.name); // Obtiene el identificador de par
            int contadorDePares = idDePares.Count(a => a == pairId); //Contando los elementos en la lista de idDePares que sean pairId
            Debug.Log("Se ha contado que en la lista idDePares existen "+ contadorDePares + " pairID iguales " + pairId);
            
            if (!idDePares.Contains(pairId) && contador < 5){
                idDePares.Add(pairId);
                Debug.Log("par id nuevo agregado a la lista de ids: " + pairId);
                contador++;
            }
            else if (idDePares.Contains(pairId) && contadorDePares < 2){
                Debug.Log("par id existente agregado a la lista de ids: " + pairId);
                idDePares.Add(pairId);
            }
        }
        foreach (var a in copiaArray){
             int pairId = ExtractNumberFromName(a.name); // Obtiene el identificador de par
             int pairsCountable = superPirateList.Count(b => b == pairId); //Contando los elementos en la lista de idDePares que sean pairId
             Debug.Log("WHATS FCKING UP WITH " + pairsCountable + "(conteo) de id's " + pairId);
             if (idDePares.Contains(pairId) && pairsCountable < 2){
                Debug.Log("Se ha agregado el sprite con id " + pairId + " al PUZZLE DE CARTAS");
                superPirateList.Add(pairId);
                gamePuzzles.Add(new Puzzle(a, pairId));
             }
        }
    }

    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }

    public void PickAPuzzle()
    {
        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].sprite.name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex].sprite;
        }
        else if (!secondGuess && int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name) != firstGuessIndex)

        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].sprite.name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex].sprite;
            countGuesses++;
            StartCoroutine(CheckIfThePuzzlesMatch());
        }
    }

    IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds(1f);

        // Comprueba si los nombres de los sprites seleccionados coinciden
        bool isMatch = gamePuzzles[firstGuessIndex].pairId == gamePuzzles[secondGuessIndex].pairId;

        if (isMatch)
        {
            yield return new WaitForSeconds(.5f);
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            // Cambia el color de los botones para indicar que se ha encontrado un par
            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfTheGameIsFinished();
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            // Restablece las im�genes de los botones a la imagen de fondo si no coinciden
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }

        yield return new WaitForSeconds(.5f);
        firstGuess = secondGuess = false;
    }

    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;
        if (countCorrectGuesses == gameGuesses)
        {
            Debug.Log("Game Finished");
            Debug.Log("It took you " + countGuesses + " many guess(es) to finish the game");
            RestartGame();
        }
    }

    void Shuffle(List<Puzzle> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            // Intercambio de elementos
            Puzzle temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void RestartGame()
    {
        // Carga la escena actual nuevamente
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Definir una nueva clase para manejar los puzzles
    public class Puzzle
    {
        public Sprite sprite;
        public int pairId; // Identificador del par

        public Puzzle(Sprite sprite, int pairId)
        {
            this.sprite = sprite;
            this.pairId = pairId;
        }
    }

    int ExtractNumberFromName(string name)
    {
        // Extrae el n�mero del final del nombre del sprite
        // Asume que el nombre del sprite termina con un n�mero
        var match = Regex.Match(name, @"\d+$");
        return match.Success ? int.Parse(match.Value) : -1;
    }

    public void randomizarArrayPirata<T>(T[] array){
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
        Debug.Log("El array ha sido randomizado");
    }

}