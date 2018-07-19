Imports XtraLibrary.SecsGem

Public Class S5F6
    Inherits SecsMessageBase

    Private m_Ln As SecsItemList

    Public Sub New()
        MyBase.New(5, 6, False)

        'L[n] of L[3]
        m_Ln = New SecsItemList("Ln")
        AddItem(m_Ln)

    End Sub
End Class
