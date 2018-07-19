Imports Rohm.Apcs.Tdc

Public Class LotInformationCheck

    Private _LotNo As String
    Public Property LotNo() As String
        Get
            Return _LotNo
        End Get
        Set(ByVal value As String)
            _LotNo = value
        End Set
    End Property

    Private _OPNo As String
    Public Property OPNo() As String
        Get
            Return _OPNo
        End Get
        Set(ByVal value As String)
            _OPNo = value
        End Set
    End Property

    Private _InputQty As Integer
    Public Property InputQty() As Integer
        Get
            Return _InputQty
        End Get
        Set(ByVal value As Integer)
            _InputQty = value
        End Set
    End Property

    Private _LotSize As String
    Public Property LotSize() As String
        Get
            Return _LotSize
        End Get
        Set(ByVal value As String)
            _LotSize = value
        End Set
    End Property

    Private _CodeName As String
    Public Property CodeName() As String
        Get
            Return _CodeName
        End Get
        Set(ByVal value As String)
            _CodeName = value
        End Set
    End Property

    Private _QRCode As String
    Public Property QRCode() As String
        Get
            Return _QRCode
        End Get
        Set(ByVal value As String)
            _QRCode = value
        End Set
    End Property

    Private _PreformID As String
    Public Property PreformID() As String
        Get
            Return _PreformID
        End Get
        Set(ByVal value As String)
            _PreformID = value
        End Set
    End Property

    Private _FrameSEQ As String
    Public Property FrameSEQ() As String
        Get
            Return _FrameSEQ
        End Get
        Set(ByVal value As String)
            _FrameSEQ = value
        End Set
    End Property

    Private _pass As Boolean
    Public Property Pass() As Boolean
        Get
            Return _pass
        End Get
        Set(ByVal value As Boolean)
            _pass = value
        End Set
    End Property

    Private _errCode As Integer
    Public Property ErrCode() As Integer
        Get
            Return _errCode
        End Get
        Set(ByVal value As Integer)
            _errCode = value
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

    Private _bonding As String
    Public Property Bonding() As String
        Get
            Return _bonding
        End Get
        Set(ByVal value As String)
            _bonding = value
        End Set
    End Property

    Private _TDCReplyMessage As String
    Public Property TDCReplyMessage() As String
        Get
            Return _TDCReplyMessage
        End Get
        Set(ByVal value As String)
            _TDCReplyMessage = value
        End Set
    End Property

    Private _TDCMode As RunModeType
    Public Property TDCMode() As RunModeType
        Get
            Return _TDCMode
        End Get
        Set(ByVal value As RunModeType)
            _TDCMode = value
        End Set
    End Property

End Class
