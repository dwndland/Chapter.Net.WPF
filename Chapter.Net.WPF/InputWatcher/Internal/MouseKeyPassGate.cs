// -----------------------------------------------------------------------------------------------------------------
// <copyright file="MouseKeyPassGate.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Input;
using Chapter.Net.WinAPI.Data;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF
{
    internal class MouseKeyPassGate
    {
        private readonly EventSequenceRecorder _recorder;

        public MouseKeyPassGate(MouseAction mouseAction)
        {
            MouseAction = mouseAction;
            _recorder = new EventSequenceRecorder();
            switch (MouseAction)
            {
                case MouseAction.LeftDoubleClick:
                    _recorder.Sequence(WM.LBUTTONDOWN, WM.LBUTTONUP, WM.LBUTTONDOWN, WM.LBUTTONUP);
                    break;
                case MouseAction.RightDoubleClick:
                    _recorder.Sequence(WM.RBUTTONDOWN, WM.RBUTTONUP, WM.RBUTTONDOWN, WM.RBUTTONUP);
                    break;
                case MouseAction.MiddleDoubleClick:
                    _recorder.Sequence(WM.MBUTTONDOWN, WM.MBUTTONUP, WM.MBUTTONDOWN, WM.MBUTTONUP);
                    break;
            }
        }

        public MouseAction MouseAction { get; }

        public bool Pass(IntPtr wParam, IntPtr lParam)
        {
            switch (MouseAction)
            {
                case MouseAction.LeftClick:
                    return wParam.ToInt32() == WM.LBUTTONUP;
                case MouseAction.RightClick:
                    return wParam.ToInt32() == WM.RBUTTONUP;
                case MouseAction.MiddleClick:
                case MouseAction.WheelClick:
                    return wParam.ToInt32() == WM.MBUTTONUP;
                case MouseAction.LeftDoubleClick when wParam.ToInt32() == WM.LBUTTONDOWN:
                    return _recorder.Pass(WM.LBUTTONDOWN);
                case MouseAction.LeftDoubleClick when wParam.ToInt32() == WM.LBUTTONUP:
                    return _recorder.Pass(WM.LBUTTONUP);
                case MouseAction.RightDoubleClick when wParam.ToInt32() == WM.RBUTTONDOWN:
                    return _recorder.Pass(WM.RBUTTONDOWN);
                case MouseAction.RightDoubleClick when wParam.ToInt32() == WM.RBUTTONUP:
                    return _recorder.Pass(WM.RBUTTONUP);
                case MouseAction.MiddleDoubleClick when wParam.ToInt32() == WM.MBUTTONDOWN:
                    return _recorder.Pass(WM.MBUTTONDOWN);
                case MouseAction.MiddleDoubleClick when wParam.ToInt32() == WM.MBUTTONUP:
                    return _recorder.Pass(WM.MBUTTONUP);
                default:
                    return false;
            }
        }
    }
}