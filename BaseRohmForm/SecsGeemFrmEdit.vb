
Imports XtraLibrary.SecsGem

Public Class SecsGeemFrmEdit
    Private SEC_Hex As SECSGEM
    Private strHexResult As String
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        strHexResult = txtResult.Text
        Me.Hide()
    End Sub

    Public Function DisplaySecMessage(ByVal strHex As String) As String
        
        clearControl()
        txtHexFormat.Text = strHex
        ShowSML(strHex)
        Me.ShowDialog()

        If strHexResult Like "Error*" Then '160818 \783 S6F11 Err afeter 'Invalid data lenght' occur
            Return strHexResult
        ElseIf strHexResult <> "" Then
            Return strHexResult
        Else
            Return Nothing
        End If

    End Function
    Private Sub ShowSML(ByVal mHex As String)      '160818 \783 S6F11 Err afeter 'Invalid data lenght' occur


        Try

            If OprData.tLib Then    '160707 \783 SML Convert tLib

                SEC_Hex = New SECSGEM(ConvertStringToByte(mHex), CType(TypeOfSECSMSG.HSMS, SECSMSG_Header.TypeOfSECSMSG))
                If mHex Is Nothing Then
                    txtStream.Text = "0"
                    txtFunction.Text = "0"
                End If




                Dim mc As HsmsHost = MDIParent1.Host
                Dim msg As SecsMessageBase = mc.MessageParser.ToSecsMessage(ConvertStringToByte(mHex))       '======160915 \783 S6F11 Err afeter 'Invalid data lenght' occur Revise

                'Dim msg As SecsMessageBase = MDIParent1.m_Host_ToSecsMessage(ConvertStringToByte(mHex))     '======160818 \783 S6F11 Err afeter 'Invalid data lenght' occur
                rtxtSecMsg.Text = SmlBuilder.ToSmlString(msg)
                rtxtSecMsg.Text = rtxtSecMsg.Text.Substring(rtxtSecMsg.Text.IndexOf("<"))
                txtStream.Text = CStr(msg.Stream)
                txtFunction.Text = CStr(msg.Function)
                cbxWBit.Checked = msg.NeedReply

            Else
                SEC_Hex = New SECSGEM(ConvertStringToByte(mHex), CType(TypeOfSECSMSG.HSMS, SECSMSG_Header.TypeOfSECSMSG))
                SEC_Hex.SMLConvert()
                rtxtSecMsg.Text = (SEC_Hex.SMLOutput)
                txtStream.Text = CStr(SEC_Hex.Sec_Stream)
                txtFunction.Text = CStr(SEC_Hex.Sec_Function)
                cbxWBit.Checked = SEC_Hex.Sec_wBit
            End If


        Catch ex As Exception
            SaveCatchLog(ex.ToString, "ShowSML()")
        End Try


    End Sub

    Private Sub clearControl()
        Me.txtHexFormat.Text = ""
        Me.txtResult.Text = ""
        Me.rtxtSecMsg.Text = ""
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        'strHexResult = ""
        Me.Hide()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Try
            'Mid(txtHexFormat.Text, 13, 30) &
            Dim ret As Byte() = SEC_Hex.SMLRevert(rtxtSecMsg.Text)
            Dim sLenByte As String
            sLenByte = Convert.ToString((ret.Length + 10), 16).ToUpper
            sLenByte = sLenByte.PadLeft(8, "0"c)
            Dim sLen As String = ""
            For i As Integer = 0 To 7 Step 2
                sLen += sLenByte.Substring(i, 2) + " "
            Next

            If SEC_Hex.Header Is Nothing Then
                SEC_Hex.Header = New Byte() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            End If

            If cbxWBit.Checked = True Then
                SEC_Hex.Header(2) = CByte(128 + CInt(txtStream.Text))
            Else
                SEC_Hex.Header(2) = CByte(txtStream.Text)
            End If
            SEC_Hex.Header(3) = CByte(txtFunction.Text)

            txtResult.Text = sLen + ConvToString(SEC_Hex.Header) + ConvToString(ret)
        Catch ex As Exception

        End Try

    End Sub

    '160706 \783 NewSML Convert
    Private Sub rbn_nLib_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbn_nLib.CheckedChanged
        OprData.tLib = False
    End Sub

    Private Sub rbn_tLib_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbn_tLib.CheckedChanged
        OprData.tLib = True
    End Sub

    Private Sub SecsGeemFrmEdit_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        If OprData.tLib Then
            rbn_tLib.Checked = True
        Else
            rbn_nLib.Checked = True
        End If


    End Sub

    '--------------------------
End Class