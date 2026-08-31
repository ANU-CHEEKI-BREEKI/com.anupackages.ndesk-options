//  *****************************************************************************
//  File:       Utils.cs
//  Solution:   NDesk.Options
//  Project:    Tests
//  Date:       09/26/2017
//  Author:     Latency McLaughlin
//  Copywrite:  Bio-Hazard Industries - 1998-2017
//  *****************************************************************************

using System;

namespace Tests {
  internal static class Utils {
    public static void AssertException<T>(Type exception, string message, T a, Action<T> action) {
      Type actualType = null;
      string stack = null;
      string actualMessage = null;
      try {
        action(a);
      } catch (Exception e) {
        actualType = e.GetType();
        actualMessage = e.Message;
        if (!(actualType == exception))
          stack = e.ToString();
      }
      if (!(actualType == exception)) {
        throw new InvalidOperationException($"Assertion failed: Expected Exception Type {exception}, got {actualType}.\n" + $"Actual Exception: {stack}");
      }
      if (Normalize(actualMessage) != Normalize(message)) {
        throw new InvalidOperationException($"Assertion failed:\n\tExpected: {message}\n\t  Actual: {actualMessage}");
      }
    }

    // expected messages are written with \r\n, but the runtime builds them with
    // Environment.NewLine, which is \n on macOS and Linux editors
    private static string Normalize(string message) {
      return message?.Replace("\r\n", "\n");
    }
  }
}