Public Class frmFirstInspection
    Dim _alarm As String = ""
    Dim ThicknessArray(3) As Integer
    Dim frmKey As New frmKeyboard
    'Public m_DenpyoChipSizeX As String
    'Public m_DenpyoChipSizeY As String
    'Public m_DenpyoTsukaigeNo As String
    'Public m_DenpyoRubberNo As String

    Function ConditionFirstInsp() As Boolean
        Dim ret As Boolean = False
        ConditionFirstInsp = ret
        If tbRubberColletNo.Text = "" Then
            _alarm = "กรุณากรอก Rubber Collet No."
            tbRubberColletNo.Select()
            Exit Function
        ElseIf tbTsukaigeNeedNo.Text = "" Then
            _alarm = "กรุณากรอก Tsukaige No."
            tbTsukaigeNeedNo.Select()
            Exit Function
        ElseIf rdBlockChcekA.Checked = False AndAlso rdBlockChcekB.Checked = False AndAlso rdBlockChcekC.Checked = False Then
            _alarm = "กรุณาเลือกโหมด Block check"
            Exit Function
        ElseIf rdTsukaigeA.Checked = False AndAlso rdTsukaigeB.Checked = False AndAlso rdTsukaigeC.Checked = False Then
            _alarm = "กรุณาเลือกโหมด Tsukaige condition"
            Exit Function
        ElseIf rdRubberCheckA.Checked = False AndAlso rdRubberCheckB.Checked = False AndAlso rdRubberCheckC.Checked = False Then
            _alarm = "กรุณาเลือกโหมด Rubber Check"
            Exit Function

        End If

        If My.Settings.PasteType = False Then 'Solder
            If tbChipSizeY.Text = "" OrElse tbChipSizeX.Text = "" Then
                _alarm = "กรุณากรอก ChipSize X,Y"
                Exit Function
            End If
        Else 'Preform
            If cbPasteNozzleType.Text = "" Then
                _alarm = "กรุณาเลือก NozzleType"
                Exit Function
            ElseIf cbxPastNozzleNo.Text = "" Then
                _alarm = "กรุณาเลือก NozzleNo"
                Exit Function
            ElseIf rbPasteNozzleA.Checked = False AndAlso rbPasteNozzleB.Checked = False AndAlso rbPasteNozzleC.Checked = False Then
                _alarm = "กรุณาเลือกโหมด Nozzle"
                Exit Function
                End
            End If
        End If



        ret = True
        Return ret

    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If frmKey.Visible = True Then
            frmKey.Visible = False
        End If

        If MessageBox.Show("คุณต้องการ Lot Start ใช่หรือไม่", "", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        If ConditionFirstInsp() = False Then
            m_frmWarningDialog(_alarm, False)
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK

    End Sub


    Private Sub InitialComboBoxItems()
        cbPasteNozzleType.Items.Clear()
        With cbPasteNozzleType
            .Items.Add("Single")
            .Items.Add("Dual")
            .Items.Add("Stamp Nozzle")
            .Items.Add("Mullti Nozzle")
            .Items.Add("Accurate")
            .Items.Add("")
        End With

    End Sub


    Private Sub cbxPasteNozzleType_LostFocus1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPasteNozzleType.LostFocus
        cbxPastNozzleNo.Items.Clear()
        Select Case cbPasteNozzleType.Text
            Case "Single"
                With cbxPastNozzleNo
                    .Items.Add("S1")
                    .Items.Add("S2")
                    .Items.Add("S3")
                    .Items.Add("S4")
                    .Items.Add("")
                End With
            Case "Dual"
                With cbxPastNozzleNo
                    .Items.Add("T1")
                    .Items.Add("T2")
                    .Items.Add("")
                End With
            Case "Stamp Nozzle"
                With cbxPastNozzleNo
                    .Items.Add("X1")
                    .Items.Add("X2")
                    .Items.Add("X3")
                    .Items.Add("X4")
                    .Items.Add("X5")
                    .Items.Add("")
                End With
            Case "Mullti Nozzle"
                With cbxPastNozzleNo
                    .Items.Add("M1")
                    .Items.Add("M2")
                    .Items.Add("M3")
                    .Items.Add("M4")
                    .Items.Add("M5")
                    .Items.Add("M6")
                    .Items.Add("M7")
                    .Items.Add("M8")
                    .Items.Add("M9")
                    .Items.Add("M10")
                    .Items.Add("M11")
                    .Items.Add("M12")
                    .Items.Add("M13")
                    .Items.Add("M14")
                    .Items.Add("M15")
                    .Items.Add("M16")
                    .Items.Add("M17")
                    .Items.Add("M18")
                    .Items.Add("M19")
                    .Items.Add("M20")
                    .Items.Add("M21")
                    .Items.Add("M22")
                    .Items.Add("M23")
                    .Items.Add("M24")
                    .Items.Add("M25")
                    .Items.Add("")
                End With
            Case "Accurate"
                With cbxPastNozzleNo
                    .Items.Add("0.15")
                    .Items.Add("0.20")
                    .Items.Add("0.25")
                    .Items.Add("SHN-0.15")
                    .Items.Add("SHN-0.20")
                    .Items.Add("")
                End With
        End Select
    End Sub

    Private Sub frmFirstInspection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitialComboBoxItems()
        If My.Settings.PasteType = False Then 'Solder
            gbNozzle.Visible = False
        Else 'Preform
            gbNozzle.Visible = True
        End If
    End Sub


    Private Sub tbTsukaigePinStrock_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbTsukaigePinStrock.Click, tbPreformThickness2.Click, tbPreformThickness1.Click, tbPreformThickness0.Click, tbPreformThickness3.Click, tbChipSizeY.Click, tbChipSizeX.Click, tbTsukaigeNeedNo.Click, tbRubberColletNo.Click
        If frmKey.IsDisposed = True Then
            frmKey = New frmKeyboard
        End If

        Dim tb As TextBox = CType(sender, TextBox)
        DefaultColorTextbox()
        frmKey.TargetText = tb
        tb.BackColor = Color.Yellow
        If tb.Name = "tbRemark" Then
            frmKey.Height = 338
        Else
            frmKey.Height = 144
        End If
        frmKey.Show()
        frmKey.Focus()

    End Sub

    Private Sub tbTsukaigePinStrock_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbTsukaigePinStrock.Enter, tbPreformThickness2.Enter, tbPreformThickness1.Enter, tbPreformThickness0.Enter, tbPreformThickness3.Enter, tbChipSizeY.Enter, tbChipSizeX.Enter, tbTsukaigeNeedNo.Enter, tbRubberColletNo.Enter
        If frmKey.IsDisposed = True Then
            frmKey = New frmKeyboard
        End If


        Dim tb As TextBox = CType(sender, TextBox)
        DefaultColorTextbox()
        frmKey.TargetText = tb
        tb.BackColor = Color.Yellow
        If tb.Name = "tbRemark" Then
            frmKey.Height = 338
        Else
            frmKey.Height = 144
        End If
        frmKey.Show()
        frmKey.Focus()

    End Sub


    Private Sub DefaultColorTextbox()
        tbTsukaigePinStrock.BackColor = Color.WhiteSmoke
        tbPreformThickness2.BackColor = Color.WhiteSmoke
        tbPreformThickness1.BackColor = Color.WhiteSmoke
        tbPreformThickness0.BackColor = Color.WhiteSmoke
        tbPreformThickness3.BackColor = Color.WhiteSmoke
        tbChipSizeY.BackColor = Color.WhiteSmoke
        tbChipSizeX.BackColor = Color.WhiteSmoke
        tbTsukaigeNeedNo.BackColor = Color.WhiteSmoke
        tbRubberColletNo.BackColor = Color.WhiteSmoke
    End Sub

    Private Sub tbPreformThickness0_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPreformThickness0.TextChanged, tbPreformThickness1.TextChanged, tbPreformThickness2.TextChanged, tbPreformThickness3.TextChanged
        Dim tb As TextBox = CType(sender, TextBox)
        CalCulateThicknessPreform(tb, tbPreformAver, tbPreformR, gbPreform)
    End Sub
    Private Sub CalCulateThicknessPreform(ByVal TextboxThickness As TextBox, ByVal AverageTb As TextBox, ByVal RTextbox As TextBox, ByVal gb As GroupBox)
        Dim sum As Integer
        Dim max As Integer
        Dim min As Integer
        Dim intnum As Integer
        Dim GroubCon As Control = gbPreform
        If IsNumeric(TextboxThickness.Text) Then

            For i = 0 To 3
                ThicknessArray(i) = 0
            Next
            For Each ctrl In gb.Controls
                If TypeOf ctrl Is TextBox Then
                    Dim tb As TextBox = CType(ctrl, TextBox)
                    If tb.Text = "" Then
                        intnum = 0
                    Else
                        intnum = CInt(tb.Text)
                    End If
                        If tb.Name = "tbPreformThickness0" Then
                            ThicknessArray(0) = intnum
                        ElseIf tb.Name = "tbPreformThickness1" Then
                            ThicknessArray(1) = intnum
                        ElseIf tb.Name = "tbPreformThickness2" Then
                            ThicknessArray(2) = intnum
                        ElseIf tb.Name = "tbPreformThickness3" Then
                            ThicknessArray(3) = intnum
                        End If
                End If
            Next

            max = CInt(TextboxThickness.Text)
            min = CInt(TextboxThickness.Text)
            For i = 0 To 3
                sum += ThicknessArray(i)
                If max <= CInt(ThicknessArray(i)) Then
                    max = CInt(ThicknessArray(i))
                End If
                If min >= CInt(ThicknessArray(i)) Then
                    min = CInt(ThicknessArray(i))
                End If
            Next
            AverageTb.Text = Math.Round(sum / 4, 2).ToString
            RTextbox.Text = (max - min).ToString
        Else
            If TextboxThickness.Text <> "" Then
                TextboxThickness.Text = ""
            ElseIf tbPreformThickness0.Text = "" AndAlso tbPreformThickness1.Text = "" AndAlso tbPreformThickness2.Text = "" AndAlso tbPreformThickness3.Text = "" Then
                tbPreformAver.Text = ""
                tbPreformR.Text = ""
            End If
        End If
    End Sub


End Class