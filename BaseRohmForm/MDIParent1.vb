Imports System.Windows.Forms
Imports System.Threading
Imports XtraLibrary.SecsGem
Imports Rohm.Apcs.Tdc
Imports System.ComponentModel
Imports System.IO

Public Class MDIParent1
    'Revision History ---------------------------------------------------------------
    '+ FEB.16.16,Prasarn    | Software issue
    '+ SecsGem All Reply Event to Frm Product    |Jun.14.2016


    '===============================================================================

#Region "Commomn Define"

    Dim WithEvents MainControlfrm As New Form1
    Dim WithEvents FrmProduct As ProcessForm
    Dim WithEvents FrmProdTable As ProductionTable
    Dim WithEvents FrmSetting As Setting
    Dim WithEvents FrmSecs As SecsGemFrm
    Dim WithEvents FrmTCPClient As TcpIpClientTest
    Dim WithEvents FrmHelper As ProductionTable

    'Dim WithEvents slMessage As New ToolStripLabel
    'Event LotRequestReply(ByVal rpl As TdcResponse)
    'Event LotSetReply(ByVal rpl As TdcResponse) 
    'Event LotEndReply(ByVal rpl As TdcResponse)
   

    'Secs ---

    Private m_GemOption As New GemOption
    Public m_Host As HsmsHost
   
    Private Delegate Sub UpdateTextDelegate(ByVal text As String)
    Private Delegate Sub UpdateTextDelegate1(ByVal text As String, ByVal ControlName As StatusLabel)

    Enum StatusLabel
        FrmSecs_slblCnnState
        FrmSecs_sblStatus
        FrmSecs_slbControlState
        FrmSecs_lbS2F44
        FrmSecs_lbSpool

    End Enum
    'TDC ---

    'Private m_TdcService As TdcService

    'CS Protocol
    Private WithEvents CstProtocol As TcpIpClient


#End Region


    Private Sub MDIParent1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Initial setting ----------------------------------------------------------------------------------
        OprData.DisaTableFrmShow = My.Settings.MDITableFrmDisable                  'True = without table
        OprData.QrReadSystemOn = My.Settings.MDIQRSystem                      'True = Qr Read SystemOn (OPID,WorkID)    '160629 \783 QrSystemON/OFF ,161029 \783 HMI Setting
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US") '160712 \783 Fix display in Anno Domini 
        If My.Settings.MDISizable Then
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        End If

        '---------------------------------------------------------------------------------------------------
        Try
            Me.WindowState = FormWindowState.Maximized
            MainControlfrm.MdiParent = Me
            MainControlfrm.Width = Me.Width - 5
            MainControlfrm.Show()

            ' Folder Build ---------------------------------------------------------------------------------
            MakeDirectories()

            '-----------------------------------------------------------------------------------------------
            If Not My.Settings.SECS_Enable Then
                GoTo ByPassSecs
            End If
            'Secs ----
            m_GemOption.DeviceId = My.Settings.GEM_DeviceID
            m_GemOption.Protocol = GemProtocol.HSMS 'fixed---- Setting at Code

            Dim hshsParams As HsmsParameters = New HsmsParameters()
            m_GemOption.HsmsParameters = hshsParams

            hshsParams.IPAddress = My.Settings.EquipmentIP
            hshsParams.PortNo = My.Settings.SECS_PortNumber
            hshsParams.Mode = HsmsConnectProcedure.ACTIVE

            hshsParams.T3_Interval = My.Settings.GEM_T3_Interval
            hshsParams.T5_Interval = My.Settings.GEM_T5_Interval
            'In case of equipment consumed sending time of massive SECS message more than T6
            'the equipment can not reply Linktest.Resp within 5 secs (default)
            'or the host received but can not process it (busy as same as not received)
            'I have to increase T6 interval from 5 to 20 secsonds
            hshsParams.T6_Interval = My.Settings.GEM_T6_Interval
            hshsParams.T7_Interval = My.Settings.GEM_T7_Interval
            hshsParams.Linktest_Interval = My.Settings.GEM_LinkTest_Interval
            hshsParams.LinktestEnabled = My.Settings.GEM_LinkTest_Enabled

            Dim equipmentModel As EquipmentModel = New EquipmentModel(My.Settings.MCType)
            equipmentModel.Connection = m_GemOption
            Dim factory As SecsHostFactory = New SecsHostFactory()
            m_Host = CType(factory.Create(equipmentModel), XtraLibrary.SecsGem.HsmsHost)

            m_Host.LogDirectory = DIR_LOG
            m_Host.HsmsLogEnabled = True

            'Dim hsmsHost As HsmsHost = CType(m_Host, HsmsHost)

            AddHandler m_Host.ReceivedPrimaryMessage, AddressOf m_Host_ReceivedPrimaryMessage
            AddHandler m_Host.ReceivedSecondaryMessage, AddressOf m_Host_ReceivedSecondaryMessage
            AddHandler m_Host.HsmsStateChanged, AddressOf m_Host_HsmsStateChanged
            AddHandler m_Host.ErrorNotification, AddressOf m_Host_ErrorNotification
            AddHandler m_Host.ConversionErrored, AddressOf m_Host_ConversionErrored
            AddHandler m_Host.TracedSmlLog, AddressOf m_CommLog        '160930 \783 Add comlog FrmSecs
            Dim secsParser As SecsMessageParserBase = m_Host.MessageParser

            ''Example ==='
            'Dim msg As SecsMessageBase = m_Host.MessageParser.ToSecsMessage(Nothing)
            'Dim smlText As String = SmlBuilder.ToSmlString(msg)
            'm_Host.Send(msg)
            '============
            Try
                secsParser.RegisterCustomSecsMessage(GetType(S1F4))
                secsParser.RegisterCustomSecsMessage(GetType(S1F14E))   ' Regis in Recive data from eqiptment for change to class format. 
                secsParser.RegisterCustomSecsMessage(GetType(S1F18))    '160630 \783 AutoLoad Revise
                secsParser.RegisterCustomSecsMessage(GetType(S2F34))
                secsParser.RegisterCustomSecsMessage(GetType(S2F36))
                secsParser.RegisterCustomSecsMessage(GetType(S2F38))
                secsParser.RegisterCustomSecsMessage(GetType(S2F41))
                secsParser.RegisterCustomSecsMessage(GetType(S2F42))     '160903 \783 Add S2F42
                secsParser.RegisterCustomSecsMessage(GetType(S2F14))
                secsParser.RegisterCustomSecsMessage(GetType(S2F16))
                secsParser.RegisterCustomSecsMessage(GetType(S2F17))
                secsParser.RegisterCustomSecsMessage(GetType(S5F1))
                secsParser.RegisterCustomSecsMessage(GetType(S5F4))
                secsParser.RegisterCustomSecsMessage(GetType(S6F11))
                secsParser.RegisterCustomSecsMessage(GetType(S6F24))
                secsParser.RegisterCustomSecsMessage(GetType(S7F2))
                secsParser.RegisterCustomSecsMessage(GetType(S7F4))
                secsParser.RegisterCustomSecsMessage(GetType(S7F6))
                secsParser.RegisterCustomSecsMessage(GetType(S7F18))
                secsParser.RegisterCustomSecsMessage(GetType(S7F20))
                secsParser.RegisterCustomSecsMessage(GetType(S10F1))

                If My.Settings.MCType = "2100HS" Then
                    secsParser.RegisterCustomSecsMessage(GetType(S12F1Esec))  'Upload
                    secsParser.RegisterCustomSecsMessage(GetType(S12F5Esec)) 'Upload
                    secsParser.RegisterCustomSecsMessage(GetType(S12F9)) 'Upload

                    secsParser.RegisterCustomSecsMessage(GetType(S12F3)) 'Download
                    secsParser.RegisterCustomSecsMessage(GetType(S12F15)) 'Download

                ElseIf My.Settings.MCType = "2009SSI" Then
                    secsParser.RegisterCustomSecsMessage(GetType(S12F1Esec))  'Upload
                    secsParser.RegisterCustomSecsMessage(GetType(S12F5Esec)) 'Upload
                    secsParser.RegisterCustomSecsMessage(GetType(S12F9)) 'Upload

                    secsParser.RegisterCustomSecsMessage(GetType(S12F3)) 'Download
                    secsParser.RegisterCustomSecsMessage(GetType(S12F15)) 'Download
                ElseIf My.Settings.MCType = "Canon-D10R" Then
                    'Upload
                    secsParser.RegisterCustomSecsMessage(GetType(S12F1CanonUpload))
                    secsParser.RegisterCustomSecsMessage(GetType(S12F2CanonUpload))
                    secsParser.RegisterCustomSecsMessage(GetType(S12F5CanonUpload))
                    secsParser.RegisterCustomSecsMessage(GetType(S12f6CanonUpload))
                    secsParser.RegisterCustomSecsMessage(GetType(S12F9CanonUpload))
                    secsParser.RegisterCustomSecsMessage(GetType(S12F10CanonUpload))

                    'Download
                    secsParser.RegisterCustomSecsMessage(GetType(S12F15CanonDownload))
                    secsParser.RegisterCustomSecsMessage(GetType(S12F16CanonDownload))
                    secsParser.RegisterCustomSecsMessage(GetType(S12F3CanonDownload))
                    secsParser.RegisterCustomSecsMessage(GetType(S12F4CanonDownload))
                End If

            Catch ex As Exception
                slMessage.Text = "SecsParser.RegisterCustomSecsMessage Error" & Format(Now, " |HH:mm:ss.fff")          '160913 \783 Add catch Error
                SaveCatchLog(ex.ToString, "SecsParser.RegisterCustomSecsMessage")
            End Try

            If DownloadReportSetting() Like "False*" Then      'Load Define Report from server
                MsgBox("ไม่สามารถโหลดจาก Sever ได้ กรุณาโหลดใหม่")
                Me.Close()
                'LoadFrFile()                                   'Load Define Report from file
            End If

            m_Host.Connect()
            '---------

