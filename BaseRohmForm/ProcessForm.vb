Imports System.Threading
Imports System.ComponentModel
Imports System.IO
Imports Rohm.Apcs.Tdc
Imports XtraLibrary.SecsGem
Imports MapConverterForCanon
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Data.SqlClient
Imports System.Xml.Serialization
Imports Rohm.Ems
Imports CellController.ServiceReference1


Public Class ProcessForm
#Region "Commomn Define"


    Event E_MakeAlarmCellCon(ByVal AlarmMessage As String, ByVal Location As String, ByVal Status As String, ByVal AlarmID As String)
    Event E_Update_dgvProductionInfo1(ByVal _CarrierID As String, ByVal LotID As String, ByVal Package As String, ByVal Device As String, ByVal REMARK As String, ByVal StartTime As String)
    Event E_Update_dgvProductionInfoEnd(ByVal _UnloadCarrierID As String, ByVal LotNo As String, ByVal Count As String, ByVal Remark As String)
    Event E_Update_dgvProductionDetail(ByVal itemID As String, ByVal type As String, ByVal action As String, ByVal location As String)
    Event E_AlarmTable(ByVal AlarmALCD As Boolean, ByVal AlarmALID As String, ByVal AlarmALTX As String, ByVal AlarmType As String)
    Event E_FormFill()
    Event E_ProductionTableCall(ByVal TabPage As String)
    Event E_ConsoleShow()
    Event E_CsProtocol_SendMsg(ByVal msg As String)
    'Event E_QRReadSuccess()
    Event E_SlInfo(ByVal msg As String)
    Event E_EqConnect()
    Event E_QRReadOPIDSuccess()
    Event E_LRCheck(ByVal EqNo As String, ByVal LotNo As String)
    Event E_LSCheck(ByVal EQNo As String, ByVal LotNo As String, ByVal StartTime As Date, ByVal OPID As String, ByVal StartMode As RunModeType)
    Event E_LECheck(ByVal EQNo As String, ByVal LotNo As String, ByVal EndTime As Date, ByVal GoodPcs As Integer, ByVal NgPcs As Integer, ByVal OPID As String, ByVal EndMode As EndModeType)
    Event E_HostSend(ByVal msg As SecsMessageBase)
    Event E_TransactionDataSave(ByVal QrData As String)
    Event E_HostReply(ByVal Pri As SecsMessageBase, ByVal Secn As SecsMessageBase)
    Event E_HelperCall(ByVal Tabpage As String)
    Dim m_flagLotEnd As Boolean = False
    Dim WaferMap As New MapData
    Dim m_MapUpload As MapUploadParameter
    Dim m_statePPSelect As Boolean = False
    Private Property m_SelfData As Object

    Dim _WhenPreeSetUpButton As Boolean = False
    Dim _WhenInputDataAlready As Boolean = False
    Dim m_lock As Boolean = False
    Dim m_release As Boolean = False
    ' Dim m_lotplan As Boolean = False
    Dim _DefineReportLinkEventEnable As Boolean = False
    Dim m_ppselect As Boolean = False
    Dim _reconSec As Integer = 0
    Dim c_Buffer As String
    Dim c_PreviousCommand As String
    Dim c_MapUploadXmlPath As String = My.Application.Info.DirectoryPath & "\CurrentWafer.xml"
    Private c_Resize As Resize = New Resize()
    'Explain Event  ----------------------------------------------------------------------------------------------------------------------

    'Event E_MakeAlarmCellCon                   : Display Alarm of processing(Code) in table
    'Event E_Update_dgvProductionInfo1          : Production start carrier information table 
    'Event E_Update_dgvProductionInfoEnd        : Production end carrier information table
    'Event E_Update_dgvProductionDetail         : Production detail table        
    'Event E_AlarmTable                         : Secs/Gem or Equipment Alarm table
    'Event E_FormFill                           : Maximize Production form
    'Event E_ProductionTableCall                : Table form call
    'Event E_ConsoleShow                        : Communication log console (Use in Secs/Gem Only)                          :
    'Event E_CsProtocol_SendMsg                 : Custom protocol send
    'Event E_QRReadSuccess                      : QR read data send (Autherize check), must set parameter 'OprData.QrData, OprData.OPID' first
    'Event E_SlInfo                             : Show message at State label in MDIParent form
    'Event E_EqConnect                          : Connect to equipment
    'Event E_QRReadOPIDSuccess                  : OPID QR data
    'Event E_LRCheck                            : LR Check TDC Auto reply to Sub LR_Reply()
    'Event E_LSCheck                            : LS Check TDC Auto reply to Sub LS_Reply()
    'Event E_LECheck                            : LE Check TDC Auto reply to Sub LE_Reply()
    'Event E_HostSend                           : Secs/Gem protcol send
    'Event E_TransactionDataSave                : Save QR to Transaction data

    '----------------------------------------------------------------------------------------------------------------------------------------



    'Dim PathXmlObj As String = "D:\RohmSystem\rCellcon\" & My.Settings.MCType
    'Dim BackUpObj As String = "D:\RohmSystem\rCellcon\" & My.Settings.MCType & "\BackUpObj"
    'Dim BackUpObjOld As String = "D:\RohmSystem\rCellcon\" & My.Settings.MCType & "\BackUpObjOld"


    Private Delegate Sub S6F11Delegate(ByVal Obj As S6F11)
    Private Delegate Sub S5F1Delegate(ByVal Obj As S5F1)
    Private Delegate Sub S2F42Delegate(ByVal CMD As String, ByVal Reply As S2F42)
    Private Delegate Sub SxFxxDelegate(ByVal e As SecondarySecsMessageEventArgs)
    Private Delegate Sub SxFxxPriDelegate(ByVal state As Object)
    Private connection As SqlConnection = Nothing
    Private command As SqlCommand = Nothing
    Private dataToWatch As DataSet = Nothing
    Private Const tableName1 As String = "DBData" '"DailyProcessOperationRate"

    'Public warning As New frmWarning
    Dim m_Equipment As New Equipment
    Dim WaferMapData As New List(Of String)
    Public m_QRReadAlarm As String
    Dim AlarmHashT As New Hashtable
    Dim m_PreformAlarm As String
    Dim m_frmRecipe As frmRecirpeChange
    Dim LotEndFlag As Boolean
    Dim m_prepareSetup As Boolean = False
    Dim c_TypeChangeMode As Boolean = False
    Dim c_RepeatInput As Boolean = False
    Dim c_FrmFinal As frmFinalInspection
    Dim c_LotSetUp As Boolean = False
    Structure AlarmKeys
        Dim AlarmID As Integer
        Dim AlarmMessage As String
        Dim AlarmNo As Integer
        Dim AlarmSet As Boolean
    End Structure

    Public m_EmsClient As EmsServiceClient = New EmsServiceClient("DB", "http://webserv.thematrix.net:7777/EmsService")

#End Region



#Region "Form main zone"



    Private Sub ProcessForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        OprData.FRMProductAlive = False
        m_EmsClient.Stop()
        WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
    End Sub

    Private Sub ProcessForm_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim myPen As Pen
        myPen = New Pen(Color.RoyalBlue, 17)
        e.Graphics.DrawLine(myPen, 0, 10, Me.Width, 10)
        myPen = New Pen(Color.MidnightBlue, 1)
        e.Graphics.DrawLine(myPen, 0, 19, Me.Width, 19)
        myPen = New Pen(Color.PowderBlue, 33)
        e.Graphics.DrawLine(myPen, 0, 110, Me.Width, 110)
        'myPen = New Pen(Color.CadetBlue, 1)
        'e.Graphics.DrawLine(myPen, 1, 122, Me.Width, 122)

    End Sub

    Protected Overrides ReadOnly Property CreateParams() As CreateParams   'Disable Close(x) Button
        Get
            Dim param As CreateParams = MyBase.CreateParams
            param.ClassStyle = param.ClassStyle Or &H200
            Return param
        End Get
    End Property

    ' Command For Clear all data table

    Private Sub FrmTableDataClear()                 '160712 \783 PrdTable Clear
        RaiseEvent E_AlarmTable(False, _PrdTableClear, "", "")
        RaiseEvent E_MakeAlarmCellCon(_PrdTableClear, "", "", "")
        RaiseEvent E_Update_dgvProductionDetail(_PrdTableClear, "", "", "")
        RaiseEvent E_Update_dgvProductionInfo1(_PrdTableClear, "", "", "", "", "")
    End Sub



    Private Sub AlarmTable(ByVal AlarmALCD As Boolean, ByVal AlarmALID As String, ByVal AlarmALTX As String, Optional ByVal AlarmType As String = "0")
        RaiseEvent E_AlarmTable(AlarmALCD, AlarmALID, AlarmALTX, AlarmType)
    End Sub
    Private Sub MakeAlarmCellCon(ByVal AlarmMessage As String, Optional ByVal Location As String = "", Optional ByVal Status As String = "", Optional ByVal AlarmID As String = "")
        RaiseEvent E_MakeAlarmCellCon(AlarmMessage, Location, Status, AlarmID)
    End Sub
    Private Sub Update_dgvProductionInfo1(ByVal _CarrierID As String, ByVal LotID As String, ByVal Package As String, ByVal Device As String, Optional ByVal Remark As String = "", Optional ByVal StartTime As String = "")
        RaiseEvent E_Update_dgvProductionInfo1(_CarrierID, LotID, Package, Device, Remark, StartTime)
    End Sub
    Private Sub Update_dgvProductionDetail(ByVal itemID As String, ByVal type As String, ByVal action As String, Optional ByVal location As String = "")
        RaiseEvent E_Update_dgvProductionDetail(itemID, type, action, location)
    End Sub
    Private Sub Update_dgvProductionInfoEnd(ByVal _UnloadCarrierID As String, ByVal LotNo As String, Optional ByVal Count As String = "", Optional ByVal Remark As String = "")
        RaiseEvent E_Update_dgvProductionInfoEnd(_UnloadCarrierID, LotNo, Count, Remark)
    End Sub

    'Private Sub EqConnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EqConnectToolStripMenuItem.Click

    '    If Not (My.Settings.CsProtocol_Enable Or My.Settings.SECS_Enable) Then    'Enable Check
    '        Exit Sub
    '    End If
    '    If Not (OprData.CSConnect = "Disconnect") Then  ''Or CommuniationState Like "NOT COMMUNICATING") Then  '160627 EqConnect revise
    '        Exit Sub
    '    End If

    '    RaiseEvent E_EqConnect()

    'End Sub

    Private Sub pbxLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbxLogo.Click
        RaiseEvent E_FormFill()
    End Sub



    Private Sub ProcessForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim TypeOfLanguage = New System.Globalization.CultureInfo("en")                        'Change keyboard to Eng keyboard
        InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(TypeOfLanguage)

        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        lbProcess.Text = "PROCESS : " & My.Settings.ProcessName
        OprData.FRMProductAlive = True

        Dim permission As New AuthenticationUser.AuthenUser
        If OprData.CSConnect = "Disconnect" And My.Settings.CsProtocol_Enable Then
            Me.BackColor = Color.Red
        End If
        If Not CommuniationState Like "COMMUNICATING*" And My.Settings.SECS_Enable Then
            Me.BackColor = Color.Red
        End If

        ' lbMcNo.Text = My.Settings.EquipmentNo

        If My.Settings.SECS_Enable Then
            If m_DefinedReportDic.Count = 0 Then
                MsgBox("Process Deny : Define Report 'm_DefinedReportDic' is empty' Please Download Report")
                Me.BeginInvoke(New MethodInvoker(AddressOf Me.Close))   'Close form in the form load event
            End If

            CountGoodDiesTimer.Enabled = True
            lbSECS.Visible = True

            Try
                RequestSVID_CurrentState()
            Catch ex As Exception

            End Try

        ElseIf My.Settings.SerialPortEnable = True Then
            lbSECS.Visible = True
            lbSECS.Text = "RS232 Communication"
            lbSerialPort.Text = My.Settings.SerialPortNo
            CountGoodDiesTimer.Enabled = False
            Try
                SerialPort1.PortName = My.Settings.SerialPortNo
                If SerialPort1.IsOpen Then
                    SerialPort1.Close()
                End If
                SerialPort1.Open()
            Catch ex As Exception
                SerialPort1.Close()
            End Try

            btLotStart1024.Visible = False
            btSet1024.Visible = False

            btFrame1024.Visible = False
            btPreform1024.Visible = False

        Else
            CountGoodDiesTimer.Enabled = False
        End If

        If Not CommuniationState Like "COMMUNICATING*" And My.Settings.SECS_Enable Then
            Me.BackColor = Color.Red
        End If

        Try
            If My.Settings.UserAuthenOP = "NOUSE" Then
                'Exit Sub
            Else
                If permission.CheckMachineAutomotive(My.Settings.ProcessName, My.Settings.EquipmentNo) Then
                    pbxAutoM.Visible = True
                End If
            End If

            'LoadAlarmMessage
            If Not My.Computer.Network.IsAvailable Then                'unplug check
                MsgBox("PC Nework point unplug")
                GoTo DBxServerEndLoop
            End If
            If Not My.Computer.Network.Ping(_ipDbxUser) Then            'Can Pink if Computer Connect only
                MsgBox("การเชื่อมต่อกับฐานข้อมูล DB.X ล้มเหลวไม่สามารถดำเนินการต่อได้")
                GoTo DBxServerEndLoop
            End If
            pbxLogo.BringToFront()                  'Display revise 160627 \783 


DBxServerEndLoop:

        Catch ex As Exception    'Net Work Error

        End Try

        If My.Settings.WaferMappingUse = False Then
            lbCheckState.Text = "No checking"
        End If

        LoadAlarmInfoTable()
        CellConTag = ReadFromXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml")
        UpdateDispaly()
        UpdateDisplayMaterial()
        PreformExp_Tick(Nothing, EventArgs.Empty)
        LoadTorinokoshiTable()

        If My.Settings.SECS_Enable = True Then
            btSet1024.Visible = True
        Else
            btSet1024.Visible = False
        End If

        Try
            Dim reg As EmsMachineRegisterInfo = New EmsMachineRegisterInfo(My.Settings.EquipmentNo, "DB-" & My.Settings.EquipmentNo, "DB", My.Settings.MCType, CellConTag.LotID, CellConTag.TotalGood, CellConTag.TotalNG, 0, 0, 0)
            m_EmsClient.Register(reg)
        Catch ex As Exception
        End Try


    End Sub



    Private Sub ProductTableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent E_ProductionTableCall("tbAlarmCellCon")
    End Sub

    Private Sub SecsConsoleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent E_ConsoleShow()
    End Sub


    Private Sub BMRequestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BMRequestToolStripMenuItem.Click
        Dim tmpStr As String

        tmpStr = "MCNo=" & My.Settings.EquipmentNo
        tmpStr = tmpStr & "&LotNo=" & CellConTag.LotID
        If CellConTag.LotStartTime <> Nothing Then 'AndAlso lbEndTime.Text = "" Then
            tmpStr = tmpStr & "&MCStatus=Running"
        Else
            tmpStr = tmpStr & "&MCStatus=Stop"
        End If

        tmpStr = tmpStr & "&AlarmNo="
        tmpStr = tmpStr & "&AlarmName="

        Call Shell("C:\Program Files\Internet Explorer\iexplore.exe http://webserv.thematrix.net/LsiPETE/LSI_Prog/Maintenance/MainloginPD.asp?" & tmpStr, vbNormalFocus)
        Process.Start("C:\WINDOWS\system32\osk.exe")
    End Sub

    Private Sub PMRepairToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PMRepairToolStripMenuItem.Click
        Dim MCNo As String = My.Settings.EquipmentNo
        Call Shell("C:\Program Files\Internet Explorer\iexplore.exe http://webserv.thematrix.net/LsiPETE/LSI_Prog/Maintenance/MainPMlogin.asp?" & "MCNo=" & MCNo, vbNormalFocus)
        Process.Start("C:\WINDOWS\system32\osk.exe")
    End Sub


    Private Sub ByAutoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ByAutoToolStripMenuItem.Click  '161019 \783
        Try
            Dim requestUrl As String             'Call Andon by pass parameter 161029 \783
            requestUrl = String.Format("http://webserv.thematrix.net/andontmn/Client/Default.aspx?p={0}&mc={1}&lot={2}&pkg={3}&dv={4}&line={5}&op={6}",
                                        My.Settings.ProcessName, My.Settings.EquipmentNo, CellConTag.LotID, CellConTag.Package, CellConTag.DeviceName, "", CellConTag.OPID)
            Call Shell("C:\Program Files\Internet Explorer\iexplore.exe " & requestUrl, AppWinStyle.NormalFocus)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ByManualToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ByManualToolStripMenuItem.Click
        Try
            Call Shell("C:\Program Files\Internet Explorer\iexplore.exe http://webserv/andontmn", AppWinStyle.NormalFocus) 'Web andon for manual M/C     'Maual input
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub APCSStaffToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles APCSStaffToolStripMenuItem.Click
        Call Shell("C:\Program Files\Internet Explorer\iexplore.exe http://webserv.thematrix.net/ApcsStaff", AppWinStyle.NormalFocus)

    End Sub

    Private Sub WorkRecordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WorkRecordToolStripMenuItem.Click
        Try
            Call Shell("C:\Program Files\Internet Explorer\iexplore.exe http://webserv/ERECORD/", AppWinStyle.NormalFocus)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



#End Region


