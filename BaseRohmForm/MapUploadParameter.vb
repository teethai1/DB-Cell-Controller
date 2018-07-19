<Serializable()> _
Public Class MapUploadParameter

    Private c_McNo As String
    Public Property McNo() As String
        Get
            Return c_McNo
        End Get
        Set(ByVal value As String)
            c_McNo = value
        End Set
    End Property


    Private c_WaferNo As String
    Public Property WaferNo() As String
        Get
            Return c_WaferNo
        End Get
        Set(ByVal value As String)
            c_WaferNo = value
        End Set
    End Property

    Private _LotNo As String
    Public Property LotNo() As String
        Get
            Return _LotNo
        End Get
        Set(ByVal value As String)
            _LotNo = value
        End Set
    End Property


    Private _WaferLotNo As String
    Public Property WaferLotNo() As String
        Get
            Return _WaferLotNo
        End Get
        Set(ByVal value As String)
            _WaferLotNo = value
        End Set
    End Property


    Private _Package As String
    Public Property Pacakge() As String
        Get
            Return _Package
        End Get
        Set(ByVal value As String)
            _Package = value
        End Set
    End Property


    Private _RecipeName As String
    Public Property RecipeName() As String
        Get
            Return _RecipeName
        End Get
        Set(ByVal value As String)
            _RecipeName = value
        End Set
    End Property


    Private _LotStartTime As Date
    Public Property LotStartTime() As Date
        Get
            Return _LotStartTime
        End Get
        Set(ByVal value As Date)
            _LotStartTime = value
        End Set
    End Property

    Private _UploadTime As Date
    Public Property UploadTime() As Date
        Get
            Return _UploadTime
        End Get
        Set(ByVal value As Date)
            _UploadTime = value
        End Set
    End Property



    Private m_MID As String
    Private m_IDTYP As Byte
    Private m_FNLOC As UShort
    Private m_FFROT As UShort
    Private m_ORLOC As Byte
    Private m_RPSEL As Byte

    Private m_Reep As List(Of Point)

    Private m_DUTMS As String
    Private m_XDIES As UShort
    Private m_YDIES As UShort
    Private m_ROWCT As UShort
    Private m_COLCT As UShort
    Private m_NULBC As String
    Private m_PRDCT As UInteger
    Private m_PRAXI As Byte

    Dim m_MAPFT As New Byte
    Dim m_MLCL As New UInteger

    Dim m_BINLT As String
    Dim m_STRP As Short



    Public Property MID() As String
        Get
            Return m_MID
        End Get
        Set(ByVal value As String)
            m_MID = value
        End Set
    End Property

    Public Property Reep() As List(Of Point)
        Get
            Return m_Reep
        End Get
        Set(ByVal value As List(Of Point))
            m_Reep = value
        End Set
    End Property

    Public Property STRP() As Short
        Get
            Return m_STRP
        End Get
        Set(ByVal value As Short)
            m_STRP = value
        End Set
    End Property



    Public Property IDTYP() As Byte
        Get
            Return m_IDTYP
        End Get
        Set(ByVal value As Byte)
            m_IDTYP = value
        End Set
    End Property


    Public Property FNLOC() As UShort
        Get
            Return m_FNLOC
        End Get
        Set(ByVal value As UShort)
            m_FNLOC = value
        End Set
    End Property

    Public Property FFROT() As UShort
        Get
            Return m_FFROT
        End Get
        Set(ByVal value As UShort)

        End Set
    End Property

    Public Property ORLOC() As Byte
        Get
            Return m_ORLOC
        End Get
        Set(ByVal value As Byte)
            m_ORLOC = value
        End Set
    End Property

    Public Property RPSEL() As Byte
        Get
            Return m_RPSEL
        End Get
        Set(ByVal value As Byte)
            m_RPSEL = value
        End Set
    End Property

    Public Property DUTMS() As String
        Get
            Return m_DUTMS
        End Get
        Set(ByVal value As String)
            m_DUTMS = value
        End Set
    End Property

    Public Property XDIES() As UShort
        Get
            Return m_XDIES
        End Get
        Set(ByVal value As UShort)
            m_XDIES = value
        End Set
    End Property

    Public Property YDIES() As UShort
        Get
            Return m_YDIES
        End Get
        Set(ByVal value As UShort)
            m_YDIES = value
        End Set
    End Property

    Public Property ROWCT() As UShort
        Get
            Return m_ROWCT
        End Get
        Set(ByVal value As UShort)
            m_ROWCT = value
        End Set
    End Property

    Public Property COLCT() As UShort
        Get
            Return m_COLCT
        End Get
        Set(ByVal value As UShort)
            m_COLCT = value
        End Set
    End Property

    Public Property NULBC() As String
        Get
            Return m_NULBC
        End Get
        Set(ByVal value As String)
            m_NULBC = value
        End Set
    End Property

    Public Property PRDCT() As UInteger
        Get
            Return m_PRDCT
        End Get
        Set(ByVal value As UInteger)
            m_PRDCT = value
        End Set
    End Property

    Public Property PRAXI() As Byte
        Get
            Return m_PRAXI
        End Get

        Set(ByVal value As Byte)
            m_PRAXI = value
        End Set

    End Property


    Public Property MAPFT() As Byte
        Get
            Return m_MAPFT
        End Get

        Set(ByVal value As Byte)
            m_MAPFT = value
        End Set

    End Property

    Public Property MLCL() As UInteger
        Get
            Return m_MLCL
        End Get

        Set(ByVal value As UInteger)
            m_MLCL = value
        End Set
    End Property

    Public Property BINLT() As String
        Get
            Return m_BINLT
        End Get
        Set(ByVal value As String)
            m_BINLT = value
        End Set
    End Property

End Class
