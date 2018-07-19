Public Class Form1


#Region "Commomn Define"


    Event MeCLose()
    Event ProductionClick()
    Event ProdTableClick(ByVal tabpages As String)
    Event SettingClick()
    Event SecsGemClick()
    Event TCPClientClick()
    Event OpenComForm()
    Event E_SlInfo(ByVal msg As String)             '160624 AddMsg to MDI  \783
#End Region




    'Private Sub Form1_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    Dim myPen As Pen
    '    myPen = New Pen(Color.RoyalBlue, 17)
    '    e.Graphics.DrawLine(myPen, 0, 10, Me.Width, 10)

    '    myPen = New Pen(Color.PowderBlue, 30)
    '    e.Graphics.DrawLine(myPen, 0, 110, Me.Width, 110)


    'End Sub


    Private Sub btnLogin_Click(sender As System.Object, e As System.EventArgs) Handles btnLogin.Click, PictureBox4.Click
        Dim LoginFrm As New LoginForm1
        LoginFrm.ShowDialog()
        If Not OprData.UserLoginResult Then
            Exit Sub
        End If
        btnLogin.Text = OprData.UserLevel.ToString
        RaiseEvent OpenComForm()

    End Sub

   
    'Private Sub btnProduction_Click(sender As System.Object, e As System.EventArgs)
    '    RaiseEvent ProductionClick()
    'End Sub


    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim Ans = MsgBox("ต้องการปิดโปรแกมทั้งหมด ใช่หรือไม่ ?", MsgBoxStyle.YesNo, "Cellcon Exit")
        If Ans = MsgBoxResult.No Then
            Exit Sub
        End If
        RaiseEvent MeCLose()
    End Sub

    
    Private Sub btnProdTable_Click(sender As System.Object, e As System.EventArgs) Handles btnProdTable.Click, pbxProdTable.Click

        RaiseEvent ProdTableClick("tbAlarmCellCon")

    End Sub

   
    Private Sub Form1_Deactivate(sender As Object, e As System.EventArgs) Handles Me.Deactivate

        Timer1.Enabled = False
        Timer1.Enabled = True
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Me.WindowState = FormWindowState.Minimized  '160624 Fix Display

        Timer1.Stop()
        Timer1.Enabled = False
    End Sub
    Protected Overrides ReadOnly Property CreateParams() As CreateParams   'Disable Close(x) Button
        Get
            Dim param As CreateParams = MyBase.CreateParams
            param.ClassStyle = param.ClassStyle Or &H200
            Return param
        End Get
    End Property




    Private Sub btnSetting_Click(sender As System.Object, e As System.EventArgs) Handles btnSetting.Click, pbSetting.Click
        RaiseEvent SettingClick()

    End Sub

    
    Private Sub btnSecsGem_Click(sender As System.Object, e As System.EventArgs) Handles btnSecsGem.Click, pbSecsGem.Click
        RaiseEvent SecsGemClick()
    End Sub

    Private Sub pbxKeyBoard_Click(sender As System.Object, e As System.EventArgs) Handles pbxKeyBoard.Click, btnKeyboard.Click
        KeyBoardOpen()
    End Sub

    Private oskProcess As Process
    Private Sub KeyBoardOpen()
        Try
            If Me.oskProcess Is Nothing OrElse Me.oskProcess.HasExited Then
                If Me.oskProcess IsNot Nothing AndAlso Me.oskProcess.HasExited Then
                    Me.oskProcess.Close()
                End If
                Me.oskProcess = Process.Start("C:\Windows\System32\OSK.EXE")

            End If
        Catch ex As Exception

        End Try


    End Sub
  

    Private Sub pbxTCPClient_Click(sender As System.Object, e As System.EventArgs) Handles pbxTCPClient.Click, btnTCPClient.Click
        If Not My.Settings.CsProtocol_Enable Then

            RaiseEvent E_SlInfo("ไม่สามารถใช้งานได้เนื่องจาก CS Protocol Disable")      '160624 Add msg \783
            Exit Sub
        End If
        RaiseEvent TCPClientClick()

    End Sub

    Private Sub btnTDCLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTDCLock.Click, pbxTDCLock.Click  '160624 TDC LockAdd \783


        If Not OprData.MachineLockByTDC Then                            '160628 \783 Unloack Condition change
            If OprData.UserLevel = CommonData.Level.OP Then
                RaiseEvent E_SlInfo("OP Level ไม่สามารถปลดล็อคการใช้งาน TDC ได้")
                Exit Sub
            End If
            If Not My.Settings.TDC_Enable Then
                RaiseEvent E_SlInfo(" ไม่สามารถปลดล็อคการใช้งาน TDC ได้ เนื่องจาก TDC Disable")
                Exit Sub
            End If

            pbxTDCLock.BackgroundImage = My.Resources.Unlock_256
            OprData.MachineLockByTDC = True
            btnTDCLock.Text = "TDC UnLock"
        Else
            pbxTDCLock.BackgroundImage = My.Resources.Lock_256
            OprData.MachineLockByTDC = False
            btnTDCLock.Text = "TDC Lock"
        End If

    End Sub

    
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, PictureBox1.Click
        Dim frmTorino As New frmTorinokoshi
        If frmTorino.ShowDialog() = Windows.Forms.DialogResult.OK Then
            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
        End If

    End Sub

End Class
