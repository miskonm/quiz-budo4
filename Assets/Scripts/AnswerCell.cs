using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerCell : MonoBehaviour
{
    #region Variables

    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private Image _image;

    [Header("Colors")]
    [SerializeField] private Color _defaultColor = Color.white;
    [SerializeField] private Color _wrongColor = Color.red;
    [SerializeField] private Color _rightColor = Color.green;

    private Action<AnswerCell> _buttonCallback;

    #endregion


    #region Properties

    public Answer Answer { get; private set; }

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        _button.onClick.AddListener(ButtonClicked);
    }

    #endregion


    #region Public methods

    public void Setup(Answer answer)
    {
        Answer = answer;
        _label.text = answer.Text;
    }

    public void OnButtonClicked(Action<AnswerCell> clickCallback)
    {
        _buttonCallback = clickCallback;
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void SetDefaultState()
    {
        _image.color = _defaultColor;
    }

    public void SetStateDependOnAnswer()
    {
        _image.color = Answer.IsRight ? _rightColor : _wrongColor;
    }

    public void SetClickable(bool isClickable)
    {
        _button.enabled = isClickable;
    }

    #endregion


    #region Private methods

    private void ButtonClicked()
    {
        _buttonCallback?.Invoke(this);
    }

    #endregion
}