Imports System.IO
Imports System.Runtime.Serialization.Formatters.Soap
Imports System.Xml.Serialization
Imports Rohm.Apcs.Tdc

Module ModuleAppGeneral

#Region "Commomn Define"

    Public PathPicObj As String = My.Application.Info.DirectoryPath & "\" & My.Settings.MCType & "\Picture"                  '160714 \783 Change location
    Public PathXmlObj As String = My.Application.Info.DirectoryPath & "\" & My.Settings.MCType
    Public BackUpObj As String = My.Application.Info.DirectoryPath & "\" & My.Settings.MCType & "\BackUpObj"
    Public BackUpObjOld As String = My.Application.Info.DirectoryPath & "\" & My.Settings.MCType & "\BackUpObjOld"
    Public DIR_LOG As String = My.Application.Info.DirectoryPath & "\LOG"
    Public RecipeDir As String = My.Application.Info.DirectoryPath & "\" & My.Settings.MCType & "\Recipe" '160727 RecipeBodyManage
    Public OprData As New CommonData
    Public UserTable As New DataTable
    Public CellconObjPath As String = My.Application.Info.DirectoryPath & "\" & My.Settings.MCType & "\" & "CellconObj"
    Public WaferMapDir As String = My.Application.Info.DirectoryPath & "\" & My.Settings.MCType & "\WaferMap"     '170203 \783 WaferMapManage
    Public m_TdcService As TdcService

    Friend Const _ipServer = "172.16.0.100"                      'ZION Server
    Friend Const _ipDbxUser = "172.16.0.102"                     'DBX,APCS  Server
    Friend Const _PrdTableClear = "Production Table Clear"       '160712 \783 PrdTable Clear
    Friend CelconVer As String = "Ver1.00"                      'Referrence with softwrae control (Please get from DataBase when load from and set this value)
    Friend NetVerSion As String = "Ver1.01_180508 Fix Production time"
    Friend CellConTag As New CellConObj
    Friend Para As CellConObj
    Friend MatPara As New Material
    Public m_warningDisplay As frmWarning


    Enum CellConState
        LotStart = 1
        LotEnd = 2
        LotAlarm = 3
        LotStop = 4
        LotClear = 5
    End Enum
 

    Public Sub MakeDirectories()
        Try
            If Not (Directory.Exists(DIR_LOG)) Then
                Directory.CreateDirectory(DIR_LOG)
            End If
            If (Not System.IO.Directory.Exists(PathPicObj)) Then
                System.IO.Directory.CreateDirectory(PathPicObj)
            End If
            If (Not System.IO.Directory.Exists(PathXmlObj)) Then
                System.IO.Directory.CreateDirectory(PathXmlObj)
            End If
            If (Not System.IO.Directory.Exists(CellconObjPath)) Then
                System.IO.Directory.CreateDirectory(CellconObjPath)
            End If
            If (Not System.IO.Directory.Exists(BackUpObj)) Then
                System.IO.Directory.CreateDirectory(BackUpObj)
            End If
            If (Not System.IO.Directory.Exists(BackUpObjOld)) Then
                System.IO.Directory.CreateDirectory(BackUpObjOld)
            End If
            If (Not System.IO.Directory.Exists(RecipeDir)) Then  '160727 RecipeBodyManage
                System.IO.Directory.CreateDirectory(RecipeDir)
            End If
            If (Not System.IO.Directory.Exists(WaferMapDir)) Then  '170203 \783 WaferMapManage
                System.IO.Directory.CreateDirectory(WaferMapDir)
            End If
        Catch ex As Exception
            SaveCatchLog(ex.Message, "MakeDirectories()")
        End Try
    End Sub

    Public Sub SaveCatchLog(ByVal message As String, ByVal fnName As String)
        Using sw As StreamWriter = New StreamWriter(Path.Combine(DIR_LOG, "Catch_" & Now.ToString("yyyyMMdd") & ".log"), True)
            sw.WriteLine(Now.ToString("yyyy/MM/dd HH:mm:ss.fff") & " " & fnName & ">" & message)
        End Using
    End Sub

