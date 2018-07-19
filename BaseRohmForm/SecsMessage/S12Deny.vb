Imports XtraLibrary.SecsGem
Public Class S12Deny


    Inherits SecsMessageBase

    Private m_L15 As New SecsItemList("L0")
    Public Sub New(ByVal intFunction As Integer)
        MyBase.New(12, CByte(intFunction), False)
        Me.AddItem(m_L15)

    End Sub

End Class
