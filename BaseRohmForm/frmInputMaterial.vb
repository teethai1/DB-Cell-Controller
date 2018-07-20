Imports System.IO
Public Class frmInputMaterial
    Dim m_PreformAlarm As String
    Dim m_FrameAlarm As String

    Public m_FrameMarkerLotNo As String
    Public m_FrameType As String
    Public m_FrameQR As String

    Public m_PreformQR As String
    Public m_PreformType As String
    Public m_PreformLotNo As String
    Public m_PreformInput As Date
    Public m_PreformEXP As Date

    Dim QRCodeSpliter As WorkingSlipQRCode
    Public Sub tbRevQR_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbRevQR.KeyPress
        If e.KeyChar = Chr(13) Then
            If lbCaption.Text.Contains("Preform") = True Then
                If e.KeyChar = Convert.ToChar(13) Then
                    If PreformCheck(tbRevQR.Text) = False Then
                        m_frmWarningDialog(m_PreformAlarm, True)
                        tbRevQR.Text = ""
                        ProgressBar1.Value = 0
                        tbRevQR.Focus()
                    Else
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                    End If
                End If
            ElseIf lbCaption.Text.Contains("Frame") = True Then
                If tbRevQR.Text.Length <> 12 OrElse IsNumeric(tbRevQR.Text) = False Then
                    m_frmWarningDialog("QR Framไม่ถูกต้องกรุณาตรวจสอบ ค่าที่อ่านได้คือ (" & tbRevQR.Text & ")", True)
                    tbRevQR.Text = ""
                    tbRevQR.Focus()
                    ProgressBar1.Value = 0
                    Exit Sub
                End If

                'เชคข้อมูลใน(ฐานข้อมูล)
                Try
                    Dim FrameMat As New DBxDataSet.IS_MATL_STOCK_FILEDataTable
                    Dim FrameMatAdapter As New DBxDataSetTableAdapters.IS_MATL_STOCK_FILETableAdapter
                    FrameMatAdapter.FrameMaterail_IS(FrameMat, tbRevQR.Text)

                    If FrameMat.Rows.Count = 0 Then
                        m_frmWarningDialog("QR Frame(" & tbRevQR.Text & ") นี้ยังไม่ถูกบันทึกลงในฐานข้อมูลของ Frame Material ติดต่อห้อง Frame Material หรือ Manual Input", True)

                        Dim frmFrameMan As New frmFrameInputManual
                        If frmFrameMan.ShowDialog = Windows.Forms.DialogResult.OK Then
                            m_FrameMarkerLotNo = frmFrameMan.tbMarkerLotNo.Text
                            m_FrameType = frmFrameMan.tbFrameType.Text
                            m_FrameQR = tbRevQR.Text
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                            Exit Sub
                        Else
                            Me.DialogResult = Windows.Forms.DialogResult.No
                            Me.Close()
                            Exit Sub
                        End If

                    End If


                    m_FrameMarkerLotNo = FrameMat(0).FRAME_LOT1.Trim
                    m_FrameType = FrameMat(0).PROD_NAME.Trim
                    m_FrameQR = tbRevQR.Text


                    Me.DialogResult = Windows.Forms.DialogResult.OK

                Catch ex As Exception
                    m_frmWarningDialog("Exception Error กรุณา ตรวจสอบ Catch Log", True)
                    tbRevQR.Text = ""
                    tbRevQR.Focus()
                End Try
            Else
                tbRevQR.Text = ""
                tbRevQR.Focus()
            End If
        End If

    End Sub

    Private Sub frmInputQR_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        tbRevQR.Focus()
    End Sub


    Private Sub frmInputQR_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Size = CType(New Point(479, 134), Drawing.Size)

        tbRevQR.Focus()

        If lbCaption.Text.Contains("Preform") = True Then
            btManual.Visible = True
            PictureBox1.Image = My.Resources.PreformID
        ElseIf lbCaption.Text.Contains("Frame") = True Then
            btManual.Visible = True
            btManual.Text = "Frame Manual"
            PictureBox1.Image = My.Resources.FrameID
        End If

        If My.Settings.MCType = "IDBW" Then
            ProgressBar1.Visible = False
        End If

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbRevQR.KeyPress
        Try


            ProgressBar1.Value = CInt(CInt(tbRevQR.Text.Length) / 50 * 100)

        Catch ex As Exception

        End Try
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, Button9.Click, Button8.Click, Button7.Click, Button6.Click, Button5.Click, Button4.Click, Button3.Click, Button2.Click, Button12.Click, Button11.Click
        Dim bt As Button = CType(sender, Button)
        tbInputMan.Focus()
        If bt.Text <> "BS" Then
            SendKeys.Send(bt.Text)
        Else
            SendKeys.Send("{BS}")
        End If
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        If lbCaption.Text.Contains("Preform") = True Then
            If PreformCheck("MAT," & tbInputMan.Text) = False Then
                m_frmWarningDialog(m_PreformAlarm, True)
            Else
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If

        ElseIf lbCaption.Text.Contains("Frame") = True Then

            If tbInputMan.Text.Length <> 12 OrElse IsNumeric(tbInputMan.Text) = False Then
                m_frmWarningDialog("QR Framไม่ถูกต้องกรุณาตรวจสอบ ค่าที่อ่านได้คือ (" & tbInputMan.Text & ")", True)
                tbInputMan.Focus()
                Exit Sub
            End If

            'เชคข้อมูลใน(ฐานข้อมูล)
            Try
                Dim FrameMat As New DBxDataSet.IS_MATL_STOCK_FILEDataTable
                Dim FrameMatAdapter As New DBxDataSetTableAdapters.IS_MATL_STOCK_FILETableAdapter
                FrameMatAdapter.FrameMaterail_IS(FrameMat, tbInputMan.Text)

                If FrameMat.Rows.Count = 0 Then
                    m_frmWarningDialog("QR Frame(" & tbInputMan.Text & ") นี้ยังไม่ถูกบันทึกลงในฐานข้อมูลของ Frame Material ติดต่อห้อง Frame Material หรือ Manual Input", True)

                    Dim frmFrameMan As New frmFrameInputManual
                    If frmFrameMan.ShowDialog = Windows.Forms.DialogResult.OK Then
                        m_FrameMarkerLotNo = frmFrameMan.tbMarkerLotNo.Text
                        m_FrameType = frmFrameMan.tbFrameType.Text
                        m_FrameQR = tbInputMan.Text
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                        Exit Sub
                    Else
                        Me.DialogResult = Windows.Forms.DialogResult.No
                        Me.Close()
                        Exit Sub
                    End If

                End If


                m_FrameMarkerLotNo = FrameMat(0).FRAME_LOT1.Trim
                m_FrameType = FrameMat(0).PROD_NAME.Trim
                m_FrameQR = tbInputMan.Text

                Me.DialogResult = Windows.Forms.DialogResult.OK

            Catch ex As Exception
                m_frmWarningDialog("Exception Error กรุณา ตรวจสอบ Catch Log", True)
                tbRevQR.Text = ""
                tbRevQR.Focus()
            End Try
        End If

    End Sub




    Private Sub btManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btManual.Click
        If Me.Size <> CType(New Point(479, 585), Drawing.Size) Then
            Me.Size = CType(New Point(479, 585), Drawing.Size) ''ขนาด Manaul
            tbInputMan.Text = ""
            tbInputMan.Focus()
        Else
            Me.Size = CType(New Point(479, 134), Drawing.Size) 'ขนาด เดิม
            tbRevQR.Focus()
        End If


    End Sub


    Private Function PreformCheck(ByVal QR_Preform As String) As Boolean
        Try

            '=============================== เชคตัวอักษร format ==========================
            Dim revData As String = QR_Preform.Replace(".", "")
            Dim SplitData As String() = revData.Split(CChar(","))
            If UBound(SplitData) <> 1 Then 'NG
                m_PreformAlarm = ("รูปแบบข้อมุล Preform Material QR ไม่ถูกต้อง ค่าที่อ่านได้คือ(" & QR_Preform & ")") ', "Preform Material QR Input", "รูปแบบข้อมุล ไม่ใช (MAT,ตัวเลข PreForm ID)")
                Return False
            Else
                If SplitData(0).Contains("MAT") = False OrElse IsNumeric(SplitData(1)) = False Then
                    m_PreformAlarm = ("รูปแบบข้อมุล Preform Material QR ไม่ถูกต้อง ค่าที่อ่านได้คือ(" & QR_Preform & ")") ', "Preform Material QR Input", "รูปแบบข้อมุล ไม่ใช (MAT,ตัวเลข PreForm ID)")
                    Return False
                End If
            End If
            '=========================== เชค Preform Status "New Out" เท่านั้น ========================
            Dim QRPreform As String = SplitData(1)
            Dim PreformStatusAdpater As New DBxDataSetTableAdapters.QueriesTableAdapter1
            Dim PreformState As String = PreformStatusAdpater.PreformStatus(CInt(QRPreform))
            If PreformState Is Nothing Then
                m_PreformAlarm = ("ข้อมูล Preform (" & QRPreform & ") ไม่มีในระบบ Material กรุณานำไปเข้าระบบที่เครื่อง Material ก่อน นำมาใช้งาน") ', "Preform Material QR Input", "ไม่พบค่านี้ใน MAT.Transaction Table")
                Return False
            ElseIf PreformState = "New In" Then
                m_PreformAlarm = ("Preform สถานะเป็น New In กรุณาเปลี่ยนสถานะที่เครื่อง Material เป็นสถานะ New Out เพื่อใช้งาน") ', "Preform Material QR Input", "TransactionType เป็น New In ไม่ได้")
                Return False
            ElseIf PreformState = "New Out" Then
                GoTo PreformStateOK
            ElseIf PreformState = "Terminate" Then
                m_PreformAlarm = ("Preform(" & QRPreform & ") นี้หมดอายุแล้ว กรุณาเปลี่ยน Preform") ', "Preform Material QR Input", "TransactionType เป็น Terminate ไม่ได้")
                Return False
            Else
                m_PreformAlarm = ("ข้อมูล Preform : MAT," & QRPreform & " สถานะ " & PreformState & " ไม่สามารถใช้งานได้") ', "Preform Material QR Input", "ตรวจสอบ TransactionType สถานะอื่น")
                Return False
            End If
            '=========================================================

