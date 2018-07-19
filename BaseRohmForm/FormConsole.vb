Imports XtraLibrary.SecsGem

Public Class FormConsole

    Private Shared _Instance As FormConsole

    Public Shared Function GetInstance(ByVal ctrl As MDIParent1) As FormConsole
        If _Instance Is Nothing OrElse _Instance.IsDisposed Then
            _Instance = New FormConsole(ctrl)
        End If
        Return _Instance
    End Function

    Private m_Controller As MDIParent1

    Public Sub New(ByVal ctrl As MDIParent1)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        m_Controller = ctrl

    End Sub

    Private Sub FormConsole_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        RemoveHandler m_Controller.Host.TracedSmlLog, AddressOf ThreadSafe_SecsTracedLog
    End Sub

    Private Sub FormConsole_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        AddHandler m_Controller.Host.TracedSmlLog, AddressOf ThreadSafe_SecsTracedLog
        Dim scr = Screen.FromPoint(Me.Location)                    'POP Window on TopRight
        Me.Location = New Point(scr.WorkingArea.Right - Me.Width, scr.WorkingArea.Bottom - Me.Height)
    End Sub
    
    Private Sub ThreadSafe_SecsTracedLog(ByVal sender As Object, ByVal e As TraceLogEventArgs)
        If Me.InvokeRequired Then
            'http://kristofverbiest.blogspot.com/2007/02/avoid-invoke-prefer-begininvoke.html
            Me.BeginInvoke(New TraceLogEventHandler(AddressOf ThreadSafe_SecsTracedLog), sender, e)
            Exit Sub
        End If
        Try
            txtCommand.AppendText(String.Format("{0:yyyy/MM/dd HH:mm:ss.fff} [{1}] " & Environment.NewLine & "{2}" & Environment.NewLine, _
                                             e.TimeStamp, e.Direction, e.SML))
        Catch ex As Exception
            SaveCatchLog(ex.Message, "ThreadSafe_SecsTracedLog")
        End Try
    End Sub

    
  
End Class