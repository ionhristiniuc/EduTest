using System;
using System.Collections.Generic;
using EduTestClient.Services;
using EduTestClient.Services.Utils;
using EduTestContract.Models;
using EduTestContract.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class QuestionsServiceTest
    {
        [TestMethod]
        public async void AddVariantQuestion()
        {
            var question = new VariantQuestionModel()
            {
                Content = "Choose an answer!",
                Enabled = true,
                TopicId = 1,
                Type = QuestionType.Radio,
                UserId = 1,
                Variants = new List<VariantModel>()
                {
                    new VariantModel() { Body = "Answer A", Correct = false},
                    new VariantModel() { Body = "Answer B", Correct = false},
                    new VariantModel() { Body = "Answer C", Correct = true},
                }
            };

            IAccountService accountService = new AccountService();
            var resp = await accountService.Authenticate("ionhristiniuc@yahoo.com", "ion123");
            IQuestionsService service = new QuestionsService(resp.access_token, new JsonSerializer());
            Assert.IsTrue(service.AddQuestion(question.TopicId, question).Result);
        }
    }
}