ByPassSecs:


            m_TdcService = TdcService.GetInstance()
            m_TdcService.ConnectionString = My.Settings.ApcsDBConnString

            Dim logger As TdcLoggerTextWriter = CType(m_TdcService.Logger, TdcLoggerTextWriter)
            logger.LogFolder = DIR_LOG & "\"


            If My.Settings.CsProtocol_Enable Then     'Custom Protocol
                CstProtocol = New TcpIpClient
                CstProtocol.ReadContinue = True
                CstProtocol.Listener_Click(CStr(My.Settings.CsProtocolPort), My.Settings.EquipmentIP)
                Reconnect.Enabled = True    '160715 \783 Reconnect CS Protocol
            End If

            UserTable.ReadXmlSchema(My.Application.Info.DirectoryPath & "\UserLoginSchema.xml")
            UserTable.ReadXml(My.Application.Info.DirectoryPath & "\UserLogin.xml")


            '-----------Show ProductionFrm with/without table

            ProductionFrm()
            ProductTableFrm()
            '----------------------------------------
            FrmProcessFill()  'Show when Alarm_160623 \783
        Catch ex As Exception
            SaveCatchLog(ex.ToString, "MDIParent1_Load")
            If ex.ToString = "RegisterCustomSecsMessage Error""" Then     '160715 \783 RegisterCustomSecsMessage Error
                MsgBox("RegisterCustomSecsMessage Error Application will restart")
                Me.Close()
            End If
        End Try


    End Sub
    Private Sub MDIParent1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown      '160624 Change width
        slMessage.Width = Me.Width - 200
    End Sub
    Protected Overrides ReadOnly Property CreateParams() As CreateParams   'Disable Close(x) Button
        Get
            Dim param As CreateParams = MyBase.CreateParams
            param.ClassStyle = param.ClassStyle Or &H200
            Return param
        End Get
    End Property
#Region "SecsGem"

    Private Sub OpenConsole()
        Try
            If Not My.Settings.SECS_Enable Then
                Exit Sub
            End If
            Dim frm As FormConsole = FormConsole.GetInstance(Me)
            frm.Text = "Network Console"
            frm.Owner = Me
            frm.Show()
            frm.Activate()
            If frm.WindowState = FormWindowState.Minimized Then
                frm.WindowState = FormWindowState.Normal
            End If
        Catch ex As Exception
            SaveCatchLog(ex.Message, "OpenConsole()")
        End Try
    End Sub

    Public ReadOnly Property Host() As HsmsHost
        Get
            Return m_Host
        End Get
    End Property
    Private Sub m_Host_HsmsStateChanged(ByVal sender As Object, ByVal e As HsmsStateChangedEventArgs)
        UpdateStateThreadSafe(e.State.ToString, StatusLabel.FrmSecs_slblCnnState)   '160627 \783 Eq comm revise
        If e.State = HsmsState.SELECTED And My.Settings.S1F13_Setting Then  '160627 \783 Eq comm revise
            Send_S1F13_EstablishCommunication()
            Exit Sub
        End If


    End Sub

    Private Sub m_Host_ErrorNotification(ByVal sender As Object, ByVal e As SecsErrorNotificationEventArgs)
        If e.Source Is Nothing Then
            UpdateInformationThreadSafe(e.Message & " TID:= " & e.TransactionId.ToString())
        Else
            UpdateInformationThreadSafe(e.Message & " S" & e.Source.ToString() & "F" & _
                                e.Source.Function.ToString() & " TID:= " & e.TransactionId.ToString())
        End If
    End Sub

    Private Sub m_Host_ConversionErrored(ByVal sender As Object, ByVal e As ConversionErrorEventArgs)
        UpdateInformationThreadSafe("ConversionErrored : " & e.Exception.ToString().Substring(0, 30))
        SaveCatchLog(e.Exception.ToString(), "ConversionErrored")
        SaveCatchLog(ConvertBytesToHexString(e.Data), "ConversionErrored")

    End Sub

    Private Sub m_CommLog(ByVal sender As Object, ByVal e As TraceLogEventArgs)  '160930 \783 Revise FrmSecsDisplay
        If Me.InvokeRequired Then
            'http://kristofverbiest.blogspot.com/2007/02/avoid-invoke-prefer-begininvoke.html
            Me.BeginInvoke(New TraceLogEventHandler(AddressOf m_CommLog), sender, e)
            Exit Sub
        End If
        Try
            If FrmSecs Is Nothing OrElse FrmSecs.IsDisposed Then
                Exit Sub
            End If

            If FrmSecs.tbxCommLog.Text.Length > FrmSecs.tbxCommLog.MaxLength * 20 Then 'More 60K Clear display
                FrmSecs.tbxCommLog.Text = ""
            End If

            If FrmSecs.CaptureMode And FrmSecs.FilterText.Text <> "" Then          '161222 \783 Capture Keys Mode on comm Log
                Dim Filteritem As String() = FrmSecs.FilterText.Text.Split(CChar(","))
                For i = 0 To Filteritem.Length - 1
                    If Filteritem(i) = "" Then
                        Continue For
                    End If
                    If e.SML Like "*" & Filteritem(i) & "*" Then
                        FrmSecs.tbxCommLog.AppendText(String.Format("{0:yyyy/MM/dd HH:mm:ss.fff} [{1}] " & Environment.NewLine & "{2}" & Environment.NewLine, _
                                                                e.TimeStamp, e.Direction, e.SML))
                        FrmSecs.CaptureOFF = True
                        slMessage.Text = "Capture item found : " & Filteritem(i)  '170110 \783 Nofication only main
                        'FrmSecs.slbStatus.Text = "Capture item found : " & Filteritem(i) & Format(Now, " |HH:mm:ss.fff")  '170110 \783 Nofication only main
                    End If

                Next


            End If

            If Not FrmSecs.CaptureOFF Then
                FrmSecs.tbxCommLog.AppendText(String.Format("{0:yyyy/MM/dd HH:mm:ss.fff} [{1}] " & Environment.NewLine & "{2}" & Environment.NewLine, _
                                                       e.TimeStamp, e.Direction, e.SML))
            End If
          

        Catch ex As Exception
            SaveCatchLog(ex.Message & vbCrLf & ex.StackTrace, "m_CommLog()")
        End Try
    End Sub

    'Public Function m_Host_ToSecsMessage(ByVal data() As Byte) As SecsMessageBase   '======160915 \783 S6F11 Err after 'Invalid data lenght' occur Revise
    '    Dim Msg As SecsMessageBase
    '    Msg = m_Host.MessageParser.ToSecsMessage(data)
    '    Return Msg
    'End Function

    Private Function ConvertBytesToHexString(ByVal data As Byte()) As String
        Dim ret As String = ""

        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()

        For Each b As Byte In data
            If sb.Length > 0 Then
                sb.Append(" " & b.ToString("X2"))
            Else
                sb.Append(b.ToString("X2"))
            End If
        Next

        ret = sb.ToString()
        sb.Remove(0, sb.Length)

        Return ret
    End Function

    Private Sub m_Host_ReceivedPrimaryMessage(ByVal sender As Object, ByVal e As PrimarySecsMessageEventArgs)
        Dim msg As SecsMessageBase = e.Primary

        ProcessSecsMessage(msg)

    End Sub

    Private Sub m_Host_ReceivedSecondaryMessage(ByVal sender As Object, ByVal e As SecondarySecsMessageEventArgs)

        Dim priMsg As SecsMessageBase = e.Primary
        Dim sndMsg As SecsMessageBase = e.Secondary
        Try

            Select Case priMsg.Stream
                Case 1
                    Select Case priMsg.Function
                        Case 13
                            Dim reply As S1F14E = DirectCast(sndMsg, S1F14E)
                            If reply.COMMACK = COMMACK.OK Then
                                UpdateStateThreadSafe("COMMUNICATING (Host Init)", StatusLabel.FrmSecs_slblCnnState)
                                GoOnline()     ' Eq Communication Revise  160627 \783
                            End If


                        Case 3
                            If FrmSecs IsNot Nothing Then

                                Dim reply As S1F4 = DirectCast(sndMsg, S1F4)
                                FrmSecs.ReplyS1F3(reply.SV)
                            End If
                        Case 17
                            If FrmSecs IsNot Nothing Then         '160906 \783 Filter execute loop
                                Dim reply As S1F18 = DirectCast(sndMsg, S1F18)   '160630 \783 AutoLoad revise
                                If reply.ONLACK = ONLACK.Refused Then
                                    CurDefFlow = AutoDefineReportFlow.Idle

                                    UpdateStateThreadSafe("Go OnLine Error : " & reply.ONLACK.ToString, StatusLabel.FrmSecs_sblStatus)

                                    GoTo FinalExeLoop
                                End If

                                If CurDefFlow = AutoDefineReportFlow.GoOnline Then         'Use when auto send
                                    CurDefFlow = AutoDefineReportFlow.Spooling_Purge
                                    Dim SF As New S6F23(SpoolCode.Purge)
                                    HostSend(SF)
                                End If
                            End If
                    End Select 'End Select S1Fx

                Case 2
                    Select Case priMsg.Function

                        Case 13
                            If FrmSecs IsNot Nothing Then             '160906 \783 Filter execute loop
                                Dim reply As S2F14 = DirectCast(sndMsg, S2F14)
                                Dim ECV As New List(Of String)
                                For Each Val As Object In reply.GetECVs
                                    FrmSecs.lbS2F13.Text += " ECV " & " = " & Val.ToString & "   "
                                    ECV.Add(Val.ToString)
                                Next
                            End If


                        Case 15
                            If FrmSecs IsNot Nothing Then             '160906 \783 Filter execute loop
                                Dim reply As S2F16 = DirectCast(sndMsg, S2F16)
                                FrmSecs.lbS2F15.Text = "S2F15 Reply  :  " & reply.EAC.ToString

                            End If


                        Case 33

                            If FrmSecs IsNot Nothing Then         '160906 \783 Filter execute loop

                                Dim reply As S2F34 = DirectCast(sndMsg, S2F34)
                                If reply.DRACK = DRACK.OK Then

                                    'AutoDefineReport -------------------------------------
                                    If CurDefFlow = AutoDefineReportFlow.DeleteAllReport Then
                                        CurDefFlow = AutoDefineReportFlow.DefineReport
                                        Send_S2F33_DefineReport()
                                        GoTo ExitLoop1
                                    End If

                                    If CurDefFlow = AutoDefineReportFlow.DefineReport Then
                                        CurDefFlow = AutoDefineReportFlow.LinkReport
                                        Send_S2F35_LinkReport()
                                        GoTo ExitLoop1
                                    End If

                                    '---------------------------------------------------------

                                Else

                                    'AutoDefineReport -------------------------------------
                                    If CurDefFlow = AutoDefineReportFlow.DeleteAllReport Then
                                        UpdateStateThreadSafe("DeleteAllReport Reply : " & reply.DRACK.ToString, StatusLabel.FrmSecs_sblStatus)
                                        CurDefFlow = AutoDefineReportFlow.Idle
                                        GoTo ExitLoop1
                                    End If
                                    If CurDefFlow = AutoDefineReportFlow.DefineReport Then
                                        UpdateStateThreadSafe("DefineReport Reply : " & reply.DRACK.ToString, StatusLabel.FrmSecs_sblStatus)
                                        CurDefFlow = AutoDefineReportFlow.Idle
                                        GoTo ExitLoop1
                                    End If
                                    '---------------------------------------------------------

                                End If
