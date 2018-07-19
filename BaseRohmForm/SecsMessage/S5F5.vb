
Imports XtraLibrary.SecsGem

''' <summary>
''' List Alarms Request
''' </summary>
''' <remarks></remarks>
Public Class S5F5
    Inherits SecsMessageBase

    Private m_ALIDVECTOR As SecsItemU4

    Public Sub New()
        MyBase.New(5, 5, True)

        'Comment: Host sends zero length item for all, otherwise ALID vector
        m_ALIDVECTOR = New SecsItemU4("ALIDVECTOR", Nothing)
        AddItem(m_ALIDVECTOR)

    End Sub



End Class
