﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Portfolio.Common;
using Portfolio.Data.Models;

namespace Portfolio.Web.ViewModels
{
    public class TaskInputModel
    {
        private readonly Task task;

        public TaskInputModel()
            : this(new Task())
        {            
        }

        public TaskInputModel(Task task)
        {
            Ensure.ArgumentIsNotNull(task, "task");
            this.task = task;
        }

        public string ActionName
        {
            get { return IsNew ? "New" : "Edit"; }
        }

        public SelectList Categories { get; set; }

        public string ControllerName
        {
            get { return "Tasks"; }
        }

        public int SelectedCategory { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description
        {
            get
            {
                if (task.Description == null)
                    task.Description = "";
                
                return task.Description;                
            }
            set 
            {
                task.Description = value != null ? value.Trim() : "";
            }
        }

        public string DueOn
        {
            get { return task.DueOn.HasValue ? task.DueOn.Value.ToShortDateString() : ""; }
            set { task.DueOn = value.SafeParseDateTime(); }
        }

        public int Id
        {
            get { return task.Id; }
            set { task.Id = value; }
        }

        public bool IsNew
        {
            get { return Id == 0; }
        }

        public object RouteValues
        {
            get
            {
                if (IsNew)
                    return new { };

                return new { id = Id };                
            }
        }

        public string Title
        {
            get { return Id == 0 ? "New Task" : "Edit Task"; }
        }

        public Task GetTask()
        {
            return task;
        }
    }
}