ExitLoop1:
                            End If

                        Case 35

                            If FrmSecs IsNot Nothing Then         '160906 \783 Filter execute loop

                                Dim reply As S2F36 = DirectCast(sndMsg, S2F36)

                                If reply.LRACK = LRACK.OK Then
                                    If CurDefFlow = AutoDefineReportFlow.LinkReport Then
                                        CurDefFlow = AutoDefineReportFlow.EnableAllReport
                                        Send_S2F37_EnableAllReport()
                                    End If

                                Else
                                    If CurDefFlow = AutoDefineReportFlow.LinkReport Then
                                        UpdateStateThreadSafe("LinkReport Reply : " & reply.LRACK.ToString, StatusLabel.FrmSecs_sblStatus)
                                        CurDefFlow = AutoDefineReportFlow.Idle
                                    End If
                                End If
                            End If

                        Case 37
                            If FrmSecs IsNot Nothing Then         '160906 \783 Filter execute loop

                                Dim reply As S2F38 = DirectCast(sndMsg, S2F38)

                                If reply.ERACK = ERACK.OK Then
                                    If CurDefFlow = AutoDefineReportFlow.EnableAllReport Then
                                        CurDefFlow = AutoDefineReportFlow.EnableAllAlarm
                                        Send_S5F3_EnableAllAlarm()
                                    End If

                                Else

                                    If CurDefFlow = AutoDefineReportFlow.EnableAllReport Then
                                        UpdateStateThreadSafe("EnableAllReport Reply : " & reply.ERACK.ToString, StatusLabel.FrmSecs_sblStatus)
                                        CurDefFlow = AutoDefineReportFlow.Idle
                                    End If

                                End If

                            End If

                        Case 43

                            If FrmSecs IsNot Nothing Then         '160906 \783 Filter execute loop
                                Dim bin = CType(sndMsg.Items(0), SecsItemList).Value(0)
                                Dim Valuex() As Byte = CType(bin.Value, Byte())
                                If Valuex(0) = 0 Then   'RSPACK spooling response 0 = OK ,1 = Reject
                                    UpdateStateThreadSafe("Reply : OK ", StatusLabel.FrmSecs_lbS2F44)
                                Else
                                    UpdateStateThreadSafe("Reply : Reject ", StatusLabel.FrmSecs_lbS2F44)
                                End If

                            End If

                    End Select 'End of Select Function of Stream 2

                Case 5
                    Select Case priMsg.Function
                        Case 3

                            If FrmSecs IsNot Nothing Then         '160906 \783 Filter execute loop

                                Dim reply As S5F4 = DirectCast(sndMsg, S5F4)
                                If reply.ACKC5 = ACKC5.OK Then
                                    If CurDefFlow = AutoDefineReportFlow.EnableAllAlarm Then
                                        UpdateStateThreadSafe("Auto Define Report Success", StatusLabel.FrmSecs_sblStatus)
                                        CurDefFlow = AutoDefineReportFlow.Idle
                                    End If


                                Else
                                    If CurDefFlow = AutoDefineReportFlow.EnableAllAlarm Then
                                        UpdateStateThreadSafe("EnableAllAlarm Reply : " & reply.ACKC5.ToString, StatusLabel.FrmSecs_sblStatus)
                                        CurDefFlow = AutoDefineReportFlow.Idle
                                    End If
                                End If
                            End If
                    End Select


                Case 6
                    Select Case priMsg.Function
                        Case 23
                            If FrmSecs IsNot Nothing Then           '160906 \783 Filter execute loop
                                Dim reply As S6F24 = DirectCast(sndMsg, S6F24)
                                '160630 \783 AutoLoad revise
                                UpdateStateThreadSafe("Reply : " & reply.RSDA.ToString, StatusLabel.FrmSecs_lbSpool)

                                If reply.RSDA = RSDA.RetryableBusy Then
                                    CurDefFlow = AutoDefineReportFlow.Idle
                                    UpdateStateThreadSafe("Spooling Purge Error : " & reply.RSDA.ToString, StatusLabel.FrmSecs_sblStatus)
                                    GoTo FinalExeLoop
                                End If
                                '161209 \783 Cancell regis report after use  spoolling
                                '    CurDefFlow = AutoDefineReportFlow.DeleteAllReport
                                '   Send_S2F33_DeleteAllReport()
                            End If
                    End Select


            End Select 'Select Stream end
