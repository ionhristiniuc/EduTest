//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EduTestData.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class AnswerBase
    {
        public int Id { get; set; }
        public int QuestionBaseId { get; set; }
        public int TestInstanceId { get; set; }
    
        public virtual QuestionBase QuestionBase { get; set; }
        public virtual TestInstance TestInstance { get; set; }
        public virtual TextareaAnswer TextareaAnswer { get; set; }
        public virtual VariantAnswer VariantAnswer { get; set; }
    }
}
