Public Class MaterialClass

    Private _PreformExp As Date
    Public Property PrefomrExp() As Date
        Get
            Return _PreformExp
        End Get
        Set(ByVal value As Date)
            _PreformExp = value
        End Set
    End Property

    Private _PreformInputDate As Date
    Public Property PreformInputDate() As Date
        Get
            Return _PreformInputDate
        End Get
        Set(ByVal value As Date)
            _PreformInputDate = value
        End Set
    End Property

    Private _PreformMakerLotNo As String
    Public Property PreformMakerLotNo() As String
        Get
            Return _PreformMakerLotNo
        End Get
        Set(ByVal value As String)
            _PreformMakerLotNo = value
        End Set
    End Property

    Private _PreformType As String
    Public Property PreformType() As String
        Get
            Return _PreformType
        End Get
        Set(ByVal value As String)
            _PreformType = value
        End Set
    End Property

    Private _PreformQR As String
    Public Property PreformQR() As String
        Get
            Return _PreformQR
        End Get
        Set(ByVal value As String)
            _PreformQR = value
        End Set
    End Property

    Private _Pass As Boolean
    Public Property Pass() As Boolean
        Get
            Return _Pass
        End Get
        Set(ByVal value As Boolean)
            _Pass = value
        End Set
    End Property

    Private _ErrMessage As String
    Public Property ErrMessage() As String
        Get
            Return _ErrMessage
        End Get
        Set(ByVal value As String)
            _ErrMessage = value
        End Set
    End Property

    Private _FrameLotNo As String
    Public Property FrameLotNo() As String
        Get
            Return _FrameLotNo
        End Get
        Set(ByVal value As String)
            _FrameLotNo = value
        End Set
    End Property

    Private _FrameQR As String
    Public Property FrameQR() As String
        Get
            Return _FrameQR
        End Get
        Set(ByVal value As String)
            _FrameQR = value
        End Set
    End Property

    Private _FrameType As String
    Public Property FrameType() As String
        Get
            Return _FrameType
        End Get
        Set(ByVal value As String)
            _FrameType = value
        End Set
    End Property


End Class
