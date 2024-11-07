﻿using DfE.CoreLibs.AsyncProcessing.Interfaces;

namespace Dfe.Complete.Application.Services.BackgroundServices.Events
{
    public class CreateReportExampleTaskCompletedEvent(string taskName, string message) : IBackgroundServiceEvent
    {
        public string TaskName { get; } = taskName;
        public string Message { get; } = message;
    }
}