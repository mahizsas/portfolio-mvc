﻿using System;
using Portfolio.Models;

namespace Portfolio
{
    public class ObjectMother
    {
        public static Tag NewTag
        {
            get
            {
                return new Tag
                {
                    Description = "Test Tag",
                    Slug = "test-tag",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
            }
        }

        public static Task NewTask
        {
            get
            {
                return new Task
                {
                    Title = "Test Task",
                    Description = "This is a my test",
                    DueOn = DateTime.UtcNow.AddMonths(1),
                    CompletedAt = null,
                    IsCompleted = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
            }
        }
    }
}