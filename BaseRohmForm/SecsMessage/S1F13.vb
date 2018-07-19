Imports XtraLibrary.SecsGem

Public Class S1F13
    Inherits SecsMessageBase

    Public Sub New()
        MyBase.New(1, 13, True)
        AddItem(New SecsItemList("L0"))
    End Sub

End Class
