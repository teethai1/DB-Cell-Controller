Imports XtraLibrary.SecsGem

Public Class S2F38
    Inherits SecsMessageBase

    Private m_ERACK As SecsItemBinary

    Public Sub New()
        MyBase.New(2, 38, False)

        m_ERACK = New SecsItemBinary("ERACK")
        Me.AddItem(m_ERACK)

    End Sub

    Public ReadOnly Property ERACK() As ERACK
        Get
            Return CType(m_ERACK.Value(0), ERACK)
        End Get
    End Property
End Class
