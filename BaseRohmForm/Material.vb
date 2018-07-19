Public Class Material

    Private _FrameSeqNo As String
    Public Property FrameSeqNo() As String
        Get
            Return _FrameSeqNo
        End Get
        Set(ByVal value As String)
            _FrameSeqNo = value

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

    Private _FrameType As String
    Public Property FrameType() As String
        Get
            Return _FrameType
        End Get
        Set(ByVal value As String)
            _FrameType = value

        End Set
    End Property

    Private _PreformLotNo As String
    Public Property PreformLotNo() As String
        Get
            Return _PreformLotNo
        End Get
        Set(ByVal value As String)
            _PreformLotNo = value

        End Set
    End Property

    Private _PreforInputDate As Date
    Public Property PreforInputDate() As Date
        Get
            Return _PreforInputDate
        End Get
        Set(ByVal value As Date)
            _PreforInputDate = value

        End Set
    End Property

    Private _PreforExpireDate As Date
    Public Property PreforExpireDate() As Date
        Get
            Return _PreforExpireDate
        End Get
        Set(ByVal value As Date)
            _PreforExpireDate = value

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
    Public Property PreformQR As String
        Get
            Return _PreformQR
        End Get
        Set(ByVal value As String)
            _PreformQR = value

        End Set
    End Property


End Class
