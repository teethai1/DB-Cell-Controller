Imports XtraLibrary.SecsGem

Public Class S7F5
    Inherits SecsMessageBase

    Public Sub New(ByVal svid As String)
        MyBase.New(7, 5, True)
        Dim svidItem As SecsItemAscii = New SecsItemAscii("PPID", svid)
        AddItem(svidItem)
    End Sub


End Class
