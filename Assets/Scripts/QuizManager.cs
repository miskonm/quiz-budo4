using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuizManager : MonoBehaviour
{
    #region Variables

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _questionLabel;
    [SerializeField] private Image _questionImage;
    [SerializeField] private Button _helpButton;

    [Header("Answer Cells")]
    [SerializeField] private AnswerCell _firstCell;
    [SerializeField] private AnswerCell _secondCell;
    [SerializeField] private AnswerCell _thirdCell;
    [SerializeField] private AnswerCell _fourthCell;

    [Header("Timer")]
    [SerializeField] private Timer _timer;
    [SerializeField] private float _nextQuestionDelay = 1f;

    [Header("Questions")]
    [SerializeField] private Question[] _questions;

    [SerializeField] private int _hp;

    private int _currentIndex;

    private Question[] _currentQuestions;
    private AnswerCell[] _answerCells;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        SetupCellsArray();
        RandomQuestions();
        _helpButton.onClick.AddListener(HelpButtonClicked);

        _firstCell.OnButtonClicked(AnswerCellClicked);
        _secondCell.OnButtonClicked(AnswerCellClicked);
        _thirdCell.OnButtonClicked(AnswerCellClicked);
        _fourthCell.OnButtonClicked(AnswerCellClicked);
    }

    private void Start()
    {
        SetupCurrentQuestion();
    }

    #endregion


    #region Private methods

    private void SetupCellsArray()
    {
        _answerCells = new[] {_firstCell, _secondCell, _thirdCell, _fourthCell};
    }

    private void RandomQuestions()
    {
        _currentQuestions = _questions;
    }

    private void AnswerCellClicked(AnswerCell clickedCell)
    {
        if (!clickedCell.Answer.IsRight)
        {
            _hp--;
        }

        clickedCell.SetStateDependOnAnswer(); // Set clicked button state
        SetRightButtonState(); // Set right button state
        // DoForAllCells((cell) =>
        // {
        //     if (cell.Answer.IsRight)
        //         cell.SetStateDependOnAnswer();
        // });
        SetClickableAllCells(false); // Turn off all buttons
        _timer.StartTimer(_nextQuestionDelay, GoToNextQuestion); // Timer
    }

    private void GoToNextQuestion()
    {
        ResetButtons();
        _currentIndex++;
        // TODO: Check game over. Check end of questions or hp == 0
        SetupCurrentQuestion();
    }

    private void ResetButtons()
    {
        // DoForAllCells(cell => cell.SetActive(true));
        // DoForAllCells(cell => cell.SetClickable(true));
        // DoForAllCells(cell => cell.SetDefaultState());

        TurnOnAllButtons();
        SetClickableAllCells(true);
        SetAllCellsDefaultState();
    }

    private void SetAllCellsDefaultState()
    {
        foreach (AnswerCell answerCell in _answerCells)
        {
            answerCell.SetDefaultState();
        }
    }

    private void TurnOnAllButtons()
    {
        foreach (AnswerCell answerCell in _answerCells)
        {
            answerCell.SetActive(true);
        }
    }

    private void SetClickableAllCells(bool isClickable)
    {
        foreach (AnswerCell answerCell in _answerCells)
        {
            answerCell.SetClickable(isClickable);
        }
    }

    private void SetRightButtonState()
    {
        foreach (AnswerCell answerCell in _answerCells)
        {
            if (answerCell.Answer.IsRight)
                answerCell.SetStateDependOnAnswer();
        }
    }

    private void DoForAllCells(Action<AnswerCell> action)
    {
        foreach (AnswerCell answerCell in _answerCells)
        {
            action?.Invoke(answerCell);
        }
    }

    private void SetupCurrentQuestion()
    {
        Question question = CurrentQuestion();

        _questionLabel.text = question.QuestionText;
        _questionImage.sprite = question.Image;
        _firstCell.Setup(question.Answer1);
        _secondCell.Setup(question.Answer2);
        _thirdCell.Setup(question.Answer3);
        _fourthCell.Setup(question.Answer4);
    }

    private Question CurrentQuestion()
    {
        return _questions[_currentIndex];
    }

    private void HelpButtonClicked()
    {
        List<AnswerCell> wrongCells = GetWrongCells();
        TurnOfTwoRandomCells(wrongCells);
    }

    private void TurnOfTwoRandomCells(List<AnswerCell> wrongCells)
    {
        int cellsCount = 0;

        while (cellsCount < 2)
        {
            int randomIndex = Random.Range(0, wrongCells.Count);

            AnswerCell answerCell = wrongCells[randomIndex];
            wrongCells.Remove(answerCell);

            answerCell.SetActive(false);

            cellsCount++;
        }
    }

    private List<AnswerCell> GetWrongCells()
    {
        List<AnswerCell> wrongCells = new List<AnswerCell>();

        foreach (AnswerCell answerCell in _answerCells)
        {
            if (!answerCell.Answer.IsRight)
            {
                wrongCells.Add(answerCell);
            }
        }

        return wrongCells;
    }

    #endregion
}