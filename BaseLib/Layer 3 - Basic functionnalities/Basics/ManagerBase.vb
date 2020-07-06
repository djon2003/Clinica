''' <summary>
''' Base class to create a manager.  Automatically add the singleton pattern by the usage of Self. All child classes have to have a protected constructor.
''' </summary>
''' <typeparam name="Self">Same class name has the one inheriting the ManagerBase (for Singleton pattern)</typeparam>
''' <remarks>Self usage allow the usage of the auto generate dropbox of the designer</remarks>
Public MustInherit Class ManagerBase(Of Self)
    Implements IManager(Of Self)

#Region "Singleton"
    Protected Sub New()
    End Sub

    Private Shared mySelf As Self = Nothing
    Private Shared _realType As Type = Nothing

    Protected Shared Sub createInstance()
        If mySelf Is Nothing Then
            If _realType Is Nothing Then _realType = GetType(Self)

            Dim constructor As System.Reflection.ConstructorInfo = _realType.GetConstructor(Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance, Nothing, New System.Type() {}, Nothing)
            If constructor Is Nothing OrElse (constructor.Attributes And Reflection.MethodAttributes.Family) <> Reflection.MethodAttributes.Family Then Throw New Exception("Derived class has to have a constructor with a protected scope")
            mySelf = constructor.Invoke(New Object() {})
        End If
    End Sub

    Protected Shared Sub setRealType(ByVal realType As Type)
        _realType = realType
    End Sub

    Public Shared Function getInstance() As Self
        createInstance()

        Return mySelf
    End Function
#End Region


End Class
