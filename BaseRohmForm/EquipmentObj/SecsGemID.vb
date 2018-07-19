<Serializable()> _
Public Class SecsGemID
    Private _LotStartECID As UInteger
    Public Property LotStartECID() As UInteger
        Get
            Return _LotStartECID
        End Get
        Set(ByVal value As UInteger)
            _LotStartECID = value
        End Set
    End Property

    Private _LotEndECID As UInteger
    Public Property LotEndECID() As UInteger
        Get
            Return _LotEndECID
        End Get
        Set(ByVal value As UInteger)
            _LotEndECID = value
        End Set
    End Property

    Private _GoodCat1SVID As UInteger
    Public Property GoodCat1SVID() As UInteger
        Get
            Return _GoodCat1SVID
        End Get
        Set(ByVal value As UInteger)
            _GoodCat1SVID = value
        End Set
    End Property

    Private _GoodCat2SVID As UInteger
    Public Property GoodCat2SVID() As UInteger
        Get
            Return _GoodCat2SVID
        End Get
        Set(ByVal value As UInteger)
            _GoodCat2SVID = value
        End Set
    End Property


    Private _NgBin1SVID As UInteger
    Public Property NgBin1SVID() As UInteger
        Get
            Return _NgBin1SVID
        End Get
        Set(ByVal value As UInteger)
            _NgBin1SVID = value
        End Set
    End Property

    Private _NgBin2SVID As UInteger
    Public Property NgBin2SVID() As UInteger
        Get
            Return _NgBin2SVID
        End Get
        Set(ByVal value As UInteger)
            _NgBin2SVID = value
        End Set
    End Property

    Private _NgBin3SVID As UInteger
    Public Property NgBin3SVID() As UInteger
        Get
            Return _NgBin3SVID
        End Get
        Set(ByVal value As UInteger)
            _NgBin3SVID = value
        End Set
    End Property

    Private _NgBin4SVID As UInteger
    Public Property NgBin4SVID() As UInteger
        Get
            Return _NgBin4SVID
        End Get
        Set(ByVal value As UInteger)
            _NgBin4SVID = value
        End Set
    End Property

    Private _NgBin5SVID As UInteger
    Public Property NgBin5SVID() As UInteger
        Get
            Return _NgBin5SVID
        End Get
        Set(ByVal value As UInteger)
            _NgBin5SVID = value
        End Set
    End Property

    Private _NgBin6SVID As UInteger
    Public Property NgBin6SVID() As UInteger
        Get
            Return _NgBin6SVID
        End Get
        Set(ByVal value As UInteger)
            _NgBin6SVID = value
        End Set
    End Property



    Private _LotIDSVID As UInteger
    Public Property LotIDSVID() As UInteger
        Get
            Return _LotIDSVID
        End Get
        Set(ByVal value As UInteger)
            _LotIDSVID = value
        End Set
    End Property

    Private _EditIndex As Integer = -1    'Use when edit only
    Public Property EditIndex() As Integer
        Get
            Return _EditIndex
        End Get
        Set(ByVal value As Integer)
            _EditIndex = value
        End Set
    End Property


End Class
