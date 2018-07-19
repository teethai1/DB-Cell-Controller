Public Class Authentication
    Dim _Expdate As Date

    Private _Ispass As Boolean
    Public Property Ispass() As Boolean
        Get
            Return _Ispass
        End Get
        Set(ByVal value As Boolean)
            _Ispass = value
        End Set
    End Property

    Private _ErrorCode As Integer
    Public Property ErrorCode() As Integer
        Get
            Return _ErrorCode
        End Get
        Set(ByVal value As Integer)
            _ErrorCode = value
        End Set
    End Property

    Private _ErrorMessage As String
    Public Property ErrorMessage() As String
        Get
            Return _ErrorMessage
        End Get
        Set(ByVal value As String)
            _ErrorMessage = value
        End Set
    End Property

    Private _IsLotAutomotive As Boolean
    Public Property IsLotAutomotive() As Boolean
        Get
            Return _IsLotAutomotive
        End Get
        Set(ByVal value As Boolean)
            _IsLotAutomotive = value
        End Set
    End Property

    Private _IsOPAutomotive As Boolean
    Public Property IsOpAutomotive() As Boolean
        Get
            Return _IsOPAutomotive
        End Get
        Set(ByVal value As Boolean)
            _IsOPAutomotive = value
        End Set
    End Property

    Private _IsGL As Boolean
    Public Property IsGL() As Boolean
        Get
            Return _IsGL
        End Get
        Set(ByVal value As Boolean)
            _IsGL = value
        End Set
    End Property

    Private _GLExp As Date
    Public Property GLExp() As Date
        Get
            Return _GLExp
        End Get
        Set(ByVal value As Date)
            _GLExp = value
        End Set
    End Property

    Private _IsOP As Boolean
    Public Property IsOP() As Boolean
        Get
            Return _IsOP
        End Get
        Set(ByVal value As Boolean)
            _IsOP = value
        End Set
    End Property

    Private _OPExp As Date
    Public Property OPExp() As Date
        Get
            Return _OPExp
        End Get
        Set(ByVal value As Date)
            _OPExp = value
        End Set
    End Property


    Private _AutomotiveExp As Date
    Public Property AutomotiveExp() As Date
        Get
            Return _AutomotiveExp
        End Get
        Set(ByVal value As Date)
            _AutomotiveExp = value
        End Set
    End Property


    'Dim ETC2 As String                          'From QR Code ,Check ETC2 = BDXX-M/BJ/C is auto motive
    'Dim strNextOperatorNo As String              'OP No.
    'Dim GetUserAuthenGroupByMCType As String       'M/C Type ( Refer with DBx.Group)
    'Dim GL_Group As String                         'GL Gruop ( Refer with DBx.Group)
    'Dim Process As String                        'Process Ex. "FL"
    'Dim MCNo As String                           'MC No Ex "FL-V-01"
    Sub PermiisionCheck(ByVal ETC2 As String, ByVal strOPNo As String, ByVal GetUserAuthenGroupByMCType As String, ByVal GL_Group As String, ByVal Procees As String, ByVal MCNo As String)
        Ispass = False
        If CheckAutomotiveLot(ETC2) = True Then 'ตรวจสอบ Lot Automotive

            If CheckMachineAutomotive(Procees, MCNo) = True Then 'ตรวจสอบเครื่องจักรสามารถรัน Automotive ได้ไหม
                If AuthenUser(strOPNo, "AUTOMOTIVE") = True Then 'ตรวจสอบ OP สามารถรัน Automotive ได้ไหม
                    AutomotiveExp = _Expdate
                    If Now > AutomotiveExp Then
                        _IsOPAutomotive = False
                        _ErrorCode = 1
                        _ErrorMessage = "OPNo:" & strOPNo & " สิทธิ์ AUTOMOTIVE EXP.:" & Format(AutomotiveExp, "yyyy/MM/dd") & " หมดอายุ กรุณาติดต่อ ETG."
                        Exit Sub
                    Else
                        _IsOPAutomotive = True
                    End If
                Else
                    _IsOPAutomotive = False
                    _ErrorCode = 2
                    _ErrorMessage = "OPNo:" & strOPNo & " ไม่มีสิทธิ์ AUTOMOTIVE กรุณาติดต่อ ETG."
                    Exit Sub
                End If
            Else
                If ErrorCode = 6 Then
                    _ErrorMessage = "MCNo:" & MCNo & " ยังไม่ได้ลงทะเบียนเครื่องจักร กรุณาติดต่อ System D&D"
                Else
                    _ErrorCode = 3
                    _ErrorMessage = "MCNo:" & MCNo & " นี้ไม่มีสิทธิ์ AUTOMOTIVE กรุณาติดต่อ System D&D"
                End If
                Exit Sub
            End If

        End If

        'หลังจากตรวจสอบ OP Lot MC เป็น Automotive

        'ตรวจสอบสิทธิ์การรันเครื่อง
        If AuthenUser(strOPNo, GL_Group) = True Then 'ตรวจสอบ GL
            GLExp = _Expdate
            If Now > _Expdate Then 'GL หมดอายุ
                IsGL = False
                GoTo OPNoCheck
            Else
                Ispass = True
                Exit Sub
            End If
        Else
