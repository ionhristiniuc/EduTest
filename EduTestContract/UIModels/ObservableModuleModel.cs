﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTestContract.Models;

namespace EduTestContract.UIModels
{
    public class ObservableModuleModel
    {
        public int CourseId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<ObservableChapterModel> Chapters { get; set; }
        public ObservableCollection<TestModel> Tests { get; set; }
    }
}
