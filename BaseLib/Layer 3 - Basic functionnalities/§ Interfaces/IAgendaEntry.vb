Public Interface IAgendaEntry
    Inherits IDBItemable

    Sub cut()
    Sub copy()
    Sub pasteTo(ByVal dateHeure As Date, ByVal noTRP As Integer, ByVal newPeriod As Integer)
End Interface
