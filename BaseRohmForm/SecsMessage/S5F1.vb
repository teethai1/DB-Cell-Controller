Imports XtraLibrary.SecsGem

Public Class S5F1
    Inherits SecsMessageBase

    Private m_L3 As SecsItemList
    Private m_ALCD As SecsItemBinary
    Private m_ALID As SecsItemU4
    Private m_ALTX As SecsItemAscii

    Public Sub New()
        MyBase.New(5, 1, True)

        m_L3 = New SecsItemList("L3")
        AddItem(m_L3)

        m_ALCD = New SecsItemBinary("ALCD")
        m_L3.AddItem(m_ALCD)

        m_ALID = New SecsItemU4("ALID")
        m_L3.AddItem(m_ALID)

        m_ALTX = New SecsItemAscii("ALTX")
        m_L3.AddItem(m_ALTX)

    End Sub

    Public ReadOnly Property AlarmCode() As Byte
        Get
            Return m_ALCD.Value(0)
        End Get
    End Property

    Public ReadOnly Property AlarmID() As UInteger
        Get
            Return m_ALID.Value(0)
        End Get
    End Property

    Public ReadOnly Property AlarmText() As String
        Get
            Return m_ALTX.Value
        End Get
    End Property

End Class