FinalExeLoop:

        Catch ex As Exception
            SaveCatchLog(ex.ToString, "m_Host_ReceivedSecondaryMessage()S" & sndMsg.Stream & "F" & sndMsg.Function & "   ")
        End Try


        If OprData.FRMProductAlive Then
            FrmProduct.m_Host_ReceivedSecondaryMessage(e)
        End If
    End Sub

    Private Sub DisConnect()
        m_Host.Disconnect()
    End Sub


    'Send SxFx -----------------------------------------------------------------------------

    Public Sub Send_S2F33_DeleteAllReport() Handles FrmSecs.E_DeleteAllReport
        Dim msg As S2F33 = New S2F33(CStr(0))
        msg.SetDeleteAllReport()
        HostSend(msg)
    End Sub

    Public Sub Send_S2F33_DefineReport() Handles FrmSecs.E_DefineReport

        Dim msg As S2F33 = New S2F33(CStr(0))

        For Each rpt As SecsDataType In m_DefinedReportDic.Values   '160722 \783  common secs data type
            msg.AddReport(rpt.RPTID(0), rpt.VID.ToArray())
        Next

        HostSend(msg)

    End Sub

    Public Sub Send_S2F35_LinkReport() Handles FrmSecs.E_LinkReport


        'Dim msg As S2F35 = New S2F35(CStr(0))
        'For Each lr As SecsDataType In m_LinkedReportDic.Values

        '    msg.AddLink(lr.CEID, lr.RPTID.ToArray())
        'Next
        'HostSend(msg)


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

        HostSend(msg)



    End Sub

    Public Sub Send_S2F37_EnableAllReport() Handles FrmSecs.E_EnableAllReport
        Dim msg As S2F37 = New S2F37()
        msg.SetEnable()
        HostSend(msg)
    End Sub
    Private Sub Send_S5F3_EnableAllAlarm()
        Dim msg As S5F3 = New S5F3(True, 0)
        HostSend(msg)
    End Sub

    Private Sub Send_S1F13_EstablishCommunication()
        If Not My.Computer.Network.IsAvailable Then                '160628 \783 Eq comm revise
            slMessage.Text = "PC Nework point unplug" & Format(Now, " |HH:mm:ss.fff")
            Exit Sub
        End If
        If Not My.Computer.Network.Ping("10.1.1.50") Then            '160628 \783 Eq comm revise  Can Ping if Computer Connect only
            MsgBox("Ping to 10.1.1.50 fail")

        End If

        If CommuniationState = "NOT_CONNECTED" Then     '160628 \783 Eq comm revise
            slMessage.Text = "HSMS Data Message Activity can not do now.  Now Comm State is " & CommuniationState & Format(Now, " |HH:mm:ss.fff")
            Exit Sub
        End If
        If CommuniationState = "NOT_SELECTED" Then     '160627 \783 Eq comm revise
            slMessage.Text = "HSMS Data Message Activity can not do now.  Now Comm State is " & CommuniationState & Format(Now, " |HH:mm:ss.fff")
            Exit Sub
        End If


        Dim msg As S1F13 = New S1F13()
        HostSend(msg)
        UpdateStateThreadSafe("NOT COMMUNICATING", StatusLabel.FrmSecs_slblCnnState)

    End Sub

    ''' <summary>
    ''' S1F17 Request ON-LINE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Send_S1F17_OnlineRequest()
        Dim msg As SecsMessage = New SecsMessage(1, 17, True)
        HostSend(msg)
    End Sub

    ''' <summary>
    ''' S1F15R	Request OFF-LINE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Send_S1F15_OfflineRequest()
        Dim msg As SecsMessage = New SecsMessage(1, 15, True)
        HostSend(msg)
    End Sub


    ''' <summary>
    ''' S2F41 Host Command Send (HCS) [PP-SELECT]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SendRemoteCommand(ByVal RCmd As String, Optional ByVal CmdPName As String = "", Optional ByVal CmdPVal As String = "")
        Dim cmd As S2F41 = New S2F41()
        cmd.RemoteCommand = RCmd
        If CmdPName <> "" And CmdPVal <> "" Then
            cmd.AddVariable(CmdPName, CmdPVal)
        End If
        Host.Send(cmd)
    End Sub
    Private Sub Send_S2F13(ByVal U32List As List(Of UInt32))
        Dim msg As S2F13 = New S2F13()
        For Each U32 As UInt32 In U32List
            msg.AddEcid(U32)
        Next
        HostSend(msg)

    End Sub
    Private Sub Send_S2F15(ByVal U32 As UInt32, ByVal Ev As String, ByVal Format As SecsFormat) Handles FrmSecs.E_S215
        Dim msg As S2F15 = New S2F15()
        msg.AddListEcid(U32, Ev, Format)
        HostSend(msg)
    End Sub
    Private Sub Send_S5F3(ByVal Enable As Boolean, Optional ByVal ALID As UInteger = Nothing)
        Dim msg As S5F3 = New S5F3(Enable, ALID)
        HostSend(msg)
    End Sub


    Private Sub Send_S10F3(ByVal ID As Byte, ByVal msgText As String)
        Dim msg As S10F3 = New S10F3(ID, msgText)
        HostSend(msg)
    End Sub

    'Send SxFx EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE


    Private Sub ProcessSecsMessage(ByVal state As Object)


        Dim msg As SecsMessageBase = DirectCast(state, SecsMessageBase)
        Try
            Select Case msg.Stream
                Case 1
                    Select Case msg.Function
                        Case 1 'Are You Online?
                            Reply_S1F1(msg)
                        Case 13 'Establish Communications Request
                            Reply_S1F13(msg)

                    End Select
                Case 2
                    Select Case msg.Function
                        Case 17 'date time request
                            Reply_S2F17(CType(msg, S2F17))
                    End Select
                Case 5
                    Select Case msg.Function
                        Case 1 'Alarm Report Send
                            Perform_S5F1(CType(msg, S5F1))
                            Exit Sub           '160906 \783 Call productionn form
                    End Select
                Case 6
                    Select Case msg.Function
                        Case 11
                            Perform_S6F11(CType(msg, S6F11))
                            Exit Sub           '160906 \783 Call productionn form
                    End Select
                Case 9
                    Perform_S9(msg)

                Case 10

                    Perform_S10(msg)
                Case 64
                    Select Case msg.Function
                        Case 1 'supply tube id was read
                            'Perform_S64F1(CType(msg, S64F1))
                        Case 3 'tube changed
                            'Perform_S64F3(CType(msg, S64F3))
                        Case 11 'request new tube id for print
                            'Perform_S64F11(CType(msg, S64F11))
                    End Select
            End Select

        Catch ex As Exception
            SaveCatchLog("ProcessSecsMessage() Primary message recieve  : S" & msg.Stream & "F" & msg.Function, ex.ToString)
            UpdateInformationThreadSafe("S" & msg.Stream & "F" & msg.Function & "Primary message recieve Catch Error : Check detail in catch log")
        End Try
        If OprData.FRMProductAlive Then
            FrmProduct.m_Host_ReceivedPrimaryMessage(state)  '160906 \783 Call productionn form
        End If
    End Sub

    Private Sub Reply_S1F1(ByVal req As SecsMessageBase)
        Dim reply As SecsMessage = New SecsMessage(1, 2, False)
        Dim l0 As SecsItemList = New SecsItemList("L0")
        reply.Items.Add(l0)
        HostReply(req, reply)
    End Sub

    Private Sub Reply_S1F13(ByVal request As SecsMessageBase)
        'm_Equipment.DeviceId = request.DeviceId
        'reply
        Dim reply As S1F14 = New S1F14()
        HostReply(request, reply)
        UpdateStateThreadSafe("COMMUNICATING (Equip Init)", StatusLabel.FrmSecs_slblCnnState)
    End Sub

    Private Sub Reply_S2F17(ByVal request As S2F17)
        Dim reply As S2F18 = New S2F18(TimeFormat.A12) 'current time
        HostReply(request, reply)
    End Sub

    Private Sub Reply_S5F1(ByVal request As S5F1)
        'send back ackknowledge
        Dim reply As S5F2 = New S5F2()
        HostReply(request, reply)
    End Sub

    Private Sub Perform_S6F11(ByVal request As S6F11)

        'reply(acknowledge)
        Dim s6f12 As S6F12 = New S6F12()
        HostReply(request, s6f12)

        If OprData.FRMProductAlive Then
            FrmProduct.OnS6F11(request) '160801 \783 Add parameter m_Equipment
        End If
    End Sub

  
    Private Sub Perform_S5F1(ByVal request As S5F1)

        Reply_S5F1(CType(request, S5F1))

        If OprData.FRMProductAlive Then
            FrmProduct.OnS5F1(request) '160801 \783 Add parameter m_Equipment
        End If

    End Sub




    Private Sub Perform_S9(ByVal msg As SecsMessageBase)
        Select Case msg.Function
            Case 1 'Unrecognized Device ID!
                UpdateInformationThreadSafe("S" & msg.Stream & "F" & msg.Function & " Unrecognized Device ID")
                'm_Display.AddSecsGemLogThreadSafe(msg, "Unrecognized Device ID")
            Case 3 'Unrecognized Stream Type
                UpdateInformationThreadSafe("S" & msg.Stream & "F" & msg.Function & " Unrecognized Stream Type")
                'm_Display.AddSecsGemLogThreadSafe(msg, "Unrecognized Stream Type")
            Case 5 '"Unrecognized Function Type
                UpdateInformationThreadSafe("S" & msg.Stream & "F" & msg.Function & " Unrecognized Stream Type")
                'm_Display.AddSecsGemLogThreadSafe(msg, "Unrecognized Function Type")
            Case 7 'Illegal Data
                UpdateInformationThreadSafe("S" & msg.Stream & "F" & msg.Function & " Illegal Data")
                'm_Display.AddSecsGemLogThreadSafe(msg, "Illegal Data")
            Case 9 'Transaction Timer Timeout
                UpdateInformationThreadSafe("S" & msg.Stream & "F" & msg.Function & " Transaction Timer Timeout")
                'm_Display.AddSecsGemLogThreadSafe(msg, "Transaction Timer Timeout")
            Case 11 'Data Too Long
                UpdateInformationThreadSafe("S" & msg.Stream & "F" & msg.Function & " Data Too Long")
                'm_Display.AddSecsGemLogThreadSafe(msg, "Data Too Long")
            Case 13 'Conversation Timeout2
                UpdateInformationThreadSafe("S" & msg.Stream & "F" & msg.Function & " Conversation Timeout")
                'm_Display.AddSecsGemLogThreadSafe(msg, "Conversation Timeout")
        End Select
    End Sub

    Private Sub Perform_S10(ByVal msg As SecsMessageBase)
        Select Case msg.Function
            Case 1
                Dim s10f2 As S10F2 = New S10F2(ACKC10.Accepted)
                HostReply(msg, s10f2)
                Dim CommandS10F1 As New S10F1
                CommandS10F1 = CType(msg, S10F1)
                'S10F1ThreadSafe("Message : " & CommandS10F1.Text & "   Terminal ID : " & CommandS10F1.TIDx)
                S10F1ThreadSafe(CommandS10F1.Text)
        End Select

    End Sub

    Private Sub SendHexTrain(ByVal HexTrain As String) Handles FrmSecs.E_SendStrData
        If HexTrain = "" Then
            Exit Sub
        End If
        Dim byteArray As Byte() = ConvertStringToByte(HexTrain)
        Dim msg As SecsMessageBase = m_Host.MessageParser.ToSecsMessage(byteArray)
        'Dim smlText As String = SmlBuilder.ToSmlString(msg)
        HostSend(msg)

    End Sub


    Private Sub HostSend(ByVal msg As SecsMessageBase) Handles FrmSecs.E_HostSend, FrmProduct.E_HostSend
        Try
            If CommuniationState = "NOT_SELECTED" Then     '160627 \783 Eq comm revise
                slMessage.Text = "HSMS Data Message Activity can not do now.  Now Comm State is " & CommuniationState & Format(Now, " |HH:mm:ss.fff")
                Exit Sub
            End If
            If CommuniationState = "NOT_CONNECTED" Then     '160628 \783 Eq comm revise
                slMessage.Text = "HSMS Data Message Activity can not do now.  Now Comm State is " & CommuniationState & Format(Now, " |HH:mm:ss.fff")
                Exit Sub
            End If


            m_Host.Send(msg)

        Catch ex As Exception
            SaveCatchLog(ex.ToString, "HostSent()")
            slMessage.Text = "HostSend Error" & Format(Now, " |HH:mm:ss.fff")
        End Try

    End Sub

    Private Sub HostReply(ByVal Pri As SecsMessageBase, ByVal Secn As SecsMessageBase) Handles FrmProduct.E_HostReply
        Try
            m_Host.Reply(Pri, Secn)
        Catch ex As Exception
            SaveCatchLog(ex.ToString, "HostReply()")
        End Try

    End Sub



    Private m_S10F1 As UpdateTextDelegate = New UpdateTextDelegate(AddressOf S10F1ThreadSafe)
    Private Sub S10F1ThreadSafe(ByVal informationText As String)
        If Me.InvokeRequired Then
            Me.BeginInvoke(m_S10F1, informationText)
            Exit Sub
        End If

        Try
            If FrmSecs IsNot Nothing Then
                FrmSecs.TextBox2.Text = informationText
            End If
        Catch ex As Exception
            SaveCatchLog(ex.ToString, "S10F1ThreadSafe")
        End Try



    End Sub
