using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HandleData
{
    [CreateAssetMenu(fileName ="Question_Data")]
    public class ScriptTable_Question : ScriptableObject
    {
        public string question;
        public string answerA;
        public string answerB;
        public string answerC;
        public string answerD;
        public string correctAnswer;
    }

}
