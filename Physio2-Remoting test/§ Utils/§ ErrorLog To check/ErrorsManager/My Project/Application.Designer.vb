'------------------------------------------------------------------------------
' <auto-generated>
'     Ce code a �t� g�n�r� par un outil.
'     Version du runtime :2.0.50727.3625
'
'     Les modifications apport�es � ce fichier peuvent provoquer un comportement incorrect et seront perdues si
'     le code est r�g�n�r�.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    'REMARQUE�: ce fichier �tant g�n�r� automatiquement, ne le modifiez pas directement. Pour apporter des modifications,
    ' ou si vous rencontrez des erreurs de g�n�ration dans ce fichier, acc�dez au Concepteur de projets
    ' (allez dans les propri�t�s du projet ou double-cliquez sur le noeud My project dans
    ' l'Explorateur de solutions), puis apportez vos modifications sous l'onglet Application.
    '
    Partial Friend Class MyApplication
        
        <Global.System.Diagnostics.DebuggerStepThroughAttribute()>  _
        Public Sub new()
            MyBase.New(Global.Microsoft.VisualBasic.ApplicationServices.AuthenticationMode.Windows)
            Me.IsSingleInstance = false
            Me.EnableVisualStyles = true
            Me.SaveMySettingsOnExit = true
            Me.ShutDownStyle = Global.Microsoft.VisualBasic.ApplicationServices.ShutdownMode.AfterMainFormCloses
        End Sub
        
        <Global.System.Diagnostics.DebuggerStepThroughAttribute()>  _
        Protected Overrides Sub onCreateMainForm()
            Me.MainForm = Global.ErrorsManager.Form1
        End Sub
    End Class
End Namespace