#End Region

#Region "TDC"

    ''  Description
    ''Public Enum RunModeType
    ''    Normal = 0
    ''    Separated = 1
    ''    SeparatedEnd = 2
    ''End Enum

    ''Public Enum EndModeType
    ''    Normal = 1
    ''    AbnormalEndReset = 2
    ''    AbnormalEndAccumulate = 3
    ''End Enum

    Private Structure TDC_Parameter
        Dim LotNo As String
        Dim StartMode As RunModeType
        Dim TimeStamp As Date
        Dim GoodPcs As Integer
        Dim NgPcs As Integer
        Dim EndMode As EndModeType
        Dim EqNo As String
        Dim OPID As String
    End Structure

  


    Private Sub LotSet(ByVal EQNo As String, ByVal LotNo As String, ByVal StartTime As Date, ByVal OPID As String, Optional ByVal StartMode As RunModeType = RunModeType.Normal)
      
        ' If Not My.Settings.TDC_Enable Then
        'Exit Sub
        ' End If
        Dim Parameter As New TDC_Parameter
        Parameter.LotNo = LotNo
        Parameter.EqNo = EQNo
        Parameter.OPID = OPID
        Parameter.StartMode = StartMode
        Parameter.TimeStamp = StartTime


        Dim Task As New BackgroundWorker
        AddHandler Task.DoWork, AddressOf LotSet_Dowork
        AddHandler Task.RunWorkerCompleted, AddressOf LotSet_RunWorkerCompleted
        Task.RunWorkerAsync(Parameter)

    End Sub
    Dim lotSetS As New Object
    Private Sub LotSet_Dowork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Try
            SyncLock lotSetS
                Dim agr As New TDC_Parameter
                agr = CType(e.Argument, TDC_Parameter)
                Dim res As TdcResponse = m_TdcService.LotSet(agr.EqNo, agr.LotNo, agr.TimeStamp, agr.OPID, agr.StartMode)
                e.Result = res
            End SyncLock


        Catch ex As Exception
            SaveCatchLog(ex.ToString, "LotSet_Dowork()")

        End Try
    End Sub

    Private Sub LotSet_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        Dim result As New TdcResponse
        result = CType(e.Result, TdcResponse)
        'Dim CellCon As New CellConObj

        If OprData.FRMProductAlive Then
            FrmProduct.LS_Reply(result)
        End If

        'If result.HasError Then
        '    Select Case result.ErrorCode
        '        Case "01"
        '        Case "02"
        '        Case "03"
        '        Case "04"
        '        Case "05"
        '        Case "06"
        '        Case "70"
        '        Case "71"
        '        Case "72"
        '        Case "99"
        '    End Select
        'End If
    End Sub


    Private Sub LotEnd(ByVal EQNo As String, ByVal LotNo As String, ByVal EndTime As Date, ByVal GoodPcs As Integer, ByVal NgPcs As Integer, ByVal OPID As String, Optional ByVal EndMode As EndModeType = EndModeType.Normal)
        ' If Not My.Settings.TDC_Enable Then
        'Exit Sub
        ' End If
        Dim Parameter As New TDC_Parameter
        Parameter.LotNo = LotNo
        Parameter.EndMode = EndMode
        Parameter.TimeStamp = EndTime
        Parameter.GoodPcs = GoodPcs
        Parameter.NgPcs = NgPcs
        Parameter.EqNo = EQNo
        Parameter.OPID = OPID
        Dim Task As New BackgroundWorker
        AddHandler Task.DoWork, AddressOf LotEnd_Dowork
        AddHandler Task.RunWorkerCompleted, AddressOf LotEnd_RunWorkerCompleted
        Task.RunWorkerAsync(Parameter)


    End Sub

    Dim LotEndS As New Object
    Private Sub LotEnd_Dowork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Try
            SyncLock LotEndS
                Dim agr As New TDC_Parameter
                agr = CType(e.Argument, TDC_Parameter)
                Dim res As TdcResponse = m_TdcService.LotEnd(agr.EqNo, agr.LotNo, agr.TimeStamp, agr.GoodPcs, agr.NgPcs, agr.EndMode, agr.OPID)
                e.Result = res
            End SyncLock

        Catch ex As Exception
            SaveCatchLog(ex.ToString, "LotEnd_Dowork()")

        End Try
    End Sub

    Private Sub LotEnd_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)


        Dim result As New TdcResponse
        result = CType(e.Result, TdcResponse)
        If OprData.FRMProductAlive Then
            FrmProduct.LE_Reply(result)
        End If

    End Sub


#End Region

