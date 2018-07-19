Public Class LoginForm1

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

#Region "Commomn Define"

   

#End Region
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        'If Pass Please set True
        If UsernameTextBox.Text = "ADMIN" Then
            If PasswordTextBox.Text = My.Settings.UserAdminPass Then
                OprData.UserLoginResult = True
                OprData.UserLevel = CommonData.Level.ADMIN
                Me.Close()
                Exit Sub
            End If
        End If



        For i = 0 To UserTable.Rows.Count - 1
            Dim r As DataRow = UserTable.Rows(i)
            If r.Item("UserName").ToString = UsernameTextBox.Text And r.Item("PassWord").ToString = PasswordTextBox.Text Then
                Dim Val As Integer = CInt([Enum].Parse(GetType(CommonData.Level), CStr(r.Item("UserType")))) '160624 Revise \783
                OprData.UserLevel = CType(Val, CommonData.Level)                                             '160624 Revise \783

                OprData.UserID = UsernameTextBox.Text
                OprData.UserLoginResult = True
                Me.Close()
                Exit Sub
            End If
        Next


        OprData.UserID = UsernameTextBox.Text
        OprData.UserLoginResult = True
        OprData.UserLevel = CommonData.Level.OP
        Me.Close()

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

  
End Class