#Region "Xml File Manage (ObjClass serailize in folder EquipmentObj)"

    '1. 'WrXml()   For Write to Serailize file to path PathXmlObj
    '    RdXml()   For Read to Serailize file
    '2. After lotend 




    ' -------------------Keep all file of  PathXmlObj in LotNo and move to BackUpObj Directory
    Private Sub MakeLotFolderToBackUp(ByVal LotNo As String)
        Dim LotDirName As String = BackUpObj & "\" & LotNo & "_" & Format(Now, "yyyyMMddTHHmmss")
        System.IO.Directory.CreateDirectory(LotDirName)
        For Each fi As FileInfo In New DirectoryInfo(CellconObjPath).GetFiles
            File.Move(fi.FullName, LotDirName & "\" & fi.Name)
        Next
        BackUpLotClean()
    End Sub

    ''' Clean log by limit folder size  delete form old to new defualt 5M
    '''

    Private Sub CleanLog(Optional ByVal DirSizeLimit_Mbyte As Integer = 200)   '161212 \783 Add clean log

        Try
            Dim Mlmt As Integer = DirSizeLimit_Mbyte * 1000000
            Dim orderedFiles = New System.IO.DirectoryInfo(DIR_LOG).GetFiles().OrderByDescending(Function(x) x.CreationTime).ToArray   'Order by create data(Not Modify) 
            Dim index As Integer = 0
            Dim DirSize As Integer = 0

            For i = 0 To orderedFiles.Length - 1
                DirSize = CInt(DirSize + orderedFiles(i).Length)  'File Over Size count
                index = +1
                If Mlmt < DirSize Then
                    index = i
                    Exit For
                End If
            Next
            If index >= orderedFiles.Length - 1 Then
                Exit Sub
            End If

            For i = index To orderedFiles.Length - 1  'Delete from index that over size
                File.Delete(orderedFiles(i).FullName)
            Next


        Catch ex As Exception
            SaveCatchLog(ex.ToString, "CleanLog()")
        End Try

    End Sub

    '--------------------- Cleanning Directory in  backup 
    Private WithEvents BackUp As New BackgroundWorker

    Friend Sub BackUpLotClean()
        BackUp.RunWorkerAsync()
    End Sub
    Private Sub DoBackup(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackUp.DoWork
        Try

            Dim Dirs = Directory.GetDirectories(BackUpObj)
            If Dirs.Count > 100 Then   'Store 100 Folders over then move to BackUpOld 100 files
                For Each DirSo In Dirs
                    Dim DirInfo As New System.IO.DirectoryInfo(DirSo)
                    Directory.Move(DirSo, Path.Combine(BackUpObjOld, DirInfo.Name))
                Next
            End If
            Dim OldDirs = Directory.GetDirectories(BackUpObjOld)
            If OldDirs.Count > 100 Then           'if over 100  Folders del 10 Folders of BackUpOld
                Dim DirDes = From l In OldDirs Order By Directory.GetCreationTime(l) Ascending    'SortFile by Modify time
                For i = 0 To 10
                    Directory.Delete(DirDes(i), True)             'Del 10 Folders
                Next

            End If
            e.Result = ""
        Catch ex As Exception
            e.Result = ex.ToString
        End Try

    End Sub
    Private Sub Backup_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackUp.RunWorkerCompleted
        Dim result As String = CStr(e.Result)
        SaveCatchLog(result, "DoBackUp()")
    End Sub



#End Region

#Region "SecsComm"

    'Remote Command change in S2F41 Class for customize to each CPVAL format.------------------160705 \783 Addition Remote example
    'Example for CPVAL is string type
    Private Sub SendRemoteCommand(ByVal RCmd As String, Optional ByVal CmdPName As String = "", Optional ByVal CmdPVal As String = "")
        Dim cmd As S2F41 = New S2F41()
        cmd.RemoteCommand = RCmd
        If CmdPName <> "" And CmdPVal <> "" Then
            cmd.AddVariable(CmdPName, CmdPVal)
        End If
        RaiseEvent E_HostSend(cmd)
    End Sub


    '---------------------------------------------------------------------------------------------------------------------


    Private m_SxFxx As SxFxxDelegate = New SxFxxDelegate(AddressOf m_Host_ReceivedSecondaryMessage)

    Friend Sub m_Host_ReceivedSecondaryMessage(ByVal e As SecondarySecsMessageEventArgs)
        If Me.InvokeRequired Then
            'http://kristofverbiest.blogspot.com/2007/02/avoid-invoke-prefer-begininvoke.html
            Me.BeginInvoke(m_SxFxx, e)
            Exit Sub
        End If

        Dim priMsg As SecsMessageBase = e.Primary
        Dim sndMsg As SecsMessageBase = e.Secondary

        Select Case sndMsg.Stream
            Case 1
                Select Case sndMsg.Function
                    Case 4 'SVID REQ
                        Dim priMesData As S1F3 = CType(priMsg, S1F3)
                        Dim sndMesData As S1F4 = CType(sndMsg, S1F4)
                        If priMesData.sv.Count = 1 Then
                            Select Case priMesData.sv(0)
                                Case "2009"  'Recipe Name 2100HS ,2009SSI
                                    If m_frmRecipe IsNot Nothing Then
                                        If m_frmRecipe.Visible = True Then
                                            m_frmRecipe.Visible = False
                                        End If
                                    End If

                                    If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then
                                        Dim ReplyRecipefromEq As String = sndMesData.SV(0)
                                        Dim ReplyRecipefromCellCon As String = CellConTag.Recipe & ".dbrcp"
                                        If ReplyRecipefromCellCon <> ReplyRecipefromEq Then
                                            m_frmRecipe = New frmRecirpeChange(Me)
                                            m_frmRecipe.m_NewRecipe = ReplyRecipefromCellCon
                                            m_frmRecipe.m_CurrentRecipe = ReplyRecipefromEq

                                            m_frmRecipe.Show()
                                        Else
                                            ReleaseMachine()

                                            m_frmWarningDialog("Set up เรียบร้อย", False, 60000)

                                        End If
                                    End If
                                Case "2031" 'Status change2100HS
                                    Dim CurrentState As Integer = CInt(sndMesData.SV(0))
                                    m_Equipment.EQStatus = CType(CurrentState, EquipmentStateEsec)
                                    lbEqState.Text = m_Equipment.EQStatus.ToString

                                    Select Case CurrentState
                                        Case EquipmentStateEsec.Executing
                                            CellConTag.StateCellCon = CellConState.LotStart
                                            If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then
                                                LotStartup()
                                                lbStart1024.Text = Format(CellConTag.LotStartTime, "yyyy/MM/dd HH:mm:ss")
                                                m_frmWarningDialog("กรุณาทำ First Insp.", False, 60000)
                                            ElseIf CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                                                Try
                                                    CellConTag.StateCellCon = CellConState.LotStart
                                                    m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Running", TmeCategory.NetOperationTime)
                                                Catch ex As Exception
                                                End Try
                                            End If
                                        Case EquipmentStateEsec.MaterialNotReady
                                            CellConTag.StateCellCon = CellConState.LotAlarm
                                            If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                                                Try
                                                    m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Alarm", TmeCategory.ChokotieLoss)
                                                Catch ex As Exception
                                                End Try
                                            End If
                                        Case EquipmentStateEsec.StoppedReady
                                            CellConTag.StateCellCon = CellConState.LotStop
                                            If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                                                Try
                                                    m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Stop", TmeCategory.StopLoss)
                                                Catch ex As Exception
                                                End Try
                                            End If
                                    End Select

                                Case "111634" 'Status change  2009SSI
                                    Dim CurrentState As Integer = CInt(sndMesData.SV(0))
                                    m_Equipment.EQStatus = CType(CurrentState, EquipmentStateEsec)
                                    lbEqState.Text = m_Equipment.EQStatus.ToString

                                    Select Case CurrentState
                                        Case EquipmentStateEsec.MC_Executing
                                            CellConTag.StateCellCon = CellConState.LotStart
                                            If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then
                                                LotStartup()
                                                lbStart1024.Text = Format(CellConTag.LotStartTime, "yyyy/MM/dd HH:mm:ss")
                                                m_frmWarningDialog("กรุณาทำ First Insp.", False, 60000)
                                            ElseIf CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                                                Try
                                                    CellConTag.StateCellCon = CellConState.LotStart
                                                    m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Running", TmeCategory.NetOperationTime)
                                                Catch ex As Exception
                                                End Try
                                            End If
                                        Case EquipmentStateEsec.NotReady
                                            CellConTag.StateCellCon = CellConState.LotAlarm
                                            If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                                                Try
                                                    m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Alarm", TmeCategory.ChokotieLoss)
                                                Catch ex As Exception
                                                End Try
                                            End If
                                        Case EquipmentStateEsec.Ready
                                            CellConTag.StateCellCon = CellConState.LotStop
                                            If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                                                Try
                                                    m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Stop", TmeCategory.StopLoss)
                                                Catch ex As Exception
                                                End Try
                                            End If

                                    End Select

                                Case "151126269" 'GoodDies 2100HS

                                    If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing Then
                                        Dim GoodDies As Integer = CInt(sndMesData.SV(0))
                                        CellConTag.TotalGood = GoodDies
                                        lbGood1024.Text = CStr(GoodDies)
                                        WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag

                                        Try
                                            m_EmsClient.SetOutput(My.Settings.EquipmentNo, CellConTag.TotalGood, CellConTag.TotalNG)
                                        Catch ex As Exception
                                        End Try
                                    End If
                                Case "110316" 'GoodDies 2009SSI
                                    If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing Then
                                        Dim GoodDies As Integer = CInt(sndMesData.SV(0))
                                        CellConTag.TotalGood = GoodDies
                                        lbGood1024.Text = CStr(GoodDies)
                                        WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag

                                        Try
                                            m_EmsClient.SetOutput(My.Settings.EquipmentNo, CellConTag.TotalGood, CellConTag.TotalNG)
                                        Catch ex As Exception
                                        End Try
                                    End If


                                    '$$$$$$$$$$$$$$$$$$################################## Canon D10R ########################################################################
                                Case "1002" 'Status change  Canon D10R
                                    Dim CurrentState As Integer = CInt(sndMesData.SV(0))
                                    m_Equipment.EQStatusCanon = CType(CurrentState, EquipmentStateCanon)
                                    lbEqState.Text = m_Equipment.EQStatusCanon.ToString


                                    Select Case CurrentState
                                        Case EquipmentStateCanon.EXECUTING
                                            If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then
                                                LotStartup()
                                                lbStart1024.Text = Format(CellConTag.LotStartTime, "yyyy/MM/dd HH:mm:ss")
                                                m_frmWarningDialog("กรุณาทำ First Insp.", False, 60000)
                                            ElseIf CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then 'Running+
                                                Try
                                                    CellConTag.StateCellCon = CellConState.LotStart
                                                    m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Running", TmeCategory.NetOperationTime)
                                                Catch ex As Exception
                                                End Try
                                            End If
                                        Case EquipmentStateCanon.TROUBLE 'ALARM
                                            If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                                                Try
                                                    CellConTag.StateCellCon = CellConState.LotAlarm
                                                    m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Alarm", TmeCategory.ChokotieLoss)
                                                Catch ex As Exception
                                                End Try
                                            End If
                                        Case EquipmentStateCanon.IDEL 'STOP
                                            If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                                                Try
                                                    CellConTag.StateCellCon = CellConState.LotStop
                                                    m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Stop", TmeCategory.StopLoss)
                                                Catch ex As Exception
                                                End Try
                                            End If
                                            If c_LotSetUp = True Then
                                                SetupLot()
                                            End If
                                            c_LotSetUp = False
                                    End Select
                                Case "112" 'EquipmentStateCanon.EXECUTING
                                    If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing Then
                                        Dim GoodDies As Integer = CInt(sndMesData.SV(0))
                                        CellConTag.TotalGood = GoodDies
                                        lbGood1024.Text = CStr(GoodDies)
                                        WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                                        Try
                                            m_EmsClient.SetOutput(My.Settings.EquipmentNo, CellConTag.TotalGood, CellConTag.TotalNG)
                                        Catch ex As Exception
                                        End Try
                                    End If
                                Case "1016" 'Recipe Check Canon

                                    If m_frmRecipe IsNot Nothing Then
                                        If m_frmRecipe.Visible = True Then
                                            m_frmRecipe.Visible = False
                                        End If
                                    End If

                                    If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then
                                        Dim ReplyRecipefromEq As String = sndMesData.SV(0) 'X699(660X790),X693_(700X850)
                                        Dim ReplyRecipefromCellCon As String = CellConTag.Recipe 'Working slip
                                        If ReplyRecipefromCellCon <> ReplyRecipefromEq Then
                                            m_frmRecipe = New frmRecirpeChange(Me)
                                            m_frmRecipe.m_NewRecipe = ReplyRecipefromCellCon
                                            m_frmRecipe.m_CurrentRecipe = ReplyRecipefromEq

                                            m_frmRecipe.Show()
                                        Else
                                            'm_retry = True
                                            RemoteCMD_Remote()
                                        End If
                                    End If
                            End Select
                        End If

                End Select
            Case 2
                Select Case sndMsg.Function    '160903 \783 add S2F42
                    Case 16

                        Dim reply As New S2F16
                        reply = DirectCast(sndMsg, S2F16)

                        Select Case My.Settings.MCType
                            Case "Canon-D10R"

                                If _WhenPreeSetUpButton = True Then
                                    _WhenPreeSetUpButton = False
                                    If reply.EAC = EAC.OK Then
                                        RemoteCMD_Release()
                                        m_frmWarningDialog("Set up เรียบร้อย", False, 30000)
                                    Else
                                        m_frmWarningDialog("Set up ไม่สำเร็จ เครื่องอาจจะไม่พร้อมใช้งาน กรุณาลองใหม่อีกครั้ง", False, 30000)
                                        RemoteCMD_Local()
                                    End If
                                End If

                                If _WhenInputDataAlready = True Then
                                    _WhenInputDataAlready = False
                                    If reply.EAC = EAC.OK Then
                                        RemoteCMD_Release()
                                        m_frmWarningDialog("Set up เรียบร้อย", False, 30000)
                                    Else
                                        m_frmWarningDialog("Set up ไม่สำเร็จ เครื่องอาจจะไม่พร้อมใช้งาน กรุณาลองใหม่อีกครั้ง", False, 30000)
                                        RemoteCMD_Local()
                                    End If
                                End If

                            Case "2100HS"
                                If reply.EAC = EAC.OK Then
                                    m_frmWarningDialog("Set up เรียบร้อย", False, 30000)
                                Else
                                    m_frmWarningDialog("Set up ไม่สำเร็จ ที่ MC กรุณาเปลี่ยนเป็นหน้าฟอร์ม Production แล้วกด Set up อีกครั้ง", False, 30000)
                                End If

                            Case "2009SSI"
                                If reply.EAC = EAC.OK Then
                                    m_frmWarningDialog("Set up เรียบร้อย", False, 30000)
                                Else
                                    m_frmWarningDialog("Set up ไม่สำเร็จ ที่ MC กรุณาเปลี่ยนเป็นหน้าฟอร์ม Production แล้วกด Set up อีกครั้ง", False, 30000)
                                End If

                        End Select

                    Case 42
                        Dim reply As New S2F42
                        reply = DirectCast(sndMsg, S2F42)
                        Dim remote As New S2F41
                        remote = DirectCast(priMsg, S2F41)

                        Select Case remote.RemoteCommand

                            Case "LOT-CREATE" 'ESEC 2100HS
                                If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing Then

                                    If m_frmRecipe IsNot Nothing Then
                                        m_frmRecipe.Visible = False
                                    End If

                                    If reply.HCACK = HCACK.RejectedAlreadyInDesired Then
                                        m_frmWarningDialog("LotNo นี้ไม่สามารถ Create Lotได้ มีข้อมูลซ้ำที่ MC " & vbCrLf & " กรุณาลบข้อมูลในระบบที่ MC", False)
                                    ElseIf reply.HCACK = HCACK.CannotDoNow Then
                                        m_frmWarningDialog("สถานะเครื่องตอนนี้ไม่พร้อมใช้งาน ต้องอยู่ในสถานะ Ready" & vbCrLf & "หรือไม่อยู่ที่หน้า Production ที่คอมพิวเตอร์ MC.  จะไม่สามารถใช้ได้", False)
                                    Else 'OK
                                        If My.Settings.AutoLoad = True Then  'Autoload
                                            RecipeCheck()
                                        Else
                                            ReleaseMachine()
                                            m_frmWarningDialog("Set up เรียบร้อย", False, 60000)
                                        End If
                                    End If
                                End If
                            Case "PP-SELECT"
                                If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing Then

                                    If My.Settings.MCType = "2100HS" Then
                                        m_statePPSelect = False
                                        If reply.HCACK = HCACK.RejectedAlreadyInDesired Then  'ESEC 2100HS
                                            m_frmWarningDialog("Recipe Err 05", False)
                                        ElseIf reply.HCACK = HCACK.CannotDoNow Then 'ESEC 2100HS
                                            m_frmWarningDialog("สถานะเครื่องตอนนี้ไม่พร้อมใช้งาน ต้องอยู่ในสถานะ Ready" & vbCrLf & "หรือไม่อยู่ที่หน้า Production ที่คอมพิวเตอร์ MC.  จะไม่สามารถใช้ได้", False)
                                        Else 'OK
                                            m_statePPSelect = True 'ESEC 2100HS
                                            m_frmWarningDialog("กำลังเปลี่ยน Recipe กรุณารอสักครู่", False)
                                        End If

                                    ElseIf My.Settings.MCType = "Canon-D10R" Then
                                        If reply.HCACK = HCACK.RejectedAlreadyInDesired Then
                                            m_frmWarningDialog("ไม่สามารถเปลี่ยน Recipe ได้กรุณาลองใหม่อีกครั้ง", False)
                                            RemoteCMD_Local()
                                        ElseIf reply.HCACK = HCACK.CannotDoNow Then
                                            m_frmWarningDialog("สถานะเครื่องตอนนี้ไม่พร้อมใช้งาน ต้องอยู่ในสถานะ Ready" & vbCrLf & "หรือไม่อยู่ที่หน้า Production ที่คอมพิวเตอร์ MC.  จะไม่สามารถใช้ได้", False)
                                            RemoteCMD_Local()
                                        Else 'OK
                                            m_ppselect = True
                                            m_frmWarningDialog("กำลังเปลี่ยน Recipe กรุณารอสักครู่", False)
                                        End If

                                    End If
                                End If
                            Case "REMOTE"

                                If m_lock = True Then  'Canon-D10R
                                    m_lock = False
                                    RemoteCMD_Lock()
                                ElseIf m_release = True Then  'Canon-D10R
                                    m_release = False
                                    RemoteCMD_Release()
                                ElseIf _WhenInputDataAlready = True And (reply.HCACK = HCACK.CannotDoNow) Then  'Canon-D10R 
                                    If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing Then
                                        RemoteCMD_LotPlan(CellConTag.OPID, CellConTag.LotID, CUInt(CellConTag.INPUTQty))
                                    End If
                                ElseIf _WhenPreeSetUpButton = True AndAlso (reply.HCACK = HCACK.OK Or reply.HCACK = HCACK.CannotDoNow) Then 'เมื่อกด Set up ส่ง Remote หลังจากนั้น MC ตอบ 00 หรือ 02 ให้ส่ง LotPlan
                                    If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing Then
                                        RemoteCMD_LotPlan(CellConTag.OPID, CellConTag.LotID, CUInt(CellConTag.INPUTQty))
                                    End If
                                End If

                            Case "LOCAL"
                                'Canon-D10R
                            Case "LOTPLAN"
                                If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing Then   'Canon-D10R

                                    If _WhenPreeSetUpButton = True Then
                                        If reply.HCACK = HCACK.OK Then 'Canon-D10R
                                            'หาจำนวน Frame ใน Lotนั้
                                            CellConTag.FrameCountTotal = CInt(Math.Ceiling(CellConTag.INPUTQty / CellConTag.PCSPerFrame))
                                            S2F15_SetInputQty(CUInt(CellConTag.FrameCountTotal)) 'Send Frame
                                            Exit Sub
                                        Else
                                            m_frmWarningDialog("Set up ไม่สำเร็จ กรุณาตรวจสอบเครื่องให้อยู่ในสถานะพร้อมรันและลองใหม่อีกครั้ง", False, 30000)
                                        End If
                                    End If

                                    If _WhenInputDataAlready = True Then
                                        If reply.HCACK = HCACK.OK Then 'Canon-D10R
                                            'หาจำนวน Frame ใน Lotนั้
                                            CellConTag.FrameCountTotal = CInt(Math.Ceiling(CellConTag.INPUTQty / CellConTag.PCSPerFrame))
                                            S2F15_SetInputQty(CUInt(CellConTag.FrameCountTotal)) 'Send Frame
                                        Else
                                            m_frmWarningDialog("Set up ไม่สำเร็จ กรุณาตรวจสอบเครื่องให้อยู่ในสถานะพร้อมรันและลองใหม่อีกครั้ง", False, 30000)
                                            'RemoteCMD_Local()
                                        End If
                                    End If

                                End If
                            Case "RELEASE"
                                'Thread.Sleep(4000)
                                RemoteCMD_Local()
                            Case "LOCK"

                                Thread.Sleep(4000)
                                RemoteCMD_Local()
                        End Select
                End Select
            Case 7
                Select Case sndMsg.Function

                    Case 2
                        Dim reply As S7F2 = DirectCast(sndMsg, S7F2)      '160727 RecipeBodyManage
                        If reply.PPGNT = PPGNT.Ok Then
                            Dim msg As New S7F3
                            'Dim bFile As Byte() = File.ReadAllBytes(RecipeDir & "\" & OprData.PPIDMange & ".bin")
                            Dim RecipeR As New PDE
                            RecipeR = ReadFromXmlPDE(RecipeDir & "\" & OprData.PPIDMange & ".xml") '160811 \783 recipe Program Develop Element

                            msg.Setparameter(OprData.PPIDMange, RecipeR.PPBody)
                            RaiseEvent E_HostSend(msg)

                        Else
                            RaiseEvent E_SlInfo("HostRequestDownLoadToEq Abort. Equipment  >> " & reply.PPGNT.ToString)
                        End If

                    Case 4
                        Dim reply As S7F4 = DirectCast(sndMsg, S7F4)      '160727 RecipeBodyManage
                        If reply.ACK7 = ACKC7.Accepted Then
                            RaiseEvent E_SlInfo("HostRequestDownLoadToEq Successful")
                            For Each _file As String In Directory.GetFiles(RecipeDir)   'After success will delete all file ,Protect load mis form remaining file 
                                File.Delete(_file)
                            Next

                        Else

                            RaiseEvent E_SlInfo("HostRequestDownLoadToEq Error. Equipment  >> " & reply.ACK7.ToString)
                        End If

                    Case 6
                        Dim rcmd As S7F6 = DirectCast(sndMsg, S7F6)
                        If rcmd.PPID = "PPIDisNull" Then     '160916 \783 Support ListZero Length
                            MakeAlarmCellCon("HostRequestUplaodToHost is Denied", "S7F6")
                            RaiseEvent E_SlInfo("S7SF6 Request denied")
                            Exit Sub
                        End If

                        Dim recipex As New PDE       'Can Set anymore property of PDE 
                        recipex.PPBody = rcmd.PPBody

                        Dim temptextspilt As String() = rcmd.PPID.Split(CChar("\"))

                        recipex.Name = temptextspilt(temptextspilt.Length - 1)
                        recipex.FullDownLoadname = rcmd.PPID
                        recipex.PathOfFile = RecipeDir
                        recipex.CreateDate = Format(Now, "yyyyMMddTHHmmss")
                        recipex.MCType = My.Settings.MCType
                        WriteToXmlPDE(RecipeDir & "\" & recipex.Name & "_" & Format(Now, "yyyyMMddTHHmmss") & ".xml", recipex) '160811 \783 recipe Program Definition Element


                        RaiseEvent E_SlInfo("HostRequestUplaodToHost Successful PPID : " & rcmd.PPID)
                    Case 18
                        Dim reply As S7F18 = DirectCast(sndMsg, S7F18)
                        If reply.ACK7 = ACKC7.Accepted Then

                        End If

                End Select 'End S7

        End Select



    End Sub

    Private m_SxFxxPri As SxFxxPriDelegate = New SxFxxPriDelegate(AddressOf m_Host_ReceivedPrimaryMessage)
    Friend Sub m_Host_ReceivedPrimaryMessage(ByVal state As Object)
        If Me.InvokeRequired Then
            'http://kristofverbiest.blogspot.com/2007/02/avoid-invoke-prefer-begininvoke.html
            Me.BeginInvoke(m_SxFxxPri, state)
            Exit Sub
        End If
        Try

            Dim msg As SecsMessageBase = DirectCast(state, SecsMessageBase)
            Select Case msg.Stream
                Case 12
                    Select Case msg.Function
                        'Map Upload
                        Case 1
                            If My.Settings.MCType = "2100HS" Or My.Settings.MCType = "2009SSI" Then
                                PreformS12F1(CType(msg, S12F1Esec))
                            Else
                                PreformS12F1(CType(msg, S12F1CanonUpload))
                            End If
                        Case 5
                            If My.Settings.MCType = "2100HS" Or My.Settings.MCType = "2009SSI" Then
                                Perform_S12F5(CType(msg, S12F5Esec))
                            Else
                                Perform_S12F5(CType(msg, S12F5CanonUpload))
                            End If
                        Case 9
                            If My.Settings.MCType = "2100HS" Or My.Settings.MCType = "2009SSI" Then
                                Perform_S12F9(CType(msg, S12F9))
                            Else
                                Perform_S12F9(CType(msg, S12F9CanonUpload))
                            End If

                            'Map DownLoad
                        Case 3
                            If My.Settings.MCType = "2100HS" Or My.Settings.MCType = "2009SSI" Then
                                Perform_S12F3(CType(msg, S12F3))
                            Else
                                Perform_S12F3(CType(msg, S12F3CanonDownload))
                            End If

                        Case 15
                            If My.Settings.MCType = "2100HS" Or My.Settings.MCType = "2009SSI" Then
                                Perform_S12F15(CType(msg, S12F15))
                            Else
                                Perform_S12F15(CType(msg, S12F15CanonDownload))
                            End If
                    End Select
            End Select


        Catch ex As Exception
            SaveCatchLog(ex.ToString, "m_Host_ReceivedPrimaryMessage()_ProcessForm")
        End Try

    End Sub


    Private m_S6F11 As S6F11Delegate = New S6F11Delegate(AddressOf OnS6F11)
    Friend Sub OnS6F11(ByVal request As S6F11) '160801 \783 Add parameter m_Equipment
        If Me.InvokeRequired Then
            'http://kristofverbiest.blogspot.com/2007/02/avoid-invoke-prefer-begininvoke.html
            Me.BeginInvoke(m_S6F11, request)
            Exit Sub
        End If
        request.ApplyStatusVariableValue(m_Equipment, m_DefinedReportDic)  'Macthing  S6F11 with  Define report for decode SVID

        'm_Equipment are object of SVIDs if usage by S6F11  , Please define property of equiptment object refer to SVID Name in equipment specification. 
        'Must set SVID value in S6F11 Class to m_Equipment object

        Select Case request.CEID ''Control Status
            'm_equipment are SVID of CEID that define in report.
            Case CStr(151126075) 'LotEnd  2100HS
                If CellConTag.Torinokoshi = False And CellConTag.LotStartTime <> Nothing Then
                    LockMachine()
                    LotEndSecsGem(CInt(m_Equipment.GoodPcs))
                End If
            Case CStr(3255)   'Status Change 2100HS
                EQStatusChange(m_Equipment.EQStatus)
            Case CStr(14)   'Status Change 2009SSI
                EQStatusChange(m_Equipment.EQStatus)
            Case CStr(41) 'PP-Selected 2100HS ,2009SSI
                If m_statePPSelect = True Then
                    If CellConTag.Recipe & ".dbrcp" = m_Equipment.CurrentPPID Then
                        ReleaseMachine()
                        m_frmWarningDialog("Set up เรียบร้อย กรุณากด Start ก่อนไป First Insp", False, 60000)
                    Else
                        m_frmWarningDialog("Set up ไม่สำเร็จ กรุณากดปุ่ม Set up ใหม่หรือ Inputข้อมูลใหม่อีกครั้ง", False)
                    End If
                    m_statePPSelect = False
                End If
            Case CStr(1038330) 'Lot End 2009SSI
                If CellConTag.Torinokoshi = False And CellConTag.LotStartTime <> Nothing Then
                    LockMachine()
                    LotEndSecsGem(CInt(m_Equipment.GoodPcs))
                End If

                '############################################# Canon-D10R #######################################################

            Case CStr(101) 'Canon ProcessStateChange
                EQStatusChange(m_Equipment.EQStatusCanon)
            Case CStr(108) 'Canon Alarm
                If m_Equipment.AlarmID = "101003" AndAlso m_Equipment.AlarmState = AlarmState.AlarmSet Then
                    If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then   'And CellConTag.Torinokoshi = False 
                        'LockMachine()
                        LotEndSecsGem(CInt(m_Equipment.GoodPcs))
                    End If
                End If
            Case CStr(121) 'Canon LotStart
                If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then 'LotStartUp
                    LotStartup()
                    UpdateDispaly()
                    WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                End If
            Case CStr(102) 'Canon ProcessStart
                If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then 'LotStartUp
                    LotStartup()
                    UpdateDispaly()
                    WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                End If
            Case CStr(105) 'Process Change Finish (recipe changed)
                If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then
                    If m_ppselect = True Then
                        m_ppselect = False
                        RemoteCMD_LotPlan(CellConTag.OPID, CellConTag.LotID, CUInt(CellConTag.INPUTQty))
                    End If
                End If
                'Case CStr(104) 'Canon ProcessEnd
                '    If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then   'And CellConTag.Torinokoshi = False 
                '        LockMachine()
                '        LotEndSecsGem(CInt(m_Equipment.GoodPcs))
                '    End If
                'Case CStr(122) 'Canon LotEnd
                '    If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then  'And CellConTag.Torinokoshi = False 
                '        LockMachine()
                '        LotEndSecsGem(CInt(m_Equipment.GoodPcs))
                '    End If
            Case CStr(131) 'LotPlan
                If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then 'LotStartUp
                    m_frmWarningDialog("Set up เรียบร้อย กรุณากด Start ก่อนไป First Insp", False, 30000)

                    Thread.Sleep(4000)
                    RemoteCMD_Release()
                    RemoteCMD_Local()
                End If
            Case CStr(110) 'Local
            Case CStr(111) 'Remote
                If _WhenInputDataAlready = True Then
                    If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing Then
                        RemoteCMD_LotPlan(CellConTag.OPID, CellConTag.LotID, CUInt(CellConTag.INPUTQty))
                    End If
                End If
                'If (CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing) And m_lotplan = True Then
                '    'm_retry = False
                '    RemoteCMD_LotPlan(CellConTag.OPID, CellConTag.LotID, CUInt(CellConTag.INPUTQty))
                'End If
        End Select

    End Sub

    Private m_S5F1 As S5F1Delegate = New S5F1Delegate(AddressOf OnS5F1)
    Friend Sub OnS5F1(ByVal request As S5F1)
        If Me.InvokeRequired Then
            'http://kristofverbiest.blogspot.com/2007/02/avoid-invoke-prefer-begininvoke.html
            Me.BeginInvoke(m_S5F1, request)
            Exit Sub
        End If

        If CellConTag.LotStartTime <> Nothing AndAlso CellConTag.LotEndTime = Nothing AndAlso CellConTag.LotID <> "" Then

            Dim AlarmStatus As Integer = request.AlarmCode
            Dim DecToBinary As String = Convert.ToString(request.AlarmCode, 2)
            If DecToBinary.Length <= 7 Then   'AlarmClear
                AddAlarmToTable(request.AlarmID, "Clear", CellConTag.LotID, request.AlarmText)
            Else 'AlarmSet
                AddAlarmToTable(request.AlarmID, "Set", CellConTag.LotID, request.AlarmText)
            End If
        ElseIf CellConTag.LotStartTime = Nothing AndAlso CellConTag.LotEndTime = Nothing AndAlso m_statePPSelect = True Then
            If request.AlarmText.Contains("recipe") = True OrElse request.AlarmText.Contains("Recipe") Then
                m_frmWarningDialog("ไม่สามารถเปลี่ยน Recipe ได้กรุณาตรวจสอบด้วยครับ", False)
            End If
            m_statePPSelect = False
        End If

        dgvAlarmTable.DataSource = DBxDataSet.DBAlarmInfo

    End Sub


    Public Sub AddAlarmToTable(ByVal AlarmNo As UInteger, ByVal SetAndClear As String, ByVal LotNo As String, ByVal AlarmMes As String)

        Dim QeryAp As New DBxDataSetTableAdapters.QueriesTableAdapter1
        Dim DBAlarmID As Object = QeryAp.SearchAlarmID(My.Settings.MCType, CStr(AlarmNo))

        If DBAlarmID Is Nothing Then
            Exit Sub
        End If

        Select Case SetAndClear
            Case "Clear"
                For Each strdata As DBxDataSet.DBAlarmInfoRow In DBxDataSet.DBAlarmInfo
                    If strdata.AlarmID = CInt(DBAlarmID) AndAlso strdata.IsClearTimeNull = True Then
                        strdata.ClearTime = Now
                    End If
                Next
            Case "Set"
                CellConTag.TotalAlarm = CShort(CellConTag.TotalAlarm + 1)
                Dim strDataRow As DBxDataSet.DBAlarmInfoRow = DBxDataSet.DBAlarmInfo.NewDBAlarmInfoRow
                strDataRow.AlarmID = CInt(DBAlarmID)
                strDataRow.LotNo = LotNo
                strDataRow.RecordTime = Now
                strDataRow.MCNo = "DB-" & My.Settings.EquipmentNo
                strDataRow.MessageAlarm = AlarmMes
                DBxDataSet.DBAlarmInfo.Rows.Add(strDataRow)

                If My.Settings.MCType = "Canon-D10R" Then
                    Select Case AlarmNo
                        'Alarm Major Count Canon
                        Case 22001 'Pick up
                            CellConTag.AlarmPickup = CShort(CellConTag.AlarmPickup + 1)
                            lbalmPickup1024.Text = CStr(CellConTag.AlarmPickup)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                            'Preform ไม่มี
                        Case 22002 'Bonder
                            CellConTag.AlarmBonder = CShort(CellConTag.AlarmBonder + 1)
                            lbalmBonder1024.Text = CStr(CellConTag.AlarmBonder)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                        Case 4136 'Frame Out
                            CellConTag.AlarmFrameOut = CShort(CellConTag.AlarmFrameOut + 1)
                            lbalmFrameOut1024.Text = CStr(CellConTag.AlarmFrameOut)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                        Case 11083 'Bridge Inspection
                            CellConTag.AlarmBridgeInsp = CShort(CellConTag.AlarmBridgeInsp + 1)
                            lbalmBridge1024.Text = CStr(CellConTag.AlarmBridgeInsp)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                        Case 13080 'Preform Inspection
                            CellConTag.AlarmPreformInsp = CShort(CellConTag.AlarmPreformInsp + 1)
                            lbalmPreformInsp1024.Text = CStr(CellConTag.AlarmPreformInsp)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                    End Select
                ElseIf My.Settings.MCType = "2100HS" Then
                    Select Case AlarmNo
                        '2100HS
                        Case 252182592 'Pickup
                            CellConTag.AlarmPickup = CShort(CellConTag.AlarmPickup + 1)
                            lbalmPickup1024.Text = CStr(CellConTag.AlarmPickup)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                        Case 252313679 'Bonder
                            CellConTag.AlarmBonder = CShort(CellConTag.AlarmBonder + 1)
                            lbalmBonder1024.Text = CStr(CellConTag.AlarmBonder)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                        Case 252510280 'FrameOut
                            CellConTag.AlarmFrameOut = CShort(CellConTag.AlarmFrameOut + 1)
                            lbalmFrameOut1024.Text = CStr(CellConTag.AlarmFrameOut)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                        Case 252641319 'Bridge Inspection
                            CellConTag.AlarmBridgeInsp = CShort(CellConTag.AlarmBridgeInsp + 1)
                            lbalmBridge1024.Text = CStr(CellConTag.AlarmBridgeInsp)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                        Case 252706835 'PReform Inps.
                            CellConTag.AlarmPreformInsp = CShort(CellConTag.AlarmPreformInsp + 1)
                            lbalmPreformInsp1024.Text = CStr(CellConTag.AlarmPreformInsp)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                        Case 252706838 'PReform Inps.
                            CellConTag.AlarmPreformInsp = CShort(CellConTag.AlarmPreformInsp + 1)
                            lbalmPreformInsp1024.Text = CStr(CellConTag.AlarmPreformInsp)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                        Case 252641291 'Postbond Inspection          
                        Case 252641292 'Postbond Inspection
                        Case 252641296 'Postbond Inspection
                        Case 252641327 'Postbond Inspection
                    End Select
                ElseIf My.Settings.MCType = "2009SSI" Then
                    Select Case AlarmNo
                        '2009SSI
                        Case 38524 'Pickup
                            CellConTag.AlarmPickup = CShort(CellConTag.AlarmPickup + 1)
                            lbalmPickup1024.Text = CStr(CellConTag.AlarmPickup)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
                        Case 45660 'Preform
                            CellConTag.AlarmPreform = CShort(CellConTag.AlarmPreform + 1)
                            lbalmPreform1024.Text = CStr(CellConTag.AlarmPreform)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
                        Case 45661 'Preform
                            CellConTag.AlarmPreform = CShort(CellConTag.AlarmPreform + 1)
                            lbalmPreform1024.Text = CStr(CellConTag.AlarmPreform)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
                        Case 45670 'Preform
                            CellConTag.AlarmPreform = CShort(CellConTag.AlarmPreform + 1)
                            lbalmPreform1024.Text = CStr(CellConTag.AlarmPreform)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
                        Case 45671 'Preform
                            CellConTag.AlarmPreform = CShort(CellConTag.AlarmPreform + 1)
                            lbalmPreform1024.Text = CStr(CellConTag.AlarmPreform)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
                        Case 45672 'Preform
                            CellConTag.AlarmPreform = CShort(CellConTag.AlarmPreform + 1)
                            lbalmPreform1024.Text = CStr(CellConTag.AlarmPreform)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
                        Case 45673 'Preform
                            CellConTag.AlarmPreform = CShort(CellConTag.AlarmPreform + 1)
                            lbalmPreform1024.Text = CStr(CellConTag.AlarmPreform)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
                        Case 38563 'Bonder
                            CellConTag.AlarmBonder = CShort(CellConTag.AlarmBonder + 1)
                            lbalmBonder1024.Text = CStr(CellConTag.AlarmBonder)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
                        Case 49505 'FrameOut
                            CellConTag.AlarmFrameOut = CShort(CellConTag.AlarmFrameOut + 1)
                            lbalmFrameOut1024.Text = CStr(CellConTag.AlarmFrameOut)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
                        Case 49508 'FrameOut
                            CellConTag.AlarmFrameOut = CShort(CellConTag.AlarmFrameOut + 1)
                            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
                        Case 850311 'Postbond Inspection
                        Case 850321 'Postbond Inspection
                        Case 850322 'Postbond Inspection
                        Case 850323 'Postbond Inspection
                        Case 850324 'Postbond Inspection
                    End Select
                End If
        End Select
        lbAlmTotal1024.Text = CellConTag.TotalAlarm.ToString
        SaveAlarmInfoTable()
    End Sub

    Private Sub SaveAlarmInfoToDBx()

        If DBxDataSet.DBAlarmInfo.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim apAlarmInfo As New DBxDataSetTableAdapters.DBAlarmInfoTableAdapter
        Try
            If apAlarmInfo.Update(DBxDataSet.DBAlarmInfo) <> 0 Then
                DBxDataSet.DBAlarmInfo.Rows.Clear()
            End If

        Catch ex As Exception
            SaveCatchLog(ex.ToString, "SaveAlarmInfoToDBx()")
            DBxDataSet.DBAlarmInfo.Rows.Clear()
        End Try
        SaveAlarmInfoTable()
    End Sub


    Private Sub HostRequestDownLoadToEq(ByVal PPID As String) '160727 RecipeBodyManage
        Dim msg As New S7F1
        If File.Exists(RecipeDir & "\" & PPID & ".xml") = True Then
            OprData.PPIDMange = PPID
            Dim bFile As Byte() = ReadFromXmlPDE(RecipeDir & "\" & PPID & ".xml").PPBody     '160811 \783 recipe Program Definition Element
            msg.Setparameter(PPID, bFile.Length)
            RaiseEvent E_HostSend(msg)
        Else
            RaiseEvent E_SlInfo("ไม่พบไฟล์ชื่อ " & PPID & "ใน Folder" & RecipeDir)
        End If

    End Sub

    Private Sub HostRequestUplaodToHost(ByVal PPID As String)   '160727 RecipeBodyManage

        Dim S7F5 As New S7F5(PPID)
        RaiseEvent E_HostSend(S7F5)
    End Sub


#End Region


    Private Sub WaferMapDownLoadToEq(ByVal S12F3R As S12F3)  'Request by Eq download to Eq.
        Dim WaferIndex As Integer
        Dim res As New MapData

        If S12F3R.MID.Length <> 7 Then   'Check format 9999-99
            MakeAlarmCellCon("Wafer No. format Err(9999-99) : " & S12F3R.MID, "Machine Ring WaferNo. Read")
            Dim S12F19x As New S12F19(MAPER.FormatError, 0)
            RaiseEvent E_HostReply(S12F3R, S12F19x)
            Exit Sub
        End If

        If Not Directory.Exists(WaferMapDir & "\" & OprData.WaferLotID) Then   'Check target Map file 
            MakeAlarmCellCon("ไม่พบ Directory " & WaferMapDir & "\" & OprData.WaferLotID)
            Dim S12F19x As New S12F19(MAPER.IDNoFound, 0)
            RaiseEvent E_HostReply(S12F3R, S12F19x)
            Exit Sub
        End If

        If Not IsNumeric(Microsoft.VisualBasic.Right(S12F3R.MID, 2)) Then     'Check wafer no. is numberic ?
            MakeAlarmCellCon("Wafer No. not found : " & S12F3R.MID, "Ring WaferNo. Read")
            Dim S12F19x As New S12F19(MAPER.IDNoFound, 0)
            RaiseEvent E_HostReply(S12F3R, S12F19x)
            Exit Sub
        End If

        If Not OprData.WaferLotID Like "*" & Microsoft.VisualBasic.Left(S12F3R.MID, 4) Then 'Check Read ID and Work Slip ID
            MakeAlarmCellCon("Wafer No (" & S12F3R.MID & ").ไม่ตรงกับข้อมูลใน Working Slip WaferID (" & OprData.WaferLotID & ")", "Ring WaferNo. Read")
            Dim S12F19x As New S12F19(MAPER.IDNoFound, 0)
            RaiseEvent E_HostReply(S12F3R, S12F19x)
            Exit Sub

        End If


        WaferIndex = CInt(Microsoft.VisualBasic.Right(S12F3R.MID, 2))
        WaferMapData.Clear()

        res = RohmMapConvert.Read(WaferMapDir & "\" & OprData.WaferLotID, S12F3R.FNLOC, S12F3R.NULBC, S12F3R.BCEQU, "M", WaferIndex)

        OprData.WaferID = S12F3R.MID

        If (CellConTag.WaferID.Exists(Function(x) x = OprData.WaferID)) Then     'if exist remove  and new add
            CellConTag.WaferID.Remove(OprData.WaferID)
            CellConTag.WaferID.Add(OprData.WaferID)
        Else
            CellConTag.WaferID.Add(OprData.WaferID)
        End If

        Dim S12F4 As New S12F4
        Dim RefpList As New List(Of Point)
        RefpList.Add(res.REFP)
        S12F4.SetS12F4_Esec(S12F3R.MID, S12F3R.IDTYP, S12F3R.FNLOC, S12F3R.ORLOC, RefpList, CUShort(res.ROWCT), CUShort(res.COLCT), res.PRDCT, S12F3R.BCEQU, S12F3R.NULBC)
        RaiseEvent E_HostReply(S12F3R, S12F4)
        WaferMapData = res.BINLT
        RaiseEvent E_SlInfo("Host sends Map set up data")

    End Sub

    Public Function WaferMapReadFromZion() As Boolean

        Try

            If Not My.Computer.Network.IsAvailable Then                'unplug check
                MakeAlarmCellCon("PC Nework point unplug")
                Return False
            End If

            If Not My.Computer.Network.Ping(_ipDbxUser) Then            'Can Pink if Computer Connect only
                MakeAlarmCellCon("การเชื่อมต่อกับ" & _ipServer & "ล้มเหลวไม่สามารถดำเนินการต่อได้")
                Return False
            End If

            If Directory.Exists(WaferMapDir) Then                    'Delete All 
                Directory.Delete(WaferMapDir, True)
            End If

            If Not Directory.Exists("\\" & _ipServer & "\WaferMapping\" & OprData.WaferLotID) Then
                MakeAlarmCellCon("ไม่พบ Wafer LotID " & OprData.WaferLotID & " ใน Server(" & _ipServer & ")")
                Return False
            End If

            Directory.CreateDirectory("\\" & _ipServer & "\WaferMapping\" & OprData.WaferLotID)
            My.Computer.FileSystem.CopyDirectory("\\" & _ipServer & "\WaferMapping\" & OprData.WaferLotID & "\", WaferMapDir & "\" & OprData.WaferLotID)
            RaiseEvent E_SlInfo("WaferMapping load from Server Successful")
            Return True

        Catch ex As Exception
            SaveCatchLog(ex.ToString, "WaferMapReadFromZion()")
            MakeAlarmCellCon("Copy directory WaferMapping  from Server Err")
            Return False
        End Try


    End Function



#Region "TDC"
    'USE Raise Event for Send TDC
    'Event E_LRCheck(ByVal EqNo As String, ByVal LotNo As String)
    'Event E_LSCheck(ByVal EQNo As String, ByVal LotNo As String, ByVal StartTime As Date, ByVal OPID As String, ByVal StartMode As RunModeType)
    'Event E_LECheck(ByVal EQNo As String, ByVal LotNo As String, ByVal EndTime As Date, ByVal GoodPcs As Integer, ByVal NgPcs As Integer, ByVal OPID As String, ByVal EndMode As EndModeType)

    Public Sub LS_Reply(ByVal Rpl As TdcResponse)
        Try
            ' Rpl .LotNoTag 
            If OprData.MachineLockByTDC Then  'UnLock = True
                CellConTag.LSReply = "True :" & "TDCUnlockMode Active " & Format(Now, "yyyyMMddTHH:mm:ss") '170126 \783 CellconTag
                GoTo OKAns
            End If
            If Rpl.HasError Then
                CellConTag.LSReply = "False :" & Rpl.ErrorCode & " : " & Rpl.ErrorMessage & " " & Format(Now, "yyyyMMddTHH:mm:ss") '170126 \783 CellconTag
                MakeAlarmCellCon(Rpl.LotNo & " : " & Rpl.ErrorMessage)
                Select Case Rpl.ErrorCode
                    Case "01"
                    Case "02"
                    Case "03"
                    Case "04"
                    Case "05"
                    Case "06"
                    Case "70"
                    Case "71"
                    Case "72"
                    Case "99"
                End Select
                GoTo EndLoop
            End If
            CellConTag.LSReply = "True : " & Format(Now, "yyyyMMddTHH:mm:ss") '170126 \783 CellconTag
OKAns:

            'Coding here if Ans OK
EndLoop:
            ' lbLotInfoTDC.Text = CellConTag.LSReply
            WriteToXmlCellcon(CellconObjPath & "\" & Rpl.LotNo & ".xml", CellConTag)  '170126 \783 CellconTag

        Catch ex As Exception
            SaveCatchLog(ex.ToString, "LS_Reply()")
        End Try
    End Sub

    Public Sub LE_Reply(ByVal Rpl As TdcResponse)
        Try
            ' Rpl .LotNoTag 
            If OprData.MachineLockByTDC Then  'UnLock = True
                CellConTag.LEReply = "True :" & "TDCUnlockMode Active " & Format(Now, "yyyyMMddTHH:mm:ss") '170126 \783 CellconTag
                GoTo OKAns
            End If
            If Rpl.HasError Then
                CellConTag.LEReply = "False :" & Rpl.ErrorCode & " : " & " " & Rpl.ErrorMessage & " " & Format(Now, "yyyyMMddTHH:mm:ss") '170126 \783 CellconTag
                MakeAlarmCellCon(Rpl.LotNo & " : " & Rpl.ErrorMessage)
                Select Case Rpl.ErrorCode
                    Case "01"
                    Case "02"
                    Case "03"
                    Case "04"
                    Case "05"
                    Case "06"
                    Case "70"
                    Case "71"
                    Case "72"
                    Case "99"
                End Select
                GoTo EndLoop
            End If
            CellConTag.LEReply = "True :" & Format(Now, "yyyyMMddTHH:mm:ss") '170126 \783 CellconTag
OKAns:

            'Coding here if Ans OK

EndLoop:
            'lbLotInfoTDC.Text = Rpl.ErrorCode & " : " & " " & Rpl.ErrorMessage
            WriteToXmlCellcon(CellconObjPath & "\" & Rpl.LotNo & ".xml", CellConTag)  '170126 \783 CellconTag
            'If CellconTagList.ContainsKey(Rpl.LotNo) Then
            '    CellconTagList.Remove(Rpl.LotNo)
            'End If

            CellConTag = New CellConObj              'Clear data

        Catch ex As Exception
            SaveCatchLog(ex.ToString, "LE_Reply()")
        End Try
    End Sub


#End Region

#Region "Custom Protolcol"

    Friend Sub RcvManage(ByVal data As String)       'If  My.Settings.CsProtocol Disable data will not come

        Dim Parameter As String
        Parameter = data
        Dim RcvManage_Task As New BackgroundWorker
        AddHandler RcvManage_Task.DoWork, AddressOf RcvManage_Dowork
        AddHandler RcvManage_Task.RunWorkerCompleted, AddressOf RcvManage_RunComplete
        RcvManage_Task.RunWorkerAsync(Parameter)
    End Sub


    Private Sub RcvManage_Dowork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Dim Agr As String
        Agr = CType(e.Argument, String)

        Dim CmdHeader As String
        Dim Cmddata() As String = Agr.Split(CChar(","))
        CmdHeader = Cmddata(0)

        Select Case CmdHeader
            'Case "LR "
            '    LR(Agr)

            'Case "LS "
            '    Send("LS,00")       'LS No use Equiptment can not stop after sent LS to Cellcon

            'Case "LE "
            '    LE(Agr)
            'BackUpLotClean()

            'Case "SC "

            '    SC(Agr)

        End Select


        e.Result = Agr
    End Sub

    Private Sub RcvManage_RunComplete(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)

        Dim Agr As String
        Agr = CType(e.Result, String)

    End Sub

    Private Sub Send(ByVal str As String)              ''If  My.Settings.CsProtocol Disable data will not go
        If Not My.Settings.CsProtocol_Enable Then
            Exit Sub
        End If

        RaiseEvent E_CsProtocol_SendMsg(str & vbCr)
    End Sub

    Delegate Sub AccessControlDelg(ByVal data As String)
    Private m_LR As AccessControlDelg = New AccessControlDelg(AddressOf LR)
    Private m_SC As AccessControlDelg = New AccessControlDelg(AddressOf SC)
    Private m_LE As AccessControlDelg = New AccessControlDelg(AddressOf LE)

    'LR ,QR CodeData,OpNo,InputQty,RecipeName,EqStationNo[CR]
    Private Sub LR(ByVal Cmddatax As String)
        If Me.InvokeRequired Then
            Me.BeginInvoke(m_LR, Cmddatax)
            Exit Sub
        End If
        Try
            'USE Raise Event for Send TDC
            'Event E_LRCheck(ByVal EqNo As String, ByVal LotNo As String)
            'LotRequest()

        Catch ex As Exception
            SaveCatchLog(ex.ToString, "LR()")
        End Try

    End Sub


    'SC ,99,AlarmNo[Cr]   --- 01 AlarmSet,00 AalrmClear
    Private Sub SC(ByVal Cmddatax As String)
        If Me.InvokeRequired Then
            Me.BeginInvoke(m_SC, Cmddatax)
            Exit Sub
        End If
        Try

        Catch ex As Exception
            SaveCatchLog(ex.ToString, "SC()")
        End Try


    End Sub


    ' LE ,LotNo.[CR]

    Private Sub LE(ByVal Cmddatax As String)
        If Me.InvokeRequired Then
            Me.BeginInvoke(m_LE, Cmddatax)
            Exit Sub
        End If

        Try
            'USE Raise Event for Send TDC
            'Event E_LECheck(ByVal EQNo As String, ByVal LotNo As String, ByVal EndTime As Date, ByVal GoodPcs As Integer, ByVal NgPcs As Integer, ByVal OPID As String, ByVal EndMode As EndModeType)


        Catch ex As Exception
            SaveCatchLog(ex.ToString, "LE()")
        End Try


    End Sub


    Private Sub SendRM_LOCK()
        'Send("RM,LOCK")
    End Sub


#End Region

#Region "Lot Management"

#End Region

#Region "===  KeyBoard Control"
    Dim KYB As KeyBoard

    Private Sub KeyBoardCall(ByVal OBJ As TextBox, ByVal NumpadKeys As Boolean, Optional ByVal infoImage As System.Drawing.Image = Nothing, Optional ByVal Tag As String = "")
        If KYB Is Nothing Then
            KYB = New KeyBoard
        ElseIf KYB.IsDisposed Then
            KYB = New KeyBoard
        End If
        KYB.TargetTextBox = OBJ
        KYB.tbxMonitorx.Text = OBJ.Text
        KYB.tbxMonitorx.Select(KYB.tbxMonitorx.Text.Length, 0)
        KYB.Owner = Me
        KYB.StartPosition = FormStartPosition.Manual
        Dim xsize As Rectangle = Screen.PrimaryScreen.Bounds
        KYB.Left = 10
        KYB.Top = 0
        KYB.TopMost = True
        KYB.NumPad = NumpadKeys                        'Numpad =True , Keyboard = False
        KYB.pbxHelper.BackgroundImage = infoImage
        KYB.TagID = Tag

        KYB.Show()
        AddHandler KYB.FormClosed, AddressOf KYB_close

    End Sub


    Private Sub KeyBoardCall(ByVal OBJ As Label, ByVal NumpadKeys As Boolean, Optional ByVal infoImage As System.Drawing.Image = Nothing, Optional ByVal Tag As String = "")

        If KYB Is Nothing Then
            KYB = New KeyBoard
        ElseIf KYB.IsDisposed Then
            KYB = New KeyBoard
        End If
        KYB.TargetLabel = OBJ
        KYB.tbxMonitorx.Text = OBJ.Text
        KYB.tbxMonitorx.Select(KYB.tbxMonitorx.Text.Length, 0)
        KYB.Owner = Me
        KYB.StartPosition = FormStartPosition.Manual
        Dim xsize As Rectangle = Screen.PrimaryScreen.Bounds
        KYB.Left = 10
        KYB.Top = 0
        KYB.TopMost = True
        KYB.NumPad = NumpadKeys                        'Numpad =True , Keyboard = False
        KYB.pbxHelper.BackgroundImage = infoImage
        KYB.TagID = Tag
        KYB.Show()
        AddHandler KYB.FormClosed, AddressOf KYB_close

    End Sub

    Private Sub KeyBoardCallDialog(ByVal OBJ As Label, ByVal NumpadKeys As Boolean, Optional ByVal infoImage As System.Drawing.Image = Nothing, Optional ByVal Tag As String = "")

        If KYB Is Nothing Then
            KYB = New KeyBoard
        ElseIf KYB.IsDisposed Then
            KYB = New KeyBoard
        End If
        KYB.TargetLabel = OBJ
        KYB.tbxMonitorx.Text = OBJ.Text
        KYB.tbxMonitorx.Select(KYB.tbxMonitorx.Text.Length, 0)
        KYB.StartPosition = FormStartPosition.Manual
        Dim xsize As Rectangle = Screen.PrimaryScreen.Bounds
        KYB.Left = 10
        KYB.Top = 0
        KYB.TopMost = True
        KYB.NumPad = NumpadKeys                        'Numpad =True , Keyboard = False
        KYB.pbxHelper.BackgroundImage = infoImage
        KYB.TagID = Tag

        KYB.ShowDialog()
        KYB.Close()
        KYB.TagID = ""
    End Sub



    Private Sub KYB_close(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        lbProcess.Focus()                   'tbxCtrl unfocus
        KYB.TagID = ""
    End Sub





#End Region

#Region "Debug"

    Public Sub ax()
        MsgBox("text")
    End Sub


    Dim ID As Integer
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)



        'AlarmTable(True, CStr(ID), "TEXT")
        'Update_dgvProductionInfo1(CStr(A), "555", "22", "33")
        'ID += 1
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim AX As New Thread(AddressOf A1)
        Dim BX As New Thread(AddressOf B1)
        AX.Start()
        BX.Start()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim CX As New Thread(AddressOf c1)
        CX.Start()
    End Sub


    Dim OBJA As New Object
    Dim A As Integer
    Private Sub A1()

        'SyncLock OBJA
        'Thread.Sleep(2000)
        'End SyncLock

        Update_dgvProductionDetail(CStr(A), "AA", "")
        A += 1
    End Sub
    Private Sub B1()

        SaveCatchLog("TestCatchlog", "B")
        'Thread.Sleep(100)
        'SyncLock OBJA
        'Thread.Sleep(2000)
        'End SyncLock
        'Update_dgvProductionInfo1("Carr", CStr(A), "B", "BB")
    End Sub
    Private Sub c1()

        Update_dgvProductionInfoEnd(CStr(A), "555", "C")
        A += 1
    End Sub


    Private Sub S2F43UseSample()

        Dim SF As New S2F43
        Dim Func As Byte() = {11, 12, 13}
        SF.AddStream(6, Func)
        Func = {1, 2, 3}
        SF.AddStream(1, Func)
        RaiseEvent E_HostSend(SF)

    End Sub

    Private Sub DataBaseUpdateByDataRowSample()
        'Try
        '    Dim tb As New DBxDataSet.IPDDataDataTable
        '    Dim Qry As New DBxDataSetTableAdapters.IPDDataTableAdapter
        '    Qry.FillBy(tb, "1111111111")
        '    Dim Dr As DBxDataSet.IPDDataRow
        '    Dr = CType(tb.Rows(0), DBxDataSet.IPDDataRow)
        '    Dr.InFrame = 23
        '    Try
        '        Dim a = Qry.Update(Dr)

        '    Catch ex As InvalidOperationException
        '        MsgBox("Update Fail : " & ex.ToString)
        '    Catch ex As DBConcurrencyException
        '        MsgBox("Update Fail : " & ex.ToString)

        '    End Try

        'Catch ex As Exception


        'End Try
    End Sub


#End Region

#Region "User Coding Area"


    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Dim s2f15 As New S2F15
        If Button2.Text = "ENABLE" Then
            s2f15.AddListEcid(151126402, "1", SecsFormat.U2)    'Enable = 1
            Button2.Text = "DISABLE"

        Else
            s2f15.AddListEcid(151126402, "0", SecsFormat.U2)
            Button2.Text = "ENABLE"
        End If
        RaiseEvent E_HostSend(s2f15)

    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        OprData.WaferLotID = TextBox1.Text
        WaferMapReadFromZion()
    End Sub


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


    Dim FlagExpPreform As Boolean = False 'False = หมดอายุ ,Ture = ไม่หมดอายุ 
    Private Sub PreformExp_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreformExp.Tick
        Dim strColor As String
        If OprData.PreformExpDate2 <> Nothing Then 'Preform2
            strColor = PreformExpColor(OprData.PreformExpDate2)
            FlagExpPreform = True
            Select Case strColor
                Case PreformStatus.Blue40.ToString

                    lbPreformExp2_1024.BackColor = Color.Blue
                    lbPreformExp2_1024.ForeColor = Color.White

                Case PreformStatus.Sky32_40.ToString

                    lbPreformExp2_1024.BackColor = Color.SkyBlue
                    lbPreformExp2_1024.ForeColor = Color.Black

                Case PreformStatus.Green24_32.ToString

                    lbPreformExp2_1024.BackColor = Color.GreenYellow
                    lbPreformExp2_1024.ForeColor = Color.Black

                Case PreformStatus.Yello10_24.ToString

                    lbPreformExp2_1024.BackColor = Color.Yellow
                    lbPreformExp2_1024.ForeColor = Color.Black

                Case PreformStatus.Orange4_10.ToString
                    lbPreformExp2_1024.BackColor = Color.Orange
                    lbPreformExp2_1024.ForeColor = Color.Black

                Case PreformStatus.DarkOrange0_4.ToString

                    lbPreformExp2_1024.BackColor = Color.DarkOrange
                    lbPreformExp2_1024.ForeColor = Color.Black

                Case Else

                    lbPreformExp2_1024.BackColor = Color.Red
                    lbPreformExp2_1024.ForeColor = Color.Black
                    FlagExpPreform = False
            End Select
        ElseIf OprData.PreformExpDate1 <> Nothing Then 'Preform1 then

            strColor = PreformExpColor(OprData.PreformExpDate1)
            FlagExpPreform = True
            Select Case strColor
                Case PreformStatus.Blue40.ToString
                    lbPreformExp1_1024.BackColor = Color.Blue
                    lbPreformExp1_1024.ForeColor = Color.White

                Case PreformStatus.Sky32_40.ToString
                    lbPreformExp1_1024.BackColor = Color.SkyBlue
                    lbPreformExp1_1024.ForeColor = Color.Black

                Case PreformStatus.Green24_32.ToString
                    lbPreformExp1_1024.BackColor = Color.GreenYellow
                    lbPreformExp1_1024.ForeColor = Color.Black

                Case PreformStatus.Yello10_24.ToString

                    lbPreformExp1_1024.BackColor = Color.Yellow
                    lbPreformExp1_1024.ForeColor = Color.Black

                Case PreformStatus.Orange4_10.ToString
                    lbPreformExp1_1024.BackColor = Color.Orange
                    lbPreformExp1_1024.ForeColor = Color.Black

                Case PreformStatus.DarkOrange0_4.ToString

                    lbPreformExp1_1024.BackColor = Color.DarkOrange
                    lbPreformExp1_1024.ForeColor = Color.Black

                Case Else
                    lbPreformExp1_1024.BackColor = Color.Red
                    lbPreformExp1_1024.ForeColor = Color.Black

                    FlagExpPreform = False
            End Select
        End If



    End Sub

    Private Function PreformExpColor(ByVal exp As Date) As String
        Dim ret As String = ""
        Dim MinTotal As Long
        MinTotal = DateDiff(DateInterval.Minute, Now, exp)

        If MinTotal > 2400 Then ' > 40 HR น้ำเงิน
            ret = PreformStatus.Blue40.ToString
        ElseIf MinTotal > 1920 AndAlso MinTotal < 2400 Then '32-40 HR ฟ้า
            ret = PreformStatus.Sky32_40.ToString
        ElseIf MinTotal > 1440 AndAlso MinTotal < 1920 Then '24-32 HR เขียว
            ret = PreformStatus.Green24_32.ToString
        ElseIf MinTotal > 600 AndAlso MinTotal < 1440 Then '10-24 HR เหลือง
            ret = PreformStatus.Yello10_24.ToString ' หมดอายุ
        ElseIf MinTotal > 240 AndAlso MinTotal < 600 Then 'ใช้งานได้ปกติ4-10 HR ส้มอ่อน
            ret = PreformStatus.Orange4_10.ToString ' หมดอายุ
        ElseIf MinTotal < 240 AndAlso MinTotal > 0 Then 'ใกล้หมดไม่ให้รันต่อ 0-4 HR ส้มเข้ม
            ret = PreformStatus.DarkOrange0_4.ToString
        Else 'แดง
            ret = PreformStatus.Expired.ToString ' หมดอายุ
        End If

        Return ret
    End Function


    Private Sub MaterialDefaultColor()
        'lbFrameQR1.BackColor = Color.LightCyan
        'lbFrameQR2.BackColor = Color.LightGreen
        'lbFrameLotNo1.BackColor = Color.LightCyan
        'lbFrameLotNo2.BackColor = Color.LightGreen
        'lbFrameType1.BackColor = Color.LightCyan
        'lbFrameType2.BackColor = Color.LightGreen
        'lbPreformQR1.BackColor = Color.LightCyan
        'lbPreformQR2.BackColor = Color.LightGreen
        'lbPreformType1.BackColor = Color.LightCyan
        'lbPreformType2.BackColor = Color.LightGreen
        'lbPreformLife1.BackColor = Color.LightCyan
        'lbPreformLife2.BackColor = Color.LightGreen
        'lbPreformLotNo1.BackColor = Color.LightCyan
        'lbPreformLotNo2.BackColor = Color.LightGreen
        'lbPreformLife1.ForeColor = Color.Black
        'lbPreformLife2.ForeColor = Color.Black

    End Sub
    Public Function QRWorkingSlipInputInitailCheck(ByVal BeAfRead As Boolean, Optional ByVal WorkSlipQR As WorkingSlipQRCode = Nothing) As Boolean   'Before Read = True, After Read =False


        'Before Read Working Slip check ===================================================================================================
        '------------------------------
        If BeAfRead Then
            If CellConTag.FrameLotNo_1st = "" Then
                m_QRReadAlarm = "ยังไม่ได้ใส่ข้อมูล Material Frame : LotNo"
                Return False
            ElseIf CellConTag.FrameSeqNo_1st = "" Then
                m_QRReadAlarm = "ยังไม่ได้ใส่ข้อมูล Material Frame : FrameQR"
                Return False
            ElseIf CellConTag.FrameType_1st = "" Then
                m_QRReadAlarm = "ยังไม่ได้ใส่ข้อมูล Material Frame : FrameType"
                Return False
            End If
            If CellConTag.PreforExpireDate_1st = Nothing Then
                m_QRReadAlarm = "ยังไม่ได้ใส่ข้อมูล Material Preform : PreformLife"
                Return False
            End If

            If FlagExpPreform = False Then
                m_QRReadAlarm = "Preform หมดอายุ กรุณาเปลี่ยนด้วยครับ "
                CellConTag.PreformLotNo_1st = ""
                CellConTag.PreformLotNo_2nd = ""
                CellConTag.PreforInputDate_1st = Nothing
                CellConTag.PreforInputDate_2nd = Nothing
                CellConTag.PreforExpireDate_1st = Nothing
                CellConTag.PreforExpireDate_2nd = Nothing
                CellConTag.PreformType_1st = ""
                CellConTag.PreformType_2nd = ""
                CellConTag.PreformQR_1st = ""
                CellConTag.PreformQR_2nd = ""
                MaterialDefaultColor()
                UpdateDisplayMaterial()
                Return False
            End If

            If CellConTag.PreformLotNo_2nd <> "" Then
                CellConTag.PreformLotNo_1st = CellConTag.PreformLotNo_2nd
                CellConTag.PreforInputDate_1st = CellConTag.PreforInputDate_2nd
                CellConTag.PreforExpireDate_1st = CellConTag.PreforExpireDate_2nd
                CellConTag.PreformQR_1st = CellConTag.PreformQR_2nd
                CellConTag.PreformType_1st = CellConTag.PreformType_2nd
                CellConTag.PreforExpireDate_1st = CellConTag.PreforExpireDate_2nd
                CellConTag.PreformQR_1st = CellConTag.PreformQR_2nd

                CellConTag.PreformLotNo_2nd = ""
                CellConTag.PreforInputDate_2nd = Nothing
                CellConTag.PreforExpireDate_2nd = Nothing
                CellConTag.PreformQR_2nd = ""
                CellConTag.PreformType_2nd = ""
                CellConTag.PreformQR_2nd = ""

                WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag 

                MaterialDefaultColor()
                UpdateDisplayMaterial()
            End If


            'After Read Working Slip check ========================================================================================================
            '----------------------------
        Else
            Dim strPreformType As String
            Dim strFrameTYpe As String
            If CellConTag.PreformType_2nd.Trim <> "" Then      'ถ้ามี Preform1,2 ให้ตรวจสอบอันหลัง
                strPreformType = CellConTag.PreformType_2nd.Replace("MAT-", "")
            Else
                strPreformType = CellConTag.PreformType_1st.Replace("MAT-", "")
            End If

            If CellConTag.FrameType_2nd <> "" Then
                strFrameTYpe = CellConTag.FrameType_2nd.ToUpper.Trim
            Else
                strFrameTYpe = CellConTag.FrameType_1st.ToUpper.Trim
            End If

            If My.Settings.MCType = "Canon-D10R" And My.Settings.SECS_Enable = True Then
                Dim queryFrame As New DBxDataSetTableAdapters.QueriesTableAdapter1
                Dim framepcs As Short? = queryFrame.SearchFramePCS(WorkSlipQR.FrameType)
                If framepcs Is Nothing Then
                    m_QRReadAlarm = "Frame Type: " & WorkSlipQR.FrameType.Trim & " ไม่มีจำนวน PCS/Frame ในระบบกรุณา GL เพื่อเพิ่มลงในระบบ"
                    Return False
                Else
                    Para.PCSPerFrame = CInt(framepcs)
                End If
            End If

            'If My.Settings.CompareFrameDiable = False Then
            '    If strFrameTYpe <> WorkSlipQR.FrameType.ToUpper.Trim Then
            '        m_QRReadAlarm = "Material System FrameType:(" & strFrameTYpe & ") ไม่ตรงกับ Working Slip FrameType:(" & WorkSlipQR.FrameType.ToUpper.Trim & ")"
            '        Return False
            '    End If
            'Else
            If WorkSlipQR.FrameType.Contains(" ") = True Then
                    Dim strFrameSpit As String() = WorkSlipQR.FrameType.Split(CChar(" "))
                    If strFrameTYpe <> strFrameSpit(0) Then
                        m_QRReadAlarm = "Material System FrameType:(" & strFrameTYpe & ") ไม่ตรงกับ Working Slip FrameType:(" & WorkSlipQR.FrameType.ToUpper.Trim & ")"
                        Return False
                    End If
                Else
                    If strFrameTYpe <> WorkSlipQR.FrameType.ToUpper.Trim Then
                        m_QRReadAlarm = "Material System FrameType:(" & strFrameTYpe & ") ไม่ตรงกับ Working Slip FrameType:(" & WorkSlipQR.FrameType.ToUpper.Trim & ")"
                        Return False
                    End If
                End If
            ' End If


            If PackageDeviceComparePreform(strPreformType, WorkSlipQR) = False Then 'ตรวจสอบว่า Device ,Package สามารถใช้กับ Preform ตัวนี้ได้หรือไม่
                Return False
            End If

        End If
        Return True
    End Function

    Private Function PackageDeviceComparePreform(ByVal stPreformType As String, ByVal WorkSlipQR As WorkingSlipQRCode) As Boolean
        Dim ret As Boolean = False

        Try
            Dim GetPreformFromDenpyoAdapter As New DBxDataSetTableAdapters.QueriesTableAdapter1
            Dim strPreformTypeFromMAT As String = stPreformType 'QueriesTableAdapter1.FindModelFromStockItem(CInt(ID))
            Dim strPreformTypeDenpyo As String = GetPreformFromDenpyoAdapter.GetPreformFromDenpyo(WorkSlipQR.LotNo)

            If strPreformTypeFromMAT Is Nothing OrElse strPreformTypeDenpyo Is Nothing Then
                'ตรวจสอบความถูกต้องโดยคน เพื่อให้ทาง Process ตัดสินใจเอง ไม่ตค้องรอแก้ไขดาต้าเบส
                m_QRReadAlarm = "ไม่พบข้อมุล PrefromType ของ LotNo. (" & WorkSlipQR.LotNo & ")ในระบบ(DenpyoPrintTable)"
                'If MsgBox("ตรวจสอบความถูกต้องของ PreformType โดยคน กด 'OK' หรือยกด 'CANCEL'เพื่อไปตรวจสอบการขึ้นทะเบียนและดำเนินการใหม่", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                '    Return True
                'End If
                Return False
            End If
            Dim tmpPreformMAT As String = strPreformTypeFromMAT.Replace("-", "").Trim
            Dim tmpPreformMatSystem As String = ""
            Dim devicex As String = WorkSlipQR.Device.Trim
            Dim packagex As String = WorkSlipQR.Package.Trim
            If strPreformTypeDenpyo Like "SOLDER" Then    'if solder type check master type FROM [DBx].[dbo].[MasterPackageDevice]
                tmpPreformMatSystem = GetPreformFromDenpyoAdapter.SolderMasterPackageDevice(devicex, packagex)
                tmpPreformMatSystem = tmpPreformMatSystem.Replace("-", "").Trim
                If tmpPreformMatSystem = "" Then
                    m_QRReadAlarm = "ไม่พบข้อมุล PrefromType ของ Package. (" & packagex & ") Device. (" & devicex & ")ในระบบ Material"
                    Return False
                End If
            Else

                tmpPreformMatSystem = strPreformTypeDenpyo.Replace("-", "").Trim
            End If

            If tmpPreformMAT.Contains(tmpPreformMatSystem) = True Then
                ret = True
            Else
                m_QRReadAlarm = "PrefromType จาก Master Data(" & tmpPreformMatSystem & ")ไม่ตรงกับ PreformType จาก Material System(" & strPreformTypeFromMAT & ")ติดต่อระบบผู้ดูแลระบบ Material"
                'If MsgBox("ตรวจสอบความถูกต้องของ PreformType โดยคน กด 'OK' หรือยกด 'CANCEL'เพื่อไปตรวจสอบการขึ้นทะเบียนและดำเนินการใหม่", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                '    Return True
                'End If
                ret = False
            End If

        Catch ex As Exception

            SaveCatchLog(ex.ToString, " PackageDeviceComparePreform()")
            m_QRReadAlarm = "Catch Error ตรวจสอบดู CatchLog"
            ret = False
        End Try

        Return ret
    End Function

#End Region


    Private Sub LotEndClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btLotEnd1024.Click

        If My.Settings.MCType = "IDBW" Then

            If CellConTag.LotStartTime <> Nothing AndAlso CellConTag.LotEndTime = Nothing Then
                If MessageBox.Show("คุณต้องการ Lot End ใช่ไหมครับ", "", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
            ElseIf CellConTag.LotID = Nothing Or CellConTag.LotStartTime = Nothing Then
                Exit Sub
            End If

            c_FrmFinal = New frmFinalInspection(Me)
            If c_FrmFinal.ShowDialog() = Windows.Forms.DialogResult.OK Then

                SetParameterFrmFinalInsp(c_FrmFinal)

                CellConTag.StateCellCon = CellConState.LotEnd
                UpdateDispaly()
                UpdateDisplayMaterial()

                'LotEnd
                WriteToXmlCellcon(CellconObjPath & "\" & CellConTag.LotID & ".xml", CellConTag)
                WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)

                If picFirst1024.Visible = True Then
                    SaveLotEndToDbx()
                    CellConTag.StateCellCon = CellConState.LotEnd
                    m_frmWarningDialog("กรุณาทำ Final Insp.", False, 30000)
                Else
                    CellConTag.StateCellCon = CellConState.LotEnd
                    m_frmWarningDialog("Lot End เรียบร้อยแล้ว ยังไม่ทำ First Insp. " & vbCrLf & "กรุณาทำ First Insp. ด้วยครับ ", False, 30000)
                End If

                If c_FrmFinal.c_EndMode = EndModeType.Normal Then
                    LotEnd(My.Settings.ProcessName & "-" & My.Settings.EquipmentNo, CellConTag.LotID, CellConTag.LotEndTime, CellConTag.GoodAdjust, CellConTag.NGAdjust, CellConTag.OPID, EndModeType.Normal)
                ElseIf c_FrmFinal.c_EndMode = EndModeType.AbnormalEndAccumulate Then
                    LotEnd(My.Settings.ProcessName & "-" & My.Settings.EquipmentNo, CellConTag.LotID, CellConTag.LotEndTime, CellConTag.GoodAdjust, CellConTag.NGAdjust, CellConTag.OPID, EndModeType.AbnormalEndAccumulate)
                End If

                SaveAlarmInfoToDBx()

                Try
                    m_EmsClient.SetOutput(My.Settings.EquipmentNo, CellConTag.GoodAdjust, CellConTag.NGAdjust)
                    m_EmsClient.SetLotEnd(My.Settings.EquipmentNo)
                    m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Stop", TmeCategory.StopLoss)
                Catch ex As Exception
                End Try

            End If

            Exit Sub
        End If

        m_flagLotEnd = True

        If My.Settings.SECS_Enable = True Then

            If Me.BackColor = Color.Red Then 'ถ้าเป็น Secsgem จะต้องเช็คก่อนส่ง

                m_frmWarningDialog("กรุณเชื่อมต่อCellCon กับ M/C ด้วยครับ", False)
                Exit Sub
            End If

            If (CellConTag.LotStartTime <> Nothing AndAlso CellConTag.LotEndTime <> Nothing) OrElse (CellConTag.LotStartTime <> Nothing AndAlso CellConTag.LotEndTime = Nothing) Then
                Dim ap As New DBxDataSetTableAdapters.QueriesTableAdapter1
                Dim QCModeFirst As String = ap.SearchQCFirstMode(CellConTag.LotID, CellConTag.LotStartTime)
                If QCModeFirst = "" OrElse QCModeFirst Is Nothing Then
                    m_frmWarningDialog("กรุณาทำ First Insp. ก่อนครับ", False)
                    Exit Sub
                End If

                If CellConTag.LotEndTime = Nothing Then
                    If MessageBox.Show("คุณต้องการ Lot End ใช่ไหมครับ", "", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
                        Exit Sub
                    End If
                End If

                'If MessageBox.Show("คุณต้องการ Lot End ใช่ไหมครับ", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim frmfinal As New frmFinalInspection(Me)
                If frmfinal.ShowDialog() = Windows.Forms.DialogResult.OK Then

                    SetParameterFrmFinalInsp(frmfinal)

                    CellConTag.StateCellCon = CellConState.LotEnd
                    UpdateDispaly()
                    UpdateDisplayMaterial()

                    'LotEnd
                    WriteToXmlCellcon(CellconObjPath & "\" & CellConTag.LotID & ".xml", CellConTag)
                    WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
                    SaveLotEndToDbx()
                    If frmfinal.c_EndMode = EndModeType.Normal Then
                        LotEnd(My.Settings.ProcessName & "-" & My.Settings.EquipmentNo, CellConTag.LotID, CellConTag.LotEndTime, CellConTag.GoodAdjust, CellConTag.NGAdjust, CellConTag.OPID, EndModeType.Normal)
                    ElseIf frmfinal.c_EndMode = EndModeType.AbnormalEndAccumulate Then
                        LotEnd(My.Settings.ProcessName & "-" & My.Settings.EquipmentNo, CellConTag.LotID, CellConTag.LotEndTime, CellConTag.GoodAdjust, CellConTag.NGAdjust, CellConTag.OPID, EndModeType.AbnormalEndAccumulate)
                    End If

                    LockMachine()
                    SaveAlarmInfoToDBx()

                    Try
                        m_EmsClient.SetOutput(My.Settings.EquipmentNo, CellConTag.GoodAdjust, CellConTag.NGAdjust)
                        m_EmsClient.SetLotEnd(My.Settings.EquipmentNo)
                        m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Stop", TmeCategory.StopLoss)
                    Catch ex As Exception
                    End Try

                    m_frmWarningDialog("กรุณาทำ Final Insp.", False)
                ElseIf frmfinal.ShowDialog() = Windows.Forms.DialogResult.Retry Then '"ปลดล๊อค"

                    m_frmWarningDialog("Unlock MC เรียบร้อยกรุณา สามารถ Start Lot ได้แล้วครับ", False)
                Else

                    If CellConTag.OPCheck = Nothing Then
                        m_frmWarningDialog("กรุณาทำ กดปุ่ม LotEnd เพื่อยืนยันนวนงาน , Alarm ด้วยครับ", False)
                    Else
                        m_frmWarningDialog("กรุณาทำ Final Insp", False)
                    End If

                End If

            End If

        Else

            If CellConTag.LotStartTime <> Nothing AndAlso CellConTag.LotEndTime = Nothing Then
                Dim ap As New DBxDataSetTableAdapters.QueriesTableAdapter1
                Dim QCModeFirst As String = ap.SearchQCFirstMode(CellConTag.LotID, CellConTag.LotStartTime)
                If QCModeFirst = "" OrElse QCModeFirst Is Nothing Then
                    m_frmWarningDialog("กรุณาทำ First Insp. ก่อนครับ", False)
                    Exit Sub
                End If


                If MessageBox.Show("คุณต้องการ Lot End ใช่ไหมครับ", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    Dim frmfinal As New frmFinalInspection(Me)
                    If frmfinal.ShowDialog() = Windows.Forms.DialogResult.OK Then

                        SetParameterFrmFinalInsp(frmfinal)

                        CellConTag.StateCellCon = CellConState.LotEnd

                        UpdateDispaly()
                        UpdateDisplayMaterial()

                        'LotEnd
                        WriteToXmlCellcon(CellconObjPath & "\" & CellConTag.LotID & ".xml", CellConTag)
                        WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
                        SaveLotEndToDbx()

                        If frmfinal.c_EndMode = EndModeType.Normal Then
                            LotEnd(My.Settings.ProcessName & "-" & My.Settings.EquipmentNo, CellConTag.LotID, CellConTag.LotEndTime, CellConTag.GoodAdjust, CellConTag.NGAdjust, CellConTag.OPID, EndModeType.Normal)
                        ElseIf frmfinal.c_EndMode = EndModeType.AbnormalEndAccumulate Then
                            LotEnd(My.Settings.ProcessName & "-" & My.Settings.EquipmentNo, CellConTag.LotID, CellConTag.LotEndTime, CellConTag.GoodAdjust, CellConTag.NGAdjust, CellConTag.OPID, EndModeType.AbnormalEndAccumulate)
                        End If

                        Try

                            m_EmsClient.SetOutput(My.Settings.EquipmentNo, CellConTag.GoodAdjust, CellConTag.NGAdjust)
                            m_EmsClient.SetLotEnd(My.Settings.EquipmentNo)
                            m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Stop", TmeCategory.StopLoss)
                        Catch ex As Exception
                        End Try

                        m_frmWarningDialog("กรุณาทำ Final Insp.", False)

                    End If
                End If

            End If
        End If


        m_flagLotEnd = False
    End Sub

    Private Sub SaveLotEndToDbx()

        DBxDataSet.DBData.Rows.Clear()
        DbDataTableAdapter1.FillBy(DBxDataSet.DBData, CellConTag.LotID, CellConTag.LotStartTime)
        For Each strDataRow As DBxDataSet.DBDataRow In DBxDataSet.DBData.Rows
            If strDataRow.LotNo = CellConTag.LotID AndAlso strDataRow.LotStartTime = CellConTag.LotStartTime Then
                With strDataRow

                    .TotalGood = CellConTag.TotalGood

                    'Alarm
                    .AlarmTotal = CellConTag.TotalAlarm
                    .AlarmBonder = CellConTag.AlarmBonder
                    .AlarmBridgeInsp = CellConTag.AlarmBridgeInsp
                    .AlarmFrameOut = CellConTag.AlarmFrameOut
                    .AlarmPickUp = CellConTag.AlarmPickup
                    .AlarmPreform = CellConTag.AlarmPreform
                    .AlarmPreformInsp = CellConTag.AlarmPreformInsp

                    'Mecha
                    .DoubleFrame = CellConTag.DoubleFrame
                    .FrameBent = CellConTag.FrameBent
                    .FrameBurn = CellConTag.FrameBurn
                    .BondingNG = CellConTag.BondingNG

                    .InputQtyAdjust = CellConTag.InputAdjust
                    .TotalGoodAdjust = CellConTag.GoodAdjust
                    .TotalNGAdjust = CellConTag.NGAdjust
                    .NoChipQTY = CellConTag.NoChip

                    .OPCheck = CellConTag.OPCheck

                    .LotEndTime = CellConTag.LotEndTime

                    'MAt 2 
                    If CellConTag.FrameLotNo_2nd <> "" Then
                        .FrameSecondLot = CellConTag.FrameLotNo_2nd
                    End If
                    If CellConTag.FrameSeqNo_2nd <> "" Then
                        .FrameSecondSEQ_NO = CellConTag.FrameSeqNo_2nd
                    End If
                    If CellConTag.PreformLotNo_2nd <> "" Then
                        .PreformSecondLot = CellConTag.PreformLotNo_2nd
                    End If
                    If CellConTag.PreformQR_2nd <> "" Then
                        .PreformSecondID = CInt(CellConTag.PreformQR_2nd)
                    End If
                    If CellConTag.PreforExpireDate_2nd <> Nothing Then
                        .PreformSecondLiftTime = CellConTag.PreforExpireDate_2nd
                    End If

                    If My.Settings.SECS_Enable = True OrElse My.Settings.SerialPortEnable = True Then
                        Try
                            .RunTime = CSng(CellConTag.RunTimeCount.TotalMinutes)
                            .AlarmTime = CSng(CellConTag.AlarmTimeCount.TotalMinutes)
                            .StopTime = CSng(CellConTag.StopTimeCount.TotalMinutes)
                        Catch ex As Exception

                        End Try
                    End If

                    .Remark = CellConTag.Remark
                End With

                If DbDataTableAdapter1.Update(strDataRow) <> 0 Then
                    m_frmWarningDialog("Lot End เรียบร้อยแล้ว กรุณาทำ Final Insp.", False, 60000)
                End If

            End If
        Next

    End Sub

    Private Sub Button7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btLotStart1024.Click

        If CellConTag.LotStartTime = Nothing AndAlso CellConTag.LotEndTime <> Nothing AndAlso CellConTag.LotID = "" Then
            FinalInspCompleted()
        ElseIf CellConTag.LotStartTime <> Nothing AndAlso CellConTag.LotEndTime = Nothing Then
            Exit Sub
        End If

        If My.Settings.SECS_Enable = True Then
            SecsGemStateChecking()
        Else
            SetupLot()
        End If


        'If My.Settings.MCType = "Canon-D10R" And My.Settings.SECS_Enable = True Then 'And lbEqState.Text = "Eq State" Then
        '    RequestSVID_CurrentState()
        '    Thread.Sleep(3000) 'รอสถานะ กรณีที่เปิดเครื่อแล้วมันไม่เชค สถานะ ครั้งเดียว
        'End If

        'If CellConTag.LotStartTime = Nothing AndAlso CellConTag.LotEndTime <> Nothing AndAlso CellConTag.LotID = "" Then
        '    FinalInspCompleted()
        'ElseIf CellConTag.LotStartTime <> Nothing AndAlso CellConTag.LotEndTime = Nothing Then
        '    Exit Sub
        'End If

        'Dim ret As String = FirstFinalChecking(CellConTag)
        'If ret <> "" Then
        '    m_frmWarningDialog(ret, False)
        '    Exit Sub
        'End If

        'If My.Settings.SECS_Enable = True AndAlso Me.BackColor = Color.Red Then 'ถ้าเป็น Secsgem จะต้องเช็คก่อนส่ง
        '    m_frmWarningDialog("กรุณเชื่อมต่อCellCon กับ M/C ด้วยครับ", False)
        '    Exit Sub
        'ElseIf My.Settings.SECS_Enable = True Then
        '    If My.Settings.MCType = "Canon-D10R" Then
        '        If m_Equipment.EQStatusCanon <> EquipmentStateCanon.IDEL Then
        '            m_frmWarningDialog("สถานะเครื่องจักรตอนนี้ไม่พร้อมใช้งาน กรุณาตรวจสอบ", False)
        '            Exit Sub
        '        End If
        '    End If
        'End If


        '_WhenInputDataAlready = False
        '_WhenPreeSetUpButton = False

        'If QRWorkingSlipInputInitailCheck(True) = True Then
        '    Dim frminput As New frmdisplayinput(Me)
        '    frminput.lbcaption.Text = "กรุณาสแกน QR Code"
        '    If frminput.ShowDialog = Windows.Forms.DialogResult.OK Then
        '        Matparameter()
        '        CellConTag = New CellConObj
        '        CellConTag = Para

        '        CellConTag.WaferLotNoFromDepyo = AllWaferLotNoFromDenpyo(CellConTag.LotID)
        '        CellConTag.WaferLotNoListSplited = WaferLotNoSliter(CellConTag.WaferLotNoFromDepyo)
        '        CellConTag.WaferNoList = FliterWaferNo(CellConTag.WaferLotID, CellConTag.WaferLotNoFromDepyo)

        '        If My.Settings.WaferMappingUse = True Then
        '            CopyWaferMap(CellConTag.WaferLotID)
        '        End If

        '        Try
        '            m_EmsClient.SetCurrentLot(My.Settings.EquipmentNo, CellConTag.LotID, 0)
        '        Catch ex As Exception
        '        End Try

        '        If My.Settings.SECS_Enable = False Then
        '            'LotStart
        '            CellConTag.LotStartTime = CDate(Format(Now, "yyyy/MM/dd HH:mm:ss"))

        '            Try
        '                m_EmsClient.SetCurrentLot(My.Settings.EquipmentNo, CellConTag.LotID, 0)
        '                m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Running", TmeCategory.NetOperationTime)
        '            Catch ex As Exception
        '            End Try

        '            SaveLotStartToDbx()
        '            LotSet(My.Settings.ProcessName & "-" & My.Settings.EquipmentNo, CellConTag.LotID, CellConTag.LotStartTime, CellConTag.OPID, CType(CellConTag.LSMode, RunModeType))
        '        Else 'Secs-gem
        '            DeleteAllFolder()
        '            If My.Settings.MCType = "2100HS" OrElse My.Settings.MCType = "2009SSI" Then
        '                S2F15_SetInputQty(CUInt(CellConTag.INPUTQty))
        '                If My.Settings.AutoLoad = False Then '2100HS,2009SSI
        '                    ReleaseMachine()
        '                    m_frmWarningDialog("Set up เรียบร้อย กรุณากด Start ก่อน Insp.", False, 60000)
        '                Else
        '                    RecipeCheck()
        '                End If
        '            ElseIf My.Settings.MCType = "Canon-D10R" Then 'Canon-D10R
        '                If My.Settings.AutoLoad = False Then
        '                    _WhenInputDataAlready = True
        '                    RemoteCMD_Remote()
        '                Else
        '                    RecipeCheck()
        '                End If
        '            End If
        '        End If

        '        WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
        '        WriteToXmlCellcon(CellconObjPath & "\" & "Recovery" & ".xml", CellConTag)  '170126 \783 CellconTag

        '        UpdateDispaly()
        '        UpdateDisplayMaterial()
        '    End If
        'Else
        '    m_frmWarningDialog(m_QRReadAlarm, False)
        'End If

    End Sub

    Sub ProductTime()
        Select Case CellConTag.StateCellCon
            Case CellConState.LotStart
                CellConTag.RunTimeCount += TimeSpan.FromSeconds(1)
                lbRuntime1024.Text = CellConTag.RunTimeCount.ToString
                lbRuntime1024.BackColor = Color.Lime
                lbAlarmTime1024.BackColor = Color.Honeydew
                lbStopTime1024.BackColor = Color.Honeydew

            Case CellConState.LotAlarm
                CellConTag.AlarmTimeCount += TimeSpan.FromSeconds(1)
                lbAlarmTime1024.Text = CellConTag.AlarmTimeCount.ToString
                lbRuntime1024.BackColor = Color.Honeydew
                lbAlarmTime1024.BackColor = Color.Yellow
                lbStopTime1024.BackColor = Color.Honeydew

            Case CellConState.LotStop
                CellConTag.StopTimeCount += TimeSpan.FromSeconds(1)

                lbStopTime1024.Text = CellConTag.StopTimeCount.ToString
                lbRuntime1024.BackColor = Color.Honeydew
                lbAlarmTime1024.BackColor = Color.Honeydew
                lbStopTime1024.BackColor = Color.Red

        End Select
    End Sub

    Public Sub UpdateDispaly()
        CellConTag.Process = My.Settings.ProcessName


        '1024x768
        lbMc1024.Text = My.Settings.ProcessName & "-" & My.Settings.EquipmentNo
        lbLotNo1024.Text = CellConTag.LotID
        lbpackage1024.Text = CellConTag.Package
        lbDevice1024.Text = CellConTag.DeviceName
        lbRecipe1024.Text = CellConTag.Recipe
        lbOP1024.Text = CellConTag.OPID
        lbInput1024.Text = CellConTag.INPUTQty.ToString
        'lbWaferNo.Text =CellConTag.WaferLotID 
        lbwaferLotNo1024.Text = CellConTag.WaferLotID
        lbGood1024.Text = CellConTag.TotalGoodPcs
        lbNg1024.Text = CellConTag.TotalNGPcs
        lbLotReqReply1024.Text = CellConTag.LRReply
        If CellConTag.LotStartTime <> Nothing Then
            lbStart1024.Text = Format(CellConTag.LotStartTime, "yyyy/MM/dd HH:mm:ss")
        Else
            lbStart1024.Text = ""
        End If

        If CellConTag.LotEndTime <> Nothing Then
            lbEnd1024.Text = Format(CellConTag.LotEndTime, "yyyy/MM/dd HH:mm:ss")
        Else
            lbEnd1024.Text = ""
        End If

        If CellConTag.Torinokoshi = True Then
            lbtorino.Visible = True
        Else
            lbtorino.Visible = False
        End If

        If CellConTag.LotStartTime <> Nothing AndAlso CellConTag.LotEndTime <> Nothing AndAlso CellConTag.OPCheck <> Nothing Then
            lbInput1024.Text = CStr(CellConTag.InputAdjust)
            lbGood1024.Text = CStr(CellConTag.GoodAdjust)
            lbNg1024.Text = CStr(CellConTag.NGAdjust)
        End If


        'Major Alarm
        lbalmPreform1024.Text = CellConTag.AlarmPreform.ToString
        lbalmBonder1024.Text = CellConTag.AlarmBonder.ToString
        lbalmFrameOut1024.Text = CellConTag.AlarmFrameOut.ToString
        lbalmPickup1024.Text = CellConTag.AlarmPickup.ToString
        lbalmBridge1024.Text = CellConTag.AlarmBridgeInsp.ToString
        lbalmPreformInsp1024.Text = CellConTag.AlarmPreformInsp.ToString
        lbAlmTotal1024.Text = CellConTag.TotalAlarm.ToString

        'Production time
        lbRuntime1024.Text = CellConTag.RunTimeCount.ToString
        lbStopTime1024.Text = CellConTag.StopTimeCount.ToString
        lbAlarmTime1024.Text = CellConTag.AlarmTimeCount.ToString
        lbWaferNo1024.Text = CellConTag.CurrentWaferID



        If (CellConTag.StateCellCon = CellConState.LotEnd Or CellConTag.StateCellCon = CellConState.LotClear) Then
            lbRuntime1024.BackColor = Color.Honeydew
            lbAlarmTime1024.BackColor = Color.Honeydew
            lbStopTime1024.BackColor = Color.Honeydew
        End If

        lbRubberCollet.Text = CellConTag.RubberColletID

        SqlDependencyFunction(CellConTag.LotID)

    End Sub

    Private Sub Matparameter()
        Para.FrameLotNo_1st = CellConTag.FrameLotNo_1st
        Para.FrameSeqNo_1st = CellConTag.FrameSeqNo_1st
        Para.FrameType_1st = CellConTag.FrameType_1st
        Para.FrameLotNo_2nd = CellConTag.FrameLotNo_2nd
        Para.FrameSeqNo_2nd = CellConTag.FrameSeqNo_2nd
        Para.FrameType_2nd = CellConTag.FrameType_2nd
        Para.PreformLotNo_1st = CellConTag.PreformLotNo_1st
        If CellConTag.PreforInputDate_1st <> Nothing Then
            Para.PreforInputDate_1st = CellConTag.PreforInputDate_1st
        End If
        If CellConTag.PreforExpireDate_1st <> Nothing Then
            Para.PreforExpireDate_1st = CellConTag.PreforExpireDate_1st
        End If
        Para.PreformQR_1st = CellConTag.PreformQR_1st
        Para.PreformType_1st = CellConTag.PreformType_1st
        Para.PreformLotNo_2nd = CellConTag.PreformLotNo_2nd
        If CellConTag.PreforInputDate_2nd <> Nothing Then
            Para.PreforInputDate_2nd = CellConTag.PreforInputDate_2nd
        End If
        If CellConTag.PreforExpireDate_2nd <> Nothing Then
            Para.PreforExpireDate_2nd = CellConTag.PreforExpireDate_2nd
        End If
        Para.PreformQR_2nd = CellConTag.PreformQR_2nd
        Para.PreformType_2nd = CellConTag.PreformType_2nd

        'Set parameter ครั้งแรก
        Para.AlarmBonder = 0
        Para.AlarmBridgeInsp = 0
        Para.AlarmFrameOut = 0
        Para.AlarmPickup = 0
        Para.AlarmPreform = 0
        Para.AlarmPreformInsp = 0

        Para.TotalGood = 0
        Para.TotalNG = 0

        Para.RunTimeCount = Nothing
        Para.AlarmTimeCount = Nothing
        Para.StopTimeCount = Nothing
        Para.TotalAlarm = 0
        Para.StateCellCon = CellConState.LotClear
    End Sub

    Private Sub SaveLotStartToDbx()
        Dim LotStartRow As DBxDataSet.DBDataRow = DBxDataSet.DBData.NewDBDataRow
        With LotStartRow
            .LotNo = CellConTag.LotID
            .LotStartTime = CellConTag.LotStartTime
            .MCNo = My.Settings.ProcessName & "-" & My.Settings.EquipmentNo
            .MCType = My.Settings.MCType
            .InputQty = CellConTag.INPUTQty
            .OPNo = CellConTag.OPID
            .NetVersion = NetVerSion

            'first Insp.
            .TsukaigeNeedNo = CellConTag.TsukaigeNeedNo
            .TsukaigeCheck = CellConTag.TsukaigeMode
            .TsukiageStrock = CellConTag.TsukaigePinStrock
            .RubberColletNo = CellConTag.RubberColletNo
            .RubberCondition = CellConTag.RubberMode
            .BlockCheck = CellConTag.BlockCheck

            .Chipsize1 = CellConTag.ChipSizeX
            .Chipsize2 = CellConTag.ChipSizeY

            If My.Settings.PasteType = False Then 'Solder
                .SolderThickness1 = CellConTag.PreformThickness1
                .SolderThickness2 = CellConTag.PreformThickness2
                .SolderThickness3 = CellConTag.PreformThickness3
                .SolderThickness4 = CellConTag.PreformThickness4
                .SolderThicknessAvg = CellConTag.PreformThicknessAverage
                .SolderThicknessR = CellConTag.PreformThicknessR
            Else 'Preform
                .PreformThickness1 = CellConTag.PreformThickness1
                .PreformThickness2 = CellConTag.PreformThickness2
                .PreformThickness3 = CellConTag.PreformThickness3
                .PreformThickness4 = CellConTag.PreformThickness4
                .PreformThicknessAvg = CellConTag.PreformThicknessAverage
                .PreformThicknessR = CellConTag.PreformThicknessR
                .PasteNozzleCond = CellConTag.PasteNozzleMode
                .PasteNozzleType = CellConTag.PasteNozzleType
                .PateNozzleNo = CellConTag.PastNozzleNo
            End If


            .FrameFirstLot = CellConTag.FrameLotNo_1st
            .FrameFirstSEQ_NO = CellConTag.FrameSeqNo_1st

            .PreformFirstLot = CellConTag.PreformLotNo_1st
            .PreformFirstID = CInt(CellConTag.PreformQR_1st)


            If CellConTag.PreformType_1st.Length > 10 Then
                .PreformType = CellConTag.PreformType_1st.Substring(0, 10)
            Else
                .PreformType = CellConTag.PreformType_1st
            End If

            If CellConTag.PreforExpireDate_1st <> Nothing Then
                .PreformFirstLiftTime = CellConTag.PreforExpireDate_1st
            End If

        End With
        DBxDataSet.DBData.Rows.Add(LotStartRow)

        Try
            For Each aDatarow As DBxDataSet.DBDataRow In DBxDataSet.DBData.Rows
                If aDatarow.LotNo = CellConTag.LotID AndAlso aDatarow.LotStartTime = CellConTag.LotStartTime Then
                    Try

                        If DbDataTableAdapter1.Update(aDatarow) <> 0 Then
                            m_frmWarningDialog("กรุณาทำ First Insp.", False, 100000)
                            Exit For
                        Else
                            m_frmWarningDialog("กรุณาInputใหม่อีกครั้ง", False)
                        End If

                    Catch ex As Exception
                        m_frmWarningDialog("กรุณาInputใหม่อีกครั้ง", False)
                    End Try
                End If
            Next


        Catch ex As Exception

        End Try

    End Sub


    Private Sub UpdateDisplayMaterial()

        If CellConTag.PreforExpireDate_2nd <> Nothing Then
            OprData.PreformExpDate2 = CDate(CellConTag.PreforExpireDate_2nd)
        Else
            OprData.PreformExpDate2 = Nothing
        End If

        '1024x768
        lbQRFrame1_1024.Text = CellConTag.FrameSeqNo_1st
        lbFrameLotNo1_1024.Text = CellConTag.FrameLotNo_1st
        lbFrameType1_1024.Text = CellConTag.FrameType_1st


        lbPreformLotNo1_1024.Text = CellConTag.PreformLotNo_1st
        If CellConTag.PreforInputDate_1st <> Nothing Then
            lbPreformInput1_1024.Text = Format(CellConTag.PreforInputDate_1st, "yyyy/MM/dd HH:mm:ss")
        Else
            lbPreformInput1_1024.Text = ""
        End If
        If CellConTag.PreforExpireDate_1st <> Nothing Then
            lbPreformExp1_1024.Text = Format(CellConTag.PreforExpireDate_1st, "yyyy/MM/dd HH:mm:ss")
        Else
            lbPreformExp1_1024.Text = ""
        End If

        If CellConTag.PreforExpireDate_1st <> Nothing Then
            OprData.PreformExpDate1 = CDate(CellConTag.PreforExpireDate_1st)
        Else
            OprData.PreformExpDate1 = Nothing
        End If

        lbPreformQR1_1024.Text = CellConTag.PreformQR_1st
        lbPreformType1_1024.Text = CellConTag.PreformType_1st


        lbQRFrame2_1024.Text = CellConTag.FrameSeqNo_2nd
        lbFrameLotNo2_1024.Text = CellConTag.FrameLotNo_2nd
        lbFrameType2_1024.Text = CellConTag.FrameType_2nd
        lbPreformLotNo2_1024.Text = CellConTag.PreformLotNo_2nd

        If CellConTag.PreforInputDate_2nd <> Nothing Then
            lbPreformInput2_1024.Text = Format(CellConTag.PreforInputDate_2nd, "yyyy/MM/dd HH:mm:ss")
        Else
            lbPreformInput2_1024.Text = ("")
        End If
        If CellConTag.PreforExpireDate_2nd <> Nothing Then
            lbPreformExp2_1024.Text = Format(CellConTag.PreforExpireDate_2nd, "yyyy/MM/dd HH:mm:ss")
        Else
            lbPreformExp2_1024.Text = ""
        End If

        If CellConTag.PreforExpireDate_2nd <> Nothing Then
            OprData.PreformExpDate2 = CDate(CellConTag.PreforExpireDate_2nd)
        Else
            OprData.PreformExpDate2 = Nothing
        End If
        lbPreformQR2_1024.Text = CellConTag.PreformQR_2nd
        lbPreformType2_1024.Text = CellConTag.PreformType_2nd

        lbCheckWeferLotNo.Text = CellConTag.WaferLotID


        'Tabcontrol Select a current material
        'Frame
        If CellConTag.FrameSeqNo_2nd <> Nothing Then
            tabFrame1024.SelectedIndex = 1
        Else
            tabFrame1024.SelectedIndex = 0
        End If

        If CellConTag.PreformQR_2nd <> Nothing Then
            tabPreform1024.SelectedIndex = 1
        Else
            tabPreform1024.SelectedIndex = 0
        End If


    End Sub



    '#####################################################################################################################################
    Private Sub FinalInspCompleted()

        Dim NewPara As New CellConObj
        With NewPara
            .PreformLotNo_1st = CellConTag.PreformLotNo_1st
            .PreforInputDate_1st = CellConTag.PreforInputDate_1st
            .PreforExpireDate_1st = CellConTag.PreforExpireDate_1st
            .PreformQR_1st = CellConTag.PreformQR_1st
            .PreformType_1st = CellConTag.PreformType_1st
            .PreformLotNo_2nd = CellConTag.PreformLotNo_2nd
            .PreforInputDate_2nd = CellConTag.PreforInputDate_2nd
            .PreforExpireDate_2nd = CellConTag.PreforExpireDate_2nd
            .PreformQR_2nd = CellConTag.PreformQR_2nd
            .PreformType_2nd = CellConTag.PreformType_2nd
            .StateCellCon = CellConState.LotClear

            .CurrentWaferLotID = CellConTag.CurrentWaferLotID
            .CurrentWaferID = CellConTag.CurrentWaferID
            .CurrentLotNo = CellConTag.CurrentLotNo

            .RubberColletID = CellConTag.RubberColletID
        End With

        CellConTag = New CellConObj
        CellConTag = NewPara

        UpdateDispaly()
        UpdateDisplayMaterial()


        DeleteAllFolder()


    End Sub

    Private Sub SqlDependencyFunction(ByVal lotNo As String)
        'Dim lotno As String = tbLotnO.Text
        'changeCount1 = 0
        'Me.Label2.Text = String.Format(statusMessage, changeCount)

        ' Remove any existing dependency connection, then create a new one.
        SqlDependency.Stop(My.Settings.SqlDepencyCon)
        SqlDependency.Start(My.Settings.SqlDepencyCon)

        If connection Is Nothing Then
            connection = New SqlConnection(My.Settings.SqlDepencyCon)
        End If

        'If command1 Is Nothing Then
        ' GetSQL is a local procedure that returns
        ' a paramaterized SQL string. You might want
        ' to use a stored procedure in your application.
        command = New SqlCommand(GetSQLTest(lotNo), connection)
        'End If


        If dataToWatch Is Nothing Then
            dataToWatch = New DataSet()
        End If

        GetData1()


    End Sub

    Private Function GetSQLTest(ByVal LotNo As String) As String
        '****** WARNING *****
        'http://www.codeproject.com/Articles/12335/Using-SqlDependency-for-data-change-events
        'If the query is not correct, an event will immediately be sent with:
        'EX: if statement is not include "dbo" the error will be notified
        'Return "SELECT ID,Name,Class,HP,SP,LVL,EXP,EXP_MAX FROM dbo.Charactor"
        'Return "SELECT RohmDate,ProcessName,OPRate,LoadTime FROM dbo.DailyProcessOperationRate WHERE ProcessName = 'DB'"
        If LotNo Is Nothing Then
            LotNo = ""
        End If
        Return "SELECT QCFirstLotMode, QCFinishLotMode,LotStartTime,LotEndTime FROM dbo.DBData WHERE Lotno = '" & LotNo & "' and MCNo = '" & My.Settings.ProcessName & "-" & My.Settings.EquipmentNo & "'"  'and LotStartTime = '" & Format(CellConTag.LotStartTime, "yyyy-MM-dd HH:mm:ss") & "'"
    End Function

    Private Sub GetData1()
        ' Empty the dataset so that there is only
        ' one batch worth of data displayed.
        dataToWatch.Clear()

        ' Make sure the command object does not already have
        ' a notification object associated with it.
        command.Notification = Nothing

        ' Create and bind the SqlDependency object
        ' to the command object.
        Dim dependency1 As New SqlDependency(command)
        AddHandler dependency1.OnChange, AddressOf dependency1_OnChange

        Using adapter As New SqlDataAdapter(command)
            adapter.Fill(dataToWatch, tableName1)

            Me.DataGridView2.DataSource = dataToWatch
            Me.DataGridView2.DataMember = tableName1

            If CellConTag.LotID <> "" OrElse CellConTag.LotID IsNot Nothing Then
                If DataGridView2.RowCount <> 0 Then
                    For i = 0 To DataGridView2.RowCount - 1
                        If CDate(DataGridView2.Item(2, i).Value) = CellConTag.LotStartTime Then
                            If DataGridView2.Item(1, i).Value IsNot DBNull.Value Then
                                If CStr(DataGridView2.Item(1, i).Value) <> "" Then 'FinalQC

                                    picFirst1024.Visible = False
                                    FinalInspCompleted()


                                    WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                                End If
                            ElseIf DataGridView2.Item(0, i).Value IsNot DBNull.Value Then
                                If CStr(DataGridView2.Item(0, i).Value) <> "" Then 'FirstQC

                                    picFirst1024.Visible = True

                                    If (CellConTag.LotStartTime = CDate(DataGridView2.Item(2, i).Value) And CellConTag.LotEndTime <> Nothing) And DataGridView2.Item(3, i).Value Is DBNull.Value And CellConTag.OPCheck <> Nothing Then
                                        SaveLotEndToDbx()
                                    ElseIf (CellConTag.LotStartTime = CDate(DataGridView2.Item(2, i).Value) And CellConTag.LotEndTime <> Nothing) And DataGridView2.Item(3, i).Value Is DBNull.Value And CellConTag.OPCheck = Nothing Then
                                        If m_flagLotEnd = False Then
                                            LotEndClick(Nothing, EventArgs.Empty)
                                        End If
                                    End If

                                End If
                            End If
                        End If
                    Next
                End If
            End If
        End Using

    End Sub
    Private Sub dependency1_OnChange(ByVal sender As Object, ByVal e As SqlNotificationEventArgs)

        ' This event will occur on a thread pool thread.
        ' It is illegal to update the UI from a worker thread
        ' The following code checks to see if it is safe
        ' update the UI.
        Dim i As ISynchronizeInvoke = CType(Me, ISynchronizeInvoke)

        ' If InvokeRequired returns True, the code
        ' is executing on a worker thread.
        If i.InvokeRequired Then
            ' Create a delegate to perform the thread switch
            Dim tempDelegate As New OnChangeEventHandler(AddressOf dependency1_OnChange)

            Dim args() As Object = {sender, e}

            ' Marshal the data from the worker thread
            ' to the UI thread.
            i.BeginInvoke(tempDelegate, args)

            Return
        End If

        ' Remove the handler since it's only good
        ' for a single notification
        Dim dependency1 As SqlDependency = CType(sender, SqlDependency)

        RemoveHandler dependency1.OnChange, AddressOf dependency1_OnChange

        ' At this point, the code is executing on the
        ' UI thread, so it is safe to update the UI.
        'changeCount1 += 1
        'Me.Label2.Text = String.Format(statusMessage, changeCount1)

        ' Add information from the event arguments to the list box
        ' for debugging purposes only.
        'With Me.ListBox2.Items
        '    .Clear()
        '    .Add("Info:   " & e.Info.ToString())
        '    .Add("Source: " & e.Source.ToString())
        '    .Add("Type:   " & e.Type.ToString())
        'End With


        If e.Type <> SqlNotificationType.Change Then
            Exit Sub
        End If





        ' Reload the dataset that's bound to the grid.
        GetData1()
    End Sub

    Private Sub WBWIPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WBWIPToolStripMenuItem.Click
        Try
            Call Shell("C:\Program Files\Internet Explorer\iexplore.exe http://webserv/WBWIP/CheckWBWIP.aspx?", AppWinStyle.NormalFocus)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lbTime.Text = Format(Now, "yyyy/MM/dd HH:mm:ss")

        If CellConTag.Torinokoshi = True Then
            lbtorino.Visible = True
        Else
            lbtorino.Visible = False
        End If

        If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
            ProductTime()
        End If


        If My.Settings.WaferMappingUse = True Then
            Dim dirFiles As String() = Directory.GetFiles(WaferMapDir)
            Dim dirfolderMap As String() = Directory.GetDirectories(WaferMapDir)
            If dirFiles.Count <> 0 Then
                pic1024x768.Image = My.Resources.T
                lbCheckState.Text = "OK"
            ElseIf dirfolderMap.Count <> 0 AndAlso My.Settings.MCType = "IDBW" Then
                pic1024x768.Image = My.Resources.T

                lbCheckState.Text = "OK"
            Else

                lbCheckState.Text = "NG"
            End If
        End If

        If Me.BackColor = Color.Red Then
            If My.Settings.MCType = "Canon-D10R" Then
                _reconSec += 1
                If _reconSec >= 20 Then
                    _reconSec = 0
                    MDIParent1.m_Host.Connect()
                End If
            End If
        End If
    End Sub

    Private Sub CalculatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalculatorToolStripMenuItem.Click
        Process.Start("C:\Windows\system32\calc.exe")
    End Sub

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
    End Sub


    Private Sub LotEnd(ByVal EQNo As String, ByVal LotNo As String, ByVal EndTime As Date, ByVal GoodPcs As Integer, ByVal NgPcs As Integer, ByVal OPID As String, Optional ByVal EndMode As EndModeType = EndModeType.Normal)
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

    End Sub

    Private Sub btnFrameQR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btPreform1024.Click

        Dim frmMat As New frmInputMaterial
        frmMat.lbCaption.Text = "Scan Preform QR Code"
        If frmMat.ShowDialog = Windows.Forms.DialogResult.OK Then
            If (CellConTag.LotStartTime <> Nothing AndAlso CellConTag.LotEndTime = Nothing) = True Then 'Running Preform2

                Dim preformType As String = frmMat.m_PreformType
                Dim workingSlip As New WorkingSlipQRCode
                workingSlip.SplitQRCode(CellConTag.QrData)

                If PackageDeviceComparePreform(preformType, workingSlip) = False Then
                    m_frmWarningDialog(m_QRReadAlarm, False)
                    Exit Sub
                End If

                CellConTag.PreforExpireDate_2nd = frmMat.m_PreformEXP
                CellConTag.PreforInputDate_2nd = frmMat.m_PreformInput
                CellConTag.PreformLotNo_2nd = frmMat.m_PreformLotNo
                CellConTag.PreformQR_2nd = frmMat.m_PreformQR
                CellConTag.PreformType_2nd = frmMat.m_PreformType

            Else           'New Lot  Preform1

                CellConTag.PreforExpireDate_1st = frmMat.m_PreformEXP
                CellConTag.PreforInputDate_1st = frmMat.m_PreformInput
                CellConTag.PreformLotNo_1st = frmMat.m_PreformLotNo
                CellConTag.PreformQR_1st = frmMat.m_PreformQR
                CellConTag.PreformType_1st = frmMat.m_PreformType

                CellConTag.PreforExpireDate_2nd = Nothing
                CellConTag.PreforInputDate_2nd = Nothing
                CellConTag.PreformLotNo_2nd = ""
                CellConTag.PreformLotNo_2nd = ""
                CellConTag.PreformQR_2nd = ""
                CellConTag.PreformType_2nd = ""

            End If
            UpdateDisplayMaterial()
            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
        End If
    End Sub

    Private Sub btnPreformQR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFrame1024.Click

        If CellConTag.LotStartTime = Nothing AndAlso CellConTag.LotEndTime <> Nothing AndAlso CellConTag.LotID = "" Then
            FinalInspCompleted()
        End If

        Dim frmMat As New frmInputMaterial
        frmMat.lbCaption.Text = "Scan Frame Bar Code"
        If frmMat.ShowDialog = Windows.Forms.DialogResult.OK Then
            If CellConTag.LotStartTime = Nothing AndAlso CellConTag.LotEndTime = Nothing Then 'frame 1
                CellConTag.FrameLotNo_1st = frmMat.m_FrameMarkerLotNo
                CellConTag.FrameSeqNo_1st = frmMat.m_FrameQR
                CellConTag.FrameType_1st = frmMat.m_FrameType

                CellConTag.FrameLotNo_2nd = ""
                CellConTag.FrameSeqNo_2nd = ""
                CellConTag.FrameType_2nd = ""
            ElseIf CellConTag.LotStartTime <> Nothing AndAlso CellConTag.LotEndTime = Nothing AndAlso CellConTag.LotID <> "" Then 'Frame2


                Dim frameLotNo As String = frmMat.m_FrameMarkerLotNo
                Dim frameQR As String = frmMat.m_FrameQR
                Dim frameType As String = frmMat.m_FrameType

                Dim MatClass As MaterialClass
                MatClass = CheckFrameCondition(frameLotNo, frameType, CellConTag.QrData)
                If MatClass.Pass = False Then
                    m_frmWarningDialog(MatClass.ErrMessage, False)
                    Exit Sub
                End If

                CellConTag.FrameLotNo_2nd = frameLotNo
                CellConTag.FrameSeqNo_2nd = frameQR
                CellConTag.FrameType_2nd = frameType
            End If
            UpdateDisplayMaterial()
            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
        End If

    End Sub

    Function CheckFrameCondition(ByVal FrameLotNo As String, ByVal FrameType As String, ByVal QRCode As String) As MaterialClass

        Dim matClass As New MaterialClass
        Dim WorkSlipQR As New WorkingSlipQRCode
        WorkSlipQR.SplitQRCode(QRCode)
        If FrameType <> WorkSlipQR.FrameType.ToUpper.Trim Then
            matClass.ErrMessage = "Material System FrameType:(" & FrameType & ") ไม่ตรงกับ Working Slip FrameType:(" & WorkSlipQR.FrameType.ToUpper.Trim & ")"
            matClass.Pass = False
            Return matClass
        End If

        matClass.Pass = True
        Return matClass
    End Function

    Function CheckPreformCondition() As MaterialClass

    End Function


    Private Sub RemoteCreateLot(ByVal LotNo As String)
        Dim S2F41 As New S2F41
        S2F41.RemoteCommand = "LOT-CREATE"
        S2F41.AddVariable("LotName", LotNo)

        MDIParent1.m_Host.Send(S2F41)

    End Sub

    Private Sub RecipeCheck()
        Dim S1F3 As New S1F3
        If My.Settings.MCType = "2100HS" Or My.Settings.MCType = "2009HS" Then
            S1F3.AddSvid(2009)
        ElseIf My.Settings.MCType = "Canon-D10R" Then 'Canon
            S1F3.AddSvid(1016)
        End If

        MDIParent1.m_Host.Send(S1F3)
    End Sub

    Public Sub RemotePP_SELECT(ByVal recipeName As String)
        Dim S2F41 As New S2F41
        If My.Settings.MCType = "2100HS" OrElse My.Settings.MCType = "2009SSI" Then
            S2F41.RemoteCommand = "PP-SELECT"
            S2F41.AddVariable("PPNAME", recipeName)
        ElseIf My.Settings.MCType = "Canon-D10R" Then
            S2F41.RemoteCommand = "PP-SELECT"
            S2F41.AddVariable("PPID", recipeName)
        End If
        MDIParent1.m_Host.Send(S2F41)
    End Sub

    Private Sub LockMachine()
        Dim s2f15 As New S2F15
        If My.Settings.MCType = "2100HS" Then
            s2f15.AddListEcid(151126402, "0", SecsFormat.U2)
        ElseIf My.Settings.MCType = "2009SSI" Then
            s2f15.AddListEcid(11780, "1", SecsFormat.U2)
        Else
            RemoteCMD_Remote()
            m_lock = True
            'RemoteCMD_Lock()
            Exit Sub
        End If
        MDIParent1.m_Host.Send(s2f15)
    End Sub

    Private Sub ReleaseMachine()

        Dim s2f15 As New S2F15
        If My.Settings.MCType = "2100HS" Then
            s2f15.AddListEcid(151126402, "1", SecsFormat.U2)
        ElseIf My.Settings.MCType = "2009SSI" Then
            s2f15.AddListEcid(11780, "2", SecsFormat.U2)
        Else
            RemoteCMD_Remote()
            m_release = True
            Exit Sub
        End If
        MDIParent1.m_Host.Send(s2f15)
    End Sub

    Private Sub Button6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        RemoteCreateLot(tbLotCreate.Text)
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        RecipeCheck()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        RemotePP_SELECT(tbRecipe.Text)
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click, btRelease.Click
        If InputBox("") = "005588" Then
            ReleaseMachine()
        End If
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click, btLock.Click
        If InputBox("") = "005588" Then
            LockMachine()
        End If
    End Sub

    Private Sub SaveAlarmInfoTable()
        Dim TableXMLPath As String = My.Application.Info.DirectoryPath & "\DBAlarmInfo.xml"
        Using sw As New StreamWriter(TableXMLPath)
            DBxDataSet.DBAlarmInfo.WriteXml(sw)
        End Using
    End Sub

    Private Sub LoadAlarmInfoTable()
        Try
            Dim TableXMLPath As String = My.Application.Info.DirectoryPath & "\DBAlarmInfo.xml"
            If File.Exists(TableXMLPath) = False Then
                Exit Sub
            End If
            DBxDataSet.DBAlarmInfo.ReadXml(TableXMLPath)

            dgvAlarmTable.DataSource = DBxDataSet.DBAlarmInfo
        Catch ex As Exception

        End Try
    End Sub


    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        If MessageBox.Show("คุณต้องการลบโฟล์เดอร์ Wafer LotNo ใน Cellcon หรือไม่", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            DeleteAllFolder()
        End If

    End Sub

    Private Sub DeleteAllFolder()

        Dim FilesPath As String() = Directory.GetFiles(WaferMapDir)
        If FilesPath.Count <> 0 Then
            For Each DataPath As String In FilesPath
                System.IO.File.Delete(DataPath)
            Next
        End If

        If My.Settings.MCType = "IDBW" Then
            Dim folderPath As String() = Directory.GetDirectories(WaferMapDir)
            If folderPath.Count <> 0 Then
                For Each strFolderPath As String In folderPath
                    Directory.Delete(strFolderPath, True)
                Next
            End If

            If My.Settings.CopyToMapDriverEnable = True Then
                Try
                    Dim folderZPath As String() = Directory.GetDirectories(My.Settings.CopyMappingToMapDriver)
                    If folderZPath.Count <> 0 Then
                        For Each strFolderPath As String In folderZPath
                            Directory.Delete(strFolderPath, True)
                        Next
                    End If
                Catch ex As Exception

                End Try
            End If

        End If

    End Sub


    Private Sub CopyWaferMap(ByVal waferLotNo As String)
        If waferLotNo = "" Then
            MsgBox("ไม่มี MAP")
            Exit Sub
        End If
        If Directory.Exists("\\172.16.0.100\WaferMapping\" & waferLotNo) = True Then
            If My.Settings.MCType = "IDBW" Then
                My.Computer.FileSystem.CopyDirectory("\\172.16.0.100\WaferMapping\" & waferLotNo, WaferMapDir & "\" & waferLotNo, True) '& "\" & waferLotNo
                If My.Settings.CopyToMapDriverEnable = True Then
                    Try
                        My.Computer.FileSystem.CopyDirectory("\\172.16.0.100\WaferMapping\" & waferLotNo, My.Settings.CopyMappingToMapDriver & "\" & waferLotNo, True) '& "\" & waferLotNo
                    Catch ex As Exception
                    End Try
                End If
            Else
                My.Computer.FileSystem.CopyDirectory("\\172.16.0.100\WaferMapping\" & waferLotNo, WaferMapDir, True) '& "\" & waferLotNo
            End If

        Else
            If My.Settings.MCType = "IDBW" Then
                Exit Sub
            End If
            m_frmWarningDialog("ไม่มี Wafer Map ในระบบ \\Zion\wafermapping กรุณาติดต่อผู้ที่เกี่ยวข้อง" & vbCrLf & "เครื่องจักรสามารถใช้งานได้ปกติ ถ้าไม่มีการโหลด MAP ผ่าน Cellcon", False, 60000)
        End If
    End Sub

    Function ConfirmLotEnd() As Boolean
        Dim frmfinal As New frmFinalInspection(Me)
        If frmfinal.ShowDialog() = Windows.Forms.DialogResult.OK Then

            SetParameterFrmFinalInsp(frmfinal)

            UpdateDispaly()
            UpdateDisplayMaterial()

            'LotEnd
            WriteToXmlCellcon(CellconObjPath & "\" & CellConTag.LotID & ".xml", CellConTag)
            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
            SaveLotEndToDbx()

            If frmfinal.c_EndMode = EndModeType.Normal Then
                LotEnd(My.Settings.ProcessName & "-" & My.Settings.EquipmentNo, CellConTag.LotID, CellConTag.LotEndTime, CellConTag.GoodAdjust, CellConTag.NGAdjust, CellConTag.OPID, EndModeType.Normal)
            ElseIf frmfinal.c_EndMode = EndModeType.AbnormalEndAccumulate Then
                LotEnd(My.Settings.ProcessName & "-" & My.Settings.EquipmentNo, CellConTag.LotID, CellConTag.LotEndTime, CellConTag.GoodAdjust, CellConTag.NGAdjust, CellConTag.OPID, EndModeType.AbnormalEndAccumulate)
            End If

            Return True
        End If

        Return False

    End Function

    Function NotConfirmFirstAndFinal() As Boolean
        Dim frmfinal As New frmFinalInspection(Me)
        If frmfinal.ShowDialog() = Windows.Forms.DialogResult.OK Then

            SetParameterFrmFinalInsp(frmfinal)

            UpdateDispaly()
            UpdateDisplayMaterial()

            'LotEnd
            WriteToXmlCellcon(CellconObjPath & "\" & CellConTag.LotID & ".xml", CellConTag)
            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)

            'SaveLotEndToDbx()

            LotEnd(My.Settings.ProcessName & "-" & My.Settings.EquipmentNo, CellConTag.LotID, CellConTag.LotEndTime, CellConTag.GoodAdjust, CellConTag.NGAdjust, CellConTag.OPID, EndModeType.Normal)
            Return True
        End If

        Return False

    End Function

    Private Sub LotEndIDBW(ByVal GoodDie As Integer)

        CellConTag.LotEndTime = CDate(Format(Now, "yyyy/MM/dd HH:mm:ss"))
        CellConTag.TotalGood = GoodDie
        Try
            m_EmsClient.SetOutput(My.Settings.EquipmentNo, CellConTag.TotalGood, CellConTag.TotalNG)
        Catch ex As Exception
        End Try

        If picFirst1024.Visible = True Then
            If ConfirmLotEnd() = False Then
                Exit Sub
            End If

            CellConTag.StateCellCon = CellConState.LotEnd
            m_frmWarningDialog("กรุณาทำ Final Insp.", False, 30000)
        Else
            NotConfirmFirstAndFinal()
            CellConTag.StateCellCon = CellConState.LotEnd
            m_frmWarningDialog("Lot End เรียบร้อยแล้ว ยังไม่ทำ First Insp. " & vbCrLf & "กรุณาทำ First Insp. ด้วยครับ ", False, 30000)
        End If

        Try
            m_EmsClient.SetOutput(My.Settings.EquipmentNo, CellConTag.GoodAdjust, CellConTag.NGAdjust)
            m_EmsClient.SetLotEnd(My.Settings.EquipmentNo)
            m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Stop", TmeCategory.StopLoss)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub LotEndSecsGem(ByVal GoodDie As Integer)

        CellConTag.LotEndTime = CDate(Format(Now, "yyyy/MM/dd HH:mm:ss"))
        CellConTag.TotalGood = GoodDie

        Try
            m_EmsClient.SetOutput(My.Settings.EquipmentNo, CellConTag.TotalGood, CellConTag.TotalNG)
        Catch ex As Exception
        End Try

        DBxDataSet.DBData.Rows.Clear()
        DbDataTableAdapter1.FillBy(DBxDataSet.DBData, CellConTag.LotID, CellConTag.LotStartTime)
        For Each strDataRow As DBxDataSet.DBDataRow In DBxDataSet.DBData.Rows
            If strDataRow.LotNo = CellConTag.LotID AndAlso strDataRow.LotStartTime = CellConTag.LotStartTime Then
                If strDataRow.IsQCFirstLotModeNull = True And strDataRow.IsQCFinishLotModeNull = True Then 'กรณีไม่ได้ทำ First แล้ว LotEnd
                    If NotConfirmFirstAndFinal() = True Then
                        CellConTag.StateCellCon = CellConState.LotEnd
                        m_frmWarningDialog("Lot End เรียบร้อยแล้ว ยังไม่ทำ First Insp. " & vbCrLf & "กรุณาทำ First Insp. ด้วยครับ ", False, 30000)
                    Else
                        If My.Settings.MCType <> "Canon-D10R" Then
                            CellConTag.StateCellCon = CellConState.LotEnd
                            m_frmWarningDialog("Lot End เรียบร้อยแต่ยังไม่ได้ยืนยัน จำนวนงาน,Alarm และFirst Insp. กรุณาทำ First Insp ก่อน", False, 30000)
                        Else 'Canon-D10R ไม่ได้ทำ LotEnd และ ไม่ได้ทำ First Final
                            CellConTag.LotEndTime = Nothing
                        End If
                    End If
                ElseIf strDataRow.IsQCFirstLotModeNull = False And strDataRow.IsQCFinishLotModeNull = True Then 'กรณีทำ First แล้ว LotEnd (ปกติ)
                    If ConfirmLotEnd() = True Then
                        CellConTag.StateCellCon = CellConState.LotEnd
                        m_frmWarningDialog("กรุณาทำ Final Insp.", False)
                    Else
                        If My.Settings.MCType = "Canon-D10R" OrElse My.Settings.MCType = "IDBW" Then
                            CellConTag.LotEndTime = Nothing
                        End If
                    End If
                End If
            End If
        Next

        SaveAlarmInfoToDBx()
        WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
        UpdateDispaly()

    End Sub


    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSet1024.Click


        If My.Settings.SECS_Enable = True AndAlso Me.BackColor = Color.Red Then 'ถ้าเป็น Secsgem จะต้องเช็คก่อนส่ง
            m_frmWarningDialog("กรุณเชื่อมต่อCellCon กับ M/C ด้วยครับ", False)
            Exit Sub
        End If

        If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then
            If My.Settings.MCType = "2100HS" OrElse My.Settings.MCType = "2009SSI" Then
                S2F15_SetInputQty(CUInt(CellConTag.INPUTQty))
                If My.Settings.AutoLoad = False Then '2100HS,2009SSI
                    ReleaseMachine()
                    m_frmWarningDialog("Set up เรียบร้อย", False, 60000)
                Else
                    RecipeCheck()
                End If
            ElseIf My.Settings.MCType = "Canon-D10R" Then 'Canon-D10R
                If My.Settings.AutoLoad = False Then
                    _WhenPreeSetUpButton = True
                    RemoteCMD_Remote()
                Else
                    RecipeCheck()
                End If
            End If
        End If
    End Sub

    Private Sub EQStatusChange(ByVal SVIDStatusChange As EquipmentStateEsec)
        lbEqState.Text = SVIDStatusChange.ToString
        Select Case SVIDStatusChange
            Case EquipmentStateEsec.Executing 'Run 2100HS
                If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then 'LotStartUp
                    LotStartup()
                    UpdateDispaly()
                    WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                ElseIf CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then 'Running
                    CellConTag.StateCellCon = CellConState.LotStart
                    Try
                        CellConTag.StateCellCon = CellConState.LotStart
                        m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Running", TmeCategory.NetOperationTime)
                    Catch ex As Exception
                    End Try
                End If
            Case EquipmentStateEsec.MaterialNotReady 'Alarm 2100HS
                If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                    CellConTag.StateCellCon = CellConState.LotAlarm
                    Try
                        CellConTag.StateCellCon = CellConState.LotAlarm
                        m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Alarm", TmeCategory.ChokotieLoss)
                    Catch ex As Exception
                    End Try
                End If
            Case EquipmentStateEsec.StoppedReady 'STOP 2100HS
                If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                    CellConTag.StateCellCon = CellConState.LotStop
                    Try
                        CellConTag.StateCellCon = CellConState.LotStop
                        m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Stop", TmeCategory.StopLoss)
                    Catch ex As Exception
                    End Try
                End If

            Case EquipmentStateEsec.MC_Executing 'Run 2009SSI 
                If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then 'LotStartUp
                    LotStartup()
                    UpdateDispaly()
                    WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                ElseIf CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                    CellConTag.StateCellCon = CellConState.LotStart
                    Try
                        CellConTag.StateCellCon = CellConState.LotStart
                        m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Running", TmeCategory.NetOperationTime)
                    Catch ex As Exception
                    End Try
                End If
            Case EquipmentStateEsec.Ready 'STOP 2009SSI
                If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                    Try
                        CellConTag.StateCellCon = CellConState.LotStop
                        m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Stop", TmeCategory.StopLoss)
                    Catch ex As Exception
                    End Try
                End If
            Case EquipmentStateEsec.NotReady 'ALarm 2009SSI
                If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                    Try
                        CellConTag.StateCellCon = CellConState.LotAlarm
                        m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Alarm", TmeCategory.ChokotieLoss)
                    Catch ex As Exception
                    End Try
                End If
        End Select

    End Sub

    Private Sub EQStatusChange(ByVal SVIDStatusChange As EquipmentStateCanon)
        lbEqState.Text = SVIDStatusChange.ToString
        Select Case SVIDStatusChange
            Case EquipmentStateCanon.EXECUTING
                If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then 'LotStartUp
                    LotStartup()
                    UpdateDispaly()
                    WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                ElseIf CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then 'Running+
                    CellConTag.StateCellCon = CellConState.LotStart
                    Try
                        CellConTag.StateCellCon = CellConState.LotStart
                        m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Running", TmeCategory.NetOperationTime)
                    Catch ex As Exception
                    End Try
                End If
            Case EquipmentStateCanon.TROUBLE
                If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                    Try
                        CellConTag.StateCellCon = CellConState.LotAlarm
                        m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Alarm", TmeCategory.ChokotieLoss)
                    Catch ex As Exception
                    End Try
                End If
            Case EquipmentStateCanon.IDEL
                If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                    Try
                        CellConTag.StateCellCon = CellConState.LotStop
                        m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Stop", TmeCategory.StopLoss)
                    Catch ex As Exception
                    End Try
                End If
        End Select

    End Sub


    Private Sub LotStartup()
        CellConTag.LotStartTime = CDate(Format(Now, "yyyy/MM/dd HH:mm:ss"))
        CellConTag.StateCellCon = CellConState.LotStart

        Try
            CellConTag.StateCellCon = CellConState.LotStart
            m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Running", TmeCategory.NetOperationTime)
        Catch ex As Exception
        End Try

        SaveLotStartToDbx()
        WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
        LotSet(My.Settings.ProcessName & "-" & My.Settings.EquipmentNo, CellConTag.LotID, CellConTag.LotStartTime, CellConTag.OPID, CType(CellConTag.LSMode, RunModeType))
    End Sub

    Public Sub RequestSVID_CurrentState()
        Dim s1f3 As New S1F3
        If My.Settings.MCType = "2100HS" Then
            s1f3.AddSvid(2031)
        ElseIf My.Settings.MCType = "2009SSI" Then
            s1f3.AddSvid(111634)
        ElseIf My.Settings.MCType = "Canon-D10R" Then 'Canon
            s1f3.AddSvid(1002)
        End If

        MDIParent1.Host.Send(s1f3)
    End Sub

    Public Sub RequestSVID_GoodDies()
        Dim s1f3 As New S1F3
        If My.Settings.MCType = "2100HS" Then
            s1f3.AddSvid(151126269)
        ElseIf My.Settings.MCType = "2009SSI" Then
            s1f3.AddSvid(110316)
        Else 'Canon
            s1f3.AddSvid(112)
        End If
        MDIParent1.Host.Send(s1f3)
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        RequestSVID_CurrentState()
    End Sub


    Private Sub CountGoodDies_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CountGoodDiesTimer.Tick
        If CellConTag.LotStartTime <> Nothing AndAlso CellConTag.LotEndTime = Nothing Then
            If Me.BackColor <> Color.Red Then
                RequestSVID_GoodDies()
            End If
        ElseIf CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing Then
            If Me.BackColor <> Color.Red And My.Settings.MCType = "Canon-D10R" Then
                RequestSVID_CurrentState()
            End If
        End If

        If CellConTag.LotStartTime = Nothing AndAlso CellConTag.LotID <> "" Then ' DB-ME-02 ไม่มี Event Status Change ส่งมาให้ Cellcon S6F11
            If Me.BackColor <> Color.Red Then
                If My.Settings.MCType <> "Canon-D10R" Then
                    RequestSVID_CurrentState()
                End If
            End If
        End If

    End Sub

    '############################################################## Map Download ###########################################################################
    'Wafer Host to Equipment 
    Private Delegate Sub m_S12F3Delegate(ByVal _S12F3 As S12F3)
    Private m_S12F3 As m_S12F3Delegate = New m_S12F3Delegate(AddressOf Perform_S12F3)

    Public Sub Perform_S12F3(ByVal request As S12F3)
        If Me.InvokeRequired Then
            Me.BeginInvoke(m_S12F3, request)
            Exit Sub
        End If

        If request.MID.Trim = "" Then 'อ่านไม่เจอ
            Dim Zero As New S12Deny(4)
            MDIParent1.Host.Reply(request, Zero)
            m_frmWarningDialog("เครื่องจักรไม่ได้ส่งข้อมูล Wafer No. กรุณาลองใหม่อีกครั้ง", False)
            Exit Sub
        End If


        Dim EquipmentWaferLotNo As String() = request.MID.Split(CChar("-"))
        Dim CellconWaferLotNo As String()
        Dim PrefixWaferNO As String = ""
        Try
            CellconWaferLotNo = CellConTag.WaferLotID.Split(CChar("-"))
            If CellconWaferLotNo.Count = 2 Then
                PrefixWaferNO = CellconWaferLotNo(1) 'เอาเฉพาะคำหน้า Ex. 1WD6V-2307H, 1WD6V-2307
                If PrefixWaferNO.Length > 4 Then '2307H
                    PrefixWaferNO = PrefixWaferNO.Substring(0, 4) '2307
                End If
            Else 'ผิดฟอแมต
                Dim Zero As New S12Deny(4)
                MDIParent1.Host.Reply(request, Zero)
                m_frmWarningDialog("WaferLotNo รูปแบบข้อมูลที่ส่งให้ CellCon Ex.XXXX-XX ไม่ถูกต้องกรุณาตรวจสอบ", False)
                Exit Sub
            End If

            If request.MID.Length <> 7 Then
                Dim Zero As New S12Deny(4)
                MDIParent1.Host.Reply(request, Zero)
                m_frmWarningDialog("MC:WaferLotNo " & request.MID & " ไม่เท่่ากับ 7 ตัวอักษร Ex.XXXX-XX กรุณาตรวจสอบ !", False)
                Exit Sub
            End If

            'เงื่อนไขการตรวจสอบ Wafer Lot No มีข้อมูลใน เครื่องไหม
            'เปรียบเทียบกันระหว่าง WaferLotNo จาก Working Slip กับ Eq
            Dim prefixMidEq As String = request.MID.Substring(0, 4)
            If PrefixWaferNO <> prefixMidEq Then 'เชค Wafer Map ที่ได้จาก MC (MID) มาเชคใน WaferNo Plan
                Dim Zero As New S12Deny(4)
                MDIParent1.Host.Reply(request, Zero)
                m_frmWarningDialog("CellCon WaferLotNo " & CellConTag.WaferLotID & " ไม่สามารถใช้ MC:WaferNo " & request.MID & " กรุณาตรวจสอบ", False)
                Exit Sub
            End If

        Catch ex As Exception

        End Try
        '================================================================

        Dim s12f4 As New S12F4()


        Dim PathMapFolder As String = WaferMapDir '& "\" & lbWaferLotNo.Text 'Original
        Dim intRotation As Integer = request.FNLOC
        Dim NULBC As String = request.NULBC
        Dim BCEQU As String = request.BCEQU 'Good '"." = pick up

        'Select Case intRotation
        '    Case 0
        '        intRotation = 0 '270 '90
        '    Case 90
        '        intRotation = 90 '0
        '    Case 180
        '        intRotation = 180 '90
        '    Case 270
        '        intRotation = 270 '180
        'End Select
        If My.Settings.MCType = "2100HS" Then
            intRotation = 180
        ElseIf My.Settings.MCType = "2009SSI" Then
            'intRotation = CInt(tbrotate.Text)
            'ElseIf My.Settings.MCType = "Canon-D10R" Then
            '    NULBC = "X"
        End If


        Dim countFiles As String() = Directory.GetFiles(WaferMapDir) 'ไม่มี Map ในโฟลเดอ
        If countFiles.Count = 0 Then
            Dim Zero As New S12Deny(4)
            MDIParent1.Host.Reply(request, Zero)
            m_frmWarningDialog("ไม่มีไฟล์ Wafer LotNo :" & CellConTag.WaferLotID & vbCrLf & "ในระบบ \\zion\WaferMapping กรุณาตรวจสอบหรือติดต่อผู้ที่เกี่ยวข้่อง", False)
            Exit Sub
        End If

        Dim DataSplit1 As String() = request.MID.Split(CChar("-"))
        WaferMap = RohmMapConvert.Read(PathMapFolder, intRotation, NULBC, BCEQU, "M", CInt(DataSplit1(1)), CInt(request.ORLOC))


        Dim shortROWCT As UShort = CUShort(WaferMap.ROWCT)
        Dim shortCOLCT As UShort = CUShort(WaferMap.COLCT)
        Dim intPRDCT As UInteger = CUInt(WaferMap.PRDCT)
        Dim PointREEP As New List(Of Point)
        Dim refpoint As Point = WaferMap.REFP



        PointREEP.Add(refpoint)

        s12f4.SetS12F4_Esec(request.MID, request.IDTYP, request.FNLOC, CByte(TextBox4.Text), PointREEP, shortROWCT, shortCOLCT, intPRDCT, BCEQU, NULBC)
        's12f4.SetS12F4_Esec(request.MID, request.IDTYP, CUShort(intRotation), request.ORLOC, PointREEP, shortROWCT, shortCOLCT, intPRDCT, BCEQU, NULBC)
        MDIParent1.Host.Reply(request, s12f4)

    End Sub


    Public Sub Perform_S12F3(ByVal request As S12F3CanonDownload)
        If Me.InvokeRequired Then
            Me.BeginInvoke(m_S12F3, request)
            Exit Sub
        End If

        If request.MID.Trim = "" Then 'อ่านไม่เจอ
            Dim Zero As New S12Deny(4)
            MDIParent1.Host.Reply(request, Zero)
            m_frmWarningDialog("เครื่องจักรไม่ได้ส่งข้อมูล Wafer No. กรุณาลองใหม่อีกครั้ง", False)
            Exit Sub
        End If

        Dim EquipmentWaferLotNo As String() = request.MID.Split(CChar("-"))
        Dim CellconWaferLotNo As String()
        Dim PrefixWaferNO As String = ""
        Try
            CellconWaferLotNo = CellConTag.WaferLotID.Split(CChar("-"))
            If CellconWaferLotNo.Count = 2 Then
                PrefixWaferNO = CellconWaferLotNo(1) 'เอาเฉพาะคำหน้า Ex. 1WD6V-2307H, 1WD6V-2307
                If PrefixWaferNO.Length > 4 Then '2307H
                    PrefixWaferNO = PrefixWaferNO.Substring(0, 4) '2307
                End If
            Else 'ผิดฟอแมต
                Dim Zero As New S12Deny(4)
                MDIParent1.Host.Reply(request, Zero)
                m_frmWarningDialog("WaferLotNo รูปแบบข้อมูลที่ส่งให้ CellCon Ex.XXXX-XX ไม่ถูกต้องกรุณาตรวจสอบ", False)
                Exit Sub
            End If

            'เงื่อนไขการตรวจสอบ Wafer Lot No มีข้อมูลใน เครื่องไหม
            'เปรียบเทียบกันระหว่าง WaferLotNo จาก Working Slip กับ Eq
            If request.MID.Length <> 7 Then
                Dim Zero As New S12Deny(4)
                MDIParent1.Host.Reply(request, Zero)
                m_frmWarningDialog("MC:WaferLotNo " & request.MID & " ไม่เท่่ากับ 7 ตัวอักษร Ex.XXXX-XX กรุณาตรวจสอบ !", False)
                Exit Sub
            End If

            Dim prefixMidEq As String = request.MID.Substring(0, 4)
            If PrefixWaferNO <> prefixMidEq Then 'เชค Wafer Map ที่ได้จาก MC (MID) มาเชคใน WaferNo Plan
                Dim Zero As New S12Deny(4)
                MDIParent1.Host.Reply(request, Zero)
                m_frmWarningDialog("CellCon WaferLotNo " & CellConTag.WaferLotID & " ไม่สามารถใช้ MC:WaferNo " & request.MID & " กรุณาตรวจสอบ", False)
                Exit Sub
            End If

        Catch ex As Exception

        End Try

        '===================================================== Download Backup =================================================================


        Dim s12f4 As New S12F4CanonDownload()
        If My.Settings.DownloadMapBackup = True Then
            If MapUpload(request.MID, CellConTag.WaferLotID) = True Then
                s12f4.SetS12F4Canon(m_MapUpload.MID, m_MapUpload.IDTYP, m_MapUpload.FNLOC, m_MapUpload.ORLOC, m_MapUpload.Reep, m_MapUpload.ROWCT, m_MapUpload.COLCT, m_MapUpload.PRDCT, "G", m_MapUpload.NULBC)
                MDIParent1.Host.Reply(request, s12f4)
                Exit Sub
            End If
        End If
        '============================================'============================================'============================================'============================================

        Dim PathMapFolder As String = WaferMapDir '& "\" & lbWaferLotNo.Text 'Original
        Dim intRotation As Integer = request.FNLOC
        Dim NULBC As String = "X"
        Dim BCEQU As String = request.BCEQU 'Good '"." = pick up

        'Select Case intRotation
        '    Case 0
        '        intRotation = 0 '270 '90
        '    Case 90
        '        intRotation = 90 '0
        '    Case 180
        '        intRotation = 180 '90
        '    Case 270
        '        intRotation = 270 '180
        'End Select


        Dim countFiles As String() = Directory.GetFiles(WaferMapDir) 'ไม่มี Map ในโฟลเดอ
        If countFiles.Count = 0 Then
            Dim Zero As New S12Deny(4)
            MDIParent1.Host.Reply(request, Zero)
            'm_frmWarningDialog("ไม่มีไฟล์ Wafer LotNo :" & lbWaferLotNo.Text & vbCrLf & "ในระบบ \\zion\WaferMapping กรุณาตรวจสอบหรือติดต่อผู้ที่เกี่ยวข้่อง", False)
            Exit Sub
        End If

        Dim DataSplit1 As String() = request.MID.Split(CChar("-"))
        WaferMap = RohmMapConvert.Read(PathMapFolder, intRotation, NULBC, BCEQU, "M", CInt(DataSplit1(1)))


        Dim shortROWCT As UShort = CUShort(WaferMap.ROWCT)
        Dim shortCOLCT As UShort = CUShort(WaferMap.COLCT)
        Dim intPRDCT As UInteger = CUInt(WaferMap.PRDCT)
        Dim PointREEP As New List(Of Point)
        'PointREEP.Add(WaferMap.REFP)
        Dim refpoint As Point = WaferMap.REFP

        PointREEP.Add(refpoint)

        s12f4.SetS12F4Canon(request.MID, request.IDTYP, request.FNLOC, request.ORLOC, PointREEP, shortROWCT, shortCOLCT, intPRDCT, BCEQU, NULBC)
        MDIParent1.Host.Reply(request, s12f4)

    End Sub



    Private Delegate Sub m_S12F15Delegate(ByVal _S12F15 As S12F15)
    Private m_S12F15 As m_S12F15Delegate = New m_S12F15Delegate(AddressOf Perform_S12F15)
    Public Sub Perform_S12F15(ByVal request As S12F15)
        If Me.InvokeRequired Then
            Me.BeginInvoke(m_S12F15, request)
            Exit Sub
        End If

        Dim mS12F16 As New S12F16()


        'เงื่อนไขการตรวจสอบ Wafer Lot No มีข้อมูลใน เครื่องไหม
        Try
            If CellConTag.WaferNoList.Contains(request.MID) = False Then 'เชค Wafer Map ที่ได้จาก MC (MID) มาเชคใน WaferNo Plan
                'Dim Zero As New S12Deny(4)
                'MDIParent1.Host.Reply(request, Zero)
                'Exit Sub
            End If
        Catch ex As Exception

        End Try
        mS12F16.SetS12F16_Esec(request.MID, request.IDTYP, WaferMap.BINLT)
        MDIParent1.Host.Reply(request, mS12F16)


    End Sub

    Public Sub Perform_S12F15(ByVal request As S12F15CanonDownload)
        If Me.InvokeRequired Then
            Me.BeginInvoke(m_S12F15, request)
            Exit Sub
        End If

        Dim mS12F16 As New S12F16()
        If My.Settings.DownloadMapBackup = True Then
            If MapUpload(request.MID, CellConTag.WaferLotID) = True Then
                mS12F16.SetS12F16_Esec(m_MapUpload.MID, m_MapUpload.IDTYP, m_MapUpload.BINLT)
                MDIParent1.Host.Reply(request, mS12F16)
                UpdateDisplayWaferID(m_MapUpload.MID)
                Exit Sub
            End If
        End If
        'เงื่อนไขการตรวจสอบ Wafer Lot No มีข้อมูลใน เครื่องไหม
        Try

            If CellConTag.WaferNoList.Contains(request.MID) = False Then 'เชค Wafer Map ที่ได้จาก MC (MID) มาเชคใน WaferNo Plan
                'Dim Zero As New S12Deny(4)
                'MDIParent1.Host.Reply(request, Zero)
                'Exit Sub
            End If

        Catch ex As Exception

        End Try
        mS12F16.SetS12F16_Esec(request.MID, request.IDTYP, WaferMap.BINLT)
        MDIParent1.Host.Reply(request, mS12F16)

        UpdateDisplayWaferID(request.MID)

    End Sub


    '################################################################################################################################
    'Map Uploade
    Private Sub PreformS12F1(ByVal request As S12F1Esec)
        m_MapUpload = New MapUploadParameter
        m_MapUpload.MID = request.MID
        m_MapUpload.IDTYP = request.IDTYP
        m_MapUpload.FNLOC = request.FNLOC
        m_MapUpload.FFROT = request.FFROT
        m_MapUpload.ORLOC = request.ORLOC
        m_MapUpload.RPSEL = request.RPSEL
        m_MapUpload.Reep = request.Reep
        m_MapUpload.DUTMS = request.DUTMS
        m_MapUpload.XDIES = request.XDIES
        m_MapUpload.YDIES = request.YDIES
        m_MapUpload.ROWCT = request.ROWCT
        m_MapUpload.COLCT = request.COLCT
        m_MapUpload.NULBC = request.NULBC
        m_MapUpload.PRDCT = request.PRDCT
        m_MapUpload.PRAXI = request.PRAXI

        Dim s12f2 As New S12F2
        MDIParent1.Host.Reply(request, s12f2)
    End Sub

    Private Sub PreformS12F1(ByVal request As S12F1CanonUpload)
        m_MapUpload = New MapUploadParameter
        m_MapUpload.MID = request.MID
        m_MapUpload.IDTYP = request.IDTYP
        m_MapUpload.FNLOC = request.FNLOC
        m_MapUpload.FFROT = request.FFROT
        m_MapUpload.ORLOC = request.ORLOC
        m_MapUpload.RPSEL = request.RPSEL
        m_MapUpload.Reep = request.Reep
        m_MapUpload.DUTMS = request.DUTMS
        m_MapUpload.XDIES = request.XDIES
        m_MapUpload.YDIES = request.YDIES
        m_MapUpload.ROWCT = request.ROWCT
        m_MapUpload.COLCT = request.COLCT
        m_MapUpload.NULBC = request.NULBC
        m_MapUpload.PRDCT = request.PRDCT
        m_MapUpload.PRAXI = request.PRAXI

        Dim s12f2 As New S12F2CanonUpload
        MDIParent1.Host.Reply(request, s12f2)
    End Sub

    Public Sub Perform_S12F5(ByVal request As S12F5Esec)

        m_MapUpload.MID() = request.MID
        m_MapUpload.IDTYP = request.IDTYP
        m_MapUpload.MAPFT = request.MAPFT
        m_MapUpload.MLCL() = request.MLCL

        Dim s12f6 As New S12f6
        MDIParent1.Host.Reply(request, s12f6)
    End Sub

    Public Sub Perform_S12F5(ByVal request As S12F5CanonUpload)

        m_MapUpload.MID() = request.MID
        m_MapUpload.IDTYP = request.IDTYP
        m_MapUpload.MAPFT = request.MAPFT
        m_MapUpload.MLCL() = request.MLCL

        Dim s12f6 As New S12f6CanonUpload
        MDIParent1.Host.Reply(request, s12f6)
    End Sub


    Private Sub Perform_S12F9(ByVal request As S12F9)
        m_MapUpload.MID = request.MID
        m_MapUpload.IDTYP = request.IDTYP
        m_MapUpload.STRP = request.STRP
        m_MapUpload.BINLT() = request.BINLT

        Dim S12F10 As S12F10 = New S12F10()
        MDIParent1.Host.Reply(request, S12F10)

        'SaveMapUpload(m_MapUpload)
    End Sub

    Private Sub Perform_S12F9(ByVal request As S12F9CanonUpload)
        m_MapUpload.MID = request.MID
        m_MapUpload.IDTYP = request.IDTYP
        m_MapUpload.STRP = request.STRP
        m_MapUpload.BINLT() = request.BINLT

        Dim S12F10 As S12F10CanonUpload = New S12F10CanonUpload()
        MDIParent1.Host.Reply(request, S12F10)


        'SaveMapUpload
        If CellConTag.CurrentLotNo <> "" Then
            m_MapUpload.LotNo = CellConTag.CurrentLotNo
        End If

        m_MapUpload.WaferLotNo = CellConTag.CurrentWaferLotID
        m_MapUpload.WaferNo = CellConTag.CurrentWaferID
        SaveMapUpload(m_MapUpload)
    End Sub



    Function GetWaferNoFromDenpyo(ByVal _LotNo As String, ByVal _waferLotNo As String) As List(Of String)
        Dim ret As List(Of String) = New List(Of String)
        Dim WaferLotSplit As String() = _waferLotNo.Split(CChar("-"))
        Dim APQuery As New DBxDataSetTableAdapters.QueriesTableAdapter1
        Dim WaferLotNo As String = APQuery.WaferLotNoPlan(_LotNo)
        Try
            If WaferLotNo <> Nothing Then
                WaferLotNo = WaferLotNo.Replace(_waferLotNo, "").Trim
                WaferLotNo = WaferLotNo.Replace("*", "").Trim
                For Each strData As String In WaferLotNo.Split(CChar(" "))
                    If IsNumeric(strData) = True Then
                        ret.Add(WaferLotSplit(1) & "-" & strData)
                    End If
                Next
            Else
                ret = Nothing
            End If
            Return ret
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Private Sub SaveMapUpload(ByVal mapData As MapUploadParameter)
        Dim stripTable As DBxDataSet.DBMapDataDataTable
        Dim stripMapDataRow As DBxDataSet.DBMapDataRow

        Dim stripAP As New DBxDataSetTableAdapters.DBMapDataTableAdapter
        stripTable = stripAP.GetSearchMapData(mapData.WaferLotNo, mapData.WaferNo)

        If stripTable.Count <> 0 Then  'ค้นหาแล้วเซฟทับ
            stripMapDataRow = CType(stripTable.Rows(0), CellController.DBxDataSet.DBMapDataRow)
            With stripMapDataRow
                .McNo = mapData.McNo
                '.WaferLotNo = mapData.WaferLotNo มีอยู่แล้ว
                '.WaferNo = mapData.WaferNo  มีอยู่แล้ว
                .LotNo = mapData.LotNo
                .UpdateTime = Now
                .MapData = ConvertMapDataToByteArray(mapData)
            End With
        Else 'เซฟใหม่
            stripMapDataRow = stripTable.NewDBMapDataRow
            With stripMapDataRow
                .McNo = mapData.McNo
                .WaferLotNo = mapData.WaferLotNo
                .WaferNo = mapData.WaferNo
                .LotNo = mapData.LotNo
                .UpdateTime = Now
                .MapData = ConvertMapDataToByteArray(mapData)
            End With
            stripTable.Rows.Add(stripMapDataRow)
        End If
        stripAP.Update(stripTable)
    End Sub

    Function MapUpload(ByVal WaferLotNo As String, ByVal WaferID As String) As Boolean
        Dim mapDataPara As New MapUploadParameter
        Dim apStripMap As New DBxDataSetTableAdapters.DBMapDataTableAdapter
        Dim getMapDataFromDB As DBxDataSet.DBMapDataDataTable = apStripMap.GetSearchMapData(WaferLotNo, WaferID)
        If getMapDataFromDB.Count <> 0 Then 'ถ้ามีข้อมูลใน Database ให้ดึงมาใช้งาน   ถ้าไม่มีให้ดึงแผ่นเต็ม
            Dim mapDataRow As DBxDataSet.DBMapDataRow = CType(getMapDataFromDB.Rows(0), CellController.DBxDataSet.DBMapDataRow)
            mapDataPara = ConvertByteArrayToMapData(mapDataRow.MapData)
            Return True
        End If

        Return False

    End Function




    Function ConvertMapDataToByteArray(ByVal MapData As MapUploadParameter) As Byte() 'SaveMap
        'SaveXML ก่อน
        Dim XmlFile As FileStream = New FileStream(c_MapUploadXmlPath, FileMode.Create)
        Dim serialize As XmlSerializer = New XmlSerializer(MapData.GetType)
        serialize.Serialize(XmlFile, MapData)
        XmlFile.Close()

        Return File.ReadAllBytes(c_MapUploadXmlPath)

    End Function
    Function ConvertByteArrayToMapData(ByVal arrBytes As Byte()) As MapUploadParameter 'LoadMap
        'Convert Bytes to XML
        File.WriteAllBytes(c_MapUploadXmlPath, arrBytes)

        'Convert XML to MapPara
        Dim mapPara As MapUploadParameter
        Using XmlFile As FileStream = New FileStream(c_MapUploadXmlPath, FileMode.Open)
            Dim serialize As XmlSerializer = New XmlSerializer(GetType(MapUploadParameter))
            mapPara = CType(serialize.Deserialize(XmlFile), MapUploadParameter)
            XmlFile.Close()
        End Using

        Return mapPara
    End Function





    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Process.Start(WaferMapDir)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        If MessageBox.Show("คุณต้องการ Copy File Wafer Lotno : " & CellConTag.WaferLotID & " จาก Zion\wafermapping หรือไม่", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            DeleteAllFolder()
            CopyWaferMap(CellConTag.WaferLotID)
        End If

    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        If InputBox("") = "005588" Then
            DeleteAllFolder()
            FinalInspCompleted()
            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
        End If
    End Sub

    Private Sub S2F15_SetInputQty(ByVal input As UInt32)
        Dim msg As S2F15 = New S2F15()
        If My.Settings.MCType = "2100HS" Then
            msg.AddListEcid(151126095, CStr(input), SecsFormat.U4) 'Set จำนวนงาน
        ElseIf My.Settings.MCType = "2009SSI" Then
            msg.AddListEcid(10311, CStr(input), SecsFormat.U4) 'Set จำนวนงาน
        ElseIf My.Settings.MCType = "Canon-D10R" Then
            msg.AddListEcid(102, CStr(input), SecsFormat.U4) 'Set จำนวน Frame
        End If
        MDIParent1.Host.Send(msg)
    End Sub

    Private Sub LoadTorinokoshiTable()
        Dim filepath As String = My.Application.Info.DirectoryPath & "\TorinokoshiTable.xml"
        If File.Exists(filepath) <> True Then
            Exit Sub
        End If

        Using sr As New StreamReader(filepath)
            DBxDataSet.TorinokoshiPackage.ReadXml(sr)
        End Using

    End Sub

    Private Sub RemoteCMD_Start()
        If My.Settings.MCType = "Canon-D10R" Then
            Try
                Dim RcmdGORemote As S2F41 = New S2F41
                RcmdGORemote.RemoteCommand = "START"
                MDIParent1.Host.Send(RcmdGORemote)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub RemoteCMD_STOP()
        If My.Settings.MCType = "Canon-D10R" Then
            Try
                Dim RcmdGORemote As S2F41 = New S2F41
                RcmdGORemote.RemoteCommand = "STOP"
                MDIParent1.Host.Send(RcmdGORemote)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub RemoteCMD_LotPlan(ByVal OP As String, ByVal LotNo As String, ByVal Input As UInteger)
        Dim RcmdGORemote As S2F41 = New S2F41
        RcmdGORemote.RemoteCommand = "LOTPLAN"
        RcmdGORemote.AddVariable("OPERATOR ID", OP)
        RcmdGORemote.AddVariable("LOT ID", LotNo)
        RcmdGORemote.AddVariable("LOT QUANTITY", Input)
        MDIParent1.Host.Send(RcmdGORemote)
    End Sub

    Private Sub RemoteCMD_Release() 'CanonD10R
        Dim RcmdGORemote As S2F41 = New S2F41
        RcmdGORemote.RemoteCommand = "RELEASE"
        MDIParent1.Host.Send(RcmdGORemote)
    End Sub

    Private Sub RemoteCMD_Lock() 'CanonD10R
        Dim RcmdGORemote As S2F41 = New S2F41
        RcmdGORemote.RemoteCommand = "LOCK"
        MDIParent1.Host.Send(RcmdGORemote)
    End Sub

    Public Sub RemoteCMD_Remote()
        Dim RcmdGORemote As S2F41 = New S2F41
        RcmdGORemote.RemoteCommand = "REMOTE"
        MDIParent1.Host.Send(RcmdGORemote)
    End Sub

    Private Sub RemoteCMD_Local()
        Dim RcmdGORemote As S2F41 = New S2F41
        RcmdGORemote.RemoteCommand = "LOCAL"
        MDIParent1.Host.Send(RcmdGORemote)
    End Sub

    Function AllWaferLotNoFromDenpyo(ByVal lotno As String) As List(Of String)
        Dim ret As New List(Of String)
        Dim WaferTable As New DBxDataSet.WaferMapDataTable
        Dim apWafer As New DBxDataSetTableAdapters.WaferMapTableAdapter
        WaferTable = apWafer.GetData(lotno)

        If WaferTable.Rows.Count <> 0 Then
            For Each WaferLot As DBxDataSet.WaferMapRow In WaferTable
                Dim tmepWaferLot As String = ""
                If WaferLot.PERETTO_NO_1 <> "" Then
                    ret.Add(WaferLot.PERETTO_NO_1)
                End If

                If WaferLot.PERETTO_NO_2 <> "" Then
                    ret.Add(WaferLot.PERETTO_NO_2)
                End If

                If WaferLot.PERETTO_NO_3 <> "" Then
                    ret.Add(WaferLot.PERETTO_NO_3)
                End If

                If WaferLot.PERETTO_NO_4 <> "" Then
                    ret.Add(WaferLot.PERETTO_NO_4)
                End If

                If WaferLot.PERETTO_NO_5 <> "" Then
                    ret.Add(WaferLot.PERETTO_NO_5)
                End If

                If WaferLot.PERETTO_NO_6 <> "" Then
                    ret.Add(WaferLot.PERETTO_NO_6)
                End If

                If WaferLot.PERETTO_NO_7 <> "" Then
                    ret.Add(WaferLot.PERETTO_NO_7)
                End If

                If WaferLot.PERETTO_NO_8 <> "" Then
                    ret.Add(WaferLot.PERETTO_NO_8)
                End If

                If WaferLot.PERETTO_NO_9 <> "" Then
                    ret.Add(WaferLot.PERETTO_NO_9)
                End If

                If WaferLot.PERETTO_NO_10 <> "" Then
                    ret.Add(WaferLot.PERETTO_NO_10)
                End If

                If WaferLot.PERETTO_NO_11 <> "" Then
                    ret.Add(WaferLot.PERETTO_NO_11)
                End If

                If WaferLot.PERETTO_NO_12 <> "" Then
                    ret.Add(WaferLot.PERETTO_NO_12)
                End If
            Next
            Return ret
        Else
            Return Nothing

        End If

    End Function

    Function WaferLotNoSliter(ByVal GetWaferLotFromDenpyo As List(Of String)) As List(Of String)

        If GetWaferLotFromDenpyo Is Nothing Then
            Return Nothing
        End If

        Dim ret As New List(Of String)
        If GetWaferLotFromDenpyo.Count = 0 Then
            Return Nothing
        End If

        For Each strWaferLotNo As String In GetWaferLotFromDenpyo
            Dim strtemp() As String = strWaferLotNo.Split(CChar(" "))
            If ret.Contains(strtemp(0)) = False Then
                ret.Add(strtemp(0))
            End If
        Next

        Return ret

    End Function

    Function FliterWaferNo(ByVal SelectWaferLotno As String, ByVal waferlistdenpyo As List(Of String)) As List(Of String)
        If waferlistdenpyo Is Nothing Then
            Return Nothing
        End If

        Dim ret As New List(Of String)
        If waferlistdenpyo.Count = 0 Then
            Return Nothing
        ElseIf SelectWaferLotno.Contains("-") = False Then
            Return Nothing
        End If

        '12345-6789A
        Dim strDataSplit() As String = SelectWaferLotno.Split(CChar("-")) '12345,6789A
        Dim suffixWaferLotNo As String = strDataSplit(1).Substring(0, 4) '6789

        For Each strwaferNo As String In waferlistdenpyo
            If strwaferNo.Contains(SelectWaferLotno) = True Then '6HK8Z-1312A  *24#

                strwaferNo = strwaferNo.Replace("*", "") '6HK8Z-1312A  24#
                strwaferNo = strwaferNo.Replace("#", "") '6HK8Z-1312A  24
                Dim strtemp() As String = strwaferNo.Split(CChar(" ")) '6HK8Z-1312A, ,24

                For Each strData As String In strtemp
                    If IsNumeric(strData) = True Then
                        ret.Add(suffixWaferLotNo & "-" & strData)
                    End If
                Next

            End If
        Next

        Return ret

    End Function

    Private Sub lbwaferLotNo1024_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbwaferLotNo1024.Click
        If My.Settings.WaferMappingUse = False Then
            Exit Sub
        End If

        If CellConTag.WaferLotNoListSplited Is Nothing Then
            Exit Sub
        End If

        If CellConTag.WaferLotNoListSplited.Count = 0 Then
            m_frmWarningDialog("ไม่มี Waferr LotNo", False, 30000)
            Exit Sub
        ElseIf CellConTag.Torinokoshi = False Then
            m_frmWarningDialog("ไม่สามารถเปลี่ยน Wafer LotNoได้ กรุณาเปลี่ยนเป็นโหมด Torinokoshi !", False, 30000)
            Exit Sub
        End If


        Dim frmwaferselect As New frmSelectWaferLotNo(CellConTag.WaferLotID)
        If frmwaferselect.ShowDialog = Windows.Forms.DialogResult.OK Then

            CellConTag.WaferLotID = frmwaferselect.m_waferSelect
            CellConTag.WaferNoList = FliterWaferNo(CellConTag.WaferLotID, CellConTag.WaferLotNoFromDepyo)

            WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
            UpdateDispaly()

            DeleteAllFolder()
            CopyWaferMap(CellConTag.WaferLotID)

        End If
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        If InputBox("") = "005588" Then
            RemoteCMD_Remote()
        End If
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        If InputBox("") = "005588" Then
            RemoteCMD_Local()
        End If
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        If InputBox("") = "005588" Then
            RemoteCMD_LotPlan(tbOP.Text, tbLotNO.Text, CUInt(tbInput.Text))
        End If
    End Sub


    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        DeleteAllFolder()
        CopyWaferMap(TextBox2.Text)
    End Sub



    Private Sub Button30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button30.Click
        Try
            CellConTag.StateCellCon = CellConState.LotStart
            m_EmsClient.SetActivity(TextBox3.Text, "Running", TmeCategory.NetOperationTime)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button32.Click
        Try
            CellConTag.StateCellCon = CellConState.LotAlarm
            m_EmsClient.SetActivity(TextBox3.Text, "Alarm", TmeCategory.ChokotieLoss)
        Catch ex As Exception
        End Try
    End Sub


    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDefine.Click
        If MessageBox.Show("คุณต้องการ Define Report && Link Event ใช่ไหม ??", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            CurDefFlow = AutoDefineReportFlow.DeleteAllReport
            DeleteAllReport()
        End If
    End Sub

    Private Sub DeleteAllReport()
        Dim msg As S2F33 = New S2F33(CStr(0))
        msg.SetDeleteAllReport()
        MDIParent1.Host.Send(msg)
    End Sub


    Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btReco.Click
        If CellConTag.LotID = Nothing And CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing Then
            If MessageBox.Show("คุณต้องการ Recovery Data", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                CellConTag = ReadFromXmlCellcon(CellconObjPath & "\" & "Recovery" & ".xml")
                UpdateDispaly()
                UpdateDisplayMaterial()
            End If
        Else
            MsgBox("ไม่สามารถ recovery data ได้")
        End If
    End Sub

    Private Sub RecoTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecoTimer.Tick
        If CellConTag.LotID <> Nothing And CellConTag.LotStartTime <> Nothing Then
            WriteToXmlCellcon(CellconObjPath & "\" & "Recovery" & ".xml", CellConTag)  '170126 \783 CellconTag
        End If
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        If Button18.Text = "-" Then
            Button18.Text = "+"
        Else
            Button18.Text = "-"
        End If
    End Sub



#Region "######################## IDBW ################################"

    Private Sub SerialPortSendData(ByVal DataSend As String)
        If SerialPort1.IsOpen Then
            SerialPort1.Write(DataSend & Chr(13))
            Commlog("CellCon_Send : " & DataSend, My.Application.Info.DirectoryPath & "\LOG\Comm.log")
        Else
            Commlog("CellCon_Send : Canon send data becuase Serise isn't open" & DataSend, My.Application.Info.DirectoryPath & "\LOG\Comm.log")
        End If
    End Sub
    Function LELotInfo(ByVal strData As String) As LotInfomationEnd
        Dim GetDataInfo As New LotInfomationEnd
        Dim ArLRData() As String = strData.Split(CChar(","))
        Try
            With GetDataInfo
                .TotalGood = CInt(CLng("&H" & ArLRData(1)))
                .TotalNG = CInt(CLng("&H" & ArLRData(2)))
                .RunningTime = CStr(CLng("&H" & ArLRData(3)))
                .StopTime = CStr(CLng("&H" & ArLRData(4)))
                .AlarmTime = CStr(CLng("&H" & ArLRData(5)))
                .NoChip = CStr(CLng("&H" & ArLRData(6)))
                .AlmBrigdeInsp = CStr(CLng("&H" & ArLRData(7)))
                .AlmPickup = CStr(CLng("&H" & ArLRData(8)))
                .MTBF = CStr(CLng("&H" & ArLRData(9)) / 60)
                .MTTR = CStr(CLng("&H" & ArLRData(10)) / 60)
                .OPRate = CStr(CLng("&H" & ArLRData(11)))
                .RPM = CStr(CLng("&H" & ArLRData(12)))
                .StandardRPM = CStr(CLng("&H" & ArLRData(13)))
            End With
        Catch ex As Exception
            Return GetDataInfo
        End Try

        Return GetDataInfo

    End Function
    Function LRDataCheckCondition(ByVal strData As String) As LotInformationCheck
        'LR,LotNo,Package,Device,OP,Input,Lotsize,CodeName,BondingConditon,QRCode,MAT,NumberMAT., QRCode Frame Lot No.
        Dim GetDataInfo As New LotInformationCheck
        Dim ArLRData() As String = strData.Split(CChar(","))
        If UBound(ArLRData) <> 13 Then
            GetDataInfo.ErrMessage = "กรุณากรอก Input Preform"
            Return GetDataInfo
        End If

        Dim strLotNo As String = ArLRData(1).Trim
        Dim strOPNo As String = ArLRData(4).Trim
        Dim strInput As Integer = CInt(CLng("&H" & ArLRData(5)))
        Dim strLotSize As String = ArLRData(6).Trim
        Dim strCodeName As String = ArLRData(7).Trim
        Dim strBonding As String = ArLRData(8).Trim
        Dim strQRCode As String = ArLRData(9)
        Dim strPreformID As String = ArLRData(11).Trim
        Dim strFrameSEQ As String = ArLRData(12).Trim

        GetDataInfo.LotNo = strLotNo
        GetDataInfo.OPNo = strOPNo
        GetDataInfo.InputQty = strInput
        GetDataInfo.LotSize = strLotSize
        GetDataInfo.CodeName = strCodeName
        GetDataInfo.Bonding = strBonding
        GetDataInfo.QRCode = strQRCode
        GetDataInfo.PreformID = strPreformID
        GetDataInfo.FrameSEQ = strFrameSEQ
        GetDataInfo.Pass = False
        strPreformID = strPreformID.Replace(".", "")

        'Check LotNo 
        If strLotNo.Length <> 10 OrElse strLotNo.IndexOf("V") <> 9 Then
            GetDataInfo.ErrMessage = "LotNo ไม่ถูกต้อง กรุณาลองใหม่อีกครั้ง"
            Return GetDataInfo
        End If

        'Check OPNo
        If strOPNo.Length <> 6 OrElse IsNumeric(strOPNo.Substring(1, 5)) = False Then
            GetDataInfo.ErrMessage = "Op ไม่ถูกต้อง กรุณาลองใหม่อีกครั้ง"
            Return GetDataInfo
        End If

        'Check PreformID
        If strPreformID = "" OrElse IsNumeric(strPreformID) = False Then
            GetDataInfo.ErrMessage = "ข้อมูลใน Preform ไม่ถูกต้อง กรุณาลองใหม่อีกครั้ง" & strPreformID
            Return GetDataInfo
        ElseIf strPreformID <> CellConTag.PreformQR_1st Then
            GetDataInfo.ErrMessage = "PreformQR:ไม่ตรงกับ CellCon กรุณาตรวจสอบด้วยครับ"
            Return GetDataInfo
        End If

        'Check FrameSEQ
        If strFrameSEQ.Length <> 12 OrElse IsNumeric(strFrameSEQ) = False Then
            GetDataInfo.ErrMessage = "FrameNo.ไม่ถูกต้อง " & strFrameSEQ
            Return GetDataInfo
        ElseIf strFrameSEQ <> CellConTag.FrameSeqNo_1st Then
            GetDataInfo.ErrMessage = "Frame QR ไม่ตรงกับ CellCon กรุณาตรวจสอบด้วยครับ"
            Return GetDataInfo
        End If

        Dim ap As New DBxDataSetTableAdapters.QueriesTableAdapter1
        Dim QRcode As String = ap.GetQRCodeFromDenpyo(GetDataInfo.LotNo)
        If QRcode = Nothing Then
            GetDataInfo.ErrMessage = "QRCode ไม่มีในระบบ APCS กรุณาติดต่อ System"
            Return GetDataInfo
        End If

        If QRcode.Length < 252 Then
            For i = QRcode.Length To 252 - 1
                QRcode &= " "
            Next
        End If

        GetDataInfo.QRCode = QRcode

        GetDataInfo.Pass = True
        Return GetDataInfo

    End Function

    Private Delegate Sub ProcessCmdDelegate(ByVal buff As String)
    Private Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) _
    Handles SerialPort1.DataReceived

        c_Buffer = c_Buffer & SerialPort1.ReadExisting.ToString
        Dim iret As Integer
        iret = InStr(1, c_Buffer, vbCr)
        If iret <> 0 Then
            c_Buffer = c_Buffer.Replace(Chr(0), "")

            'If c_PreviousCommand <> c_Buffer Then
            ProcessCmdThreadSafe(c_Buffer)
            'End If

            c_PreviousCommand = c_Buffer
            c_Buffer = ""
        End If

    End Sub

    Private Sub ProcessCmdThreadSafe(ByVal buff As String)

        If Me.InvokeRequired Then
            Me.Invoke(New ProcessCmdDelegate(AddressOf ProcessCmdThreadSafe), buff)
            Exit Sub
        End If

        Try
            Dim PMSRecvData As String
            Dim SplitData() As String
            Dim HEADER As String

            Dim RevData() As String
            Dim BufferData As String

            RevData = Split(c_Buffer, vbCr)
            BufferData = RevData(0)
            PMSRecvData = BufferData.ToUpper

            Commlog("CellCon_Rev : " & PMSRecvData, My.Application.Info.DirectoryPath & "\LOG\Comm.log")

            SplitData = Split(PMSRecvData, ",") '****SerialPort Data(Array type) 
            HEADER = SplitData(0).Trim               '****Header


            Select Case HEADER
                Case "LR" 'LR,LotNo,Package,Device,OP,Input,Lotsize,CodeName,BondingConditon,QRCode,MAT,NumberMAT., QRCode Frame Lot No.
                    'IDBW Type Change

                    If c_TypeChangeMode = True Then
                        SerialPortSendData("LP0000")
                        m_frmWarningDialog("Type Change Mode ! ถ้าต้องการ Input งานปกติ กรุณาปิดโหมดนี้", False)
                        Exit Select
                    End If

                    'Check information data 
                    If CellConTag.LotStartTime <> Nothing AndAlso CellConTag.LotEndTime = Nothing AndAlso CellConTag.LotID <> Nothing Then
                        If c_RepeatInput = True Then
                            c_RepeatInput = False
                            Me.BackColor = Color.WhiteSmoke
                            SerialPortSendData("LP0000")
                            Exit Select
                        End If
                        m_frmWarningDialog("กรุณาจบลอตก่อนรันลอตใหม่", False)
                        Exit Select
                    ElseIf CellConTag.LotStartTime <> Nothing AndAlso CellConTag.LotEndTime <> Nothing AndAlso CellConTag.LotID <> Nothing Then
                        m_frmWarningDialog("กรุณา Final Insp.ก่อนรันลอตใหม่", False)
                        Exit Select
                    End If

                    'Time out แล้วกด Clear Alarm
                    If CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing And CellConTag.LotID = SplitData(1) Then
                        SerialPortSendData("LP0000")
                        m_frmWarningDialog("Set up เรียบร้อย กรุณากด Start ก่อน First Insp.", False)
                        Exit Select
                    End If

                    If QRWorkingSlipInputInitailCheck(True) = False Then
                        m_frmWarningDialog(m_QRReadAlarm, False)
                        SerialPortSendData("LP0100")
                        c_PreviousCommand = ""
                        Exit Select
                    End If

                    'ปกติ
                    Dim getData As LotInformationCheck = LRDataCheckCondition(PMSRecvData)
                    If getData.Pass = False Then
                        m_frmWarningDialog(getData.ErrMessage, False)
                        SerialPortSendData("LP0100")
                        c_PreviousCommand = ""
                        Exit Select
                    End If

                    'Mode TDC
                    Dim reqmode As RunModeType = RunModeType.Normal

                    If LotRequestTDC(getData.LotNo, reqmode, getData) = False Then
                        SerialPortSendData("LP0100")
                        c_PreviousCommand = ""
                        Exit Select
                    End If

                    'ETG 
                    If My.Settings.PersonAuthorization = True Then
                        Dim workslip As New WorkingSlipQRCode
                        workslip.SplitQRCode(getData.QRCode)

                        Dim Authen As New Authentication
                        Authen.PermiisionCheck(workslip.DeviceTPDirection2, tbOP.Text, My.Settings.UserAuthenOP, My.Settings.UserAuthenGL, My.Settings.ProcessName, My.Settings.ProcessName & "-" & My.Settings.EquipmentNo)
                        If Authen.Ispass = False Then
                            m_frmWarningDialog(Authen.ErrorMessage, False)
                            SerialPortSendData("LP0100")
                            c_PreviousCommand = ""
                            Exit Select
                        End If

                    End If

                    If FirstInspForMC(getData) = False Then
                        SerialPortSendData("LP0100")
                        c_PreviousCommand = ""
                        Exit Select
                    End If

                    Matparameter()
                    CellConTag = New CellConObj
                    CellConTag = Para

                    CellConTag.WaferLotNoFromDepyo = AllWaferLotNoFromDenpyo(CellConTag.LotID)
                    CellConTag.WaferLotNoListSplited = WaferLotNoSliter(CellConTag.WaferLotNoFromDepyo)
                    CellConTag.WaferNoList = FliterWaferNo(CellConTag.WaferLotID, CellConTag.WaferLotNoFromDepyo)

                    DeleteAllFolder()

                    If My.Settings.WaferMappingUse = True Then
                        CopyWaferMap(CellConTag.WaferLotID)
                    End If

                    Try
                        m_EmsClient.SetCurrentLot(My.Settings.EquipmentNo, CellConTag.LotID, 0)
                    Catch ex As Exception
                    End Try

                    SerialPortSendData("LP0000")
                    m_frmWarningDialog("Set up เรียบร้อย กรุณากด Start ก่อน First Insp.", False, 60000)

                    WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                    WriteToXmlCellcon(CellconObjPath & "\" & "Recovery" & ".xml", CellConTag)  '170126 \783 CellconTag
                    UpdateDispaly()
                    UpdateDisplayMaterial()

                Case "FR" 'FR,QR Code Frame Lot No.

                    If CellConTag.LotStartTime = Nothing AndAlso CellConTag.LotEndTime <> Nothing AndAlso CellConTag.LotID = "" Then
                        FinalInspCompleted()
                    End If

                    Dim Mat As MaterialClass = FrameCheck(SplitData(1))
                    If Mat.Pass = False And Mat.ErrMessage <> "" Then
                        m_frmWarningDialog(Mat.ErrMessage, False)
                        Exit Select
                    End If


                    If CellConTag.LotStartTime = Nothing AndAlso CellConTag.LotEndTime = Nothing AndAlso CellConTag.LotID = "" Then 'frame 1
                        CellConTag.FrameLotNo_1st = Mat.FrameLotNo
                        CellConTag.FrameSeqNo_1st = Mat.FrameQR
                        CellConTag.FrameType_1st = Mat.FrameType

                        CellConTag.FrameLotNo_2nd = ""
                        CellConTag.FrameSeqNo_2nd = ""
                        CellConTag.FrameType_2nd = ""
                    ElseIf CellConTag.LotStartTime <> Nothing AndAlso CellConTag.LotEndTime = Nothing AndAlso CellConTag.LotID <> "" Then
                        CellConTag.FrameLotNo_2nd = Mat.FrameLotNo
                        CellConTag.FrameSeqNo_2nd = Mat.FrameQR
                        CellConTag.FrameType_2nd = Mat.FrameType
                    End If

                    UpdateDisplayMaterial()
                    WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag


                Case "PR" 'PR,QR Code Preform Lot No.

                    If UBound(SplitData) <> 2 OrElse SplitData(1) <> "MAT" OrElse IsNumeric(SplitData(2)) = False Then
                        m_frmWarningDialog("Preform ไม่ถูกต้องกรุณาตรวจสอบ", False)
                        Exit Select
                    End If

                    SplitData(2) = SplitData(2).Replace(".", "").Trim
                    Dim strPreformNo As String = "MAT," & SplitData(2)

                    Dim Mat As MaterialClass = PreformCheck(strPreformNo)
                    If Mat.Pass = False Then
                        m_frmWarningDialog(Mat.ErrMessage, False)
                        SerialPortSendData("LP0001")
                        Exit Select
                    End If

                    If (CellConTag.LotStartTime <> Nothing AndAlso CellConTag.LotEndTime = Nothing) = True Then 'Running Preform2
                        CellConTag.PreforExpireDate_2nd = Mat.PrefomrExp
                        CellConTag.PreforInputDate_2nd = Mat.PreformInputDate
                        CellConTag.PreformLotNo_2nd = Mat.PreformMakerLotNo
                        CellConTag.PreformQR_2nd = Mat.PreformQR
                        CellConTag.PreformType_2nd = Mat.PreformType
                    Else           'New Lot  Preform1

                        CellConTag.PreforExpireDate_1st = Mat.PrefomrExp
                        CellConTag.PreforInputDate_1st = Mat.PreformInputDate
                        CellConTag.PreformLotNo_1st = Mat.PreformMakerLotNo
                        CellConTag.PreformQR_1st = Mat.PreformQR
                        CellConTag.PreformType_1st = Mat.PreformType

                        CellConTag.PreforExpireDate_2nd = Nothing
                        CellConTag.PreforInputDate_2nd = Nothing
                        CellConTag.PreformLotNo_2nd = ""
                        CellConTag.PreformLotNo_2nd = ""
                        CellConTag.PreformQR_2nd = ""
                        CellConTag.PreformType_2nd = ""
                    End If

                    UpdateDisplayMaterial()
                    WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag

                Case "SC" 'SC,Alarm No. , Total Alarm
                    If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                        If UBound(SplitData) <> 3 Then
                            Exit Select
                        End If
                        CellConTag.StateCellCon = CellConState.LotAlarm
                        AddAlarmToTableSerialPort(SplitData(1))

                        Try
                            m_EmsClient.SetActivity(TextBox3.Text, "Alarm", TmeCategory.ChokotieLoss)
                        Catch ex As Exception
                        End Try

                    End If

                Case "SD" 'SD,GoodPcs ,NgPcs'MTBF,MTTR,OpRate,RPM,StandardRPM

                    If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing Then
                        Dim GoodDies As Integer = CInt(CLng("&H" & SplitData(1)))
                        CellConTag.TotalGood = GoodDies
                        lbGood1024.Text = CStr(GoodDies)
                        CellConTag.StateCellCon = CellConState.LotStart
                        Try
                            m_EmsClient.SetOutput(My.Settings.EquipmentNo, CellConTag.TotalGood, CellConTag.TotalNG)
                        Catch ex As Exception
                        End Try

                    ElseIf CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing Then
                        Dim GoodDies As Integer = CInt(CLng("&H" & SplitData(1)))
                        LotStartup()
                        CellConTag.TotalGood = GoodDies
                        UpdateDispaly()

                        WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
                        Try
                            CellConTag.StateCellCon = CellConState.LotStart
                            m_EmsClient.SetOutput(My.Settings.EquipmentNo, CellConTag.TotalGood, CellConTag.TotalNG)
                            m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Running", TmeCategory.NetOperationTime)
                        Catch ex As Exception
                        End Try

                        m_frmWarningDialog("กรุณาทำ First Insp.", False, 60000)
                    End If

                Case "LE" ' LE,Goodpcs ,Ngpcs ,ProductRunTime,ProductStopTime,ProductAlarmTime,PasteNG,PasteBrigdeNG,PickUpNG,MTBF,MTTR,
                    ' OpRate,RPM,StandardRPM,ShipSizeX,ShipSizeY,Bonding Condition

                    If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> "" Then
                        Dim GetData As LotInfomationEnd = LELotInfo(PMSRecvData)
                        CellConTag.TotalGood = GetData.TotalGood
                        LotEndIDBW(CInt(GetData.TotalGood))

                    End If
                Case "SA"
                    If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing Then
                        CellConTag.StateCellCon = CellConState.LotStart
                        Try
                            m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Running", TmeCategory.NetOperationTime)
                        Catch ex As Exception
                        End Try
                        WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
                    ElseIf CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing Then
                        LotStartup()
                        UpdateDispaly()

                        WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
                        Try
                            CellConTag.StateCellCon = CellConState.LotStart
                            m_EmsClient.SetOutput(My.Settings.EquipmentNo, CellConTag.TotalGood, CellConTag.TotalNG)
                            m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Running", TmeCategory.NetOperationTime)
                        Catch ex As Exception
                        End Try

                        m_frmWarningDialog("กรุณาทำ First Insp.", False, 60000)
                    End If

                Case "SB"
                    If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing And CellConTag.LotID <> Nothing Then
                        CellConTag.StateCellCon = CellConState.LotStop
                        If CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                            Try
                                m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Stop", TmeCategory.StopLoss)
                            Catch ex As Exception
                            End Try
                        End If
                    End If

                Case "QE"


                    If (CellConTag.LotStartTime = Nothing And CellConTag.LotEndTime = Nothing) OrElse (CellConTag.OPCheck <> "") Then
                        SerialPortSendData("QE00" + Chr(13))
                    ElseIf CellConTag.LotStartTime <> Nothing And CellConTag.LotEndTime = Nothing Then
                        SerialPortSendData("QE00" + Chr(13))
                    Else
                        SerialPortSendData("QE01" + Chr(13))
                        m_frmWarningDialog("กรุณา Confirm WorkRecord ที่Cellcon", False)
                    End If
            End Select


        Catch ex As Exception

            SaveCatchLog(ex.Message.ToString, "ProcessCmdThreadSafe")
        End Try
    End Sub


    Public Sub AddAlarmToTableSerialPort(ByVal AlarmNo As String)

        Dim QeryAp As New DBxDataSetTableAdapters.DBAlarmTableTableAdapter
        Dim intAlarmNo As Integer = CInt(AlarmNo)
        Dim DBalarmID As Integer
        Dim DBAlarmMessage As String = ""
        Dim alarmtable As DBxDataSet.DBAlarmTableDataTable = QeryAp.GetData(CStr(intAlarmNo), My.Settings.MCType)

        If alarmtable.Rows.Count = 0 Then
            Exit Sub
        End If
        DBalarmID = CInt(alarmtable.Rows(0)(1))
        DBAlarmMessage = alarmtable.Rows(0)(0).ToString

        CellConTag.TotalAlarm = CShort(CellConTag.TotalAlarm + 1)
        Dim strDataRow As DBxDataSet.DBAlarmInfoRow = DBxDataSet.DBAlarmInfo.NewDBAlarmInfoRow
        strDataRow.AlarmID = DBalarmID
        strDataRow.LotNo = CellConTag.LotID
        strDataRow.RecordTime = Now
        strDataRow.MCNo = "DB-" & My.Settings.EquipmentNo
        strDataRow.MessageAlarm = DBAlarmMessage
        DBxDataSet.DBAlarmInfo.Rows.Add(strDataRow)

        Select Case AlarmNo
            Case CStr(142)
                CellConTag.AlarmPickup = CShort(CellConTag.AlarmPickup + 1)   '
            Case CStr(99999)
                CellConTag.AlarmPreform = CShort(CellConTag.AlarmPreform + 1)
            Case CStr(143)
                CellConTag.AlarmBonder = CShort(CellConTag.AlarmBonder + 1)
            Case CStr(122)
                CellConTag.AlarmFrameOut = CShort(CellConTag.AlarmFrameOut + 1)
            Case CStr(163)
                CellConTag.AlarmBridgeInsp = CShort(CellConTag.AlarmBridgeInsp + 1)
            Case CStr(162)
                CellConTag.AlarmPreformInsp = CShort(CellConTag.AlarmPreformInsp + 1)
        End Select

        lbAlmTotal1024.Text = CellConTag.TotalAlarm.ToString
        SaveAlarmInfoTable()
        WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)
    End Sub


    Private Function PreformCheck(ByVal QR_Preform As String) As MaterialClass
        Dim Mat As New MaterialClass
        Try

            '=============================== เชคตัวอักษร format ==========================
            Dim revData As String = QR_Preform.Replace(".", "")
            Dim SplitData As String() = revData.Split(CChar(","))
            If UBound(SplitData) <> 1 Then 'NG
                Mat.ErrMessage = ("รูปแบบข้อมุล Preform Material QR ไม่ถูกต้อง ค่าที่อ่านได้คือ(" & QR_Preform & ")") ', "Preform Material QR Input", "รูปแบบข้อมุล ไม่ใช (MAT,ตัวเลข PreForm ID)")
                Mat.Pass = False
                Return Mat
            Else
                If SplitData(0).Contains("MAT") = False OrElse IsNumeric(SplitData(1)) = False Then
                    Mat.ErrMessage = ("รูปแบบข้อมุล Preform Material QR ไม่ถูกต้อง ค่าที่อ่านได้คือ(" & QR_Preform & ")") ', "Preform Material QR Input", "รูปแบบข้อมุล ไม่ใช (MAT,ตัวเลข PreForm ID)")
                    Mat.Pass = False
                    Return Mat
                End If
            End If
            '=========================== เชค Preform Status "New Out" เท่านั้น ========================
            Dim QRPreform As String = SplitData(1)
            Dim PreformStatusAdpater As New DBxDataSetTableAdapters.QueriesTableAdapter1
            Dim PreformState As String = PreformStatusAdpater.PreformStatus(CInt(QRPreform))
            If PreformState Is Nothing Then
                Mat.ErrMessage = ("ข้อมูล Preform (" & QRPreform & ") ไม่มีในระบบ Material กรุณานำไปเข้าระบบที่เครื่อง Material ก่อน นำมาใช้งาน") ', "Preform Material QR Input", "ไม่พบค่านี้ใน MAT.Transaction Table")
                Mat.Pass = False
                Return Mat
            ElseIf PreformState = "New In" Then
                Mat.ErrMessage = ("Preform สถานะเป็น New In กรุณาเปลี่ยนสถานะที่เครื่อง Material เป็นสถานะ New Out เพื่อใช้งาน") ', "Preform Material QR Input", "TransactionType เป็น New In ไม่ได้")
                Mat.Pass = False
                Return Mat
            ElseIf PreformState = "New Out" Then
                GoTo PreformStateOK
            ElseIf PreformState = "Terminate" Then
                Mat.ErrMessage = ("Preform(" & QRPreform & ") นี้หมดอายุแล้ว กรุณาเปลี่ยน Preform") ', "Preform Material QR Input", "TransactionType เป็น Terminate ไม่ได้")
                Mat.Pass = False
                Return Mat
            Else
                Mat.ErrMessage = ("ข้อมูล Preform : MAT," & QRPreform & " สถานะ " & PreformState & " ไม่สามารถใช้งานได้") ', "Preform Material QR Input", "ตรวจสอบ TransactionType สถานะอื่น")
                Mat.Pass = False
                Return Mat
            End If
            '=========================================================

PreformStateOK:
            Dim PreformDetailAdapter As New DBxDataSetTableAdapters.ExpirePreformTableAdapter
            Dim PreformDetail As DBxDataSet.ExpirePreformDataTable = PreformDetailAdapter.GetData(CInt(QRPreform))

            If PreformDetail.Rows.Count <> 0 Then
                Mat.PreformMakerLotNo = PreformDetail.Item(0).MakerLotNo
                Mat.PreformType = PreformDetail.Item(0).MaterialModel
                Mat.PreformInputDate = PreformDetail.Item(0).TransactionDate
                Mat.PrefomrExp = PreformDetail.Item(0).ExpireTime
            Else
                Mat.ErrMessage = ("ไม่มีข้อมูลในระบบ Preform(" & QRPreform & ") กรุณาติดต่อ System") ', "Preform Material QR Input", "ExpirePreformTableAdapter Query Rows = 0")
                Mat.Pass = False
                Return Mat
            End If

            'เชควันหมดอายุ()
            Dim strexp As String = PreformCondition(Mat.PrefomrExp)
            Select Case strexp
                Case PreformStatus.Normal.ToString

                Case PreformStatus.Lower4Hour.ToString
                    Mat.ErrMessage = ("อายุของ Preform ต่ากว่า 4 ชั่วโมง กรุณาเปลี่ยนหลอดด้วยครับ") ', "Preform life time", "LifeTime < 240 Min")
                    Mat.Pass = False
                    Return Mat
                Case PreformStatus.Expired.ToString
                    Mat.ErrMessage = ("Preform นี้หมดอายุแล้ว") ', "Preform life time", "LifeTime Over")
                    Mat.Pass = False
                    Return Mat
            End Select

            Mat.PreformQR = QRPreform

            Mat.Pass = True
        Catch ex As Exception
            SaveCatchLog(ex.ToString, "PreformCheck()")
            Mat.ErrMessage = ("Catch Error ตรวจสอบดู CatchLog") ', "PreformCheck()", "TryCatchEnd", "")
            Mat.Pass = False
        End Try

        Return Mat

    End Function

    Function FrameCheck(ByVal seq As String) As MaterialClass
        Dim mat As New MaterialClass
        If seq.Length <> 12 OrElse IsNumeric(seq) = False Then
            mat.ErrMessage = "QR Framไม่ถูกต้องกรุณาตรวจสอบ ค่าที่อ่านได้คือ (" & seq & ")"
            mat.Pass = False
            Return mat
        End If

        'เชคข้อมูลใน(ฐานข้อมูล)

        Dim FrameMat As New DBxDataSet.IS_MATL_STOCK_FILEDataTable
        Dim FrameMatAdapter As New DBxDataSetTableAdapters.IS_MATL_STOCK_FILETableAdapter
        FrameMatAdapter.FrameMaterail_IS(FrameMat, seq)

        If FrameMat.Rows.Count = 0 Then
            m_frmWarningDialog("QR Frame(" & seq & ") นี้ยังไม่ถูกบันทึกลงในฐานข้อมูลของ Frame Material ติดต่อห้อง Frame Material หรือ Manual Input", True)

            Dim frmFrameMan As New frmFrameInputManual
            If frmFrameMan.ShowDialog = Windows.Forms.DialogResult.OK Then
                mat.FrameLotNo = frmFrameMan.tbMarkerLotNo.Text
                mat.FrameType = frmFrameMan.tbFrameType.Text
                mat.FrameQR = seq

                mat.Pass = True
                Return mat
            Else
                mat.Pass = False
                Return mat
            End If
        End If


        mat.FrameLotNo = FrameMat(0).FRAME_LOT1.Trim
        mat.FrameType = FrameMat(0).PROD_NAME.Trim
        mat.FrameQR = seq
        mat.Pass = True
        Return mat

    End Function

    Function LotRequestTDC(ByVal LotNo As String, ByVal rm As RunModeType, ByRef getdata As LotInformationCheck) As Boolean
        Dim mc As String = My.Settings.ProcessName & "-" & My.Settings.EquipmentNo
        getdata.TDCMode = rm
        Dim res As TdcLotRequestResponse = m_TdcService.LotRequest(mc, LotNo, rm)

        If res.HasError Then

            Using svError As ApcsWebServiceSoapClient = New ApcsWebServiceSoapClient
                If svError.LotRptIgnoreError(mc, res.ErrorCode) = False Then
                    Dim li As LotInfo = Nothing
                    li = m_TdcService.GetLotInfo(LotNo, mc)
                    Using dlg As TdcAlarmMessageForm = New TdcAlarmMessageForm(res.ErrorCode, res.ErrorMessage, LotNo, li)
                        dlg.ShowDialog()
                        getdata.TDCReplyMessage = res.ErrorCode & " : " & res.ErrorMessage
                        Return False
                    End Using
                End If
            End Using
            getdata.TDCReplyMessage = res.ErrorCode & " : " & res.ErrorMessage
            Return True
        Else
            getdata.TDCReplyMessage = "00 : Run Normal"
            Return True
        End If

    End Function




    Function FirstInspForMC(ByVal getdata As LotInformationCheck) As Boolean
        Dim wr As New WorkingSlipQRCode
        Para = New CellConObj
        wr.SplitQRCode(getdata.QRCode)

        Para.QrData = getdata.QRCode

        Dim Denpyo As New DBxDataSetTableAdapters.LCQW_UNION_WORK_DENPYO_PRINTTableAdapter
        Dim DenpyoChipSizeX As String = ""
        Dim DenpyoChipSizeY As String = ""
        Dim DenpyoTsukaigeNo As String = ""
        Dim DenpyoRubberNo As String = ""
        Dim DenpyoPreformType As String = ""

        Dim _DenPyoTable As DBxDataSet.LCQW_UNION_WORK_DENPYO_PRINTDataTable = New DBxDataSet.LCQW_UNION_WORK_DENPYO_PRINTDataTable
        Denpyo.Fill(_DenPyoTable, wr.LotNo)
        For Each strDataRow As DBxDataSet.LCQW_UNION_WORK_DENPYO_PRINTRow In _DenPyoTable.Rows
            DenpyoChipSizeX = strDataRow.ChipSizeX
            DenpyoChipSizeY = strDataRow.ChipSizeY
            DenpyoTsukaigeNo = strDataRow.TsukaigeNo
            DenpyoRubberNo = strDataRow.RubberNo
            DenpyoPreformType = strDataRow.Preform
        Next

        Dim frmInsp As New frmFirstInspection
        frmInsp.tbChipSizeX.Text = DenpyoChipSizeX
        frmInsp.tbChipSizeY.Text = DenpyoChipSizeY
        frmInsp.tbTsukaigeNeedNo.Text = DenpyoTsukaigeNo
        frmInsp.tbRubberColletNo.Text = DenpyoRubberNo

        If frmInsp.ShowDialog <> Windows.Forms.DialogResult.OK Then 'First Insp. form
            Return False
        End If


        With Para
            'OP QR Input
            'wr.SplitQRCode(.QrData)
            .LotID = wr.LotNo
            .Package = wr.Package
            .DeviceName = wr.Device
            .WaferLotID = wr.WFLotNo
            .Recipe = wr.Code

            .OPID = getdata.OPNo
            .INPUTQty = getdata.InputQty
            .LSMode = getdata.TDCMode
            .LRReply = getdata.TDCReplyMessage

            'FirstInsp.

            .TsukaigeNeedNo = CShort(frmInsp.tbTsukaigeNeedNo.Text)
            If frmInsp.rdTsukaigeA.Checked = True Then
                .TsukaigeMode = "A"
            ElseIf frmInsp.rdTsukaigeB.Checked = True Then
                .TsukaigeMode = "B"
            Else
                .TsukaigeMode = "C"
            End If

            If frmInsp.tbTsukaigePinStrock.Text <> "" Then
                .TsukaigePinStrock = CShort(frmInsp.tbTsukaigePinStrock.Text)
            End If

            .RubberColletNo = CShort(frmInsp.tbRubberColletNo.Text)

            If frmInsp.rdRubberCheckA.Checked = True Then
                .RubberMode = "A"
            ElseIf frmInsp.rdRubberCheckB.Checked = True Then
                .RubberMode = "B"
            Else
                .RubberMode = "C"
            End If


            If frmInsp.rbPasteNozzleA.Checked = True Then
                .PasteNozzleMode = "A"
            ElseIf frmInsp.rbPasteNozzleB.Checked = True Then
                .PasteNozzleMode = "B"
            ElseIf frmInsp.rbPasteNozzleC.Checked = True Then
                .PasteNozzleMode = "C"
            End If


            If frmInsp.cbPasteNozzleType.Text <> "" Then
                .PasteNozzleType = frmInsp.cbPasteNozzleType.Text
            End If

            If frmInsp.cbxPastNozzleNo.Text <> "" Then
                .PastNozzleNo = frmInsp.cbxPastNozzleNo.Text
            End If



            If frmInsp.rdBlockChcekA.Checked = True Then
                .BlockCheck = "A"
            ElseIf frmInsp.rdBlockChcekB.Checked = True Then
                .BlockCheck = "B"
            Else
                .BlockCheck = "C"
            End If

            If frmInsp.tbChipSizeX.Text <> "" Then
                .ChipSizeX = CSng(frmInsp.tbChipSizeX.Text)
            End If
            If frmInsp.tbChipSizeY.Text <> "" Then
                .ChipSizeY = CSng(frmInsp.tbChipSizeY.Text)
            End If

            If frmInsp.tbPreformThickness0.Text <> "" Then
                .PreformThickness1 = CShort(frmInsp.tbPreformThickness0.Text)
            End If
            If frmInsp.tbPreformThickness1.Text <> "" Then
                .PreformThickness2 = CShort(frmInsp.tbPreformThickness1.Text)
            End If
            If frmInsp.tbPreformThickness2.Text <> "" Then
                .PreformThickness3 = CShort(frmInsp.tbPreformThickness2.Text)
            End If
            If frmInsp.tbPreformThickness3.Text <> "" Then
                .PreformThickness4 = CShort(frmInsp.tbPreformThickness3.Text)
            End If
            If frmInsp.tbPreformAver.Text <> "" Then
                .PreformThicknessAverage = CShort(frmInsp.tbPreformAver.Text)
            End If
            If frmInsp.tbPreformR.Text <> "" Then
                .PreformThicknessR = CShort(frmInsp.tbPreformR.Text)
            End If
        End With

        Try
            wr.TransactionDataSave(getdata.QRCode)
        Catch ex As Exception

        End Try

        Return True

    End Function

#End Region

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btMapOld.Click, btWaferMapping.Click

        If CellConTag.LotID = Nothing Then
            Exit Sub
        End If

        Dim bt As Button = CType(sender, Button)
        Dim PathMapOld As String

        If bt.Name = "btMapOld" Then
            PathMapOld = "\\zion\MapOld"
        Else
            PathMapOld = "\\zion\WaferMapping"
        End If

        Dim mapOldFolder As FolderBrowserDialog = FolderBrowserDialog1
        mapOldFolder.SelectedPath = PathMapOld
        If mapOldFolder.ShowDialog() <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        If MessageBox.Show("แน่ใจหรือไม่ว่าต้องการก๊อปปี้จาก Folder:" & mapOldFolder.SelectedPath, "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If Directory.Exists(mapOldFolder.SelectedPath) = True And mapOldFolder.SelectedPath.Contains(PathMapOld) = True Then
            If My.Settings.MCType = "IDBW" Then
                Dim strDataArry As String() = mapOldFolder.SelectedPath.Split(CChar("\"))
                My.Computer.FileSystem.CopyDirectory(mapOldFolder.SelectedPath, WaferMapDir & "\" & strDataArry(strDataArry.Length - 1), True) '& "\" & waferLotNo
            Else
                DeleteAllFolder()
                My.Computer.FileSystem.CopyDirectory(mapOldFolder.SelectedPath, WaferMapDir, True) '& "\" & waferLotNo
            End If

            m_frmWarningDialog("!!!!! คำเตือน !!!!!  กรุณาตรวจสอบ Map ก่อนรันทุกครั้ง", False)
        Else
            m_frmWarningDialog("Folder ไม่ใช่ใน MapOld กรุณาลองใหม่อีกครั้ง", False)
        End If

    End Sub

    Private Sub Button11_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        MDIParent1.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        If InputBox("") = "005588005588" Then

            Dim Backuptable As DBxDataSet.BackupDataTable
            Dim ap As New DBxDataSetTableAdapters.BackupTableAdapter
            Backuptable = ap.GetData("IDBW", "DB-" & My.Settings.EquipmentNo)
            If Backuptable.Rows.Count <> 0 Then
                For Each strData As DBxDataSet.BackupRow In Backuptable.Rows
                    CellConTag.LotID = strData.LotNo
                    CellConTag.MCNo = strData.MCNo
                    CellConTag.OPID = strData.OPNo
                    CellConTag.INPUTQty = strData.InputQty
                    CellConTag.PreformQR_1st = strData.PreformFirstID.ToString
                    CellConTag.LotStartTime = strData.LotStartTime
                    CellConTag.FrameSeqNo_1st = strData.FrameFirstSEQ_NO


                    Dim FrameMat As MaterialClass = FrameCheck(CellConTag.FrameSeqNo_1st)

                    CellConTag.FrameLotNo_1st = FrameMat.FrameLotNo
                    CellConTag.FrameSeqNo_1st = FrameMat.FrameQR
                    CellConTag.FrameType_1st = FrameMat.FrameType

                    CellConTag.FrameLotNo_2nd = ""
                    CellConTag.FrameSeqNo_2nd = ""
                    CellConTag.FrameType_2nd = ""


                    Dim PreformMat As MaterialClass = PreformCheck("MAT," & CellConTag.PreformQR_1st)

                    'New Lot  Preform1

                    CellConTag.PreforExpireDate_1st = PreformMat.PrefomrExp
                    CellConTag.PreforInputDate_1st = PreformMat.PreformInputDate
                    CellConTag.PreformLotNo_1st = PreformMat.PreformMakerLotNo
                    CellConTag.PreformQR_1st = PreformMat.PreformQR
                    CellConTag.PreformType_1st = PreformMat.PreformType

                    CellConTag.PreforExpireDate_2nd = Nothing
                    CellConTag.PreforInputDate_2nd = Nothing
                    CellConTag.PreformLotNo_2nd = ""
                    CellConTag.PreformLotNo_2nd = ""
                    CellConTag.PreformQR_2nd = ""
                    CellConTag.PreformType_2nd = ""

                    Dim DenpyoChipSizeX As String
                    Dim DenpyoChipSizeY As String
                    Dim DenpyoTsukaigeNo As String
                    Dim DenpyoRubberNo As String
                    Dim DenpyoPreformType As String
                    Dim Denpyo As New DBxDataSetTableAdapters.LCQW_UNION_WORK_DENPYO_PRINTTableAdapter
                    Dim _DenPyoTable As DBxDataSet.LCQW_UNION_WORK_DENPYO_PRINTDataTable = New DBxDataSet.LCQW_UNION_WORK_DENPYO_PRINTDataTable
                    Denpyo.Fill(_DenPyoTable, CellConTag.LotID)
                    For Each strDataRow As DBxDataSet.LCQW_UNION_WORK_DENPYO_PRINTRow In _DenPyoTable.Rows
                        DenpyoChipSizeX = strDataRow.ChipSizeX
                        DenpyoChipSizeY = strDataRow.ChipSizeY
                        DenpyoTsukaigeNo = strDataRow.TsukaigeNo
                        DenpyoRubberNo = strDataRow.RubberNo
                        DenpyoPreformType = strDataRow.Preform
                    Next

                    Dim frmInsp As New frmFirstInspection

                    frmInsp.tbChipSizeX.Text = DenpyoChipSizeX
                    frmInsp.tbChipSizeY.Text = DenpyoChipSizeY
                    frmInsp.tbTsukaigeNeedNo.Text = DenpyoTsukaigeNo
                    frmInsp.tbRubberColletNo.Text = DenpyoRubberNo

                    If frmInsp.ShowDialog <> Windows.Forms.DialogResult.OK Then 'First Insp. form
                        Me.Close()
                        Exit Sub
                    End If


                    With CellConTag
                        'OP QR Input
                        'wr.SplitQRCode(.QrData)
                        '.LotID = wr.LotNo
                        '.Package = wr.Package
                        '.DeviceName = wr.Device
                        '.WaferLotID = wr.WFLotNo
                        '.Recipe = wr.Code

                        '.INPUTQty = CInt(INPUTQty.lbResult.Text)
                        '.LSMode = reqmode
                        '.LRReply = LotReqReply

                        'FirstInsp.

                        .TsukaigeNeedNo = CShort(frmInsp.tbTsukaigeNeedNo.Text)
                        If frmInsp.rdTsukaigeA.Checked = True Then
                            .TsukaigeMode = "A"
                        ElseIf frmInsp.rdTsukaigeB.Checked = True Then
                            .TsukaigeMode = "B"
                        Else
                            .TsukaigeMode = "C"
                        End If

                        If frmInsp.tbTsukaigePinStrock.Text <> "" Then
                            .TsukaigePinStrock = CShort(frmInsp.tbTsukaigePinStrock.Text)
                        End If

                        .RubberColletNo = CShort(frmInsp.tbRubberColletNo.Text)

                        If frmInsp.rdRubberCheckA.Checked = True Then
                            .RubberMode = "A"
                        ElseIf frmInsp.rdRubberCheckB.Checked = True Then
                            .RubberMode = "B"
                        Else
                            .RubberMode = "C"
                        End If


                        If frmInsp.rbPasteNozzleA.Checked = True Then
                            .PasteNozzleMode = "A"
                        ElseIf frmInsp.rbPasteNozzleB.Checked = True Then
                            .PasteNozzleMode = "B"
                        ElseIf frmInsp.rbPasteNozzleC.Checked = True Then
                            .PasteNozzleMode = "C"
                        End If


                        If frmInsp.cbPasteNozzleType.Text <> "" Then
                            .PasteNozzleType = frmInsp.cbPasteNozzleType.Text
                        End If

                        If frmInsp.cbxPastNozzleNo.Text <> "" Then
                            .PastNozzleNo = frmInsp.cbxPastNozzleNo.Text
                        End If



                        If frmInsp.rdBlockChcekA.Checked = True Then
                            .BlockCheck = "A"
                        ElseIf frmInsp.rdBlockChcekB.Checked = True Then
                            .BlockCheck = "B"
                        Else
                            .BlockCheck = "C"
                        End If

                        If frmInsp.tbChipSizeX.Text <> "" Then
                            .ChipSizeX = CSng(frmInsp.tbChipSizeX.Text)
                        End If
                        If frmInsp.tbChipSizeY.Text <> "" Then
                            .ChipSizeY = CSng(frmInsp.tbChipSizeY.Text)
                        End If

                        If frmInsp.tbPreformThickness0.Text <> "" Then
                            .PreformThickness1 = CShort(frmInsp.tbPreformThickness0.Text)
                        End If
                        If frmInsp.tbPreformThickness1.Text <> "" Then
                            .PreformThickness2 = CShort(frmInsp.tbPreformThickness1.Text)
                        End If
                        If frmInsp.tbPreformThickness2.Text <> "" Then
                            .PreformThickness3 = CShort(frmInsp.tbPreformThickness2.Text)
                        End If
                        If frmInsp.tbPreformThickness3.Text <> "" Then
                            .PreformThickness4 = CShort(frmInsp.tbPreformThickness3.Text)
                        End If
                        If frmInsp.tbPreformAver.Text <> "" Then
                            .PreformThicknessAverage = CShort(frmInsp.tbPreformAver.Text)
                        End If
                        If frmInsp.tbPreformR.Text <> "" Then
                            .PreformThicknessR = CShort(frmInsp.tbPreformR.Text)
                        End If
                    End With


                    UpdateDispaly()
                    UpdateDisplayMaterial()
                Next
            Else
                MsgBox("Backup ไม่สำเร็จ")
            End If
        End If
    End Sub
    Private Sub SetParameterFrmFinalInsp(ByVal frmfinal As frmFinalInspection)
        With CellConTag
            'Alarm
            .AlarmBonder = CShort(frmfinal.tbAlmBonder.Text)
            .AlarmBridgeInsp = CShort(frmfinal.tbAlmBridgeInsp.Text)
            .AlarmFrameOut = CShort(frmfinal.tbAlmFrameOut.Text)
            .AlarmPickup = CShort(frmfinal.tbAlmPickup.Text)
            .AlarmPreform = CShort(frmfinal.tbAlmPreform.Text)
            .AlarmPreformInsp = CShort(frmfinal.tbAlmPreformInsp.Text)

            'Mecha
            If frmfinal.tbDoubleFrame.Text <> "" Then
                .DoubleFrame = CShort(frmfinal.tbDoubleFrame.Text)
            End If
            If frmfinal.tbFrameBent.Text <> "" Then
                .FrameBent = CShort(frmfinal.tbFrameBent.Text)
            End If
            If frmfinal.tbFrameBurn.Text <> "" Then
                .FrameBurn = CShort(frmfinal.tbFrameBurn.Text)
            End If
            If frmfinal.tbBondingNG.Text <> "" Then
                .BondingNG = CShort(frmfinal.tbBondingNG.Text)
            End If

            .InputAdjust = CInt(frmfinal.tbInputQty.Text)
            .GoodAdjust = CInt(frmfinal.tbGoodQty.Text)
            .NGAdjust = CInt(frmfinal.tbNGQty.Text)
            .NoChip = CShort(frmfinal.tbNoChip.Text)

            If My.Settings.MCType <> "IDBW" Then
                .OPCheck = Para.OPCheck
            End If

            'Save EndTime ตั้งแต่ SECSGEM End
            If CellConTag.LotEndTime = Nothing Then
                .LotEndTime = CDate(Format(Now, "yyyy/MM/dd HH:mm:ss"))
            End If

            If frmfinal.tbRemark.Text <> "" Then
                .Remark = frmfinal.tbRemark.Text
            End If

            .LEMode = frmfinal.c_EndMode

            'If frmfinal.c_EndMode = EndModeType.Normal Then
            '    CellConTag.LEMode = EndModeType.Normal
            'Else 'Reload
            '    CellConTag.LEMode = EndModeType.AbnormalEndAccumulate
            'End If

        End With

    End Sub
    Private Sub UpdateDisplayWaferID(ByVal MID As String)

        CellConTag.CurrentWaferID = MID
        CellConTag.CurrentWaferLotID = CellConTag.WaferLotID
        CellConTag.CurrentLotNo = CellConTag.LotID

        'Display WaferID after MapDownload
        lbwaferLotNo1024.Text = CellConTag.CurrentWaferID

        WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
        WriteToXmlCellcon(CellconObjPath & "\" & "Recovery" & ".xml", CellConTag)  '170126 \783 CellconTag
    End Sub



    Private Sub Button23_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        If c_TypeChangeMode = False Then
            If MessageBox.Show("คุณต้องการเปลี่ยนโหมด Type Change หรือไม่", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                c_TypeChangeMode = True
                Me.BackColor = Color.Yellow
            End If
        Else
            If MessageBox.Show("คุณต้องการเปลี่ยนโหมดทำงานปกติหรือไม่", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                c_TypeChangeMode = False
                Me.BackColor = Color.WhiteSmoke
            End If
        End If

    End Sub

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        If c_RepeatInput = True Then
            If MessageBox.Show("คุณต้องการเปลี่ยนโหมดทำงานปกติหรือไม่", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                c_RepeatInput = False
                Me.BackColor = Color.WhiteSmoke
            End If
        Else
            If MessageBox.Show("คุณต้องการเปลี่ยนโหมด ReInput Abnormal", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                c_RepeatInput = True
                Me.BackColor = Color.Orange
            End If
        End If
    End Sub

    Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim aa As New frmBMWarning
        aa.ShowDialog()
    End Sub

    Private Sub Button27_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button27.Click
        Dim aaaa As New frmFinalInspection(Me)
        aaaa.ShowDialog()
    End Sub

    Private Sub ProcessForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        c_Resize.ResizeAllControls(Me)
    End Sub


    Function FirstFinalChecking(CellCon As CellConObj) As String
        If CellCon.LotID <> "" AndAlso CellCon.LotStartTime <> Nothing Then  'กรอง QCMode  First A และ final Null 
            Dim QCTable As New DBxDataSet.QCTableDataTable
            Dim qcap As New DBxDataSetTableAdapters.QCTableTableAdapter
            QCTable = qcap.GetData(CellCon.LotID, CellCon.LotStartTime)
            For Each strdatarow As DBxDataSet.QCTableRow In QCTable.Rows
                If strdatarow.IsQCFirstLotModeNull = False And strdatarow.IsQCFinishLotModeNull = True And CellCon.LotEndTime = Nothing Then
                    Return "กรุณา End Lot ก่อน"
                ElseIf strdatarow.IsQCFirstLotModeNull = False And strdatarow.IsQCFinishLotModeNull = True And CellCon.LotEndTime <> Nothing And strdatarow.IsLotEndTimeNull = False Then
                    Return "กรุณาทำ Final Insp."
                ElseIf strdatarow.IsQCFirstLotModeNull = False And strdatarow.IsQCFinishLotModeNull = True And CellCon.LotEndTime <> Nothing Then
                    'm_frmWarningDialog("กรุณากดปุ่ม End เพื่อยืนยันจำนวนงาน , Alarm ด้วยครับ", False)
                    Return "กรุณากดปุ่ม End เพื่อยืนยันจำนวนงาน , Alarm ด้วยครับ"
                ElseIf strdatarow.IsQCFirstLotModeNull = False And strdatarow.IsQCFinishLotModeNull = False Then
                    FinalInspCompleted()
                    WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellCon)  '170126 \783 CellCon
                ElseIf strdatarow.IsQCFirstLotModeNull = True And strdatarow.IsQCFinishLotModeNull = True Then
                    Return "กรุณาทำ First Insp."
                End If
            Next
        End If
        Return ""
    End Function
    Private Sub SetupLot()

        Dim ret As String = FirstFinalChecking(CellConTag)
        If ret <> "" Then
            m_frmWarningDialog(ret, False)
            Exit Sub
        End If

        If QRWorkingSlipInputInitailCheck(True) = True Then
            Dim frminput As New frmdisplayinput(Me)
            frminput.lbcaption.Text = "กรุณาสแกน QR Code"
            If frminput.ShowDialog = Windows.Forms.DialogResult.OK Then
                Matparameter()
                CellConTag = New CellConObj
                CellConTag = Para

                CellConTag.WaferLotNoFromDepyo = AllWaferLotNoFromDenpyo(CellConTag.LotID)
                CellConTag.WaferLotNoListSplited = WaferLotNoSliter(CellConTag.WaferLotNoFromDepyo)
                CellConTag.WaferNoList = FliterWaferNo(CellConTag.WaferLotID, CellConTag.WaferLotNoFromDepyo)

                If My.Settings.WaferMappingUse = True Then
                    CopyWaferMap(CellConTag.WaferLotID)
                End If

                Try
                    m_EmsClient.SetCurrentLot(My.Settings.EquipmentNo, CellConTag.LotID, 0)
                Catch ex As Exception
                End Try


                If My.Settings.SECS_Enable = False Then
                    'LotStart
                    CellConTag.LotStartTime = CDate(Format(Now, "yyyy/MM/dd HH:mm:ss"))

                    Try
                        m_EmsClient.SetCurrentLot(My.Settings.EquipmentNo, CellConTag.LotID, 0)
                        m_EmsClient.SetActivity(My.Settings.EquipmentNo, "Running", TmeCategory.NetOperationTime)
                    Catch ex As Exception
                    End Try

                    SaveLotStartToDbx()
                    LotSet(My.Settings.ProcessName & "-" & My.Settings.EquipmentNo, CellConTag.LotID, CellConTag.LotStartTime, CellConTag.OPID, CType(CellConTag.LSMode, RunModeType))
                Else 'Secs-gem
                    DeleteAllFolder()
                    If My.Settings.MCType = "2100HS" OrElse My.Settings.MCType = "2009SSI" Then
                        S2F15_SetInputQty(CUInt(CellConTag.INPUTQty))
                        If My.Settings.AutoLoad = False Then '2100HS,2009SSI
                            ReleaseMachine()
                            m_frmWarningDialog("Set up เรียบร้อย กรุณากด Start ก่อน Insp.", False, 60000)
                        Else
                            RecipeCheck()
                        End If
                    ElseIf My.Settings.MCType = "Canon-D10R" Then 'Canon-D10R
                        If My.Settings.AutoLoad = False Then
                            _WhenInputDataAlready = True
                            RemoteCMD_Remote()
                        Else
                            RecipeCheck()
                        End If
                    End If
                End If

                WriteToXmlCellcon(CellconObjPath & "\" & "CurrentLot" & ".xml", CellConTag)  '170126 \783 CellconTag
                WriteToXmlCellcon(CellconObjPath & "\" & "Recovery" & ".xml", CellConTag)  '170126 \783 CellconTag

                UpdateDispaly()
                UpdateDisplayMaterial()
            End If
        Else
            m_frmWarningDialog(m_QRReadAlarm, False)
        End If
    End Sub
    Private Sub SecsGemStateChecking()

        If My.Settings.SECS_Enable = True AndAlso Me.BackColor = Color.Red Then 'ถ้าเป็น Secsgem จะต้องเช็คก่อนส่ง
            m_frmWarningDialog("กรุณเชื่อมต่อCellCon กับ M/C ด้วยครับ", False)
            Exit Sub
        ElseIf My.Settings.SECS_Enable = True Then
            If My.Settings.MCType = "Canon-D10R" Then
                If m_Equipment.EQStatusCanon <> EquipmentStateCanon.IDEL Then
                    m_frmWarningDialog("สถานะเครื่องจักรตอนนี้ไม่พร้อมใช้งาน กรุณาตรวจสอบ", False)
                    Exit Sub
                End If
            End If
        End If

        _WhenInputDataAlready = False
        _WhenPreeSetUpButton = False
        c_LotSetUp = True
        RequestSVID_CurrentState()
    End Sub
    Private Sub LotSetupSecsGem()
        SetupLot()
    End Sub
End Class
