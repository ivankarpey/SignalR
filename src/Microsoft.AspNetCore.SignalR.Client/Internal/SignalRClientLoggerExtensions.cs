// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.SignalR.Client.Internal
{
    internal static class SignalRClientLoggerExtensions
    {
        // Category: HubConnection
        private static readonly Action<ILogger, string, int, Exception> _preparingNonBlockingInvocation =
            LoggerMessage.Define<string, int>(LogLevel.Trace, 0, "Preparing non-blocking invocation of '{target}', with {argumentCount} argument(s).");

        private static readonly Action<ILogger, string, string, int, Exception> _preparingBlockingInvocation =
            LoggerMessage.Define<string, string, int>(LogLevel.Trace, 1, "Preparing blocking invocation of '{target}', with return type '{returnType}' and {argumentCount} argument(s).");

        private static readonly Action<ILogger, string, Exception> _registerInvocation =
            LoggerMessage.Define<string>(LogLevel.Debug, 2, "Registering Invocation ID '{invocationId}' for tracking.");

        private static readonly Action<ILogger, string, string, string, string, Exception> _issueInvocation =
            LoggerMessage.Define<string, string, string, string>(LogLevel.Trace, 3, "Issuing Invocation '{invocationId}': {returnType} {methodName}({args}).");

        private static readonly Action<ILogger, string, Exception> _sendInvocation =
            LoggerMessage.Define<string>(LogLevel.Information, 4, "Sending Invocation '{invocationId}'.");

        private static readonly Action<ILogger, string, Exception> _sendInvocationCompleted =
            LoggerMessage.Define<string>(LogLevel.Information, 5, "Sending Invocation '{invocationId}' completed.");

        private static readonly Action<ILogger, string, Exception> _sendInvocationFailed =
            LoggerMessage.Define<string>(LogLevel.Error, 6, "Sending Invocation '{invocationId}' failed.");

        //Received Invocation '{invocationId}': {methodName}({args})
        private static readonly Action<ILogger, string, string, string, Exception> _receivedInvocation =
            LoggerMessage.Define<string, string, string>(LogLevel.Trace, 7, "Received Invocation '{invocationId}': {methodName}({args}).");

        public static void PreparingNonBlockingInvocation(this ILogger logger, string target, int count)
        {
            _preparingNonBlockingInvocation(logger, target, count, null);
        }

        public static void PreparingBlockingInvocation(this ILogger logger, string target, string returnType, int count)
        {
            _preparingBlockingInvocation(logger, target, returnType, count, null);
        }

        public static void RegisterInvocation(this ILogger logger, string invocationId)
        {
            _registerInvocation(logger, invocationId, null);
        }

        public static void IssueInvocation(this ILogger logger, string invocationId, string returnType, string methodName, object[] args)
        {
            if (logger.IsEnabled(LogLevel.Trace))
            {
                var argsList = string.Join(", ", args.Select(a => a.GetType().FullName));
                _issueInvocation(logger, invocationId, returnType, methodName, argsList, null);
            }
        }

        public static void SendInvocation(this ILogger logger, string invocationId)
        {
            _sendInvocation(logger, invocationId, null);
        }

        public static void SendInvocationCompleted(this ILogger logger, string invocationId)
        {
            _sendInvocationCompleted(logger, invocationId, null);
        }

        public static void SendInvocationFailed(this ILogger logger, string invocationId, Exception exception)
        {
            _sendInvocationFailed(logger, invocationId, exception);
        }

        public static void ReceivedInvocation(this ILogger logger, string invocationId, string methodName, object[] args)
        {
            if (logger.IsEnabled(LogLevel.Trace))
            {
                var argsList = string.Join(", ", args.Select(a => a.GetType().FullName));
                _receivedInvocation(logger, invocationId, methodName, argsList, null);
            }
        }
    }
}
