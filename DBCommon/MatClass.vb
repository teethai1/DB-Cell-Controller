Public Class MatClass
    Private c_IsPass As Boolean
    Public Property IsPass() As Boolean
        Get
            Return c_IsPass
        End Get
        Set(ByVal value As Boolean)
            c_IsPass = value
        End Set
    End Property

    Private c_AlarmMessege As String
    Public Property AlarmMessege() As String
        Get
            Return c_AlarmMessege
        End Get
        Set(ByVal value As String)
            c_AlarmMessege = value
        End Set
    End Property
    Private c_MatQrCode As String
    Public Property MatQrCode() As String
        Get
            Return c_MatQrCode
        End Get
        Set(ByVal value As String)
            c_MatQrCode = value
        End Set
    End Property

    '=================================== Frame =================================
    Private c_FrameLotNo As String
    Public Property FrameLotNo() As String
        Get
            Return c_FrameLotNo
        End Get
        Set(ByVal value As String)
            c_FrameLotNo = value
        End Set
    End Property
    Private c_FrameType As String
    Public Property FrameType() As String
        Get
            Return c_FrameType
        End Get
        Set(ByVal value As String)
            c_FrameType = value
        End Set
    End Property
    '=================================== Preform =================================

    Private c_PreformLotNo As String
    Public Property PreformLotNo() As String
        Get
            Return c_PreformLotNo
        End Get
        Set(ByVal value As String)
            c_PreformLotNo = value
        End Set
    End Property
    Private c_PreformInput As Date
    Public Property PreformInput() As Date
        Get
            Return c_PreformInput
        End Get
        Set(ByVal value As Date)
            c_PreformInput = value
        End Set
    End Property
    Private c_PreformExp As Date
    Public Property PreformExp() As Date
        Get
            Return c_PreformExp
        End Get
        Set(ByVal value As Date)
            c_PreformExp = value
        End Set
    End Property
    Private c_PreformType As String
    Public Property PreformType() As String
        Get
            Return c_PreformType
        End Get
        Set(ByVal value As String)
            c_PreformType = value
        End Set
    End Property
End Class