OPNoCheck:
            If AuthenUser(strOPNo, GetUserAuthenGroupByMCType) = True Then
                OPExp = _Expdate
                If Now > _Expdate Then 'OP หมดอายุ
                    ErrorCode = 4
                    ErrorMessage = "OPNo:" & strOPNo & " EXP:" & Format(_Expdate, "yyyy/MM/dd") & " สิทธิ์การทำงานหมดอายุ กรุณาติดต่อ ETG."
                    Exit Sub
                Else
                    Ispass = True
                    Exit Sub
                End If
            Else
                ErrorCode = 5
                ErrorMessage = "OPNo:" & strOPNo & " ไม่มีสิทธิ์ " & GetUserAuthenGroupByMCType & " กรุณาติดต่อ ETG."
                Exit Sub
            End If
        End If
    End Sub

    Public Function CheckAutomotiveLot(ByVal DeviceName As String) As Boolean
        Dim ret As Boolean
        Dim PosOfHyphen As Integer
        PosOfHyphen = InStr(DeviceName, "-")
        If PosOfHyphen = 0 Then
            ret = False
        Else
            Dim rank As String
            rank = Mid(DeviceName, PosOfHyphen + 1, Len(DeviceName) - PosOfHyphen).ToUpper
            Dim PosOfBracket As Integer
            PosOfBracket = InStr(rank, "(")
            If PosOfBracket > 0 Then rank = Left(rank, PosOfBracket - 1)
            If InStr(rank, "M") > 0 Then ret = True
            If InStr(rank, "C") > 0 Then ret = True
            If InStr(rank, "BJ") > 0 Then ret = True
        End If
        Return ret
    End Function

    Public Function CheckMachineAutomotive(ByVal ProcessName As String, ByVal MCName As String) As Boolean
        Dim ta As New DBxDataSetTableAdapters.MCAutomotiveCheckTableAdapter
        Dim dtQueryInfo As New DataTable
        dtQueryInfo = ta.GetData(MCName, ProcessName)
        If dtQueryInfo IsNot Nothing Then
            If dtQueryInfo.Rows.Count < 1 Then 'Row Count = 0 ไม่มีในระบบต้องลงทะเบียน
                ErrorCode = 6
                Return False
            Else
                If dtQueryInfo.Rows(0)(2).ToString = Nothing Then
                    Return False
                End If

                If CBool(dtQueryInfo.Rows(0)(2)) = True Then
                    Return True
                Else
                    Return False
                End If

            End If
        Else
            Return False
        End If
    End Function

    Public Function AuthenUser(ByVal OPNo As String, ByVal GroupName As String) As Boolean
        Dim taAuthenUser As New DBxDataSetTableAdapters.AuthenUserTableAdapter
        Dim dtQueryInfo As New DataTable
        dtQueryInfo = taAuthenUser.GetData(OPNo, GroupName)

        If dtQueryInfo IsNot Nothing Then
            If dtQueryInfo.Rows.Count < 1 Then
                Return False
            Else
                _Expdate = CDate(dtQueryInfo.Rows(0)(3))
                Return True
            End If
        Else
            Return False
        End If
    End Function

End Class

