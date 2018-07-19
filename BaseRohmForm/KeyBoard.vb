Public Class KeyBoard

    Friend TargetTextBox As TextBox
    Friend NumPad As Boolean
    Friend TargetLabel As Label

    Private Sub KeyBoard_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If NumPad Then
            If TargetLabel IsNot Nothing Then
                If IsNumeric(TargetLabel) Then
                    TargetLabel.Text = CStr(CDbl(TargetLabel.Text))
                End If
            End If
            If TargetTextBox IsNot Nothing Then
                If IsNumeric(TargetTextBox) Then
                    TargetTextBox.Text = CStr(CDbl(TargetTextBox.Text))
                End If
            End If
        End If
    End Sub


    Private Sub KeyBoard_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If NumPad Then                                      'NumPad
            Me.Size = New Size(365, 465)
            Panel2.Hide()
            Me.Text = "NumPad"
            pbxHelper.Location = New System.Drawing.Point(Panel1.Location.X + 5, Panel1.Location.Y + Panel1.Height + 25)
        Else                                                'KeyBoard
            Me.Text = "Key Board"
            Dim i As Integer
            Dim j As Integer
            For i = 0 To 27
                Dim b1 As New BT
                If i = 26 Then
                    b1.Text = Chr(44)                        'Comma
                ElseIf i = 27 Then
                    b1.Text = Chr(Asc("-"))
                Else
                    b1.Text = Chr(65 + i)
                End If

                Panel2.Controls.Add(b1)
                j = i Mod 8
                b1.Left = 5 + (j * b1.Width)
                j = i \ 8
                b1.Top = 10 + (b1.Height * j)
                AddHandler b1.Click, AddressOf LetterClick
            Next


        End If
        If pbxHelper.BackgroundImage Is Nothing Then
            Me.Size = New Size(Me.Width, 330)
            Label2.Hide()
        Else
            Label2.Show()
        End If



    End Sub
    Private Sub LetterClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim btnKeys As Button = CType(sender, Button)
        tbxMonitorx.Focus()
        My.Computer.Keyboard.SendKeys(btnKeys.Text)

    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click   'Del
        tbxMonitorx.Text = ""
    End Sub
    Dim TextMonitorIndex As Integer = 1
    Private Sub btsLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btsLeft.Click      
            tbxMonitorx.Focus()
            My.Computer.Keyboard.SendKeys("{LEFT}")
    End Sub

    Private Sub btnBS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBS.Click
        tbxMonitorx.Focus()
        My.Computer.Keyboard.SendKeys("{BS}")

    End Sub

    Private Sub btnRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRight.Click
        tbxMonitorx.Focus()
        My.Computer.Keyboard.SendKeys("{RIGHT}")
    End Sub

    Private Sub btn0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn0.Click, btn00.Click, btn000.Click _
    , btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click
        Dim btnKeys As Button = CType(sender, Button)
        tbxMonitorx.Focus()
        My.Computer.Keyboard.SendKeys(btnKeys.Text)

    End Sub



    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If TargetTextBox IsNot Nothing Then
            TargetTextBox.Focus()
            My.Computer.Keyboard.SendKeys("{ENTER}")
        End If

        Me.Close()
    End Sub



    Private Sub tbxMonitorx_TextChanged(sender As Object, e As System.EventArgs) Handles tbxMonitorx.TextChanged
        If TargetTextBox IsNot Nothing Then
            TargetTextBox.Text = tbxMonitorx.Text
        End If
        If TargetLabel IsNot Nothing Then
            TargetLabel.Text = tbxMonitorx.Text
        End If
    End Sub
    Private _TagID As String
    Public Property TagID() As String
        Get
            Return _TagID
        End Get
        Set(ByVal value As String)
            _TagID = value

        End Set
    End Property



End Class
Public Class BT
    Inherits Button
    Public Sub New()
        MyBase.New()
        Me.Font = New Font("Tahoma", 8, FontStyle.Regular)
        Me.Size = New Size(60, 50)
        Me.BackColor = Color.WhiteSmoke

    End Sub

End Class