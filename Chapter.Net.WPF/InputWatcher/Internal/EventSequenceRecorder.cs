// -----------------------------------------------------------------------------------------------------------------
// <copyright file="EventSequenceRecorder.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Chapter.Net.WinAPI;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

internal class EventSequenceRecorder
{
    private readonly List<int> _happened;
    private readonly Stopwatch _stopwatch;
    private List<int> _sequence;

    public EventSequenceRecorder()
    {
        _stopwatch = new Stopwatch();
        _happened = [];
    }

    public void Sequence(params int[] events)
    {
        _sequence = events.ToList();
    }

    public bool Pass(int @event)
    {
        _happened.Add(@event);
        if (_happened.Count == 1)
        {
            _stopwatch.Restart();
            return false;
        }

        var entries = _happened.ToArray();
        if (_happened.Count == _sequence.Count)
        {
            _stopwatch.Stop();
            _happened.Clear();
        }

        return _sequence.SequenceEqual(entries) && _stopwatch.Elapsed.TotalMilliseconds <= User32.GetDoubleClickTime();
    }
}