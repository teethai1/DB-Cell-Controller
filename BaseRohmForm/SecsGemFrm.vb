Imports System.IO
Imports XtraLibrary.SecsGem


Public Class SecsGemFrm
#Region "Commomn Define"
    Event E_ConsoleShow()
    Event E_EstabComm()
    Event E_SendStrData(ByVal sData As String)       'HexTrain data as string
    Event E_DeleteAllReport()
    Event E_DefineReport()
    Event E_LinkReport()
    Event E_EnableAllReport()
    Event E_LoadFrFile()
    Event E_HostOffline()
    Event E_GoOnline()
    Event E_S2F13(ByVal Ecid As List(Of UInt32))
    Event E_S215(ByVal U32 As UInt32, ByVal EValue As String, ByVal Format As SecsFormat)
    Event E_S2F29()
    Event E_S10F3(ByVal ID As Integer, ByVal msg As String)
    Event E_HostSend(ByVal msg As SecsMessageBase)
    Event E_SlInfo(ByVal msg As String)


    Public CaptureMode As Boolean    '161222 \783 Capture Keys Mode on comm Log
    Public CaptureOFF As Boolean

#End Region

   

   

    Private Sub SecsGem_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If (Not System.IO.Directory.Exists(My.Application.Info.DirectoryPath & "\" & "SecsSML")) Then
            System.IO.Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\" & "SecsSML")
        End If
        If m_DefinedReportDic.Count <> 0 Then
            cbxLinkReport.Items.Add("DeleteAllReport")
            cbxLinkReport.Items.Add("Define Report")
            cbxLinkReport.Items.Add("LinkReport")
            cbxLinkReport.Items.Add("EnableAllReport")
            cbxLinkReport.Items.Add("EnableAllAalrm")
            cbxLinkReport.SelectedIndex = 0

        End If

        slblCnnState.Text = CommuniationState
        slbControlState.Text = ControlState

    End Sub

    Private Sub MaximizeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MaximizeToolStripMenuItem.Click

        If Me.Dock <> DockStyle.Fill Then
            Me.Dock = DockStyle.Fill

        Else
            Me.Dock = DockStyle.None
        End If
       


    End Sub

    Private Sub btnImportFromFile_Click(sender As System.Object, e As System.EventArgs) Handles btnImportFromFile.Click
        Dim split() As String
        Dim sr As StreamReader = Nothing
        Try


            OpenFileDialog1.InitialDirectory = My.Application.Info.DirectoryPath & "\" & "SecsSML"


            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If OpenFileDialog1.FileName = "OpenFileDialog1" Then Exit Sub
                sr = New StreamReader(OpenFileDialog1.FileName)
                Dim i As Integer = 0
                While Not sr.EndOfStream
                    If i >= dgvSendMsgPrepare.RowCount - 1 Then
                        dgvSendMsgPrepare.Rows.Add()
                    End If
                    dgvSendMsgPrepare.Rows(i).Cells(0).Value = i + 1
                    split = sr.ReadLine.Split(CChar(","))
                    dgvSendMsgPrepare.Rows(i).Cells(1).Value = split(0)
                    dgvSendMsgPrepare.Rows(i).Cells(2).Value = split(1)
                    i += 1
                End While
                sr.Close() '160707 \783 Can not write 
                'Remove over rows
                For j As Integer = dgvSendMsgPrepare.RowCount - 2 To i Step -1
                    dgvSendMsgPrepare.Rows.RemoveAt(i)
                Next
                MsgBox("Import Successfully")

            End If

        Catch ex As Exception
            If sr IsNot Nothing Then   '160707 \783 Can not write 
                sr.Close()
            End If


        End Try
    End Sub


    Private Sub btnExport_Click(sender As System.Object, e As System.EventArgs) Handles btnExport.Click
        Dim sw As StreamWriter = Nothing

        Try

            Dim saveFileDialog1 As New SaveFileDialog()

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
            saveFileDialog1.FilterIndex = 2
            'saveFileDialog1.RestoreDirectory = True
            SaveFileDialog1.InitialDirectory = My.Application.Info.DirectoryPath & "\" & "SecsSML"
            If CBool(saveFileDialog1.ShowDialog()) Then

                sw = New StreamWriter(saveFileDialog1.OpenFile())
                If sw IsNot Nothing Then
                    For i As Integer = 0 To dgvSendMsgPrepare.RowCount - 1

                        If dgvSendMsgPrepare.Rows(i).Cells(1).Value Is Nothing Then   '160707 \783 Save Error (Nothing case)
                            dgvSendMsgPrepare.Rows(i).Cells(1).Value = ""
                        End If

                        If dgvSendMsgPrepare.Rows(i).Cells(2).Value Is Nothing Then '160707 \783 Save Error (Nothing case)
                            dgvSendMsgPrepare.Rows(i).Cells(2).Value = ""
                        End If

                        sw.WriteLine(dgvSendMsgPrepare.Rows(i).Cells(1).Value.ToString & "," & dgvSendMsgPrepare.Rows(i).Cells(2).Value.ToString)

                    Next

                    sw.Close()
                End If
               
                MsgBox("Export Successful")
            End If
        Catch ex As Exception
            If sw IsNot Nothing Then
                sw.Close()
            End If
            MsgBox("Export Error")
            'slbStatus.Text = "Export Error"     '170110 \783 Nofication only main
            RaiseEvent E_SlInfo("Export Error")
        End Try
    End Sub

    Private Sub btnView_Click(sender As System.Object, e As System.EventArgs) Handles btnView.Click
        Try
            'slbStatus.Text = ""

            Dim rIndex As Integer = dgvSendMsgPrepare.CurrentRow.Index
            Dim cIndex As Integer = dgvSendMsgPrepare.CurrentCell.ColumnIndex

            Dim pt As Point = Me.PointToClient(Control.MousePosition)
            'Dim SEC_Hex As New SECSGEM(ConvertStringToByte(CStr(dgvSendMsgPrepare.CurrentRow.Cells(2).Value)), CType(TypeOfSECSMSG.HSMS, SECSMSG_Header.TypeOfSECSMSG))
            'SEC_Hex.SMLConvert()

            Dim DataByte() As Byte = ConvertStringToByte(CStr(dgvSendMsgPrepare.CurrentRow.Cells(2).Value))
            Dim ByteLenght() As Byte = ConvertStringToByte(CStr(dgvSendMsgPrepare.CurrentRow.Cells(2).Value).Substring(0, 11))  '---160818 \783 S6F11 Err length warning
            Array.Reverse(ByteLenght)
            Dim lengthOfByte As Integer = BitConverter.ToInt32(ByteLenght, 0)

            If DataByte.Length <> lengthOfByte + 4 Then
                'slbStatus.Text = "Error : Invalid data length"        '170110 \783 Nofication only main
                RaiseEvent E_SlInfo("Error : Invalid data length")
                Exit Sub
            End If                                                               '======160818 \783 S6F11 Err length warning



            Dim Mc As HsmsHost = MDIParent1.Host
            Dim msg As SecsMessageBase = Mc.MessageParser.ToSecsMessage(DataByte)       '======160915 \783 S6F11 Err afeter 'Invalid data length' occur Revise
            'Dim msg As SecsMessageBase = MDIParent1.m_Host_ToSecsMessage(DataByte)     '======160818 \783 S6F11 Err afeter 'Invalid data length' occur


            Dim smlText As String = SmlBuilder.ToSmlString(msg)
            Dim View As New SecsGemFrmView
            'View.TextBox1.Text = SEC_Hex.SMLOutput
            'View.Text = "---  S" & SEC_Hex.Sec_Stream & "F" & SEC_Hex.Sec_Function & "   ---"
            View.TextBox1.Text = smlText 'View.Text = "---  S" & SEC_Hex.Sec_Stream & "F" & SEC_Hex.Sec_Function & "   ---"
            View.Text = "---  S" & msg.Stream & "F" & msg.Function & "   ---"
            View.ShowDialog()

        Catch ex As Exception
            'slbStatus.Text = "View Error please check catch log"  '170110 \783 Nofication only main
            RaiseEvent E_SlInfo("View Error please check catch log")
            SaveCatchLog(ex.ToString, "btnView_Click()")
        End Try
    End Sub



   
    Private Sub btnEdit_Click(sender As System.Object, e As System.EventArgs) Handles btnEdit.Click
        If dgvSendMsgPrepare.CurrentRow Is Nothing Then
            Exit Sub
        End If

        'slbStatus.Text = ""   "View Error please check catch log"

        If dgvSendMsgPrepare.CurrentRow.Cells(2).Value IsNot Nothing Then
          
            'Check Lenth Loop --
            Dim DataByte() As Byte = ConvertStringToByte(CStr(dgvSendMsgPrepare.CurrentRow.Cells(2).Value))
            If DataByte.Length = 0 Then
                GoTo StartEdit
            End If
            Dim ByteLenght() As Byte = ConvertStringToByte(CStr(dgvSendMsgPrepare.CurrentRow.Cells(2).Value).Substring(0, 11))  '---160818 \783 S6F11 Err length warning
            Array.Reverse(ByteLenght)
            Dim lengthOfByte As Integer = BitConverter.ToInt32(ByteLenght, 0)

            If DataByte.Length <> lengthOfByte + 4 Then
                'slbStatus.Text = "Error : Invalid data length"    '170110 \783 Nofication only main
                RaiseEvent E_SlInfo("Error : Invalid data length")
                Exit Sub
            End If                                                               '======160818 \783 S6F11 Err length warning

            '------------------
        End If
StartEdit:  '160920 \\0783  Null Str error 

        Dim frmSMLDisp As New SecsGeemFrmEdit


        Dim result As String = frmSMLDisp.DisplaySecMessage(CStr(dgvSendMsgPrepare.CurrentRow.Cells(2).Value))

        If result Like "Error*" Then
            'slbStatus.Text = result   '170110 \783 Nofication only main
            RaiseEvent E_SlInfo(result)

        ElseIf result <> "" Then
            dgvSendMsgPrepare.CurrentRow.Cells(2).Value = result

            If result.Length > 6399 Then
                MsgBox("Length over 64K can not display")
            End If
        End If

    End Sub

    Private Sub btnSend2_Click(sender As System.Object, e As System.EventArgs) Handles btnSend2.Click
        Dim rIndex As Integer = dgvSendMsgPrepare.CurrentRow.Index
        If rIndex < dgvSendMsgPrepare.RowCount - 1 Then
            RaiseEvent E_SendStrData(CStr(dgvSendMsgPrepare.Rows.Item(rIndex).Cells(2).Value))
            dgvSendMsgPrepare.CurrentCell = dgvSendMsgPrepare.Rows(rIndex + 1).Cells(2)
        End If
    End Sub

    Private Sub SecsGemFrm_Deactivate(sender As Object, e As System.EventArgs) Handles Me.Deactivate  '160930 \783 display revise
        Timer1.Enabled = False
        Timer1.Enabled = True
        Timer1.Start()
    End Sub
    Private Sub SecsGemFrm_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated '160930 \783 display revise
        Timer1.Stop()
        Timer1.Enabled = False
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick '160930 \783 display revise
        RaiseEvent E_SlInfo("No interaction Over 5 min SecsGem Window Closed")
        Me.Close()
    End Sub
    'Private Sub btnConsole_Click(sender As System.Object, e As System.EventArgs)
    '    RaiseEvent E_ConsoleShow()
    'End Sub

    'Private Sub ConsoleToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ConsoleToolStripMenuItem.Click
    '    RaiseEvent E_ConsoleShow()
    'End Sub

    Private Sub EsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EsToolStripMenuItem.Click
        RaiseEvent E_EstabComm()
    End Sub
 

    Private Sub btnLoadReport_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles btnLoadReport.MouseDown
        'slbStatus.Text = "Loading ....." '170110 \783 Nofication only main
        RaiseEvent E_SlInfo("Loading .....")


    End Sub
    Private Sub btnLoadReport_Click(sender As System.Object, e As System.EventArgs) Handles btnLoadReport.Click

        Dim ANS As String = DownloadReportSetting()
        If ANS Like "True*" Then
            cbxLinkReport.Items.Clear()
            cbxLinkReport.Items.Add("DeleteAllReport")
            cbxLinkReport.Items.Add("Define Report")
            cbxLinkReport.Items.Add("LinkReport")
            cbxLinkReport.Items.Add("EnableAllReport")
            cbxLinkReport.Items.Add("EnableAllAalrm")
            'slbStatus.Text = "Report Download Success"   '170110 \783 Nofication only main
            RaiseEvent E_SlInfo("Report Download Success")
            cbxLinkReport.SelectedIndex = 0
        Else
            'slbStatus.Text = "Report Download Fail"
            RaiseEvent E_SlInfo("Report Download Fail") '170110 \783 Nofication only main
            cbxLinkReport.Items.Clear()
            cbxLinkReport.Text = ""
            txtSML_LinkReport.Text = ""
            m_LinkedReportDic.Clear()
            m_DefinedReportDic.Clear()
        End If


    End Sub

    Private Sub Send_S2F33_DeleteAllReport()

        Dim msg As S2F33 = New S2F33(CStr(0))
        msg.SetDeleteAllReport()
        Dim smlText As String = SmlBuilder.ToSmlString(msg)
        txtSML_LinkReport.Text = smlText

    End Sub
    Private Sub Send_S2F33_DefineReport()            'Display SML

        Dim msg As S2F33 = New S2F33(CStr(0))
        For Each rpt As SecsDataType In m_DefinedReportDic.Values  '160722 \783  common secs data type
            msg.AddReport(rpt.RPTID(0), rpt.VID.ToArray())
        Next
        Dim smlText As String = SmlBuilder.ToSmlString(msg)
        txtSML_LinkReport.Text = smlText


    End Sub
    Private Sub Send_S2F35_LinkReport()

        Dim msg As S2F35 = New S2F35(CStr(0))

        If My.Settings.MCType = "Canon-D10R" Then
            Dim u2List As List(Of UShort) = New List(Of UShort)
            For Each lr As SecsDataType In m_LinkedReportDic.Values

                For Each v As Integer In lr.RPTID
                    u2List.Add(CUShort(v))
                Next

                msg.AddLinkCanon(lr.CEID, u2List.ToArray())
                u2List.Clear()
            Next
        Else
            For Each lr As SecsDataType In m_LinkedReportDic.Values
                msg.AddLink(lr.CEID, lr.RPTID.ToArray())
            Next
        End If

        Dim smlText As String = SmlBuilder.ToSmlString(msg)
        txtSML_LinkReport.Text = smlText

    End Sub


    Private Sub Send_S2F37_EnableAllReport()

        Dim msg As S2F37 = New S2F37()
        msg.SetEnable()
        Dim smlText As String = SmlBuilder.ToSmlString(msg)
        txtSML_LinkReport.Text = smlText

    End Sub


    Private Sub Send_S5F3_EnableAllAlarmShow()
        Dim S5F3 As New S5F3(True, 0)
        Dim smlText As String = SmlBuilder.ToSmlString(S5F3)       
        txtSML_LinkReport.Text = smlText

    End Sub

    Private Sub Send_S5F3_EnableAllAlarm()
        Dim S5F3 As New S5F3(True, 0)
        RaiseEvent E_HostSend(S5F3)

    End Sub
    Private Sub Send_S1F1()
        Dim S1F1 As New S1F1()
        RaiseEvent E_HostSend(S1F1)
    End Sub
    Private Sub cbxLinkReport_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cbxLinkReport.SelectedIndexChanged

        Select Case cbxLinkReport.SelectedItem.ToString
            Case "DeleteAllReport"
                Send_S2F33_DeleteAllReport()
            Case "Define Report"
                Send_S2F33_DefineReport()
            Case "LinkReport"
                Send_S2F35_LinkReport()
            Case "EnableAllReport"
                Send_S2F37_EnableAllReport()
            Case "EnableAllAalrm"
                Send_S5F3_EnableAllAlarmShow()
        End Select
    End Sub

    Private Sub btnSendLinkReport_Click(sender As System.Object, e As System.EventArgs) Handles btnSendLinkReport.Click
        'slbStatus.Text = "                    "         '170110 \783 Nofication only main
        If cbxLinkReport.SelectedItem Is Nothing Then
            Exit Sub
        End If
        CurDefFlow = AutoDefineReportFlow.Idle
        Select Case cbxLinkReport.SelectedItem.ToString
            Case "DeleteAllReport"
                RaiseEvent E_DeleteAllReport()
            Case "Define Report"
                RaiseEvent E_DefineReport()
            Case "LinkReport"
                RaiseEvent E_LinkReport()
            Case "EnableAllReport"
                RaiseEvent E_EnableAllReport()
            Case "EnableAllAalrm"
                Send_S5F3_EnableAllAlarm()
        End Select

    End Sub

    Private Sub btnAutoSend_Click(sender As System.Object, e As System.EventArgs) Handles btnAutoSend.Click
        'slbStatus.Text = "                    "   '170110 \783 Nofication only main

        If m_DefinedReportDic.Count = 0 Then
            'slbStatus.Text = "Define report not found"      '160630 \783 AutoLoad revise
            RaiseEvent E_SlInfo("Define report not found")    '170110 \783 Nofication only main
            Exit Sub
        End If
        ' CurDefFlow = AutoDefineReportFlow.GoOnline
        'RaiseEvent E_GoOnline()
        CurDefFlow = AutoDefineReportFlow.DeleteAllReport
        RaiseEvent E_DeleteAllReport()

    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        If LinkTable.Rows.Count = 0 Then
            MsgBox("Please Download report data")
            Exit Sub
        End If
        If DefReport.Rows.Count = 0 Then
            MsgBox("Please Download report data")
            Exit Sub
        End If
        DefReport.TableName = "DefReport"
        DefReport.WriteXml(My.Application.Info.DirectoryPath & "\DefReport.xml")
        DefReport.WriteXmlSchema(My.Application.Info.DirectoryPath & "\DefReportSchema.xml")

        LinkTable.TableName = "LinkTable"
        LinkTable.WriteXml(My.Application.Info.DirectoryPath & "\LinkTable.xml")
        LinkTable.WriteXmlSchema(My.Application.Info.DirectoryPath & "\LinkTableSchema.xml")
    End Sub

    Private Sub btnLoad_Click(sender As System.Object, e As System.EventArgs) Handles btnLoad.Click
        RaiseEvent E_LoadFrFile()
    End Sub

    Private Sub HostOFFLINES1F15ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles HostOFFLINES1F15ToolStripMenuItem.Click
        RaiseEvent E_HostOffline()
    End Sub
   

    Private Sub GOONLINES1F17ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles GOONLINES1F17ToolStripMenuItem.Click
        RaiseEvent E_GoOnline()
    End Sub

    Private Sub S1F1ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles S1F1ToolStripMenuItem.Click
        Send_S1F1()   '160627 \783 Eq Comm revise
    End Sub
    Private Sub btnS2F13_Click(sender As System.Object, e As System.EventArgs) Handles btnS2F13.Click

        Dim U32List As New List(Of UInt32)
        lbS2F13.Text = "Reply :"
        If IsNumeric(tbxECID1.Text) Then
            U32List.Add(CUInt(tbxECID1.Text))
        End If
        If IsNumeric(tbxECID2.Text) Then
            U32List.Add(CUInt(tbxECID2.Text))
        End If
        If U32List.Count <> 0 Then
            RaiseEvent E_S2F13(U32List)
        End If

    End Sub

  
    Private Sub btnS2F15_Click(sender As System.Object, e As System.EventArgs) Handles btnS2F15.Click
        lbS2F15.Text = "Reply :"

        If Not IsNumeric(tbxEcid.Text) Then
            Exit Sub
        End If

        Dim Format As SecsFormat = CType([Enum].Parse(GetType(SecsFormat), cbxSecsFormat.Text), SecsFormat)   '160823 \783 Addition SecsFormat

        RaiseEvent E_S215(CUInt(tbxEcid.Text), tbxValue.Text, Format)
    End Sub
    Private Sub btnS2F43_Click(sender As System.Object, e As System.EventArgs) Handles btnS2F43.Click


        Dim SF As New S2F43
        Dim Func As Byte() = {11}
        SF.AddStream(6, Func)
        lbS2F44.Text = "Reply : Waiting.."
        RaiseEvent E_HostSend(SF)
    End Sub

    Private Sub btnTrans_Click(sender As System.Object, e As System.EventArgs) Handles btnTrans.Click
        Dim SF As New S6F23(SpoolCode.Transmit)
        lblSpool.Text = "Reply : Waiting.."
        RaiseEvent E_HostSend(SF)
    End Sub

    Private Sub btnPurge_Click(sender As Object, e As System.EventArgs) Handles btnPurge.Click
        Dim SF As New S6F23(SpoolCode.Purge)
        lblSpool.Text = "Reply : Waiting.."
        RaiseEvent E_HostSend(SF)

    End Sub

    Private Sub btnSVIDRequest_Click(sender As System.Object, e As System.EventArgs) Handles btnSVIDRequest.Click
        Dim S1F3 As New S1F3
        If tbxSVID1.Text <> "" And IsNumeric(tbxSVID1.Text) Then
            S1F3.AddSvid(CUInt(tbxSVID1.Text))
        End If

        If tbxSVID2.Text <> "" And IsNumeric(tbxSVID2.Text) Then
            S1F3.AddSvid(CUInt(tbxSVID2.Text))
        End If

        If tbxSVID3.Text <> "" And IsNumeric(tbxSVID3.Text) Then
            S1F3.AddSvid(CUInt(tbxSVID3.Text))
        End If
        lbSVID1Reply.Text = "Reply : "
        lbSVID2Reply.Text = "Reply : "
        lbSVID3Reply.Text = "Reply : "


        RaiseEvent E_HostSend(S1F3)


    End Sub

    Private Sub TabAdmin_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles TabAdmin.SelectedIndexChanged
        'slbStatus.Text = ""  '170110 \783 Nofication only main
        Select Case TabAdmin.SelectedTab.Text
            Case "Equipment Constants"
                lbS2F15.Text = "Reply :"
                lbS2F13.Text = "Reply :"
                cbxSecsFormat.SelectedIndex = 0

            Case "Spooling&svid Request"
                lbS2F44.Text = "Reply : "
                lblSpool.Text = "Reply : "
                lbSVID1Reply.Text = "Reply : "
                lbSVID2Reply.Text = "Reply : "
                lbSVID3Reply.Text = "Reply : "

                'tbxSVID1.Text = ""           '161215 \783 ให้สอดคล้องกับการใช้งานจริง 
                'tbxSVID2.Text = ""
                'tbxSVID3.Text = ""

        End Select
    End Sub


    Private Sub TextBox3_Validated(sender As Object, e As System.EventArgs) Handles TextBox3.Validated
        If Not IsNumeric(TextBox3.Text) Then
            TextBox3.Text = CStr(0)
        End If
    End Sub

  
    Private Sub btnS10F3_Click(sender As System.Object, e As System.EventArgs) Handles btnS10F3.Click
        If Not IsNumeric(TextBox3.Text) Then
            MsgBox("Terminal ID is not numberic")
            Exit Sub
        End If

        If TextBox1.Text = "" Then
            MsgBox("Nothing to sent")
            Exit Sub
        End If
        RaiseEvent E_S10F3(CInt(TextBox3.Text), TextBox1.Text)

    End Sub


    Private Sub NoneToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles NoneToolStripMenuItem.Click
        FilterText.Text = ""   '161222 \783 Capture Keys Mode on comm Log
        CaptureOFF = False
        CaptureMode = False
        'slbStatus.Text = "" '170110 \783 Nofication only main
        RaiseEvent E_SlInfo("CommLog Capture mode inactivated")
    End Sub

    Private Sub FilterText_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles FilterText.KeyUp

        If e.KeyValue = 13 Then   '161222 \783 Capture Keys Mode on comm Log
            CaptureMode = True
            'slbStatus.Text = "CommLog Capture mode activated" & Format(Now, " |HH:mm:ss.fff") '170110 \783 Nofication only main
            RaiseEvent E_SlInfo("CommLog Capture mode activated")
        Else
            CaptureMode = False
            'slbStatus.Text = "CommLog Capture mode inactivated" & Format(Now, " |HH:mm:ss.fff") '170110 \783 Nofication only main
            RaiseEvent E_SlInfo("CommLog Capture mode inactivated")


        End If

    End Sub

   

#Region "Reply Secs"

    Public Sub ReplyS1F3(ByVal SVIDVal As List(Of String))  'Return List of SVID Value


        For i = 0 To SVIDVal.Count - 1

            If tbxSVID1.Text <> "" And lbSVID1Reply.Text = "Reply : " Then
                lbSVID1Reply.Text = "Reply : " & SVIDVal(i)
                Continue For
            End If
            If tbxSVID2.Text <> "" And lbSVID2Reply.Text = "Reply : " Then
                lbSVID2Reply.Text = "Reply : " & SVIDVal(i)
                Continue For
            End If
            If tbxSVID3.Text <> "" And lbSVID3Reply.Text = "Reply : " Then
                lbSVID3Reply.Text = "Reply : " & SVIDVal(i)
                Continue For
            End If

        Next



    End Sub



#End Region

    Private Function S2F33() As Object
        Throw New NotImplementedException
    End Function



  
End Class

