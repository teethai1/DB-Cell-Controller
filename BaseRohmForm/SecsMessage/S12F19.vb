Imports XtraLibrary.SecsGem

Public Class S12F19

    Inherits SecsMessageBase

    
  

    Public Sub New(ByVal MAPER As MAPER, ByVal DATLC As Byte)
        MyBase.New(12, 19, True)
        Dim M_LIST As New SecsItemList("L2")
        AddItem(M_LIST)
        Dim m_MAPER As New SecsItemBinary("MAPER", MAPER)
        Dim m_DATLC As New SecsItemU1("DATLC", DATLC)

        M_LIST.AddItem(m_MAPER)
        M_LIST.AddItem(m_DATLC)


    End Sub



    'Public ReadOnly Property MID() As String
    '    Get
    '        'Return m_MID.Value
    '    End Get
    'End Property

    'Public ReadOnly Property IDTYP() As Byte
    '    Get
    '        Return m_IDTYP.Value(0)
    '    End Get
    'End Property

End Class