#Region "UserInterface"


    Private Sub SettingFrm() Handles MainControlfrm.SettingClick
        If Not OprData.UserLevel = CommonData.Level.ADMIN Then
            Exit Sub
        End If
        FrmSetting = New Setting
        FrmSetting.ShowDialog()
    End Sub


    Private Sub ConsoleFrm() Handles FrmProduct.E_ConsoleShow, FrmSecs.E_ConsoleShow
        OpenConsole()
    End Sub


    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CascadeToolStripMenuItem.Click
        If Not FrmProduct Is Nothing Then
            FrmProduct.Dock = DockStyle.None
            FrmProduct.Activate()
            FrmProduct.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        End If
        If Not FrmProdTable Is Nothing Then      'form open check
            FrmProdTable.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
            FrmProdTable.Dock = DockStyle.None
            FrmProdTable.Show()      '160624 Fix Display  \783 
        End If
        If FrmSecs IsNot Nothing Then
            FrmSecs.Dock = DockStyle.None
        End If
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TileVerticalToolStripMenuItem.Click
        If Not FrmProduct Is Nothing Then
            FrmProduct.Dock = DockStyle.None
            FrmProduct.Activate()
            FrmProduct.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        End If
        If Not FrmProdTable Is Nothing Then      'form open check
            FrmProdTable.Dock = DockStyle.None
            FrmProdTable.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
            FrmProdTable.Show()      '160624 Fix Display  \783

        End If
        If FrmSecs IsNot Nothing Then
            FrmSecs.Dock = DockStyle.None
        End If

        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        If Not FrmProduct Is Nothing Then
            FrmProduct.Dock = DockStyle.None
            FrmProduct.Activate()
            FrmProduct.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        End If
        If Not FrmProdTable Is Nothing Then      'form open check
            FrmProdTable.Dock = DockStyle.None
            FrmProdTable.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
            FrmProdTable.Show()      '160624 Fix Display  \783

        End If




        If FrmSecs IsNot Nothing Then
            FrmSecs.Dock = DockStyle.None
        End If
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub OpenComonForm() Handles MainControlfrm.OpenComForm
        Try
            slMessage.Text = "Login Success User Level :" & OprData.UserLevel.ToString & Format(Now, " |HH:mm:ss.fff")

            If Not OprData.UserLevel = CommonData.Level.OP Then
                Exit Sub
            End If

            For Each ChildForm As Form In Me.MdiChildren
                If ChildForm.Name = "Form1" Then
                    Continue For
                End If
                If ChildForm.Name = "ProcessForm" Then
                    Continue For
                End If
                If ChildForm.Name = "ProductionTable" Then
                    Continue For
                End If
                ChildForm.Close()
            Next

            If FrmProduct IsNot Nothing Then
                FrmProduct.Dock = DockStyle.Fill
                FrmProduct.FormBorderStyle = Windows.Forms.FormBorderStyle.None

            Else
                FrmProduct = New ProcessForm
                FrmProduct.MdiParent = Me
                FrmProduct.Dock = DockStyle.Fill
                FrmProduct.Show()

            End If
            If FrmProdTable IsNot Nothing Then
                If FrmProdTable.WindowState = FormWindowState.Minimized Then
                    FrmProdTable.WindowState = FormWindowState.Normal
                End If
                FrmProdTable.Height = 211
                FrmProdTable.Dock = DockStyle.Bottom
                FrmProdTable.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                FrmProdTable.Show()      '160624 Fix Display  \783
            Else
                If Not OprData.DisaTableFrmShow Then
                    FrmProdTable = New ProductionTable
                    FrmProdTable.MdiParent = Me
                    FrmProdTable.Dock = DockStyle.Bottom
                    FrmProdTable.Show()

                End If

            End If

            FrmProduct.Activate()


        Catch ex As Exception
            SaveCatchLog(ex.ToString, "OpenComonForm()")
        End Try
    End Sub

    Private Sub HomeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HomeToolStripMenuItem.Click
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            If ChildForm.Name = "Form1" Then
                Continue For
            End If
            If ChildForm.Name = "ProcessForm" Then
                Continue For
            End If
            If ChildForm.Name = "ProductionTable" Then
                Continue For
            End If
            ChildForm.Close()
        Next
        ProductionFrm()       '161229 \783 Display revise
        'If Not FrmProduct Is Nothing Then
        '    FrmProduct.Dock = DockStyle.Fill
        '    FrmProduct.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        '    FrmProduct.Activate()

        'End If
        'If Not FrmProdTable Is Nothing Then                    
        '    If FrmProdTable.WindowState = FormWindowState.Minimized Then
        '        FrmProdTable.WindowState = FormWindowState.Normal
        '    End If
        '    FrmProdTable.Height = 211
        '    FrmProdTable.Dock = DockStyle.Bottom
        '    FrmProdTable.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        '    FrmProdTable.Show()      '160624 Fix Display  \783
        'End If
    End Sub

    Private Sub CloseFrm() Handles MainControlfrm.MeCLose
        Me.Close()
    End Sub
    Private Sub ForceClose(ByVal Reflag As Integer) Handles FrmSetting.E_FormClosing

        Select Case Reflag
            Case Setting.SecsSettingCloseFlag.Normal
                Exit Sub
            Case Setting.SecsSettingCloseFlag.Warning
                Dim Ans = MsgBox("Restart application for take affect the setting ?", MsgBoxStyle.OkCancel)
                If Ans = MsgBoxResult.Cancel Then

                    slMessage.Text = " !!! Please restart application for take affect the setting !!! " & Format(Now, " |HH:mm:ss.fff")
                    slMessage.BackColor = Color.Red
                    Exit Sub
                End If
                Me.Close()
            Case Setting.SecsSettingCloseFlag.ForceOFF
                Me.Close()
        End Select


    End Sub

    Private Sub FormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FormToolStripMenuItem.Click
        MainControlfrm.Timer1.Enabled = False            'Reset Timer1 
        MainControlfrm.Timer1.Enabled = True

        Dim Frm As Form = Me.ActiveMdiChild
        If Not Frm.Name = "Form1" Then

            Me.MainControlfrm.Activate()
            If MainControlfrm.WindowState = FormWindowState.Minimized Then
                MainControlfrm.WindowState = FormWindowState.Normal
            End If
            MainControlfrm.Timer1.Stop()
            Exit Sub
        End If

        If MainControlfrm.WindowState = FormWindowState.Minimized Then
            MainControlfrm.WindowState = FormWindowState.Normal
            MainControlfrm.Timer1.Stop()
        Else
            MainControlfrm.WindowState = FormWindowState.Minimized
        End If



    End Sub
    Private Sub MDIMakeAlarmCellCon(ByVal AlarmMessage As String, Optional ByVal Location As String = "", Optional ByVal Status As String = "", Optional ByVal AlarmID As String = "") Handles FrmProduct.E_MakeAlarmCellCon
        If My.Settings.MDITableFrmDisable Then
            slMessage.Text = "MDITableFrmDisable config is disable (ไม่สามารถแสดง AlarmCellCon ได้ )"
        End If
        If FrmProdTable Is Nothing Then
            Exit Sub
        End If
        AlarmTimer.Enabled = True
        OprData.AlrmtimerCount = 0
        If Not FrmProdTable.Visible Then   'FrmProtable already show = True 
            ProductTableFrm()  'Show when Alarm_160623 \783
        End If

        FrmProdTable.MakeAlarmCellCon(AlarmMessage, Location, Status, AlarmID)

    End Sub
    Public Sub MDIUpdate_dgvProductionInfo1(ByVal _CarrierID As String, ByVal LotID As String, ByVal Package As String, ByVal Device As String, Optional ByVal Remark As String = "", Optional ByVal StartTime As String = "") Handles FrmProduct.E_Update_dgvProductionInfo1
        If FrmProdTable Is Nothing Or Not My.Settings.FrmProdTableInfo Then '161222 \783 Add/Remove tabpage
            Exit Sub
        End If

        FrmProdTable.Update_dgvProductionInfo1(_CarrierID, LotID, Package, Device, Remark, StartTime)
    End Sub
    Public Sub MDIUpdate_dgvProductionInfoEnd(ByVal _UnloadCarrierID As String, ByVal lotno As String, Optional ByVal Count As String = "", Optional ByVal Remark As String = "") Handles FrmProduct.E_Update_dgvProductionInfoEnd
        If FrmProdTable Is Nothing Or Not My.Settings.FrmProdTableInfo Then '161222 \783 Add/Remove tabpage
            Exit Sub
        End If
        FrmProdTable.Update_dgvProductionInfoEnd(_UnloadCarrierID, lotno, Count, Remark)
    End Sub


    Public Sub MDIUpdate_dgvProductionDetail(ByVal itemID As String, ByVal type As String, ByVal action As String, Optional ByVal location As String = "") Handles FrmProduct.E_Update_dgvProductionDetail
        If FrmProdTable Is Nothing Or Not My.Settings.FrmProdTableIDetail Then '161222 \783 Add/Remove tabpage
            Exit Sub
        End If
        FrmProdTable.Update_dgvProductionDetail(itemID, type, action, location)
    End Sub


    Private Sub MinimizeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MinimizeToolStripMenuItem.Click
        'Me.WindowState = FormWindowState.Minimized
        Me.SendToBack()
    End Sub


    Private m_UIF As UpdateTextDelegate = New UpdateTextDelegate(AddressOf UpdateInformationThreadSafe)

    Private Sub UpdateInformationThreadSafe(ByVal informationText As String) Handles FrmProdTable.E_SlInfo, FrmProduct.E_SlInfo, MainControlfrm.E_SlInfo, FrmSecs.E_SlInfo, FrmSetting.E_SlInfo, FrmHelper.E_SlInfo
        If Me.InvokeRequired Then
            'http://kristofverbiest.blogspot.com/2007/02/avoid-invoke-prefer-begininvoke.html
            Me.BeginInvoke(m_UIF, informationText)
            Exit Sub
        End If
        slMessage.Text = informationText & Format(Now, " |HH:mm:ss.fff")      '160913 \783 Add index timer for  slMessage.Text 
    End Sub

    Private m_Slb As UpdateTextDelegate1 = New UpdateTextDelegate1(AddressOf UpdateStateThreadSafe)

    Private Sub UpdateStateThreadSafe(ByVal informationText As String, ByVal ControlName As StatusLabel)
        If Me.InvokeRequired Then
            'http://kristofverbiest.blogspot.com/2007/02/avoid-invoke-prefer-begininvoke.html
            Me.BeginInvoke(m_Slb, informationText, ControlName)
            Exit Sub
        End If
        Try
            If ControlName = StatusLabel.FrmSecs_slblCnnState Then
                CommuniationState = informationText
                If FrmProduct IsNot Nothing Then

                    If informationText Like "COMMUNICATING*" Then   '160627 \783 Eq Comm Revise
                        FrmProduct.BackColor = Color.WhiteSmoke
                        If My.Settings.MCType = "Canon-D10R" Then
                            Send_S1F17_OnlineRequest()
                        End If

                        FrmProduct.RequestSVID_CurrentState()
                        FrmProduct.RequestSVID_GoodDies()
                        slMessage.Text = "SECS/GEM COMMUNICATION : " & informationText
                    Else
                        FrmProduct.BackColor = Color.Red
                        slMessage.Text = "SECS/GEM COMMUNICATION : " & informationText
                    End If

                End If

            End If
            If ControlName = StatusLabel.FrmSecs_slbControlState Then
                ControlState = informationText
            End If
            If FrmSecs Is Nothing Then
                Exit Sub
            End If
            Select Case ControlName
                Case StatusLabel.FrmSecs_slblCnnState
                    FrmSecs.slblCnnState.Text = informationText
                Case StatusLabel.FrmSecs_sblStatus
                    slMessage.Text = informationText
                    'FrmSecs.slbStatus.Text = informationText    '170110 \783 Nofication only main
                Case StatusLabel.FrmSecs_slbControlState
                    FrmSecs.slbControlState.Text = informationText
                Case StatusLabel.FrmSecs_lbS2F44
                    FrmSecs.lbS2F44.Text = informationText
                Case StatusLabel.FrmSecs_lbSpool
                    FrmSecs.lblSpool.Text = informationText
            End Select
        Catch ex As Exception
            SaveCatchLog(ex.ToString, "UpdateStateThreadSafe")
        End Try


    End Sub

    'Red Blinking
    Private Sub AlarmTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlarmTimer.Tick
        If OprData.FRMProductAlive Then
            If OprData.AlrmtimerCount Mod 2 = 0 Then
                'FrmProduct.Panel1.BackColor = Color.Red
            Else
                'FrmProduct.Panel1.BackColor = Color.WhiteSmoke
            End If
            OprData.AlrmtimerCount += 1
            AlarmTimer.Enabled = False
            AlarmTimer.Enabled = True
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        MsgBox("Cellcon Software version " & CelconVer, MsgBoxStyle.Information, NetVerSion)
    End Sub

#End Region


#Region "FrmProdTable"
    ' 161228 \783 Add Select tabpages

    
    Private Sub HelperFrm(ByVal HelperTabpages As String) Handles FrmProduct.E_HelperCall                   '170224 \783 Helper form addition
        If FrmProdTable IsNot Nothing Then
            FrmProdTable.Dock = DockStyle.None
            FrmProdTable.Hide()
        End If
        If FrmHelper Is Nothing Then
            FrmHelper = ProductionTable
            GoTo show
        End If
        If FrmHelper.IsDisposed Then
            FrmHelper = ProductionTable
            GoTo show
        End If
        FrmHelper.Height = 211
        FrmHelper.Dock = DockStyle.Bottom
        FrmHelper.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        FrmHelper.Activate()
        If FrmHelper.WindowState = FormWindowState.Minimized Then
            FrmHelper.WindowState = FormWindowState.Normal
        End If
