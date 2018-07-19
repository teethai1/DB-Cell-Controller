Imports XtraLibrary.SecsGem

Public Class tempS12F4
    Public _MID As String
    Public WriteOnly Property MID As String
        Set(ByVal value As String)
            _MID = value
        End Set
    End Property

    Public _IDTYP As Byte
    Public WriteOnly Property IDTYP As Byte
        Set(ByVal value As Byte)
            _IDTYP = value
        End Set
    End Property

    Public _FNLOC As UShort
    Public WriteOnly Property FNLOC As UShort
        Set(ByVal value As UShort)
            _FNLOC = value
        End Set
    End Property

    Public _ORLOC As Byte
    Public WriteOnly Property ORLOC As Byte
        Set(ByVal value As Byte)
            _ORLOC = value
        End Set
    End Property

    Public _RPSEL As Byte
    Public WriteOnly Property RPSEL As Byte
        Set(ByVal value As Byte)
            _RPSEL = value
        End Set
    End Property

    Public _Reep As Integer
    Public WriteOnly Property Reep As Integer
        Set(ByVal value As Integer)
            _Reep = value
        End Set
    End Property

    Public _DUTMS As String
    Public WriteOnly Property DUTMS As String
        Set(ByVal value As String)
            _DUTMS = value
        End Set
    End Property

    Public _XDIES As UShort
    Public WriteOnly Property XDIES As UShort
        Set(ByVal value As UShort)
            _XDIES = value
        End Set
    End Property

    Public _YDIES As UShort
    Public WriteOnly Property YDIES As UShort
        Set(ByVal value As UShort)
            _YDIES = value
        End Set
    End Property

    Public _ROWCT As UShort
    Public WriteOnly Property ROWCT As UShort
        Set(ByVal value As UShort)
            _ROWCT = value
        End Set
    End Property

    Public _COLCT As UShort
    Public WriteOnly Property COLCT As UShort
        Set(ByVal value As UShort)
            _COLCT = value
        End Set
    End Property

    Public _PRDCT As UShort
    Public WriteOnly Property PRDCT As UShort
        Set(ByVal value As UShort)
            _PRDCT = value
        End Set
    End Property

    Public _BCEQU As String
    Public WriteOnly Property BCEQU As String
        Set(ByVal value As String)
            _BCEQU = value
        End Set
    End Property

    Public _NULBC As String
    Public WriteOnly Property NULBC As String
        Set(ByVal value As String)
            _NULBC = value
        End Set
    End Property

    Public _MLCL As UShort
    Public WriteOnly Property MLCL As UShort
        Set(ByVal value As UShort)
            _MLCL = value
        End Set
    End Property
End Class

