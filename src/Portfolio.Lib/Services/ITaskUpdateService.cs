﻿using Portfolio.Lib.Models;

namespace Portfolio.Lib.Services
{
    public interface ITaskUpdateService
    {
        Task UpdateTask(Task model);
    }
}