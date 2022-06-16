using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Configs/Question")]
public class Question : ScriptableObject
{
    [TextArea(3, 10)]
    public string QuestionText;

    public Sprite Image;

    public Answer Answer1;
    public Answer Answer2;
    public Answer Answer3;
    public Answer Answer4;
}