Show:
        FrmHelper.MdiParent = Me
        FrmHelper.Dock = DockStyle.Bottom
        FrmHelper.tbPageMain.SelectTab(HelperTabpages)
        FrmHelper.tbPageMain.TabPages.Remove(FrmHelper.tbAlarm)
        FrmHelper.tbPageMain.TabPages.Remove(FrmHelper.tbAlarmCellCon)
        FrmHelper.tbPageMain.TabPages.Remove(FrmHelper.tbDetail)
        FrmHelper.tbPageMain.TabPages.Remove(FrmHelper.tbProductionPage)
        FrmHelper.Show()

      
        FrmHelper.pbxWorkSlipBorder.BackColor = Color.Transparent  '170224 \783 Helper form addition
        FrmHelper.pbxOPIDBorder.BackColor = Color.Transparent
        FrmHelper.btnOPID.ForeColor = Color.Black
        FrmHelper.btnWorkSlip.ForeColor = Color.Black

        If HelperTabpages = "tbOther" Then       ' This Loop must be lowest because need keep focus at TbxKey
            If Not OprData.QrReadSystemOn Then   '160629 \783 QrSystemON/OFF 
                FrmHelper.Close()
                Exit Sub
            End If
            If FrmHelper.pbxOPIDBorder.BackColor = Color.Transparent Then
                FrmHelper.OpidQrClick()
            End If
        End If

    End Sub


    Private Sub FrmProductionOPIDClick(ByVal Tabpages As String) Handles FrmProduct.E_ProductionTableCall

        ProductTableFrm(Tabpages)


        'If Tabpages = "tbOther" Then
        '    If FrmProdTable.pbxOPIDBorder.BackColor = Color.Transparent Then
        '        FrmProdTable.OpidQrClick()
        '    End If
        'End If
    End Sub


    Private Sub ProductTableFrm(Optional ByVal Tabpages As String = "tbAlarmCellCon") Handles MainControlfrm.ProdTableClick  'Product table Form Load
        If OprData.DisaTableFrmShow Then
            Exit Sub
        End If

        If FrmProdTable Is Nothing Then
            FrmProdTable = New ProductionTable
            GoTo show
        End If
        If FrmProdTable.IsDisposed Then
            FrmProdTable = New ProductionTable
            GoTo show
        End If
        FrmProdTable.Height = 211
        FrmProdTable.Dock = DockStyle.Bottom
        FrmProdTable.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        FrmProdTable.Activate()
        If FrmProdTable.WindowState = FormWindowState.Minimized Then
            FrmProdTable.WindowState = FormWindowState.Normal
        End If

        ''If Tabpages = "tbOther" Then     'Tabpages = "tbOther"  is QR Input button frmproduction form  '170224 \783 Helper form addition
        ''    If IsNothing(FrmProdTable.tbPageMain.TabPages("tbOther")) Then
        ''        FrmProdTable.tbPageMain.TabPages.Add(FrmProdTable.tbOther)
        ''        'FrmProdTable.tbPageMain.TabPages.Insert(0, FrmProdTable.tbOther)

        ''    End If

        ''Else
        ''    If Not IsNothing(FrmProdTable.tbPageMain.TabPages("tbOther")) Then
        FrmProdTable.tbPageMain.TabPages.Remove(FrmProdTable.tbOther)

        ''    End If


        ''End If

        FrmProdTable.tbPageMain.SelectTab(Tabpages)

        If FrmHelper IsNot Nothing Then
            FrmHelper.Hide()
        End If
        FrmProdTable.Show()        '160624 Fix Display bug

        'FrmProdTable.pbxWorkSlipBorder.BackColor = Color.Transparent  '170116 \783 Revise QR WOrk order  '170224 \783 Helper form addition
        'FrmProdTable.pbxOPIDBorder.BackColor = Color.Transparent
        'FrmProdTable.btnOPID.ForeColor = Color.Black
        'FrmProdTable.btnWorkSlip.ForeColor = Color.Black


        Exit Sub                   'If already open will not open again
show:


        FrmProdTable.MdiParent = Me
        FrmProdTable.Dock = DockStyle.Bottom
        FrmProdTable.tbPageMain.SelectTab(Tabpages)

        FrmProdTable.Show()



    End Sub

    'AlarmALCD: True = Set,False = Clear  AlarmType :  0 = Normal  1 = Major
    Public Sub MDIAlarmTable(ByVal AlarmALCD As Boolean, ByVal AlarmALID As String, ByVal AlarmALTX As String, Optional ByVal AlarmType As String = "0") Handles FrmProduct.E_AlarmTable
        If FrmProdTable Is Nothing Then
            Exit Sub
        End If
        FrmProdTable.AlarmTable(AlarmALCD, AlarmALID, AlarmALTX, AlarmType)

    End Sub
    Private Sub AlarmCellconReset() Handles FrmProdTable.E_AlarmCellconReset
        AlarmTimer.Enabled = False
        If OprData.FRMProductAlive Then
            'FrmProduct.Panel1.BackColor = Color.WhiteSmoke   'Alarm release Red color clear
        End If
        If FrmHelper IsNot Nothing Then
            FrmHelper.Show()
        End If
        FrmProcessFill()  'Show when Alarm_160623 \783
    End Sub



    Private Sub OPIDClick() Handles FrmHelper.E_OPIDClick     '170224 \783 Helper form addition
        FrmHelper.btnOPID.Text = "OPID"
        FrmProduct.lbOPID.Text = OprData.OPID
        slMessage.Text = "Please Read OPID QR Data" & Format(Now, " |HH:mm:ss.fff")
    End Sub

    Private Sub QR_OPIDRead() Handles FrmHelper.E_QRReadOPIDSuccess, FrmProduct.E_QRReadOPIDSuccess
        slMessage.Text = "QR OPID Read Success_" & Format(Now, " |HH:mm:ss.fff")
        FrmProduct.lbOPID.Text = OprData.OPID
        If OprData.UserLevel = CommonData.Level.ADMIN Then
            OprData.UserLevel = CommonData.Level.OP
            MainControlfrm.btnLogin.Text = "OP"
        End If
        If FrmHelper Is Nothing Then
            Exit Sub
        End If
        FrmHelper.btnOPID.Text = OprData.OPID
        FrmHelper.pbxOPIDBorder.BackColor = Color.Transparent
        FrmHelper.pbxWorkSlipBorder.BackColor = Color.Blue
    End Sub


    Private Sub TransactionDataSave(ByVal QrData As String) Handles FrmProduct.E_TransactionDataSave
        Dim WorkSlipQR As New WorkingSlipQRCode
        Dim Ans = WorkSlipQR.TransactionDataSave(OprData.QrData)
        If Ans Like "False*" Then
            slMessage.Text = Ans & " (TransactionDataSave)" & Format(Now, " |HH:mm:ss.fff")
        End If
    End Sub

    Private Sub WorkSlipDataClear() Handles FrmHelper.E_WorkSlipClick
        slMessage.Text = "Please Read Work Slip QR Data" & Format(Now, " |HH:mm:ss.fff")
        FrmProduct.lbLotNo.Text = ""
        FrmProduct.lbPackage.Text = ""
        FrmProduct.lbDevice.Text = ""
        FrmProduct.lbRecipe.Text = ""
        FrmProduct.lbStartTime.Text = ""
        FrmProduct.lbEndTime.Text = ""
        FrmHelper.pbxWorkSlipBorder.BackColor = Color.Blue
    End Sub

#End Region

#Region "FrmProduct"

    Private Sub ProductionFrm() Handles MainControlfrm.ProductionClick
        If FrmProduct Is Nothing Then
            FrmProduct = New ProcessForm
            GoTo show
        End If
        If FrmProduct.IsDisposed Then
            FrmProduct = New ProcessForm
            GoTo show
        End If
        If Not FrmProdTable Is Nothing Then
            FrmProdTable.Dock = DockStyle.None
        End If
        FrmProduct.Dock = DockStyle.Fill
        FrmProduct.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        FrmProduct.Activate()

        If My.Settings.Resolution = "1280x1024" Then
            Me.Size = New Size(1280, 1024)
            Me.MinimumSize = New Size(1280, 1024)
        Else
            Me.Size = New Size(1024, 768)
            Me.MinimumSize = New Size(1024, 768)
        End If


        Exit Sub                   'If already open will not open again
show:

        FrmProduct.MdiParent = Me
        FrmProduct.Dock = DockStyle.Fill
        FrmProduct.Show()

    End Sub

    Private Sub FrmProcessFill() Handles FrmProduct.E_FormFill


        If Not FrmProdTable Is Nothing Then
            FrmProdTable.Dock = DockStyle.None
            FrmProdTable.Hide()
        End If
        FrmProduct.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        FrmProduct.Dock = DockStyle.Fill
        FrmProduct.BringToFront()
        'FrmProduct.Activate()    'Show when Alarm_160623 \783

    End Sub

    Private Sub EqConnect() Handles FrmProduct.E_EqConnect, FrmTCPClient.E_TcpConnect, FrmSecs.E_EstabComm
        If My.Settings.CsProtocol_Enable Then     'Custom Protocol
            If Not (OprData.CSConnect = "Disconnect") Then  ''Or CommuniationState Like "NOT COMMUNICATING") Then  '160627 EqConnect revise
                Exit Sub
            End If
            CstProtocol.ReadContinue = True
            CstProtocol.Listener_Click(CStr(My.Settings.CsProtocolPort), My.Settings.EquipmentIP)
        End If

        If My.Settings.SECS_Enable Then
            Send_S1F13_EstablishCommunication()
        End If

    End Sub