Public Class S12F4E
    Inherits SecsMessageBase

    Private m_L15 As New SecsItemList("L15")

    Private m_MID As New SecsItemAscii("MID")
    Private m_IDTYP As New SecsItemBinary("IDTYP")
    Private m_FNLOC As New SecsItemU2("FNLOC")
    Private m_ORLOC As New SecsItemBinary("ORLOC")
    Private m_RPSEL As New SecsItemU1("RPSEL")
    Private m_Ln As New SecsItemList("LnREFP")
    Private m_Reep As New SecsItemI4("Reep")

    Private m_DUTMS As New SecsItemAscii("DUTMS")
    Private m_XDIES As New SecsItemU2("XDIES")
    Private m_YDIES As New SecsItemU2("YDIES")
    Private m_ROWCT As New SecsItemU2("ROWCT")
    Private m_COLCT As New SecsItemU2("COLCT")
    Private m_PRDCT As New SecsItemU2("PRDCT")
    Private m_BCEQU As New SecsItemAscii("BCEQU")
    Private m_NULBC As New SecsItemAscii("NULBC")
    Private m_MLCL As New SecsItemU2("MLCL")


    Public Sub New(ByVal _Temp As tempS12F4)
        MyBase.New(12, 4, False)
        Me.AddItem(m_L15)

        With _Temp
            m_MID.Value = ._MID
            m_IDTYP = New SecsItemBinary("", ._IDTYP)
            m_FNLOC = New SecsItemU2("", ._FNLOC)
            m_ORLOC = New SecsItemBinary("", ._ORLOC)
            m_RPSEL = New SecsItemU1("", ._RPSEL)

            m_Reep = New SecsItemI4("", ._Reep)

            m_DUTMS.Value = ._DUTMS
            m_XDIES = New SecsItemU2("", ._XDIES)
            m_YDIES = New SecsItemU2("", ._YDIES)
            m_ROWCT = New SecsItemU2("", ._ROWCT)
            m_COLCT = New SecsItemU2("", ._COLCT)
            m_PRDCT = New SecsItemU2("", ._PRDCT)
            m_BCEQU.Value = ._BCEQU
            m_NULBC.Value = ._NULBC
            m_MLCL = New SecsItemU2("", ._MLCL)
        End With

        m_L15.AddItem(m_MID)
        m_L15.AddItem(m_IDTYP)
        m_L15.AddItem(m_FNLOC)
        m_L15.AddItem(m_ORLOC)
        m_L15.AddItem(m_RPSEL)

        m_L15.AddItem(m_Ln)
        m_Ln.AddItem(m_Reep)

        m_L15.AddItem(m_DUTMS)
        m_L15.AddItem(m_XDIES)
        m_L15.AddItem(m_YDIES)
        m_L15.AddItem(m_ROWCT)
        m_L15.AddItem(m_COLCT)
        m_L15.AddItem(m_PRDCT)
        m_L15.AddItem(m_BCEQU)
        m_L15.AddItem(m_NULBC)
        m_L15.AddItem(m_MLCL)
    End Sub


    'Private _MID As String
    'Public WriteOnly Property MID As String
    '    Set(ByVal value As String)
    '        _MID = value
    '    End Set
    'End Property

    'Private _IDTYP As Byte
    'Public WriteOnly Property IDTYP As Byte
    '    Set(ByVal value As Byte)
    '        _IDTYP = value
    '    End Set
    'End Property

    'Private _FNLOC As UShort
    'Public WriteOnly Property FNLOC As UShort
    '    Set(ByVal value As UShort)
    '        _FNLOC = value
    '    End Set
    'End Property

    'Private _ORLOC As Byte
    'Public WriteOnly Property ORLOC As Byte
    '    Set(ByVal value As Byte)
    '        _ORLOC = value
    '    End Set
    'End Property

    'Private _RPSEL As Byte
    'Public WriteOnly Property RPSEL As Byte
    '    Set(ByVal value As Byte)
    '        _RPSEL = value
    '    End Set
    'End Property

    'Private _Reep As Integer
    'Public WriteOnly Property Reep As Integer
    '    Set(ByVal value As Integer)
    '        _Reep = value
    '    End Set
    'End Property

    'Private _DUTMS As String
    'Public WriteOnly Property DUTMS As String
    '    Set(ByVal value As String)
    '        _DUTMS = value
    '    End Set
    'End Property

    'Private _XDIES As UShort
    'Public WriteOnly Property XDIES As UShort
    '    Set(ByVal value As UShort)
    '        _XDIES = value
    '    End Set
    'End Property

    'Private _YDIES As UShort
    'Public WriteOnly Property YDIES As UShort
    '    Set(ByVal value As UShort)
    '        _YDIES = value
    '    End Set
    'End Property

    'Private _ROWCT As UShort
    'Public WriteOnly Property ROWCT As UShort
    '    Set(ByVal value As UShort)
    '        _ROWCT = value
    '    End Set
    'End Property

    'Private _COLCT As UShort
    'Public WriteOnly Property COLCT As UShort
    '    Set(ByVal value As UShort)
    '        _COLCT = value
    '    End Set
    'End Property

    'Private _PRDCT As UShort
    'Public WriteOnly Property PRDCT As UShort
    '    Set(ByVal value As UShort)
    '        _PRDCT = value
    '    End Set
    'End Property

    'Private _BCEQU As String
    'Public WriteOnly Property BCEQU As String
    '    Set(ByVal value As String)
    '        _BCEQU = value
    '    End Set
    'End Property

    'Private _NULBC As String
    'Public WriteOnly Property NULBC As String
    '    Set(ByVal value As String)
    '        _NULBC = value
    '    End Set
    'End Property

    'Private _MLCL As UShort
    'Public WriteOnly Property MLCL As UShort
    '    Set(ByVal value As UShort)
    '        _MLCL = value
    '    End Set
    'End Property

End Class