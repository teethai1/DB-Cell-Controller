
<Serializable()> _
Public Class PDE                             '160811 \783 recipe Program Definition Element
    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
    Private _FullDownLoadname As String
    Public Property FullDownLoadname() As String
        Get
            Return _FullDownLoadname
        End Get
        Set(ByVal value As String)
            _FullDownLoadname = value
        End Set
    End Property

    Private _ProcessUsage As Process

    Public Property ProcessUsage() As Process
        Get
            Return _ProcessUsage
        End Get
        Set(ByVal value As Process)
            _ProcessUsage = value
        End Set
    End Property
    Private _MCType As String = ""
    Public Property MCType() As String     'Type Of Recipe Machine
        Get
            Return _MCType
        End Get
        Set(ByVal value As String)
            _MCType = value
        End Set
    End Property
    Private _executable As Boolean                     'executable if this recipe is approved = true
    Public Property Executable() As Boolean
        Get
            Return _executable
        End Get
        Set(ByVal value As Boolean)
            _executable = value
        End Set
    End Property



    Private _createDate As String = ""        ' Format(Now, "yyyyMMddTHHmmss")
    Public Property CreateDate() As String
        Get
            Return _createDate
        End Get
        Set(ByVal value As String)
            _createDate = value
        End Set
    End Property
    Private _PathOfFile As String = ""         ' Path of recipe storage file
    Public Property PathOfFile() As String
        Get
            Return _PathOfFile
        End Get
        Set(ByVal value As String)
            _PathOfFile = value
        End Set
    End Property

    Public _relatedParameters As New List(Of PDEparameter)

    Private _PPBody As Byte()
    Public Property PPBody() As Byte()
        Get
            Return _PPBody
        End Get
        Set(ByVal value As Byte())
            _PPBody = value
            CheckSum = Crc8(_PPBody, _PPBody.Length)
        End Set
    End Property

    Private _CheckSum As UShort
    Public Property CheckSum() As UShort
        Get
            Return _CheckSum
        End Get
        Set(ByVal value As UShort)
            _CheckSum = value
        End Set
    End Property

    Private Function Crc8(ByVal data As Byte(), ByVal size As Integer) As Byte
        Dim checksum As Byte = 0
        For i As Integer = 0 To size - 1
            checksum = CByte((CInt(checksum) + data(i)) Mod 256)
        Next
        If checksum = 0 Then
            Return 0
        Else
            Return CByte(256 - checksum)
        End If
    End Function


    Public Enum Process
        NONE
        DB
        WB
        MP
        TC
        PL
        FL
        FT
        TP
    End Enum

    Class PDEparameter
        Public ParameterName As String = ""
        Public defaultValue As String = ""
        Public inputBounds As String = ""
        Public unit As String = ""

    End Class





End Class
