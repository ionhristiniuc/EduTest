using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduTestContract.Models;
using EduTestContract.Models.Enums;
using EduTestService.Core.Factories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EduTestService.Core.Converters
{
    public class PolymorphicQuestionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);
            QuestionBaseModel questionBase;
            var typeToken = obj[nameof(questionBase.Type)];            
            if (typeToken == null)            
                throw new ArgumentException("Missing question type", nameof(questionBase.Type));            

            QuestionType questionType = (QuestionType) typeToken.Value<int>();
            questionBase = new QuestionsFactory().CreateQuestion(questionType);

            serializer.Populate(obj.CreateReader(), questionBase);
            return questionBase;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (QuestionBaseModel);
        }
    }
}