#End Region

    Dim SynSave As New Object       'Temp for SML Edit Code
    Public Sub SaveData(ByVal fileName As String, ByVal strSaveData As String, Optional ByVal bAppend As Boolean = True)
        Try
            SyncLock (SynSave)
                Dim filePath As String = My.Application.Info.DirectoryPath & "\LOG\"
                If File.Exists(filePath & fileName) Then
                    Dim info2 As New FileInfo(filePath & fileName)
                    Dim fileSize As Double = info2.Length / 1048576
                    If fileSize > 2 Then ' if file size greater than 2 MB
                        Dim dFilePath As String
                        dFilePath = filePath + fileName.Replace("\", "\_backup\")
                        dFilePath = dFilePath.Replace(".log", "-" + Format(Now, "yyyyMMdd") + ".log")

                        Dim di As New IO.DirectoryInfo(New FileInfo(dFilePath).DirectoryName)
                        Dim aryFi As IO.FileInfo() = di.GetFiles(New FileInfo(dFilePath).Name.Replace("."c, "*."))

                        dFilePath = dFilePath.Replace(".log", "-" + CStr(aryFi.Length + 1) + ".log")
                        File.Copy(filePath & fileName, dFilePath, True)

                        'info2 = New FileInfo(dFilePath)
                        'DeleteOldLogFile(New IO.DirectoryInfo(info2.DirectoryName))
                        Kill(filePath & fileName)
                        My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\LOG\" & fileName, strSaveData + vbCrLf, bAppend)
                    Else
                        My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\LOG\" & fileName, strSaveData + vbCrLf, bAppend)
                    End If
                Else
                    My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath & "\LOG\" & fileName, strSaveData + vbCrLf, bAppend)
                End If
            End SyncLock
        Catch ex As Exception
            SaveCatchLog(ex.Message, "SaveData")
        End Try

    End Sub

    Public Sub WrXml(ByVal pathfile As String, ByVal TarObj As Object)
        'Dim xfile As String = SelPath & "Config.xml"
        Dim fs As New IO.FileStream(pathfile, IO.FileMode.Create)
        Dim bs As New SoapFormatter
        bs.Serialize(fs, TarObj)
        fs.Close()


    End Sub
    Public Function RdXml(ByVal pathfile As String) As Object
        ''Dim xfile As String = SelPath & "Config.xml"
        Dim TarObj As New Object
        If Dir(pathfile) <> "" Then
            Dim fs As New IO.FileStream(pathfile, IO.FileMode.Open)
            Dim bs As New SoapFormatter
            TarObj = bs.Deserialize(fs)
            fs.Close()
        End If
        Return TarObj





    End Function
    Public Sub WriteToXmlPDE(ByVal FileName As String, ByVal TarObj As PDE)               '160811 \783 recipe Program Definition Element

        Dim XmlFile As FileStream = New FileStream(FileName, FileMode.Create)
        Dim serialize As XmlSerializer = New XmlSerializer(TarObj.GetType)
        serialize.Serialize(XmlFile, TarObj)
        XmlFile.Close()
    End Sub

    Public Function ReadFromXmlPDE(ByVal FileName As String) As PDE                      '160811 \783 recipe Program Definition Element
        Dim XmlFile As FileStream = New FileStream(FileName, FileMode.Open)
        Dim serialize As XmlSerializer = New XmlSerializer(GetType(PDE))
        ReadFromXmlPDE = CType(serialize.Deserialize(XmlFile), PDE)
        XmlFile.Close()
        Return ReadFromXmlPDE
    End Function


    Public Sub WriteToXmlCellcon(ByVal FileName As String, ByVal TarObj As CellConObj)               '170207 \783 Cellcon Element

        Dim XmlFile As FileStream = New FileStream(FileName, FileMode.Create)
        Dim serialize As XmlSerializer = New XmlSerializer(TarObj.GetType)
        serialize.Serialize(XmlFile, TarObj)
        XmlFile.Close()
    End Sub

    Public Function ReadFromXmlCellcon(ByVal FileName As String) As CellConObj                      '170207 \783 Cellcon Element
        If File.Exists(FileName) = False Then
            Return New CellConObj
        End If
        Dim XmlFile As FileStream = New FileStream(FileName, FileMode.Open)
        Dim serialize As XmlSerializer = New XmlSerializer(GetType(CellConObj))
        ReadFromXmlCellcon = CType(serialize.Deserialize(XmlFile), CellConObj)
        XmlFile.Close()
        Return ReadFromXmlCellcon
    End Function

    Public Sub Commlog(ByVal Massage As String, ByVal Path As String) 'Comm log
        Dim logpath As clsErrorLog = clsErrorLog.GetInstance
        logpath.ErrLogFileName = Path   ' My.Application.Info.DirectoryPath & "\LOG\Comm.log"
        clsErrorLog.addlog(Format(Now, "yyyy-MM-ddTHH:mm:ss | ") & Massage)
    End Sub


    Public Function CommentFilter(ByVal str As String) As String
        Try
            Dim pos As Integer

            pos = InStr(str, ";")       'Allow use ";" Character to Comment
            If pos > 0 Then str = Mid(str, 1, pos - 1)
            Return Trim(str)
        Catch ex As Exception
            Return CStr(str.Length)
            SaveCatchLog(ex.Message, "CommentFilter")
        End Try

    End Function
    Enum PreformStatus
        Blue40
        Sky32_40
        Green24_32
        Yello10_24
        Orange4_10
        DarkOrange0_4

        Normal
        Expired
        Lower4Hour
    End Enum

    Public Sub m_frmWarningDialog(ByVal almMessage As String, ByVal dialog As Boolean)

        If m_warningDisplay Is Nothing OrElse m_warningDisplay.IsDisposed = True Then
            m_warningDisplay = New frmWarning
        End If

        If m_warningDisplay.Visible = True Then
            m_warningDisplay.Visible = False
        End If

        m_warningDisplay.WarningTimeout(almMessage)
        If dialog = True Then
            Try
                m_warningDisplay.ShowDialog()
            Catch ex As Exception
                m_warningDisplay = New frmWarning
                m_warningDisplay.WarningTimeout(almMessage)
                m_warningDisplay.ShowDialog()
            End Try
        Else
            m_warningDisplay.Show()
        End If


    End Sub
    Public Sub m_frmWarningDialog(ByVal almMessage As String, ByVal dialog As Boolean, ByVal timeout As Integer)
        If m_warningDisplay Is Nothing OrElse m_warningDisplay.IsDisposed = True Then
            m_warningDisplay = New frmWarning
        End If

        If m_warningDisplay.Visible = True Then
            m_warningDisplay.Visible = False
        End If

        m_warningDisplay.WarningTimeout(almMessage, timeout)
        If dialog = True Then
            Try
                m_warningDisplay.ShowDialog()
            Catch ex As Exception
                m_warningDisplay = New frmWarning
                m_warningDisplay.WarningTimeout(almMessage, timeout)
                m_warningDisplay.ShowDialog()
            End Try
        Else
            m_warningDisplay.Show()
        End If


    End Sub
End Module
