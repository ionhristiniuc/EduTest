using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduTestContract.Models;
using EduTestContract.Models.Enums;

namespace EduTestService.Core.Factories
{
    public class QuestionsFactory
    {
        public QuestionBaseModel CreateQuestion(QuestionType questionType)
        {
            switch (questionType)
            {
                case QuestionType.Checkbox:
                case QuestionType.Radio:
                    return new VariantQuestionModel();                    
                case QuestionType.TextArea:
                    return new TextAreaQuestionModel();                    
                default:
                    throw new NotSupportedException($"Unknown question type: {questionType}");
            }
        }
    }
}