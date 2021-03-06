﻿using PlexDL.AltoHTTP.Common.Events.EventHandlers;
using System;

namespace PlexDL.AltoHTTP.Interfaces
{
    internal interface IQueue
    {
        int QueueLength { get; }
        int CurrentIndex { get; }

        event EventHandler QueueCompleted;

        event QueueElementCompletedEventHandler QueueElementCompleted;

        void Add(string url, string destPath);

        void StartAsync();

        void ResumeAsync();

        void Pause();

        void Cancel();
    }
}