#End Region

#Region "FrmSecs"
    Private Sub SecsGemManualMode() Handles MainControlfrm.SecsGemClick
        If Not My.Settings.SECS_Enable Then
            Exit Sub
        End If

        If FrmSecs Is Nothing Then
            FrmSecs = New SecsGemFrm
            GoTo show
        End If
        If FrmSecs.IsDisposed Then
            FrmSecs = New SecsGemFrm
            GoTo show
        End If


        'If FrmProduct IsNot Nothing Then    '160930 \783 revise display
        '    If FrmProduct.IsDisposed Then
        '        GoTo Loop1
        '    End If
        '    TileHorizontalToolStripMenuItem_Click(Nothing, Nothing)   '160708 \783 SecsDebug Display revise
        'End If
Loop1:
        FrmSecs.Activate()
        Exit Sub                   'If already open will not open again
show:
        FrmSecs.MdiParent = Me
        FrmSecs.Show()
        'If FrmProduct IsNot Nothing Then    '160930 \783 revise display
        '    If FrmProduct.IsDisposed Then
        '        Exit Sub
        '    End If
        '    TileHorizontalToolStripMenuItem_Click(Nothing, Nothing)  '160708 \783 SecsDebug Display revise
        'End If

    End Sub

    Private Sub LoadFrFile() Handles FrmSecs.E_LoadFrFile

        Try
            Dim Ltable As New DataTable
            Dim DTable As New DataTable
            Ltable.ReadXmlSchema(My.Application.Info.DirectoryPath & "\LinkTableSchema.xml")
            Ltable.ReadXml(My.Application.Info.DirectoryPath & "\LinkTable.xml")
            DTable.ReadXmlSchema(My.Application.Info.DirectoryPath & "\DefReportSchema.xml")
            DTable.ReadXml(My.Application.Info.DirectoryPath & "\DefReport.xml")
            'DefReport.ReadXml(My.Application.Info.DirectoryPath & "\DefReport.xml")

            Dim TmpCall As New SECSGEMDB(My.Settings.SecsConnStr)

            m_LinkedReportDic.Clear()
            TmpCall.LoadDataTableToLink(Ltable)
            m_DefinedReportDic.Clear()
            TmpCall.LoadDataTableToDefine(DTable)

            If FrmSecs IsNot Nothing Then
                FrmSecs.cbxLinkReport.Items.Clear()
                FrmSecs.cbxLinkReport.Items.Add("DeleteAllReport")
                FrmSecs.cbxLinkReport.Items.Add("Define Report")
                FrmSecs.cbxLinkReport.Items.Add("LinkReport")
                FrmSecs.cbxLinkReport.Items.Add("EnableAllReport")
                'FrmSecs.slbStatus.Text = "Report Download FromFile Success"  '170110 \783 Nofication only main
                slMessage.Text = "Report Download FromFile Success"
                FrmSecs.cbxLinkReport.SelectedIndex = 0
            End If

            SaveCatchLog("Load Report Form File", "Info : ")
            slMessage.Text = "Report DownLoad FromFile Success" & Format(Now, " |HH:mm:ss.fff")

        Catch ex As Exception
            SaveCatchLog(ex.ToString, "btnLoad_Click()")
        End Try

    End Sub

    Private Sub HostOffline() Handles FrmSecs.E_HostOffline
        Send_S1F15_OfflineRequest()
    End Sub

    Private Sub GoOnline() Handles FrmSecs.E_GoOnline
        Send_S1F17_OnlineRequest()
    End Sub

    Private Sub S2F13(ByVal UList As List(Of UInt32)) Handles FrmSecs.E_S2F13
        Send_S2F13(UList)
    End Sub



    Private Sub S5F3(ByVal Enable As Boolean, Optional ByVal ALID As UInteger = Nothing)

        Send_S5F3(Enable, ALID)
    End Sub

    Private Sub S10F3(ByVal ID As Integer, ByVal msg As String) Handles FrmSecs.E_S10F3
        Send_S10F3(CByte(ID), msg)
    End Sub

    'Private m_SlbS As UpdateTextDelegate = New UpdateTextDelegate(AddressOf SlbStatusFrmSecs)
    'Private Sub SlbStatusFrmSecs(ByVal informationText As String)
    '    If Me.InvokeRequired Then
    '        'http://kristofverbiest.blogspot.com/2007/02/avoid-invoke-prefer-begininvoke.html
    '        Me.BeginInvoke(m_SlbS, informationText)
    '        Exit Sub
    '    End If
    '    FrmSecs.sblStatus.Text = informationText
    'End Sub



#End Region

#Region "TCPClientSim"
    Private Sub TCPClient() Handles MainControlfrm.TCPClientClick
        If Not My.Settings.CsProtocol_Enable Then
            Exit Sub
        End If

        If FrmTCPClient Is Nothing Then
            FrmTCPClient = New TcpIpClientTest
            GoTo show
        End If
        If FrmTCPClient.IsDisposed Then
            FrmTCPClient = New TcpIpClientTest
            GoTo show
        End If


        If FrmProduct IsNot Nothing Then
            If FrmProduct.IsDisposed Then
                GoTo Loop1
            End If
            TileVerticalToolStripMenuItem_Click(Nothing, Nothing)
        End If
Loop1:
        FrmTCPClient.Activate()
        Exit Sub                   'If already open will not open again
show:
        FrmTCPClient.MdiParent = Me
        FrmTCPClient.Show()
        'If FrmProduct IsNot Nothing Then
        '    If FrmProduct.IsDisposed Then
        '        Exit Sub
        '    End If
        '    TileVerticalToolStripMenuItem_Click(Nothing, Nothing)
        'End If

        If OprData.FRMProductAlive Then
            TileVerticalToolStripMenuItem_Click(Nothing, Nothing)
        End If




    End Sub






#End Region

#Region "Custom Protocol"


    Private Sub Send_CsProtocol(ByVal Msg As String) Handles FrmProduct.E_CsProtocol_SendMsg, FrmTCPClient.E_TcpDataSent
        If Not My.Settings.CsProtocol_Enable Then
            Exit Sub
        End If
        CstProtocol.btnSend_Click(Msg)
        Commlog("Send : " & Msg, My.Application.Info.DirectoryPath & "\LOG\Comm.log")

    End Sub
    Private Sub CmdXmlData(ByVal data As String) Handles CstProtocol.RcvData

        'data = data.Replace(, "")
        If OprData.FRMProductAlive Then
            FrmProduct.RcvManage(data)
        End If

        If OprData.FRMTcpIpClientTestAlive Then   'For Sim
            FrmTCPClient.CmdXmlData(data)
        End If
        Commlog("Recv : " & data, My.Application.Info.DirectoryPath & "\LOG\Comm.log")
        data = ""
    End Sub

    Private Sub LSCheck(ByVal EQNo As String, ByVal LotNo As String, ByVal StartTime As Date, ByVal OPID As String, Optional ByVal StartMode As RunModeType = RunModeType.Normal) Handles FrmProduct.E_LSCheck   '161219 \783 TDC Add RunMode and Obj
        LotSet(EQNo, LotNo, StartTime, OPID, StartMode)
    End Sub

    Private Sub LECheck(ByVal EQNo As String, ByVal LotNo As String, ByVal EndTime As Date, ByVal GoodPcs As Integer, ByVal NgPcs As Integer, ByVal OPID As String, Optional ByVal EndMode As EndModeType = EndModeType.Normal) Handles FrmProduct.E_LECheck
        LotEnd(EQNo, LotNo, EndTime, GoodPcs, NgPcs, OPID, EndMode)
    End Sub

    Private Sub ifemConect(ByVal status As String) Handles CstProtocol.TcpStatusConnect
        OprData.CSConnect = status
        ifemConect()
    End Sub
    Private Sub ifemConect()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf ifemConect))
        Else
            slMessage.Text = My.Settings.EquipmentNo & " Connection Staus : " & OprData.CSConnect & Format(Now, " |HH:mm:ss.fff")
            If Not OprData.FRMProductAlive Then
                GoTo NextS
            End If
            If OprData.CSConnect = "Disconnect" Then
                FrmProduct.BackColor = Color.Red
                Reconnect.Enabled = True              '160715 \783 Reconnect CS Protocol
            ElseIf OprData.CSConnect = "Connected" Then
                FrmProduct.BackColor = Color.WhiteSmoke
                Reconnect.Enabled = False              '160715 \783 Reconnect CS Protocol
            End If
NextS:
            If OprData.FRMTcpIpClientTestAlive Then
                FrmTCPClient.txtStatus.Text = OprData.CSConnect
            End If

        End If
    End Sub


    Private Sub Reconnect_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Reconnect.Tick   '160715 \783 Reconnect CS Protocol


        If My.Settings.SECS_Enable Then     'If SecsEnable  Cannot reconnect
            Exit Sub
        End If
        If Not My.Settings.CsProtocol_Enable Then    'Enable Check
            Exit Sub
        End If
        If Not (OprData.CSConnect = "Disconnect") Then  ''Or CommuniationState Like "NOT COMMUNICATING") Then  '160627 EqConnect revise
            Exit Sub
        End If
        EqConnect()
    End Sub


#End Region


End Class