PreformStateOK:
            Dim PreformDetailAdapter As New DBxDataSetTableAdapters.ExpirePreformTableAdapter
            Dim PreformDetail As DBxDataSet.ExpirePreformDataTable = PreformDetailAdapter.GetData(CInt(QRPreform))

            Dim PreformLotNo As String = ""
            Dim PreformType As String = ""
            Dim PreformInput As New Date
            Dim PreformExp As New Date
            If PreformDetail.Rows.Count <> 0 Then
                PreformLotNo = PreformDetail.Item(0).MakerLotNo
                PreformType = PreformDetail.Item(0).MaterialModel
                PreformInput = PreformDetail.Item(0).TransactionDate
                PreformExp = PreformDetail.Item(0).ExpireTime
            Else
                m_PreformAlarm = ("ไม่มีข้อมูลในระบบ Preform(" & QRPreform & ") กรุณาติดต่อ System") ', "Preform Material QR Input", "ExpirePreformTableAdapter Query Rows = 0")
                Return False

            End If

            'เชควันหมดอายุ()
            Dim strexp As String = PreformCondition(PreformExp)
            Select Case strexp
                Case PreformStatus.Normal.ToString

                Case PreformStatus.Lower4Hour.ToString
                    m_PreformAlarm = ("อายุของ Preform ต่ากว่า 4 ชั่วโมง กรุณาเปลี่ยนหลอดด้วยครับ") ', "Preform life time", "LifeTime < 240 Min")
                    Return False
                Case PreformStatus.Expired.ToString
                    m_PreformAlarm = ("Preform นี้หมดอายุแล้ว") ', "Preform life time", "LifeTime Over")
                    Return False
            End Select

            m_PreformEXP = PreformExp
            m_PreformInput = PreformInput
            m_PreformLotNo = PreformLotNo
            m_PreformType = PreformType
            m_PreformQR = QRPreform


            Return True
        Catch ex As Exception
            SaveCatchLog(ex.ToString, "PreformCheck()")
            m_PreformAlarm = ("Catch Error ตรวจสอบดู CatchLog") ', "PreformCheck()", "TryCatchEnd", "")
            Return False
        End Try

    End Function

    Private Function PreformCondition(ByVal exp As Date) As String
        Dim ret As String = ""
        Dim MinTotal As Long
        MinTotal = DateDiff(DateInterval.Minute, Now, exp)

        If MinTotal > 240 Then
            ret = PreformStatus.Normal.ToString
        ElseIf MinTotal < 240 AndAlso MinTotal > 0 Then 'ใกล้หมดไม่ให้รันต่อ 0-4 HR ส้มเข้ม
            ret = PreformStatus.Lower4Hour.ToString
        Else 'แดง
            ret = PreformStatus.Expired.ToString ' หมดอายุ
        End If

        Return ret
    End Function
End Class

