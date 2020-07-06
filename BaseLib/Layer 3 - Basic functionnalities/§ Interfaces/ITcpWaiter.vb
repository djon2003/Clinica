Public Interface ITcpWaiter

    Sub onOneLoopDone(ByVal currentAttemptTime As Integer, ByVal nbTries As Integer, ByVal maxTries As Integer, ByVal isNewTry As Boolean)

End Interface
